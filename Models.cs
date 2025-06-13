using DistintaTecnica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DistintaTecnica.Models
{
    // Enumerazioni per le liste dropdown
    public enum SottoProgetto
    {
        [Description("-03 ASSEMBLY")]
        Assembly,
        [Description("-04 ENCLOSED SPARE PARTS")]
        EnclosedSpareParts,
        [Description("-05 THIRD PARTIES SUPPLIES")]
        ThirdPartiesSupplies,
        [Description("-06 SHIPPING")]
        Shipping,
        [Description("-07 START UP")]
        StartUp,
        [Description("-08 OTHER COST")]
        OtherCost,
        [Description("-09 WARRANTIES")]
        Warranties
    }

    public enum TipoParteMacchina
    {
        [Description("ASPO")]
        Aspo,
        [Description("PINCH ROLL")]
        PinchRoll,
        [Description("INTESTATRICE")]
        Intestatrice,
        [Description("PREINTESTATRICE")]
        Preintestatrice,
        [Description("FLOOP")]
        Floop,
        [Description("ACCUMULATORE")]
        Accumulatore,
        [Description("PROFILA")]
        Profila,
        [Description("TAGLIO")]
        Taglio,
        [Description("VIA RULLI")]
        ViaRulli,
        [Description("LANCIANASTRO")]
        Lancianastro,
        [Description("PARTOTO_EXTRA_STD")]
        PartotoExtraStd,
        [Description("FORNITURA MATERIALE EXTRA")]
        FornituraMateriExtra,
        [Description("MAC_ATTR_ACCESSORIE")]
        MacAttrAccessorie
    }

    public enum TipoGruppo
    {
        [Description("GRUPPO")]
        Gruppo,
        [Description("VARIANTE")]
        Variante,
        [Description("PARTICOLARE")]
        Particolare,
        [Description("COMMERCIALE")]
        Commerciale
    }

    // Modelli dati principali
    public class Progetto
    {
        public int Id { get; set; }
        public string NumeroCommessa { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime DataInserimento { get; set; } = DateTime.Now;
        public string NomeDisegnatore { get; set; } = string.Empty;
        public string LetteraRevisioneInserimento { get; set; } = "A";
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public List<ParteMacchina> PartiMacchina { get; set; } = new List<ParteMacchina>();

        public override string ToString()
        {
            return $"{NumeroCommessa} - {Cliente}";
        }
    }

    public class ParteMacchina
    {
        public int Id { get; set; }
        public int ProgettoId { get; set; }
        public string SottoProgetto { get; set; } = string.Empty;
        public string TipoParteMacchina { get; set; } = string.Empty;
        public string CodiceParteMacchina { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public string RevisioneInserimento { get; set; } = "A";
        public string Stato { get; set; } = "NEW";
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public Progetto Progetto { get; set; }
        public List<Sezione> Sezioni { get; set; } = new List<Sezione>();

        public override string ToString()
        {
            return $"{CodiceParteMacchina} - {Descrizione}";
        }
    }

    public class Sezione
    {
        public int Id { get; set; }
        public int ParteMacchinaId { get; set; }
        public string CodiceSezione { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int Quantita { get; set; } = 1;
        public string RevisioneInserimento { get; set; } = "A";
        public string Stato { get; set; } = "NEW";
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public ParteMacchina ParteMacchina { get; set; }
        public List<Sottosezione> Sottosezioni { get; set; } = new List<Sottosezione>();

        public override string ToString()
        {
            return $"{CodiceSezione} - {Descrizione} (Q.tà: {Quantita})";
        }
    }

    public class Sottosezione
    {
        public int Id { get; set; }
        public int SezioneId { get; set; }
        public string CodiceSottosezione { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int Quantita { get; set; } = 1;
        public string RevisioneInserimento { get; set; } = "A";
        public string Stato { get; set; } = "NEW";
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public Sezione Sezione { get; set; }
        public List<Montaggio> Montaggi { get; set; } = new List<Montaggio>();

        public override string ToString()
        {
            return $"{CodiceSottosezione} - {Descrizione} (Q.tà: {Quantita})";
        }
    }

    public class Montaggio
    {
        public int Id { get; set; }
        public int SottosezioneId { get; set; }
        public string CodiceMontaggio { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int Quantita { get; set; } = 1;
        public string RevisioneInserimento { get; set; } = "A";
        public string Stato { get; set; } = "NEW";
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public Sottosezione Sottosezione { get; set; }
        public List<Gruppo> Gruppi { get; set; } = new List<Gruppo>();

        public override string ToString()
        {
            return $"{CodiceMontaggio} - {Descrizione} (Q.tà: {Quantita})";
        }
    }

    public class Gruppo
    {
        public int Id { get; set; }
        public int MontaggioId { get; set; }
        public string CodiceGruppo { get; set; } = string.Empty;
        public string TipoGruppo { get; set; } = "GRUPPO";
        public string Descrizione { get; set; } = string.Empty;
        public int Quantita { get; set; } = 1;
        public string RevisioneInserimento { get; set; } = "A";
        public string Stato { get; set; } = "NEW";
        public string Note { get; set; } = string.Empty;

        // Navigazione
        public Montaggio Montaggio { get; set; }

        public override string ToString()
        {
            return $"{CodiceGruppo} ({TipoGruppo}) - {Descrizione} (Q.tà: {Quantita})";
        }
    }

    public class Revisione
    {
        public int Id { get; set; }
        public string TipoElemento { get; set; } = string.Empty;
        public int ElementoId { get; set; }
        public string RevisionePrecedente { get; set; } = string.Empty;
        public string RevisioneNuova { get; set; } = string.Empty;
        public DateTime DataModifica { get; set; } = DateTime.Now;
        public string Motivo { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{TipoElemento} - {RevisionePrecedente} → {RevisioneNuova} ({DataModifica:dd/MM/yyyy})";
        }
    }

    // Classi di supporto per la UI
    public class TreeNodeData
    {
        public string Tipo { get; set; } = string.Empty;
        public int Id { get; set; }
        public object Data { get; set; }

        public TreeNodeData(string tipo, int id, object data)
        {
            Tipo = tipo;
            Id = id;
            Data = data;
        }
    }

    public class ComboBoxItem
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public ComboBoxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    // Classe per le validazioni
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();

        public string GetErrorsText()
        {
            return string.Join("\n", Errors);
        }

        public string GetWarningsText()
        {
            return string.Join("\n", Warnings);
        }
    }

    // Classe per i risultati di ricerca
    public class SearchResult
    {
        public string Tipo { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Codice { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public string Progetto { get; set; } = string.Empty;
        public string PathCompleto { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Tipo}: {Codice} - {Descrizione} [{Progetto}]";
        }
    }
}

// Extension methods per le enumerazioni
namespace DistintaTecnica.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Description ?? value.ToString();
        }

        public static List<ComboBoxItem> ToComboBoxItems<T>() where T : Enum
        {
            var items = new List<ComboBoxItem>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                items.Add(new ComboBoxItem(item.ToString(), item.GetDescription()));
            }
            return items;
        }
    }
}