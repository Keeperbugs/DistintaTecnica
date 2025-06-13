using System;
using System.Data.SQLite;
using System.IO;

namespace DistintaTecnica
{
    public class DatabaseManager
    {
        private string connectionString;
        private const string DB_NAME = "DistintaTecnica.db";

        public DatabaseManager()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_NAME);
            connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_NAME)))
            {
                CreateDatabase();
            }
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Tabella Progetti (Informazioni base)
                string createProgettiTable = @"
                    CREATE TABLE IF NOT EXISTS Progetti (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        NumeroCommessa TEXT NOT NULL,
                        Cliente TEXT NOT NULL,
                        DataInserimento DATE NOT NULL,
                        NomeDisegnatore TEXT NOT NULL,
                        LetteraRevisioneInserimento TEXT NOT NULL,
                        DataCreazione DATETIME DEFAULT CURRENT_TIMESTAMP,
                        Note TEXT
                    )";

                // Tabella Parti Macchina
                string createPartiMacchinaTable = @"
                    CREATE TABLE IF NOT EXISTS PartiMacchina (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ProgettoId INTEGER NOT NULL,
                        SottoProgetto TEXT NOT NULL,
                        ParteMacchina TEXT NOT NULL,
                        CodiceParteMacchina TEXT NOT NULL,
                        Descrizione TEXT NOT NULL,
                        RevisioneInserimento TEXT NOT NULL,
                        Stato TEXT DEFAULT 'NEW',
                        Note TEXT,
                        FOREIGN KEY (ProgettoId) REFERENCES Progetti(Id) ON DELETE CASCADE
                    )";

                // Tabella Sezioni
                string createSezioniTable = @"
                    CREATE TABLE IF NOT EXISTS Sezioni (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ParteMacchinaId INTEGER NOT NULL,
                        CodiceSezione TEXT NOT NULL,
                        Descrizione TEXT NOT NULL,
                        Quantita INTEGER NOT NULL DEFAULT 1,
                        RevisioneInserimento TEXT NOT NULL,
                        Stato TEXT DEFAULT 'NEW',
                        Note TEXT,
                        FOREIGN KEY (ParteMacchinaId) REFERENCES PartiMacchina(Id) ON DELETE CASCADE
                    )";

                // Tabella Sottosezioni
                string createSottosezioniTable = @"
                    CREATE TABLE IF NOT EXISTS Sottosezioni (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SezioneId INTEGER NOT NULL,
                        CodiceSottosezione TEXT NOT NULL,
                        Descrizione TEXT NOT NULL,
                        Quantita INTEGER NOT NULL DEFAULT 1,
                        RevisioneInserimento TEXT NOT NULL,
                        Stato TEXT DEFAULT 'NEW',
                        Note TEXT,
                        FOREIGN KEY (SezioneId) REFERENCES Sezioni(Id) ON DELETE CASCADE
                    )";

                // Tabella Montaggi
                string createMontaggiTable = @"
                    CREATE TABLE IF NOT EXISTS Montaggi (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SottosezioneId INTEGER NOT NULL,
                        CodiceMontaggio TEXT NOT NULL,
                        Descrizione TEXT NOT NULL,
                        Quantita INTEGER NOT NULL DEFAULT 1,
                        RevisioneInserimento TEXT NOT NULL,
                        Stato TEXT DEFAULT 'NEW',
                        Note TEXT,
                        FOREIGN KEY (SottosezioneId) REFERENCES Sottosezioni(Id) ON DELETE CASCADE
                    )";

                // Tabella Gruppi
                string createGruppiTable = @"
                    CREATE TABLE IF NOT EXISTS Gruppi (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MontaggioId INTEGER NOT NULL,
                        CodiceGruppo TEXT NOT NULL,
                        TipoGruppo TEXT NOT NULL, -- GRUPPO, VARIANTE, PARTICOLARE, COMMERCIALE
                        Descrizione TEXT NOT NULL,
                        Quantita INTEGER NOT NULL DEFAULT 1,
                        RevisioneInserimento TEXT NOT NULL,
                        Stato TEXT DEFAULT 'NEW',
                        Note TEXT,
                        FOREIGN KEY (MontaggioId) REFERENCES Montaggi(Id) ON DELETE CASCADE
                    )";

                // Tabella per la cronologia delle revisioni
                string createRevisioniTable = @"
                    CREATE TABLE IF NOT EXISTS Revisioni (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TipoElemento TEXT NOT NULL, -- PROGETTO, PARTE_MACCHINA, SEZIONE, SOTTOSEZIONE, MONTAGGIO, GRUPPO
                        ElementoId INTEGER NOT NULL,
                        RevisionePrecedente TEXT,
                        RevisioneNuova TEXT NOT NULL,
                        DataModifica DATETIME DEFAULT CURRENT_TIMESTAMP,
                        Motivo TEXT
                    )";

                // Tabella per gestire i progressivi dei codici auto-generati
                string createProgressiviTable = @"
                    CREATE TABLE IF NOT EXISTS ProgressiviCodici (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TipoCodice TEXT NOT NULL, -- PARTE_MACCHINA, SEZIONE, SOTTOSEZIONE
                        Prefisso TEXT NOT NULL, -- PRO, FOR, TFO, BAS, MSA, GEN, etc.
                        UltimoProgressivo INTEGER NOT NULL DEFAULT 0,
                        FormatoProgressivo TEXT NOT NULL, -- es: C00, D00, A00
                        DataUltimoAggiornamento DATETIME DEFAULT CURRENT_TIMESTAMP,
                        UNIQUE(TipoCodice, Prefisso)
                    )";

                // Tabella per definire i prefissi standard per tipo di elemento
                string createPrefissiStandardTable = @"
                    CREATE TABLE IF NOT EXISTS PrefissiStandard (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TipoElemento TEXT NOT NULL, -- PARTE_MACCHINA, SEZIONE, SOTTOSEZIONE
                        Prefisso TEXT NOT NULL,
                        Descrizione TEXT NOT NULL,
                        Attivo INTEGER DEFAULT 1
                    )";

                // Indici per migliorare le performance
                string createIndexes = @"
                    CREATE INDEX IF NOT EXISTS idx_progetti_commessa ON Progetti(NumeroCommessa);
                    CREATE INDEX IF NOT EXISTS idx_parti_macchina_progetto ON PartiMacchina(ProgettoId);
                    CREATE INDEX IF NOT EXISTS idx_sezioni_parte ON Sezioni(ParteMacchinaId);
                    CREATE INDEX IF NOT EXISTS idx_sottosezioni_sezione ON Sottosezioni(SezioneId);
                    CREATE INDEX IF NOT EXISTS idx_montaggi_sottosezione ON Montaggi(SottosezioneId);
                    CREATE INDEX IF NOT EXISTS idx_gruppi_montaggio ON Gruppi(MontaggioId);
                    CREATE INDEX IF NOT EXISTS idx_revisioni_elemento ON Revisioni(TipoElemento, ElementoId);
                    CREATE INDEX IF NOT EXISTS idx_progressivi_tipo ON ProgressiviCodici(TipoCodice, Prefisso);
                ";

                // Inserimento dei prefissi standard
                string insertPrefissiStandard = @"
                    INSERT OR IGNORE INTO PrefissiStandard (TipoElemento, Prefisso, Descrizione) VALUES
                    -- Parti Macchina
                    ('PARTE_MACCHINA', 'PRO', 'Profila'),
                    ('PARTE_MACCHINA', 'ASP', 'Aspo'),
                    ('PARTE_MACCHINA', 'PIN', 'Pinch Roll'),
                    ('PARTE_MACCHINA', 'INT', 'Intestatrice'),
                    ('PARTE_MACCHINA', 'PRE', 'Preintestatrice'),
                    ('PARTE_MACCHINA', 'FLO', 'Floop'),
                    ('PARTE_MACCHINA', 'ACC', 'Accumulatore'),
                    ('PARTE_MACCHINA', 'TAG', 'Taglio'),
                    ('PARTE_MACCHINA', 'VRU', 'Via Rulli'),
                    ('PARTE_MACCHINA', 'LAN', 'Lancianastro'),
                    
                    -- Sezioni
                    ('SEZIONE', 'FOR', 'Formatura'),
                    ('SEZIONE', 'BAS', 'Basamento'),
                    ('SEZIONE', 'MSA', 'Morse di Saldatura'),
                    ('SEZIONE', 'GEN', 'Guida Entrata Nastro'),
                    ('SEZIONE', 'CAL', 'Calibratura'),
                    ('SEZIONE', 'TAR', 'Taratura'),
                    
                    -- Sottosezioni
                    ('SOTTOSEZIONE', 'TFO', 'Trasmissioni Formatura'),
                    ('SOTTOSEZIONE', 'GFO', 'Gabbie Formatura'),
                    ('SOTTOSEZIONE', 'CFO', 'Cluster Formatura'),
                    ('SOTTOSEZIONE', 'TBA', 'Trasmissioni Basamento'),
                    ('SOTTOSEZIONE', 'SBA', 'Struttura Basamento'),
                    ('SOTTOSEZIONE', 'TMS', 'Trasmissioni Morse Saldatura'),
                    ('SOTTOSEZIONE', 'SMS', 'Struttura Morse Saldatura')
                ";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createProgettiTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createPartiMacchinaTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createSezioniTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createSottosezioniTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createMontaggiTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createGruppiTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createRevisioniTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createProgressiviTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createPrefissiStandardTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createIndexes;
                    command.ExecuteNonQuery();

                    command.CommandText = insertPrefissiStandard;
                    command.ExecuteNonQuery();
                }
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}