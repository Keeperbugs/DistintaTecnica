using System;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Operations
{
    /// <summary>
    /// Gestisce tutte le operazioni di eliminazione per la distinta tecnica
    /// </summary>
    public class DeleteOperations
    {
        private readonly Repository repository;

        public DeleteOperations(Repository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Elimina un elemento in base al suo tipo e dati
        /// </summary>
        public bool DeleteElement(TreeNodeData nodeData, IWin32Window owner = null)
        {
            try
            {
                // Conferma eliminazione con dettagli specifici
                string confirmMessage = GetDeleteConfirmationMessage(nodeData);

                var result = MessageBox.Show(
                    confirmMessage,
                    "Conferma Eliminazione",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result != DialogResult.Yes)
                    return false;

                // Verifica dipendenze prima dell'eliminazione
                var dependencies = CheckDependencies(nodeData);
                if (dependencies.HasDependencies)
                {
                    string dependencyMessage = $"ATTENZIONE: L'elemento contiene {dependencies.Count} sotto-elementi:\n\n" +
                                             $"{dependencies.Description}\n\n" +
                                             "Eliminando questo elemento verranno eliminati anche tutti i sotto-elementi.\n\n" +
                                             "Sei sicuro di voler continuare?";

                    var depResult = MessageBox.Show(
                        dependencyMessage,
                        "Eliminazione Cascata",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2
                    );

                    if (depResult != DialogResult.Yes)
                        return false;
                }

                // Esegui eliminazione
                bool success = PerformDelete(nodeData);

                if (success)
                {
                    MessageBox.Show(
                        "Elemento eliminato con successo.",
                        "Eliminazione Completata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Errore durante l'eliminazione: {ex.Message}",
                    "Errore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Ottiene il messaggio di conferma personalizzato per il tipo di elemento
        /// </summary>
        private string GetDeleteConfirmationMessage(TreeNodeData nodeData)
        {
            string elementName = GetElementDisplayName(nodeData);
            string elementCode = GetElementCode(nodeData);

            return $"Sei sicuro di voler eliminare il seguente elemento?\n\n" +
                   $"Tipo: {nodeData.Tipo}\n" +
                   $"Codice: {elementCode}\n" +
                   $"Descrizione: {elementName}\n\n" +
                   "Questa operazione non può essere annullata.";
        }

        /// <summary>
        /// Verifica le dipendenze dell'elemento
        /// </summary>
        private DependencyInfo CheckDependencies(TreeNodeData nodeData)
        {
            var info = new DependencyInfo();

            try
            {
                switch (nodeData.Tipo.ToUpper())
                {
                    case "PROGETTO":
                        var progetto = (Progetto)nodeData.Data;
                        var partiMacchina = repository.GetPartiMacchinaByProgetto(progetto.Id);
                        info.Count = partiMacchina.Count;
                        info.Description = $"- {partiMacchina.Count} Parti Macchina";
                        break;

                    case "PARTE_MACCHINA":
                        var parteMacchina = (ParteMacchina)nodeData.Data;
                        var sezioni = repository.GetSezioniByParteMacchina(parteMacchina.Id);
                        info.Count = sezioni.Count;
                        info.Description = $"- {sezioni.Count} Sezioni";
                        break;

                    case "SEZIONE":
                        var sezione = (Sezione)nodeData.Data;
                        var sottosezioni = repository.GetSottosezioniBySezione(sezione.Id);
                        info.Count = sottosezioni.Count;
                        info.Description = $"- {sottosezioni.Count} Sottosezioni";
                        break;

                    case "SOTTOSEZIONE":
                        var sottosezione = (Sottosezione)nodeData.Data;
                        var montaggi = repository.GetMontaggiBySottosezione(sottosezione.Id);
                        info.Count = montaggi.Count;
                        info.Description = $"- {montaggi.Count} Montaggi";
                        break;

                    case "MONTAGGIO":
                        var montaggio = (Montaggio)nodeData.Data;
                        var gruppi = repository.GetGruppiByMontaggio(montaggio.Id);
                        info.Count = gruppi.Count;
                        info.Description = $"- {gruppi.Count} Gruppi";
                        break;

                    case "GRUPPO":
                        // I gruppi non hanno dipendenze
                        info.Count = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                // In caso di errore nel controllo dipendenze, assumiamo che non ce ne siano
                info.Count = 0;
                info.Description = $"Errore nel controllo dipendenze: {ex.Message}";
            }

            return info;
        }

        /// <summary>
        /// Esegue l'eliminazione effettiva
        /// </summary>
        private bool PerformDelete(TreeNodeData nodeData)
        {
            switch (nodeData.Tipo.ToUpper())
            {
                case "PROGETTO":
                    var progetto = (Progetto)nodeData.Data;
                    return repository.DeleteProgetto(progetto.Id);

                case "PARTE_MACCHINA":
                    var parteMacchina = (ParteMacchina)nodeData.Data;
                    return repository.DeleteParteMacchina(parteMacchina.Id);

                case "SEZIONE":
                    var sezione = (Sezione)nodeData.Data;
                    return repository.DeleteSezione(sezione.Id);

                case "SOTTOSEZIONE":
                    var sottosezione = (Sottosezione)nodeData.Data;
                    return repository.DeleteSottosezione(sottosezione.Id);

                case "MONTAGGIO":
                    var montaggio = (Montaggio)nodeData.Data;
                    return repository.DeleteMontaggio(montaggio.Id);

                case "GRUPPO":
                    var gruppo = (Gruppo)nodeData.Data;
                    return repository.DeleteGruppo(gruppo.Id);

                default:
                    throw new InvalidOperationException($"Tipo elemento non supportato: {nodeData.Tipo}");
            }
        }

        /// <summary>
        /// Ottiene il nome da visualizzare per l'elemento
        /// </summary>
        private string GetElementDisplayName(TreeNodeData nodeData)
        {
            switch (nodeData.Data)
            {
                case Progetto p:
                    return $"{p.NumeroCommessa} - {p.Cliente}";
                case ParteMacchina pm:
                    return pm.Descrizione;
                case Sezione s:
                    return s.Descrizione;
                case Sottosezione ss:
                    return ss.Descrizione;
                case Montaggio m:
                    return m.Descrizione;
                case Gruppo g:
                    return g.Descrizione;
                default:
                    return "Elemento sconosciuto";
            }
        }

        /// <summary>
        /// Ottiene il codice dell'elemento
        /// </summary>
        private string GetElementCode(TreeNodeData nodeData)
        {
            switch (nodeData.Data)
            {
                case Progetto p:
                    return p.NumeroCommessa;
                case ParteMacchina pm:
                    return pm.CodiceParteMacchina;
                case Sezione s:
                    return s.CodiceSezione;
                case Sottosezione ss:
                    return ss.CodiceSottosezione;
                case Montaggio m:
                    return m.CodiceMontaggio;
                case Gruppo g:
                    return g.CodiceGruppo;
                default:
                    return "N/A";
            }
        }
    }

    /// <summary>
    /// Informazioni sulle dipendenze di un elemento
    /// </summary>
    public class DependencyInfo
    {
        public int Count { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool HasDependencies => Count > 0;
    }
}