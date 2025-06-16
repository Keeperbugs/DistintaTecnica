using System;
using System.Collections.Generic;
using System.Linq;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Business
{
    /// <summary>
    /// Gestore della logica di business per la libreria codici
    /// </summary>
    public class CodesLibraryManager
    {
        private readonly CodesDatabaseManager codesDatabaseManager;
        private readonly CodeGenerator codeGenerator;

        public CodesLibraryManager()
        {
            codesDatabaseManager = new CodesDatabaseManager();

            // Integrazione con il generatore di codici esistente
            var dbManager = new DatabaseManager();
            codeGenerator = new CodeGenerator(dbManager);
        }

        #region Gestione Codici

        /// <summary>
        /// Registra un nuovo codice nella libreria
        /// </summary>
        public bool RegistraCodice(string codice, string descrizione, string numeroCommessa)
        {
            if (string.IsNullOrWhiteSpace(codice) || string.IsNullOrWhiteSpace(descrizione))
                return false;

            try
            {
                // Verifica se il codice esiste già
                if (codesDatabaseManager.CodiceEsiste(codice))
                {
                    // Se esiste, incrementa solo l'utilizzo
                    return codesDatabaseManager.IncrementaUtilizzo(codice, numeroCommessa);
                }
                else
                {
                    // Se non esiste, lo inserisce nuovo
                    return codesDatabaseManager.InserisciCodice(codice, descrizione, numeroCommessa);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Cerca codici nella libreria con filtri avanzati
        /// </summary>
        public List<CodiceBiblioteca> CercaCodici(string termineRicerca = "", TipoElemento? tipoFiltro = null, int limite = 50)
        {
            try
            {
                var risultati = codesDatabaseManager.CercaCodici(termineRicerca, limite);

                // Applica filtro per tipo se specificato
                if (tipoFiltro.HasValue)
                {
                    risultati = FiltraCodiciPerTipo(risultati, tipoFiltro.Value);
                }

                return risultati;
            }
            catch (Exception)
            {
                return new List<CodiceBiblioteca>();
            }
        }

        /// <summary>
        /// Ottiene suggerimenti di codici simili durante la digitazione
        /// </summary>
        public List<SuggerimentoCodice> GetSuggerimenti(string testoDigitato, TipoElemento tipoElemento, int maxSuggerimenti = 10)
        {
            var suggerimenti = new List<SuggerimentoCodice>();

            if (string.IsNullOrWhiteSpace(testoDigitato) || testoDigitato.Length < 2)
                return suggerimenti;

            try
            {
                // Cerca codici che iniziano con il testo digitato
                var codiciTrovati = codesDatabaseManager.CercaCodici(testoDigitato, maxSuggerimenti * 2);

                // Filtra per tipo elemento
                var codiciFiltrati = FiltraCodiciPerTipo(codiciTrovati, tipoElemento);

                // Ordina per rilevanza
                foreach (var codice in codiciFiltrati.Take(maxSuggerimenti))
                {
                    var relevanza = CalcolaRelevanza(codice, testoDigitato);

                    suggerimenti.Add(new SuggerimentoCodice
                    {
                        Codice = codice.Codice,
                        Descrizione = codice.Descrizione,
                        NumeroUtilizzi = codice.NumeroUtilizzi,
                        UltimoUtilizzo = codice.UltimoUtilizzo,
                        Relevanza = relevanza,
                        TipoSuggerimento = DeterminaTipoSuggerimento(codice, testoDigitato)
                    });
                }

                // Ordina per rilevanza decrescente
                suggerimenti = suggerimenti.OrderByDescending(s => s.Relevanza)
                                         .ThenByDescending(s => s.NumeroUtilizzi)
                                         .ToList();
            }
            catch (Exception)
            {
                // In caso di errore, restituisce lista vuota
            }

            return suggerimenti;
        }

        /// <summary>
        /// Verifica se un codice può essere utilizzato e fornisce informazioni
        /// </summary>
        public RisultatoValidazioneCodice ValidaCodice(string codice, TipoElemento tipoElemento, string numeroCommessa)
        {
            var risultato = new RisultatoValidazioneCodice
            {
                Codice = codice,
                IsValido = false
            };

            if (string.IsNullOrWhiteSpace(codice))
            {
                risultato.Messaggio = "Il codice non può essere vuoto";
                return risultato;
            }

            try
            {
                // Verifica formato codice con il generatore esistente
                string tipoElementoString = tipoElemento.ToString().ToUpper();
                bool formatoValido = codeGenerator.ValidaFormatoCodice(codice, tipoElementoString);

                if (!formatoValido)
                {
                    risultato.Messaggio = $"Il formato del codice non è valido per il tipo {tipoElemento}";
                    return risultato;
                }

                // Verifica se esiste nella libreria
                bool esisteInLibreria = codesDatabaseManager.CodiceEsiste(codice);

                if (esisteInLibreria)
                {
                    var descrizioneEsistente = codesDatabaseManager.GetDescrizione(codice);
                    var codiceDettaglio = codesDatabaseManager.CercaCodici(codice, 1).FirstOrDefault();

                    risultato.IsValido = true;
                    risultato.EsisteInLibreria = true;
                    risultato.DescrizioneEsistente = descrizioneEsistente;
                    risultato.Messaggio = $"Codice trovato in libreria: {descrizioneEsistente}";

                    if (codiceDettaglio != null)
                    {
                        risultato.UtilizziPrecedenti = codiceDettaglio.NumeroUtilizzi;
                        risultato.CommessePrecedenti = codiceDettaglio.GetCommesse().ToList();

                        // Verifica se già utilizzato in questa commessa
                        if (codiceDettaglio.UtilizzatoInCommessa(numeroCommessa))
                        {
                            risultato.Messaggio += $" (già utilizzato in questa commessa)";
                        }
                        else
                        {
                            risultato.Messaggio += $" (utilizzato in {risultato.UtilizziPrecedenti} progetti)";
                        }
                    }
                }
                else
                {
                    // Codice nuovo - verifica se è univoco nel sistema principale
                    bool esisteInSistema = codeGenerator.VerificaCodiceEsistente(codice, tipoElementoString);

                    if (esisteInSistema)
                    {
                        risultato.Messaggio = "Codice già esistente nel sistema principale";
                        return risultato;
                    }

                    risultato.IsValido = true;
                    risultato.EsisteInLibreria = false;
                    risultato.Messaggio = "Codice valido - sarà aggiunto alla libreria";
                }
            }
            catch (Exception ex)
            {
                risultato.Messaggio = $"Errore durante la validazione: {ex.Message}";
            }

            return risultato;
        }

        /// <summary>
        /// Ottiene tutti i codici utilizzati in una commessa specifica
        /// </summary>
        public List<CodiceBiblioteca> GetCodiciCommessa(string numeroCommessa)
        {
            if (string.IsNullOrWhiteSpace(numeroCommessa))
                return new List<CodiceBiblioteca>();

            try
            {
                return codesDatabaseManager.GetCodiciPerCommessa(numeroCommessa);
            }
            catch (Exception)
            {
                return new List<CodiceBiblioteca>();
            }
        }

        /// <summary>
        /// Aggiorna la descrizione di un codice esistente
        /// </summary>
        public bool AggiornaDescrizione(string codice, string nuovaDescrizione)
        {
            if (string.IsNullOrWhiteSpace(codice) || string.IsNullOrWhiteSpace(nuovaDescrizione))
                return false;

            try
            {
                return codesDatabaseManager.AggiornaCodice(codice, nuovaDescrizione);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Statistiche e Analisi

        /// <summary>
        /// Ottiene statistiche complete della libreria
        /// </summary>
        public StatisticheLibreria GetStatisticheComplete()
        {
            var statistiche = new StatisticheLibreria();

            try
            {
                var statsBase = codesDatabaseManager.GetStatistiche();

                statistiche.TotaleCodici = statsBase.TotaleCodici;
                statistiche.CodicePiuUtilizzato = statsBase.CodicePiuUtilizzato;
                statistiche.UtilizziMassimi = statsBase.UtilizziMassimi;
                statistiche.UltimoUtilizzo = statsBase.UltimoUtilizzo;

                // Calcola statistiche per tipo
                var tuttiCodici = codesDatabaseManager.CercaCodici("", 1000);
                statistiche.CodiciPerTipo = CalcolaStatistichePerTipo(tuttiCodici);

                // Calcola trend utilizzi
                statistiche.TrendUtilizzi = CalcolaTrendUtilizzi(tuttiCodici);

                // Commesse più attive
                statistiche.CommessePiuAttive = GetCommessePiuAttive(tuttiCodici);
            }
            catch (Exception)
            {
                // In caso di errore, restituisce statistiche vuote
            }

            return statistiche;
        }

        /// <summary>
        /// Genera un report di utilizzo per una commessa
        /// </summary>
        public ReportCommessa GeneraReportCommessa(string numeroCommessa)
        {
            var report = new ReportCommessa
            {
                NumeroCommessa = numeroCommessa,
                DataGenerazione = DateTime.Now
            };

            if (string.IsNullOrWhiteSpace(numeroCommessa))
                return report;

            try
            {
                var codiciCommessa = GetCodiciCommessa(numeroCommessa);

                report.TotaleCodici = codiciCommessa.Count;
                report.CodiciPerTipo = CalcolaStatistichePerTipo(codiciCommessa);

                // Codici riutilizzati (utilizzati in altre commesse)
                report.CodiciRiutilizzati = codiciCommessa.Where(c => c.NumeroUtilizzi > 1).ToList();
                report.PercentualeRiutilizzo = report.TotaleCodici > 0 ?
                    (double)report.CodiciRiutilizzati.Count / report.TotaleCodici * 100 : 0;

                // Codici nuovi (creati per questa commessa)
                report.CodiciNuovi = codiciCommessa.Where(c => c.NumeroUtilizzi == 1).ToList();
            }
            catch (Exception)
            {
                // In caso di errore, restituisce report vuoto
            }

            return report;
        }

        #endregion

        #region Manutenzione

        /// <summary>
        /// Esegue la pulizia e manutenzione della libreria
        /// </summary>
        public RisultatoManutenzione EseguiManutenzione()
        {
            var risultato = new RisultatoManutenzione
            {
                DataEsecuzione = DateTime.Now
            };

            try
            {
                var statsIniziali = codesDatabaseManager.GetStatistiche();
                risultato.CodiciIniziali = statsIniziali.TotaleCodici;

                // Crea backup prima della manutenzione
                string backupPath = codesDatabaseManager.CreaBackup();
                risultato.BackupCreato = !string.IsNullOrEmpty(backupPath);
                risultato.PathBackup = backupPath;

                // TODO: Implementare logiche di pulizia se necessarie
                // Per ora, il database è semplice e non richiede pulizie particolari

                var statsFinali = codesDatabaseManager.GetStatistiche();
                risultato.CodiciFinali = statsFinali.TotaleCodici;
                risultato.Successo = true;
                risultato.Messaggio = "Manutenzione completata con successo";
            }
            catch (Exception ex)
            {
                risultato.Successo = false;
                risultato.Messaggio = $"Errore durante la manutenzione: {ex.Message}";
            }

            return risultato;
        }

        /// <summary>
        /// Crea un backup della libreria
        /// </summary>
        public string CreaBackup()
        {
            try
            {
                return codesDatabaseManager.CreaBackup();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Metodi Privati di Supporto

        /// <summary>
        /// Filtra i codici per tipo elemento
        /// </summary>
        private List<CodiceBiblioteca> FiltraCodiciPerTipo(List<CodiceBiblioteca> codici, TipoElemento tipo)
        {
            return codici.Where(c => DeterminaTipoElemento(c.Codice) == tipo).ToList();
        }

        /// <summary>
        /// Determina il tipo di elemento dal codice
        /// </summary>
        private TipoElemento DeterminaTipoElemento(string codice)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return TipoElemento.Gruppo; // Default

            string prefisso = EstrarePrefisso(codice);

            return prefisso switch
            {
                "PRO" or "ASP" or "PIN" or "INT" or "PRE" or "FLO" or "ACC" or "TAG" or "VRU" or "LAN" => TipoElemento.ParteMacchina,
                "FOR" or "BAS" or "MSA" or "GEN" or "CAL" or "TAR" => TipoElemento.Sezione,
                "TFO" or "GFO" or "CFO" or "TBA" or "SBA" or "TMS" or "SMS" => TipoElemento.Sottosezione,
                _ when codice.Contains("A") && char.IsDigit(codice.Last()) => TipoElemento.Montaggio,
                _ => TipoElemento.Gruppo
            };
        }

        /// <summary>
        /// Estrae il prefisso dal codice
        /// </summary>
        private string EstrarePrefisso(string codice)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return "";

            // Cerca la prima sequenza di lettere all'inizio
            int i = 0;
            while (i < codice.Length && char.IsLetter(codice[i]))
                i++;

            return i > 0 ? codice.Substring(0, i).ToUpper() : "";
        }

        /// <summary>
        /// Calcola la rilevanza di un codice rispetto al termine cercato
        /// </summary>
        private double CalcolaRelevanza(CodiceBiblioteca codice, string termine)
        {
            double punteggio = 0;

            string termineUpper = termine.ToUpper();
            string codiceUpper = codice.Codice.ToUpper();
            string descrizioneUpper = codice.Descrizione.ToUpper();

            // Bonus per corrispondenza esatta all'inizio
            if (codiceUpper.StartsWith(termineUpper))
                punteggio += 100;

            // Bonus per corrispondenza nella descrizione
            if (descrizioneUpper.Contains(termineUpper))
                punteggio += 50;

            // Bonus per numero di utilizzi
            punteggio += Math.Min(codice.NumeroUtilizzi * 5, 50);

            // Bonus per utilizzo recente
            var giorniDaUltimoUtilizzo = (DateTime.Now - codice.UltimoUtilizzo).Days;
            if (giorniDaUltimoUtilizzo < 30)
                punteggio += 30 - giorniDaUltimoUtilizzo;

            return punteggio;
        }

        /// <summary>
        /// Determina il tipo di suggerimento
        /// </summary>
        private TipoSuggerimento DeterminaTipoSuggerimento(CodiceBiblioteca codice, string termine)
        {
            if (codice.Codice.ToUpper().StartsWith(termine.ToUpper()))
                return TipoSuggerimento.CorrispondenzaEsatta;

            if (codice.NumeroUtilizzi > 5)
                return TipoSuggerimento.MoltoUtilizzato;

            if ((DateTime.Now - codice.UltimoUtilizzo).Days < 7)
                return TipoSuggerimento.UtilizzoRecente;

            return TipoSuggerimento.Generico;
        }

        /// <summary>
        /// Calcola statistiche per tipo di elemento
        /// </summary>
        private Dictionary<string, int> CalcolaStatistichePerTipo(List<CodiceBiblioteca> codici)
        {
            var stats = new Dictionary<string, int>();

            foreach (var codice in codici)
            {
                var tipo = DeterminaTipoElemento(codice.Codice).ToString();

                if (stats.ContainsKey(tipo))
                    stats[tipo]++;
                else
                    stats[tipo] = 1;
            }

            return stats;
        }

        /// <summary>
        /// Calcola il trend degli utilizzi nell'ultimo periodo
        /// </summary>
        private Dictionary<string, int> CalcolaTrendUtilizzi(List<CodiceBiblioteca> codici)
        {
            var trend = new Dictionary<string, int>();
            var dataLimite = DateTime.Now.AddDays(-30);

            var codiciRecenti = codici.Where(c => c.UltimoUtilizzo >= dataLimite).ToList();

            trend["UltimaMese"] = codiciRecenti.Count;
            trend["UltimaSettimana"] = codici.Where(c => c.UltimoUtilizzo >= DateTime.Now.AddDays(-7)).Count();
            trend["Oggi"] = codici.Where(c => c.UltimoUtilizzo.Date == DateTime.Now.Date).Count();

            return trend;
        }

        /// <summary>
        /// Ottiene le commesse più attive
        /// </summary>
        private List<CommessaAttiva> GetCommessePiuAttive(List<CodiceBiblioteca> codici)
        {
            var commesseCount = new Dictionary<string, int>();

            foreach (var codice in codici)
            {
                foreach (var commessa in codice.GetCommesse())
                {
                    if (commesseCount.ContainsKey(commessa))
                        commesseCount[commessa]++;
                    else
                        commesseCount[commessa] = 1;
                }
            }

            return commesseCount.OrderByDescending(kvp => kvp.Value)
                               .Take(10)
                               .Select(kvp => new CommessaAttiva
                               {
                                   NumeroCommessa = kvp.Key,
                                   NumeroCodici = kvp.Value
                               })
                               .ToList();
        }

        #endregion
    }

    #region Classi di Supporto

    /// <summary>
    /// Tipo di elemento per filtraggio
    /// </summary>
    public enum TipoElemento
    {
        ParteMacchina,
        Sezione,
        Sottosezione,
        Montaggio,
        Gruppo
    }

    /// <summary>
    /// Suggerimento di codice con informazioni aggiuntive
    /// </summary>
    public class SuggerimentoCodice
    {
        public string Codice { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int NumeroUtilizzi { get; set; }
        public DateTime UltimoUtilizzo { get; set; }
        public double Relevanza { get; set; }
        public TipoSuggerimento TipoSuggerimento { get; set; }

        public override string ToString()
        {
            return $"{Codice} - {Descrizione} (utilizzato {NumeroUtilizzi} volte)";
        }
    }

    /// <summary>
    /// Tipo di suggerimento
    /// </summary>
    public enum TipoSuggerimento
    {
        Generico,
        CorrispondenzaEsatta,
        MoltoUtilizzato,
        UtilizzoRecente
    }

    /// <summary>
    /// Risultato della validazione di un codice
    /// </summary>
    public class RisultatoValidazioneCodice
    {
        public string Codice { get; set; } = string.Empty;
        public bool IsValido { get; set; }
        public bool EsisteInLibreria { get; set; }
        public string DescrizioneEsistente { get; set; } = string.Empty;
        public string Messaggio { get; set; } = string.Empty;
        public int UtilizziPrecedenti { get; set; }
        public List<string> CommessePrecedenti { get; set; } = new List<string>();
    }

    /// <summary>
    /// Statistiche complete della libreria
    /// </summary>
    public class StatisticheLibreria
    {
        public int TotaleCodici { get; set; }
        public string CodicePiuUtilizzato { get; set; } = string.Empty;
        public int UtilizziMassimi { get; set; }
        public DateTime? UltimoUtilizzo { get; set; }
        public Dictionary<string, int> CodiciPerTipo { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> TrendUtilizzi { get; set; } = new Dictionary<string, int>();
        public List<CommessaAttiva> CommessePiuAttive { get; set; } = new List<CommessaAttiva>();
    }

    /// <summary>
    /// Report per una commessa specifica
    /// </summary>
    public class ReportCommessa
    {
        public string NumeroCommessa { get; set; } = string.Empty;
        public DateTime DataGenerazione { get; set; }
        public int TotaleCodici { get; set; }
        public Dictionary<string, int> CodiciPerTipo { get; set; } = new Dictionary<string, int>();
        public List<CodiceBiblioteca> CodiciRiutilizzati { get; set; } = new List<CodiceBiblioteca>();
        public List<CodiceBiblioteca> CodiciNuovi { get; set; } = new List<CodiceBiblioteca>();
        public double PercentualeRiutilizzo { get; set; }
    }

    /// <summary>
    /// Commessa attiva con numero di codici
    /// </summary>
    public class CommessaAttiva
    {
        public string NumeroCommessa { get; set; } = string.Empty;
        public int NumeroCodici { get; set; }
    }

    /// <summary>
    /// Risultato di un'operazione di manutenzione
    /// </summary>
    public class RisultatoManutenzione
    {
        public DateTime DataEsecuzione { get; set; }
        public bool Successo { get; set; }
        public string Messaggio { get; set; } = string.Empty;
        public int CodiciIniziali { get; set; }
        public int CodiciFinali { get; set; }
        public bool BackupCreato { get; set; }
        public string PathBackup { get; set; } = string.Empty;
    }

    #endregion
}