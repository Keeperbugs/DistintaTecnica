using System;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace DistintaTecnica.Data
{
    /// <summary>
    /// Gestore del database semplificato per la libreria codici
    /// </summary>
    public class CodesDatabaseManager
    {
        private string connectionString;
        private const string DB_NAME = "LibreriaCodici.db";

        public CodesDatabaseManager()
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

                // Tabella semplificata libreria codici
                string createLibreriaCodiciTable = @"
                    CREATE TABLE IF NOT EXISTS LibreriaCodici (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Codice TEXT NOT NULL UNIQUE,
                        Descrizione TEXT NOT NULL,
                        DataCreazione DATETIME DEFAULT CURRENT_TIMESTAMP,
                        UltimoUtilizzo DATETIME DEFAULT CURRENT_TIMESTAMP,
                        NumeroUtilizzi INTEGER DEFAULT 1,
                        CommesseUtilizzo TEXT -- Lista commesse separate da virgola dove è stato utilizzato
                    )";

                // Indici per performance
                string createIndexes = @"
                    CREATE INDEX IF NOT EXISTS idx_codice ON LibreriaCodici(Codice);
                    CREATE INDEX IF NOT EXISTS idx_descrizione ON LibreriaCodici(Descrizione);
                    CREATE INDEX IF NOT EXISTS idx_utilizzi ON LibreriaCodici(NumeroUtilizzi);
                ";

                using (var command = new SQLiteCommand(connection))
                {
                    // Crea la tabella
                    command.CommandText = createLibreriaCodiciTable;
                    command.ExecuteNonQuery();

                    // Crea gli indici
                    command.CommandText = createIndexes;
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

        /// <summary>
        /// Verifica se un codice esiste nel database
        /// </summary>
        public bool CodiceEsiste(string codice)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM LibreriaCodici WHERE Codice = @codice COLLATE NOCASE";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        return Convert.ToInt32(command.ExecuteScalar()) > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Ottiene la descrizione di un codice esistente
        /// </summary>
        public string GetDescrizione(string codice)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return string.Empty;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Descrizione FROM LibreriaCodici WHERE Codice = @codice COLLATE NOCASE";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Inserisce un nuovo codice nella libreria
        /// </summary>
        public bool InserisciCodice(string codice, string descrizione, string numeroCommessa = "")
        {
            if (string.IsNullOrWhiteSpace(codice) || string.IsNullOrWhiteSpace(descrizione))
                return false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO LibreriaCodici (Codice, Descrizione, CommesseUtilizzo) 
                        VALUES (@codice, @descrizione, @commessa)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        command.Parameters.AddWithValue("@descrizione", descrizione.Trim());
                        command.Parameters.AddWithValue("@commessa", numeroCommessa?.Trim() ?? string.Empty);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false; // Codice già esistente o altro errore
            }
        }

        /// <summary>
        /// Aggiorna la descrizione di un codice esistente
        /// </summary>
        public bool AggiornaCodice(string codice, string nuovaDescrizione)
        {
            if (string.IsNullOrWhiteSpace(codice) || string.IsNullOrWhiteSpace(nuovaDescrizione))
                return false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE LibreriaCodici 
                        SET Descrizione = @descrizione, UltimoUtilizzo = CURRENT_TIMESTAMP 
                        WHERE Codice = @codice COLLATE NOCASE";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        command.Parameters.AddWithValue("@descrizione", nuovaDescrizione.Trim());
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Incrementa il contatore utilizzi di un codice e aggiunge la commessa
        /// </summary>
        public bool IncrementaUtilizzo(string codice, string numeroCommessa = "")
        {
            if (string.IsNullOrWhiteSpace(codice))
                return false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Prima ottieni le commesse attuali
                    string selectQuery = "SELECT CommesseUtilizzo FROM LibreriaCodici WHERE Codice = @codice COLLATE NOCASE";
                    string commesseAttuali = "";

                    using (var selectCommand = new SQLiteCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@codice", codice.Trim());
                        var result = selectCommand.ExecuteScalar();
                        commesseAttuali = result?.ToString() ?? string.Empty;
                    }

                    // Aggiungi la nuova commessa se non c'è già
                    string nuoveCommesse = AggiungiCommessa(commesseAttuali, numeroCommessa);

                    // Aggiorna il record
                    string updateQuery = @"
                        UPDATE LibreriaCodici 
                        SET NumeroUtilizzi = NumeroUtilizzi + 1, 
                            UltimoUtilizzo = CURRENT_TIMESTAMP,
                            CommesseUtilizzo = @commesse
                        WHERE Codice = @codice COLLATE NOCASE";

                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        command.Parameters.AddWithValue("@commesse", nuoveCommesse);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Aggiunge una commessa alla lista delle commesse (evitando duplicati)
        /// </summary>
        private string AggiungiCommessa(string commesseAttuali, string nuovaCommessa)
        {
            if (string.IsNullOrWhiteSpace(nuovaCommessa))
                return commesseAttuali;

            if (string.IsNullOrWhiteSpace(commesseAttuali))
                return nuovaCommessa.Trim();

            // Verifica se la commessa è già presente
            var commesse = commesseAttuali.Split(',').Select(c => c.Trim()).ToList();
            if (!commesse.Contains(nuovaCommessa.Trim(), StringComparer.OrdinalIgnoreCase))
            {
                commesse.Add(nuovaCommessa.Trim());
            }

            return string.Join(", ", commesse);
        }

        /// <summary>
        /// Cerca codici nella libreria
        /// </summary>
        public List<CodiceBiblioteca> CercaCodici(string termineRicerca = "", int limite = 100)
        {
            var risultati = new List<CodiceBiblioteca>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query;

                    if (string.IsNullOrWhiteSpace(termineRicerca))
                    {
                        query = @"
                            SELECT Codice, Descrizione, NumeroUtilizzi, UltimoUtilizzo, CommesseUtilizzo 
                            FROM LibreriaCodici 
                            ORDER BY NumeroUtilizzi DESC, UltimoUtilizzo DESC 
                            LIMIT @limite";
                    }
                    else
                    {
                        query = @"
                            SELECT Codice, Descrizione, NumeroUtilizzi, UltimoUtilizzo, CommesseUtilizzo 
                            FROM LibreriaCodici 
                            WHERE Codice LIKE @termine COLLATE NOCASE 
                               OR Descrizione LIKE @termine COLLATE NOCASE 
                            ORDER BY NumeroUtilizzi DESC, UltimoUtilizzo DESC 
                            LIMIT @limite";
                    }

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(termineRicerca))
                        {
                            command.Parameters.AddWithValue("@termine", $"%{termineRicerca.Trim()}%");
                        }
                        command.Parameters.AddWithValue("@limite", limite);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                risultati.Add(new CodiceBiblioteca
                                {
                                    Codice = reader["Codice"].ToString(),
                                    Descrizione = reader["Descrizione"].ToString(),
                                    NumeroUtilizzi = Convert.ToInt32(reader["NumeroUtilizzi"]),
                                    UltimoUtilizzo = Convert.ToDateTime(reader["UltimoUtilizzo"]),
                                    CommesseUtilizzo = reader["CommesseUtilizzo"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // In caso di errore, restituisce lista vuota
            }

            return risultati;
        }

        /// <summary>
        /// Ottiene le statistiche base della libreria
        /// </summary>
        public StatisticheBiblioteca GetStatistiche()
        {
            var stats = new StatisticheBiblioteca();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Totale codici
                    string queryTotale = "SELECT COUNT(*) FROM LibreriaCodici";
                    using (var command = new SQLiteCommand(queryTotale, connection))
                    {
                        stats.TotaleCodici = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // Codice più utilizzato
                    string queryPiuUsato = @"
                        SELECT Codice, Descrizione, NumeroUtilizzi 
                        FROM LibreriaCodici 
                        ORDER BY NumeroUtilizzi DESC 
                        LIMIT 1";

                    using (var command = new SQLiteCommand(queryPiuUsato, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.CodicePiuUtilizzato = reader["Codice"].ToString();
                            stats.UtilizziMassimi = Convert.ToInt32(reader["NumeroUtilizzi"]);
                        }
                    }

                    // Ultimo utilizzo
                    string queryUltimoUtilizzo = "SELECT MAX(UltimoUtilizzo) FROM LibreriaCodici";
                    using (var command = new SQLiteCommand(queryUltimoUtilizzo, connection))
                    {
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            stats.UltimoUtilizzo = Convert.ToDateTime(result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // In caso di errore, restituisce statistiche vuote
            }

            return stats;
        }

        /// <summary>
        /// Elimina un codice dalla libreria
        /// </summary>
        public bool EliminaCodice(string codice)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM LibreriaCodici WHERE Codice = @codice COLLATE NOCASE";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codice", codice.Trim());
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Ottiene tutti i codici utilizzati in una specifica commessa
        /// </summary>
        public List<CodiceBiblioteca> GetCodiciPerCommessa(string numeroCommessa)
        {
            var risultati = new List<CodiceBiblioteca>();

            if (string.IsNullOrWhiteSpace(numeroCommessa))
                return risultati;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = @"
                        SELECT Codice, Descrizione, NumeroUtilizzi, UltimoUtilizzo, CommesseUtilizzo 
                        FROM LibreriaCodici 
                        WHERE CommesseUtilizzo LIKE @commessa COLLATE NOCASE
                        ORDER BY Codice";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@commessa", $"%{numeroCommessa.Trim()}%");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var codice = new CodiceBiblioteca
                                {
                                    Codice = reader["Codice"].ToString(),
                                    Descrizione = reader["Descrizione"].ToString(),
                                    NumeroUtilizzi = Convert.ToInt32(reader["NumeroUtilizzi"]),
                                    UltimoUtilizzo = Convert.ToDateTime(reader["UltimoUtilizzo"]),
                                    CommesseUtilizzo = reader["CommesseUtilizzo"].ToString()
                                };

                                // Verifica che la commessa sia effettivamente presente nella lista
                                if (codice.UtilizzatoInCommessa(numeroCommessa))
                                {
                                    risultati.Add(codice);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // In caso di errore, restituisce lista vuota
            }

            return risultati;
        }
        public string CreaBackup()
        {
            try
            {
                string backupDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupPath = Path.Combine(backupDir, $"LibreriaCodici_Backup_{timestamp}.db");

                string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_NAME);
                File.Copy(sourcePath, backupPath, true);

                return backupPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la creazione del backup: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Rappresenta un codice nella biblioteca
    /// </summary>
    public class CodiceBiblioteca
    {
        public string Codice { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int NumeroUtilizzi { get; set; }
        public DateTime UltimoUtilizzo { get; set; }
        public string CommesseUtilizzo { get; set; } = string.Empty;

        /// <summary>
        /// Ottiene la lista delle commesse come array
        /// </summary>
        public string[] GetCommesse()
        {
            if (string.IsNullOrWhiteSpace(CommesseUtilizzo))
                return new string[0];

            return CommesseUtilizzo.Split(',')
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .ToArray();
        }

        /// <summary>
        /// Verifica se il codice è stato utilizzato in una specifica commessa
        /// </summary>
        public bool UtilizzatoInCommessa(string numeroCommessa)
        {
            if (string.IsNullOrWhiteSpace(numeroCommessa))
                return false;

            return GetCommesse().Contains(numeroCommessa.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Codice} - {Descrizione}";
        }
    }

    /// <summary>
    /// Statistiche della biblioteca codici
    /// </summary>
    public class StatisticheBiblioteca
    {
        public int TotaleCodici { get; set; }
        public string CodicePiuUtilizzato { get; set; } = string.Empty;
        public int UtilizziMassimi { get; set; }
        public DateTime? UltimoUtilizzo { get; set; }
    }
}