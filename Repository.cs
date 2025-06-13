using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DistintaTecnica.Models;

namespace DistintaTecnica.Data
{
    public class Repository
    {
        private readonly DatabaseManager dbManager;
        private readonly CodeGenerator codeGenerator;

        public Repository(DatabaseManager databaseManager)
        {
            dbManager = databaseManager;
            codeGenerator = new CodeGenerator(databaseManager);
        }

        #region Progetti

        public List<Progetto> GetAllProgetti()
        {
            var progetti = new List<Progetto>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, NumeroCommessa, Cliente, DataInserimento, 
                           NomeDisegnatore, LetteraRevisioneInserimento, 
                           DataCreazione, Note 
                    FROM Progetti 
                    ORDER BY DataCreazione DESC";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        progetti.Add(MapProgetto(reader));
                    }
                }
            }

            return progetti;
        }

        public Progetto GetProgettoById(int id)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, NumeroCommessa, Cliente, DataInserimento, 
                           NomeDisegnatore, LetteraRevisioneInserimento, 
                           DataCreazione, Note 
                    FROM Progetti 
                    WHERE Id = @id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProgetto(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int InsertProgetto(Progetto progetto)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Progetti (NumeroCommessa, Cliente, DataInserimento, 
                                        NomeDisegnatore, LetteraRevisioneInserimento, Note)
                    VALUES (@commessa, @cliente, @dataIns, @disegnatore, @revisione, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@commessa", progetto.NumeroCommessa);
                    command.Parameters.AddWithValue("@cliente", progetto.Cliente);
                    command.Parameters.AddWithValue("@dataIns", progetto.DataInserimento);
                    command.Parameters.AddWithValue("@disegnatore", progetto.NomeDisegnatore);
                    command.Parameters.AddWithValue("@revisione", progetto.LetteraRevisioneInserimento);
                    command.Parameters.AddWithValue("@note", progetto.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool UpdateProgetto(Progetto progetto)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    UPDATE Progetti 
                    SET NumeroCommessa = @commessa, Cliente = @cliente, 
                        DataInserimento = @dataIns, NomeDisegnatore = @disegnatore, 
                        LetteraRevisioneInserimento = @revisione, Note = @note
                    WHERE Id = @id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", progetto.Id);
                    command.Parameters.AddWithValue("@commessa", progetto.NumeroCommessa);
                    command.Parameters.AddWithValue("@cliente", progetto.Cliente);
                    command.Parameters.AddWithValue("@dataIns", progetto.DataInserimento);
                    command.Parameters.AddWithValue("@disegnatore", progetto.NomeDisegnatore);
                    command.Parameters.AddWithValue("@revisione", progetto.LetteraRevisioneInserimento);
                    command.Parameters.AddWithValue("@note", progetto.Note ?? string.Empty);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteProgetto(int id)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Progetti WHERE Id = @id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region Parti Macchina

        public List<ParteMacchina> GetPartiMacchinaByProgetto(int progettoId)
        {
            var parti = new List<ParteMacchina>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                // Check which column name exists
                string columnName = GetParteMacchinaColumnName(connection);

                string query = $@"
                    SELECT Id, ProgettoId, SottoProgetto, {columnName} as TipoParteMacchina, 
                           CodiceParteMacchina, Descrizione, RevisioneInserimento, 
                           Stato, Note 
                    FROM PartiMacchina 
                    WHERE ProgettoId = @progettoId
                    ORDER BY CodiceParteMacchina";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@progettoId", progettoId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parti.Add(MapParteMacchina(reader));
                        }
                    }
                }
            }

            return parti;
        }

        private string GetParteMacchinaColumnName(SQLiteConnection connection)
        {
            try
            {
                string checkQuery = "PRAGMA table_info(PartiMacchina)";
                using (var command = new SQLiteCommand(checkQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string columnName = reader["name"].ToString();
                        if (columnName == "TipoParteMacchina")
                        {
                            return "TipoParteMacchina";
                        }
                        else if (columnName == "ParteMacchina")
                        {
                            return "ParteMacchina";
                        }
                    }
                }
                return "ParteMacchina"; // Default fallback
            }
            catch (Exception)
            {
                return "ParteMacchina"; // Default fallback
            }
        }

        public int InsertParteMacchina(ParteMacchina parte)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO PartiMacchina (ProgettoId, SottoProgetto, TipoParteMacchina, 
                                             CodiceParteMacchina, Descrizione, RevisioneInserimento, 
                                             Stato, Note)
                    VALUES (@progettoId, @sottoProgetto, @tipoParteMacchina, @codice, 
                            @descrizione, @revisione, @stato, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@progettoId", parte.ProgettoId);
                    command.Parameters.AddWithValue("@sottoProgetto", parte.SottoProgetto);
                    command.Parameters.AddWithValue("@tipoParteMacchina", parte.TipoParteMacchina);
                    command.Parameters.AddWithValue("@codice", parte.CodiceParteMacchina);
                    command.Parameters.AddWithValue("@descrizione", parte.Descrizione);
                    command.Parameters.AddWithValue("@revisione", parte.RevisioneInserimento);
                    command.Parameters.AddWithValue("@stato", parte.Stato);
                    command.Parameters.AddWithValue("@note", parte.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region Sezioni

        public List<Sezione> GetSezioniByParteMacchina(int parteMacchinaId)
        {
            var sezioni = new List<Sezione>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, ParteMacchinaId, CodiceSezione, Descrizione, 
                           Quantita, RevisioneInserimento, Stato, Note 
                    FROM Sezioni 
                    WHERE ParteMacchinaId = @parteMacchinaId
                    ORDER BY CodiceSezione";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parteMacchinaId", parteMacchinaId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sezioni.Add(MapSezione(reader));
                        }
                    }
                }
            }

            return sezioni;
        }

        public int InsertSezione(Sezione sezione)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Sezioni (ParteMacchinaId, CodiceSezione, Descrizione, 
                                       Quantita, RevisioneInserimento, Stato, Note)
                    VALUES (@parteMacchinaId, @codice, @descrizione, @quantita, 
                            @revisione, @stato, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@parteMacchinaId", sezione.ParteMacchinaId);
                    command.Parameters.AddWithValue("@codice", sezione.CodiceSezione);
                    command.Parameters.AddWithValue("@descrizione", sezione.Descrizione);
                    command.Parameters.AddWithValue("@quantita", sezione.Quantita);
                    command.Parameters.AddWithValue("@revisione", sezione.RevisioneInserimento);
                    command.Parameters.AddWithValue("@stato", sezione.Stato);
                    command.Parameters.AddWithValue("@note", sezione.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region Sottosezioni

        public List<Sottosezione> GetSottosezioniBySezione(int sezioneId)
        {
            var sottosezioni = new List<Sottosezione>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, SezioneId, CodiceSottosezione, Descrizione, 
                           Quantita, RevisioneInserimento, Stato, Note 
                    FROM Sottosezioni 
                    WHERE SezioneId = @sezioneId
                    ORDER BY CodiceSottosezione";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sezioneId", sezioneId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sottosezioni.Add(MapSottosezione(reader));
                        }
                    }
                }
            }

            return sottosezioni;
        }

        public int InsertSottosezione(Sottosezione sottosezione)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Sottosezioni (SezioneId, CodiceSottosezione, Descrizione, 
                                            Quantita, RevisioneInserimento, Stato, Note)
                    VALUES (@sezioneId, @codice, @descrizione, @quantita, 
                            @revisione, @stato, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sezioneId", sottosezione.SezioneId);
                    command.Parameters.AddWithValue("@codice", sottosezione.CodiceSottosezione);
                    command.Parameters.AddWithValue("@descrizione", sottosezione.Descrizione);
                    command.Parameters.AddWithValue("@quantita", sottosezione.Quantita);
                    command.Parameters.AddWithValue("@revisione", sottosezione.RevisioneInserimento);
                    command.Parameters.AddWithValue("@stato", sottosezione.Stato);
                    command.Parameters.AddWithValue("@note", sottosezione.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region Montaggi

        public List<Montaggio> GetMontaggiBySottosezione(int sottosezioneId)
        {
            var montaggi = new List<Montaggio>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, SottosezioneId, CodiceMontaggio, Descrizione, 
                           Quantita, RevisioneInserimento, Stato, Note 
                    FROM Montaggi 
                    WHERE SottosezioneId = @sottosezioneId
                    ORDER BY CodiceMontaggio";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sottosezioneId", sottosezioneId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            montaggi.Add(MapMontaggio(reader));
                        }
                    }
                }
            }

            return montaggi;
        }

        public int InsertMontaggio(Montaggio montaggio)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Montaggi (SottosezioneId, CodiceMontaggio, Descrizione, 
                                        Quantita, RevisioneInserimento, Stato, Note)
                    VALUES (@sottosezioneId, @codice, @descrizione, @quantita, 
                            @revisione, @stato, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sottosezioneId", montaggio.SottosezioneId);
                    command.Parameters.AddWithValue("@codice", montaggio.CodiceMontaggio);
                    command.Parameters.AddWithValue("@descrizione", montaggio.Descrizione);
                    command.Parameters.AddWithValue("@quantita", montaggio.Quantita);
                    command.Parameters.AddWithValue("@revisione", montaggio.RevisioneInserimento);
                    command.Parameters.AddWithValue("@stato", montaggio.Stato);
                    command.Parameters.AddWithValue("@note", montaggio.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region Gruppi

        public List<Gruppo> GetGruppiByMontaggio(int montaggioId)
        {
            var gruppi = new List<Gruppo>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Id, MontaggioId, CodiceGruppo, TipoGruppo, Descrizione, 
                           Quantita, RevisioneInserimento, Stato, Note 
                    FROM Gruppi 
                    WHERE MontaggioId = @montaggioId
                    ORDER BY TipoGruppo, CodiceGruppo";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@montaggioId", montaggioId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gruppi.Add(MapGruppo(reader));
                        }
                    }
                }
            }

            return gruppi;
        }

        public int InsertGruppo(Gruppo gruppo)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Gruppi (MontaggioId, CodiceGruppo, TipoGruppo, Descrizione, 
                                      Quantita, RevisioneInserimento, Stato, Note)
                    VALUES (@montaggioId, @codice, @tipo, @descrizione, @quantita, 
                            @revisione, @stato, @note);
                    SELECT last_insert_rowid();";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@montaggioId", gruppo.MontaggioId);
                    command.Parameters.AddWithValue("@codice", gruppo.CodiceGruppo);
                    command.Parameters.AddWithValue("@tipo", gruppo.TipoGruppo);
                    command.Parameters.AddWithValue("@descrizione", gruppo.Descrizione);
                    command.Parameters.AddWithValue("@quantita", gruppo.Quantita);
                    command.Parameters.AddWithValue("@revisione", gruppo.RevisioneInserimento);
                    command.Parameters.AddWithValue("@stato", gruppo.Stato);
                    command.Parameters.AddWithValue("@note", gruppo.Note ?? string.Empty);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion

        #region Validazioni e Utilità

        public ValidationResult ValidateAndCheckCode(string codice, string tipoElemento, int? excludeId = null)
        {
            var result = new ValidationResult { IsValid = true };

            // Validazione formato
            if (!codeGenerator.ValidaFormatoCodice(codice, tipoElemento))
            {
                result.IsValid = false;
                result.Errors.Add($"Il formato del codice '{codice}' non è valido per il tipo '{tipoElemento}'.");
            }

            // Verifica esistenza
            if (codeGenerator.VerificaCodiceEsistente(codice, tipoElemento))
            {
                result.Warnings.Add($"⚠️ ATTENZIONE: Il codice '{codice}' esiste già nel database!");
            }

            return result;
        }

        public List<SearchResult> SearchGlobal(string searchTerm)
        {
            var results = new List<SearchResult>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                // Cerca in tutti i livelli
                string query = @"
                    SELECT 'PROGETTO' as Tipo, p.Id, p.NumeroCommessa as Codice, 
                           (p.Cliente || ' - ' || p.NomeDisegnatore) as Descrizione, 
                           p.NumeroCommessa as Progetto, 
                           p.NumeroCommessa as PathCompleto
                    FROM Progetti p
                    WHERE p.NumeroCommessa LIKE @term OR p.Cliente LIKE @term
                    
                    UNION ALL
                    
                    SELECT 'PARTE_MACCHINA' as Tipo, pm.Id, pm.CodiceParteMacchina as Codice,
                           pm.Descrizione, p.NumeroCommessa as Progetto,
                           (p.NumeroCommessa || ' > ' || pm.CodiceParteMacchina) as PathCompleto
                    FROM PartiMacchina pm
                    INNER JOIN Progetti p ON pm.ProgettoId = p.Id
                    WHERE pm.CodiceParteMacchina LIKE @term OR pm.Descrizione LIKE @term
                    
                    ORDER BY Tipo, Codice";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@term", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new SearchResult
                            {
                                Tipo = reader["Tipo"].ToString(),
                                Id = Convert.ToInt32(reader["Id"]),
                                Codice = reader["Codice"].ToString(),
                                Descrizione = reader["Descrizione"].ToString(),
                                Progetto = reader["Progetto"].ToString(),
                                PathCompleto = reader["PathCompleto"].ToString()
                            });
                        }
                    }
                }
            }

            return results;
        }

        public CodeGenerator GetCodeGenerator()
        {
            return codeGenerator;
        }

        #endregion

        #region Mapping Methods

        private Progetto MapProgetto(SQLiteDataReader reader)
        {
            return new Progetto
            {
                Id = Convert.ToInt32(reader["Id"]),
                NumeroCommessa = reader["NumeroCommessa"].ToString(),
                Cliente = reader["Cliente"].ToString(),
                DataInserimento = Convert.ToDateTime(reader["DataInserimento"]),
                NomeDisegnatore = reader["NomeDisegnatore"].ToString(),
                LetteraRevisioneInserimento = reader["LetteraRevisioneInserimento"].ToString(),
                DataCreazione = Convert.ToDateTime(reader["DataCreazione"]),
                Note = reader["Note"].ToString()
            };
        }

        private ParteMacchina MapParteMacchina(SQLiteDataReader reader)
        {
            return new ParteMacchina
            {
                Id = Convert.ToInt32(reader["Id"]),
                ProgettoId = Convert.ToInt32(reader["ProgettoId"]),
                SottoProgetto = reader["SottoProgetto"].ToString(),
                TipoParteMacchina = GetTipoParteMacchinaFromReader(reader),
                CodiceParteMacchina = reader["CodiceParteMacchina"].ToString(),
                Descrizione = reader["Descrizione"].ToString(),
                RevisioneInserimento = reader["RevisioneInserimento"].ToString(),
                Stato = reader["Stato"].ToString(),
                Note = reader["Note"].ToString()
            };
        }

        private string GetTipoParteMacchinaFromReader(SQLiteDataReader reader)
        {
            try
            {
                // Try new column name first
                return reader["TipoParteMacchina"].ToString();
            }
            catch (Exception)
            {
                try
                {
                    // Fallback to old column name
                    return reader["ParteMacchina"].ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        private Sezione MapSezione(SQLiteDataReader reader)
        {
            return new Sezione
            {
                Id = Convert.ToInt32(reader["Id"]),
                ParteMacchinaId = Convert.ToInt32(reader["ParteMacchinaId"]),
                CodiceSezione = reader["CodiceSezione"].ToString(),
                Descrizione = reader["Descrizione"].ToString(),
                Quantita = Convert.ToInt32(reader["Quantita"]),
                RevisioneInserimento = reader["RevisioneInserimento"].ToString(),
                Stato = reader["Stato"].ToString(),
                Note = reader["Note"].ToString()
            };
        }

        private Sottosezione MapSottosezione(SQLiteDataReader reader)
        {
            return new Sottosezione
            {
                Id = Convert.ToInt32(reader["Id"]),
                SezioneId = Convert.ToInt32(reader["SezioneId"]),
                CodiceSottosezione = reader["CodiceSottosezione"].ToString(),
                Descrizione = reader["Descrizione"].ToString(),
                Quantita = Convert.ToInt32(reader["Quantita"]),
                RevisioneInserimento = reader["RevisioneInserimento"].ToString(),
                Stato = reader["Stato"].ToString(),
                Note = reader["Note"].ToString()
            };
        }

        private Montaggio MapMontaggio(SQLiteDataReader reader)
        {
            return new Montaggio
            {
                Id = Convert.ToInt32(reader["Id"]),
                SottosezioneId = Convert.ToInt32(reader["SottosezioneId"]),
                CodiceMontaggio = reader["CodiceMontaggio"].ToString(),
                Descrizione = reader["Descrizione"].ToString(),
                Quantita = Convert.ToInt32(reader["Quantita"]),
                RevisioneInserimento = reader["RevisioneInserimento"].ToString(),
                Stato = reader["Stato"].ToString(),
                Note = reader["Note"].ToString()
            };
        }

        private Gruppo MapGruppo(SQLiteDataReader reader)
        {
            return new Gruppo
            {
                Id = Convert.ToInt32(reader["Id"]),
                MontaggioId = Convert.ToInt32(reader["MontaggioId"]),
                CodiceGruppo = reader["CodiceGruppo"].ToString(),
                TipoGruppo = reader["TipoGruppo"].ToString(),
                Descrizione = reader["Descrizione"].ToString(),
                Quantita = Convert.ToInt32(reader["Quantita"]),
                RevisioneInserimento = reader["RevisioneInserimento"].ToString(),
                Stato = reader["Stato"].ToString(),
                Note = reader["Note"].ToString()
            };
        }

        #endregion
    }
}