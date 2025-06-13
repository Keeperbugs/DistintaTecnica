using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Export
{
    /// <summary>
    /// Gestisce le operazioni di esportazione per le distinte tecniche
    /// </summary>
    public class ExportManager
    {
        private readonly Repository repository;

        public ExportManager(Repository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Mostra il dialog per scegliere il tipo di export
        /// </summary>
        public void ShowExportDialog(Progetto progetto, IWin32Window owner = null)
        {
            if (progetto == null)
            {
                MessageBox.Show("Nessun progetto selezionato per l'esportazione.",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exportForm = new ExportSelectionForm(progetto);
            var result = owner == null ? exportForm.ShowDialog() : exportForm.ShowDialog(owner);

            if (result == DialogResult.OK)
            {
                PerformExport(progetto, exportForm.SelectedExportType, exportForm.SelectedOptions);
            }
        }

        /// <summary>
        /// Esegue l'esportazione in base al tipo selezionato
        /// </summary>
        private void PerformExport(Progetto progetto, ExportType exportType, ExportOptions options)
        {
            try
            {
                switch (exportType)
                {
                    case ExportType.CSV:
                        ExportToCSV(progetto, options);
                        break;
                    case ExportType.Excel:
                        ExportToExcel(progetto, options);
                        break;
                    case ExportType.PDF:
                        ExportToPDF(progetto, options);
                        break;
                    case ExportType.Text:
                        ExportToText(progetto, options);
                        break;
                    default:
                        throw new ArgumentException("Tipo di export non supportato");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'esportazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Esporta in formato CSV
        /// </summary>
        private void ExportToCSV(Progetto progetto, ExportOptions options)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "File CSV (*.csv)|*.csv";
                saveDialog.FileName = $"Distinta_{progetto.NumeroCommessa}_{DateTime.Now:yyyyMMdd}.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var csvContent = GenerateCSVContent(progetto, options);
                    File.WriteAllText(saveDialog.FileName, csvContent, Encoding.UTF8);

                    MessageBox.Show("Esportazione CSV completata con successo!",
                                  "Esportazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Genera il contenuto CSV
        /// </summary>
        private string GenerateCSVContent(Progetto progetto, ExportOptions options)
        {
            var csv = new StringBuilder();

            // Header informazioni progetto
            if (options.IncludeProjectInfo)
            {
                csv.AppendLine("=== INFORMAZIONI PROGETTO ===");
                csv.AppendLine($"Numero Commessa;{progetto.NumeroCommessa}");
                csv.AppendLine($"Cliente;{progetto.Cliente}");
                csv.AppendLine($"Data Inserimento;{progetto.DataInserimento:dd/MM/yyyy}");
                csv.AppendLine($"Disegnatore;{progetto.NomeDisegnatore}");
                csv.AppendLine($"Revisione;{progetto.LetteraRevisioneInserimento}");
                csv.AppendLine($"Note;{progetto.Note}");
                csv.AppendLine();
            }

            // Header distinta
            csv.AppendLine("=== DISTINTA TECNICA ===");
            csv.AppendLine("Livello;Tipo;Codice;Descrizione;Quantità;Revisione;Stato;Note");

            // Contenuto distinta
            var partiMacchina = repository.GetPartiMacchinaByProgetto(progetto.Id);
            foreach (var parte in partiMacchina)
            {
                if (options.IncludePartiMacchina)
                {
                    csv.AppendLine($"1;PARTE_MACCHINA;{parte.CodiceParteMacchina};{parte.Descrizione};1;{parte.RevisioneInserimento};{parte.Stato};{parte.Note}");
                }

                if (options.IncludeSezioni)
                {
                    var sezioni = repository.GetSezioniByParteMacchina(parte.Id);
                    foreach (var sezione in sezioni)
                    {
                        csv.AppendLine($"2;SEZIONE;{sezione.CodiceSezione};{sezione.Descrizione};{sezione.Quantita};{sezione.RevisioneInserimento};{sezione.Stato};{sezione.Note}");

                        if (options.IncludeSottosezioni)
                        {
                            var sottosezioni = repository.GetSottosezioniBySezione(sezione.Id);
                            foreach (var sottosezione in sottosezioni)
                            {
                                csv.AppendLine($"3;SOTTOSEZIONE;{sottosezione.CodiceSottosezione};{sottosezione.Descrizione};{sottosezione.Quantita};{sottosezione.RevisioneInserimento};{sottosezione.Stato};{sottosezione.Note}");

                                if (options.IncludeMontaggi)
                                {
                                    var montaggi = repository.GetMontaggiBySottosezione(sottosezione.Id);
                                    foreach (var montaggio in montaggi)
                                    {
                                        csv.AppendLine($"4;MONTAGGIO;{montaggio.CodiceMontaggio};{montaggio.Descrizione};{montaggio.Quantita};{montaggio.RevisioneInserimento};{montaggio.Stato};{montaggio.Note}");

                                        if (options.IncludeGruppi)
                                        {
                                            var gruppi = repository.GetGruppiByMontaggio(montaggio.Id);
                                            foreach (var gruppo in gruppi)
                                            {
                                                csv.AppendLine($"5;GRUPPO ({gruppo.TipoGruppo});{gruppo.CodiceGruppo};{gruppo.Descrizione};{gruppo.Quantita};{gruppo.RevisioneInserimento};{gruppo.Stato};{gruppo.Note}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return csv.ToString();
        }

        /// <summary>
        /// Esporta in formato Excel (placeholder)
        /// </summary>
        private void ExportToExcel(Progetto progetto, ExportOptions options)
        {
            MessageBox.Show("Funzionalità di esportazione Excel da implementare.\n\n" +
                          "Per ora viene generato un file CSV compatibile con Excel.",
                          "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Per ora esporta in CSV
            ExportToCSV(progetto, options);
        }

        /// <summary>
        /// Esporta in formato PDF (placeholder)
        /// </summary>
        private void ExportToPDF(Progetto progetto, ExportOptions options)
        {
            MessageBox.Show("Funzionalità di esportazione PDF da implementare.\n\n" +
                          "Richiede librerie aggiuntive come iTextSharp o simili.",
                          "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Esporta in formato testo
        /// </summary>
        private void ExportToText(Progetto progetto, ExportOptions options)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "File di testo (*.txt)|*.txt";
                saveDialog.FileName = $"Distinta_{progetto.NumeroCommessa}_{DateTime.Now:yyyyMMdd}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var textContent = GenerateTextContent(progetto, options);
                    File.WriteAllText(saveDialog.FileName, textContent, Encoding.UTF8);

                    MessageBox.Show("Esportazione TXT completata con successo!",
                                  "Esportazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Genera contenuto in formato testo
        /// </summary>
        private string GenerateTextContent(Progetto progetto, ExportOptions options)
        {
            var text = new StringBuilder();

            // Header
            text.AppendLine("═══════════════════════════════════════════════════════════════");
            text.AppendLine("                    DISTINTA TECNICA                          ");
            text.AppendLine("═══════════════════════════════════════════════════════════════");
            text.AppendLine();

            // Informazioni progetto
            if (options.IncludeProjectInfo)
            {
                text.AppendLine("INFORMAZIONI PROGETTO:");
                text.AppendLine($"  Numero Commessa: {progetto.NumeroCommessa}");
                text.AppendLine($"  Cliente: {progetto.Cliente}");
                text.AppendLine($"  Data Inserimento: {progetto.DataInserimento:dd/MM/yyyy}");
                text.AppendLine($"  Disegnatore: {progetto.NomeDisegnatore}");
                text.AppendLine($"  Revisione: {progetto.LetteraRevisioneInserimento}");
                if (!string.IsNullOrEmpty(progetto.Note))
                    text.AppendLine($"  Note: {progetto.Note}");
                text.AppendLine();
            }

            // Struttura progetto
            text.AppendLine("STRUTTURA PROGETTO:");
            text.AppendLine();

            var partiMacchina = repository.GetPartiMacchinaByProgetto(progetto.Id);
            foreach (var parte in partiMacchina)
            {
                if (options.IncludePartiMacchina)
                {
                    text.AppendLine($"├─ PARTE MACCHINA: {parte.CodiceParteMacchina}");
                    text.AppendLine($"│  Descrizione: {parte.Descrizione}");
                    text.AppendLine($"│  Tipo: {parte.TipoParteMacchina}");
                    text.AppendLine($"│  Sotto Progetto: {parte.SottoProgetto}");
                    text.AppendLine($"│  Rev: {parte.RevisioneInserimento} | Stato: {parte.Stato}");
                    if (!string.IsNullOrEmpty(parte.Note))
                        text.AppendLine($"│  Note: {parte.Note}");
                    text.AppendLine("│");
                }

                if (options.IncludeSezioni)
                {
                    var sezioni = repository.GetSezioniByParteMacchina(parte.Id);
                    foreach (var sezione in sezioni)
                    {
                        text.AppendLine($"│  ├─ SEZIONE: {sezione.CodiceSezione}");
                        text.AppendLine($"│  │  Descrizione: {sezione.Descrizione}");
                        text.AppendLine($"│  │  Quantità: {sezione.Quantita} | Rev: {sezione.RevisioneInserimento} | Stato: {sezione.Stato}");
                        if (!string.IsNullOrEmpty(sezione.Note))
                            text.AppendLine($"│  │  Note: {sezione.Note}");
                        text.AppendLine("│  │");

                        // Continua con sottosezioni, montaggi e gruppi...
                        // (implementazione simile per mantenere la struttura ad albero)
                    }
                }
            }

            text.AppendLine();
            text.AppendLine($"Esportato il: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

            return text.ToString();
        }
    }

    /// <summary>
    /// Tipi di esportazione supportati
    /// </summary>
    public enum ExportType
    {
        CSV,
        Excel,
        PDF,
        Text
    }

    /// <summary>
    /// Opzioni per l'esportazione
    /// </summary>
    public class ExportOptions
    {
        public bool IncludeProjectInfo { get; set; } = true;
        public bool IncludePartiMacchina { get; set; } = true;
        public bool IncludeSezioni { get; set; } = true;
        public bool IncludeSottosezioni { get; set; } = true;
        public bool IncludeMontaggi { get; set; } = true;
        public bool IncludeGruppi { get; set; } = true;
        public bool IncludeNotes { get; set; } = true;
        public bool IncludeEmptyElements { get; set; } = false;
    }
}