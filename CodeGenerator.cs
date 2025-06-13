using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DistintaTecnica
{
    public class CodeGenerator
    {
        private readonly DatabaseManager dbManager;

        public CodeGenerator(DatabaseManager databaseManager)
        {
            dbManager = databaseManager;
        }

        /// <summary>
        /// Genera un nuovo codice per Parte Macchina
        /// Formato: PRO1394C00 (Prefisso + Diametro + Spessore + Progressivo)
        /// </summary>
        public string GeneraCodiceParteMacchina(string prefisso, int diametro, int spessore)
        {
            string progressivo = GetNextProgressivo("PARTE_MACCHINA", prefisso, "C");
            return $"{prefisso}{diametro:D3}{spessore}{progressivo}";
        }

        /// <summary>
        /// Genera un nuovo codice per Sezione
        /// Formato: FOR1687C12 (Prefisso + riferimento parte macchina + progressivo)
        /// </summary>
        public string GeneraCodiceSezione(string prefisso, string codiceParteMacchinaRiferimento)
        {
            // Estrae la parte numerica dal codice della parte macchina
            string baseCode = ExtractNumericPart(codiceParteMacchinaRiferimento);
            string progressivo = GetNextProgressivo("SEZIONE", prefisso, "C");
            return $"{prefisso}{baseCode}{progressivo}";
        }

        /// <summary>
        /// Genera un nuovo codice per Sottosezione
        /// Formato: TFO1687D11 (Prefisso + riferimento + progressivo)
        /// </summary>
        public string GeneraCodiceSottosezione(string prefisso, string codiceSezioneRiferimento)
        {
            string baseCode = ExtractNumericPart(codiceSezioneRiferimento);
            string progressivo = GetNextProgressivo("SOTTOSEZIONE", prefisso, "D");
            return $"{prefisso}{baseCode}{progressivo}";
        }

        /// <summary>
        /// Verifica se un codice esiste già nel database
        /// </summary>
        public bool VerificaCodiceEsistente(string codice, string tipoElemento)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = "";

                switch (tipoElemento.ToUpper())
                {
                    case "PARTE_MACCHINA":
                        query = "SELECT COUNT(*) FROM PartiMacchina WHERE CodiceParteMacchina = @codice";
                        break;
                    case "SEZIONE":
                        query = "SELECT COUNT(*) FROM Sezioni WHERE CodiceSezione = @codice";
                        break;
                    case "SOTTOSEZIONE":
                        query = "SELECT COUNT(*) FROM Sottosezioni WHERE CodiceSottosezione = @codice";
                        break;
                    case "MONTAGGIO":
                        query = "SELECT COUNT(*) FROM Montaggi WHERE CodiceMontaggio = @codice";
                        break;
                    case "GRUPPO":
                        query = "SELECT COUNT(*) FROM Gruppi WHERE CodiceGruppo = @codice";
                        break;
                    default:
                        return false;
                }

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codice", codice);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        /// <summary>
        /// Ottiene la lista dei prefissi disponibili per un tipo di elemento
        /// </summary>
        public List<PrefissoStandard> GetPrefissiDisponibili(string tipoElemento)
        {
            var prefissi = new List<PrefissoStandard>();

            using (var connection = dbManager.GetConnection())
            {
                connection.Open();
                string query = @"
                    SELECT Prefisso, Descrizione 
                    FROM PrefissiStandard 
                    WHERE TipoElemento = @tipo AND Attivo = 1 
                    ORDER BY Prefisso";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tipo", tipoElemento);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prefissi.Add(new PrefissoStandard
                            {
                                Prefisso = reader["Prefisso"].ToString(),
                                Descrizione = reader["Descrizione"].ToString()
                            });
                        }
                    }
                }
            }

            return prefissi;
        }

        /// <summary>
        /// Valida il formato di un codice in base al tipo
        /// </summary>
        public bool ValidaFormatoCodice(string codice, string tipoElemento)
        {
            if (string.IsNullOrWhiteSpace(codice))
                return false;

            switch (tipoElemento.ToUpper())
            {
                case "GRUPPO":
                    // Gruppi: 5 cifre + identificativo (es: 51152M, 51152VA, 51152V1)
                    return codice.Length >= 6 &&
                           codice.Substring(0, 5).All(char.IsDigit) &&
                           codice.Substring(5).All(c => char.IsLetter(c) || char.IsDigit(c));

                case "COMMERCIALE":
                    // Commerciali: 6 cifre
                    return codice.Length == 6 && codice.All(char.IsDigit);

                case "PARTICOLARE":
                    // Particolari: 7 cifre
                    return codice.Length == 7 && codice.All(char.IsDigit);

                case "MONTAGGIO":
                    // Montaggi: codice numerico + A + progressivo (es: 82509A1)
                    return codice.Contains('A') &&
                           codice.IndexOf('A') > 0 &&
                           codice.Substring(0, codice.IndexOf('A')).All(char.IsDigit) &&
                           codice.Substring(codice.IndexOf('A') + 1).All(char.IsDigit);

                default:
                    return true; // Per parti macchina, sezioni e sottosezioni la validazione è più flessibile
            }
        }

        private string GetNextProgressivo(string tipoCodice, string prefisso, string lettera)
        {
            using (var connection = dbManager.GetConnection())
            {
                connection.Open();

                // Controlla se esiste già un record per questo tipo e prefisso
                string selectQuery = @"
                    SELECT UltimoProgressivo 
                    FROM ProgressiviCodici 
                    WHERE TipoCodice = @tipo AND Prefisso = @prefisso";

                using (var selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@tipo", tipoCodice);
                    selectCommand.Parameters.AddWithValue("@prefisso", prefisso);

                    var result = selectCommand.ExecuteScalar();
                    int ultimoProgressivo = result != null ? Convert.ToInt32(result) : 0;

                    // Incrementa il progressivo
                    int nuovoProgressivo = ultimoProgressivo + 1;

                    // Aggiorna o inserisce il record
                    string upsertQuery = @"
                        INSERT OR REPLACE INTO ProgressiviCodici 
                        (TipoCodice, Prefisso, UltimoProgressivo, FormatoProgressivo, DataUltimoAggiornamento)
                        VALUES (@tipo, @prefisso, @progressivo, @formato, CURRENT_TIMESTAMP)";

                    using (var upsertCommand = new SQLiteCommand(upsertQuery, connection))
                    {
                        upsertCommand.Parameters.AddWithValue("@tipo", tipoCodice);
                        upsertCommand.Parameters.AddWithValue("@prefisso", prefisso);
                        upsertCommand.Parameters.AddWithValue("@progressivo", nuovoProgressivo);
                        upsertCommand.Parameters.AddWithValue("@formato", $"{lettera}00");
                        upsertCommand.ExecuteNonQuery();
                    }

                    return $"{lettera}{nuovoProgressivo:D2}";
                }
            }
        }

        private string ExtractNumericPart(string codice)
        {
            if (string.IsNullOrEmpty(codice))
                return "0000";

            // Estrae la parte numerica dal codice (es: da PRO1394C00 estrae 1394)
            string numericPart = "";
            bool foundDigits = false;

            foreach (char c in codice)
            {
                if (char.IsDigit(c))
                {
                    numericPart += c;
                    foundDigits = true;
                }
                else if (foundDigits && char.IsLetter(c))
                {
                    // Se abbiamo trovato cifre e poi una lettera, interrompiamo
                    break;
                }
            }

            return string.IsNullOrEmpty(numericPart) ? "0000" : numericPart;
        }
    }

    public class PrefissoStandard
    {
        public string Prefisso { get; set; }
        public string Descrizione { get; set; }
    }
}