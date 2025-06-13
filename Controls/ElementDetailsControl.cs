using System;
using System.Drawing;
using System.Windows.Forms;
using DistintaTecnica.Models;

namespace DistintaTecnica.Controls
{
    /// <summary>
    /// Controllo per visualizzare i dettagli di un elemento selezionato
    /// </summary>
    public partial class ElementDetailsControl : UserControl
    {
        private TableLayoutPanel mainTable;
        private Label titleLabel;
        private Panel contentPanel;
        private object currentElement;
        private string currentElementType;

        public ElementDetailsControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Proprietà del controllo principale
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            // Layout principale
            mainTable = new TableLayoutPanel();
            mainTable.Dock = DockStyle.Fill;
            mainTable.ColumnCount = 1;
            mainTable.RowCount = 2;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Titolo
            titleLabel = new Label();
            titleLabel.Text = "Seleziona un elemento per visualizzare i dettagli";
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.AutoSize = true;
            titleLabel.Padding = new Padding(0, 0, 0, 20);
            titleLabel.Dock = DockStyle.Top;

            // Panel contenuto
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.AutoScroll = true;
            contentPanel.BackColor = Color.White;

            mainTable.Controls.Add(titleLabel, 0, 0);
            mainTable.Controls.Add(contentPanel, 0, 1);

            this.Controls.Add(mainTable);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Mostra i dettagli di un elemento
        /// </summary>
        public void ShowElementDetails(TreeNodeData nodeData)
        {
            if (nodeData == null)
            {
                ShowEmptyState();
                return;
            }

            currentElement = nodeData.Data;
            currentElementType = nodeData.Tipo;

            switch (nodeData.Tipo.ToUpper())
            {
                case "PROGETTO":
                    ShowProjectDetails((Progetto)nodeData.Data);
                    break;
                case "PARTE_MACCHINA":
                    ShowParteMacchinaDetails((ParteMacchina)nodeData.Data);
                    break;
                case "SEZIONE":
                    ShowSezioneDetails((Sezione)nodeData.Data);
                    break;
                case "SOTTOSEZIONE":
                    ShowSottosezioneDetails((Sottosezione)nodeData.Data);
                    break;
                case "MONTAGGIO":
                    ShowMontaggioDetails((Montaggio)nodeData.Data);
                    break;
                case "GRUPPO":
                    ShowGruppoDetails((Gruppo)nodeData.Data);
                    break;
                default:
                    ShowEmptyState();
                    break;
            }
        }

        private void ShowEmptyState()
        {
            titleLabel.Text = "Seleziona un elemento per visualizzare i dettagli";
            contentPanel.Controls.Clear();

            var emptyLabel = new Label();
            emptyLabel.Text = "Nessun elemento selezionato";
            emptyLabel.Font = new Font("Segoe UI", 12F);
            emptyLabel.ForeColor = Color.Gray;
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            emptyLabel.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(emptyLabel);
        }

        private void ShowProjectDetails(Progetto progetto)
        {
            titleLabel.Text = $"Progetto: {progetto.NumeroCommessa}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Numero Commessa:", progetto.NumeroCommessa, true);
            AddDetailRow(detailsTable, "Cliente:", progetto.Cliente, true);
            AddDetailRow(detailsTable, "Data Inserimento:", progetto.DataInserimento.ToString("dd/MM/yyyy"));
            AddDetailRow(detailsTable, "Nome Disegnatore:", progetto.NomeDisegnatore);
            AddDetailRow(detailsTable, "Revisione Inserimento:", progetto.LetteraRevisioneInserimento);
            AddDetailRow(detailsTable, "Data Creazione:", progetto.DataCreazione.ToString("dd/MM/yyyy HH:mm"));
            AddDetailRow(detailsTable, "Note:", progetto.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        private void ShowParteMacchinaDetails(ParteMacchina parte)
        {
            titleLabel.Text = $"Parte Macchina: {parte.CodiceParteMacchina}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Codice Parte Macchina:", parte.CodiceParteMacchina, true);
            AddDetailRow(detailsTable, "Tipo Parte Macchina:", parte.TipoParteMacchina);
            AddDetailRow(detailsTable, "Sotto Progetto:", parte.SottoProgetto);
            AddDetailRow(detailsTable, "Descrizione:", parte.Descrizione, false, true);
            AddDetailRow(detailsTable, "Revisione Inserimento:", parte.RevisioneInserimento);
            AddDetailRow(detailsTable, "Stato:", parte.Stato);
            AddDetailRow(detailsTable, "Note:", parte.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        private void ShowSezioneDetails(Sezione sezione)
        {
            titleLabel.Text = $"Sezione: {sezione.CodiceSezione}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Codice Sezione:", sezione.CodiceSezione, true);
            AddDetailRow(detailsTable, "Descrizione:", sezione.Descrizione, false, true);
            AddDetailRow(detailsTable, "Quantità:", sezione.Quantita.ToString());
            AddDetailRow(detailsTable, "Revisione Inserimento:", sezione.RevisioneInserimento);
            AddDetailRow(detailsTable, "Stato:", sezione.Stato);
            AddDetailRow(detailsTable, "Note:", sezione.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        private void ShowSottosezioneDetails(Sottosezione sottosezione)
        {
            titleLabel.Text = $"Sottosezione: {sottosezione.CodiceSottosezione}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Codice Sottosezione:", sottosezione.CodiceSottosezione, true);
            AddDetailRow(detailsTable, "Descrizione:", sottosezione.Descrizione, false, true);
            AddDetailRow(detailsTable, "Quantità:", sottosezione.Quantita.ToString());
            AddDetailRow(detailsTable, "Revisione Inserimento:", sottosezione.RevisioneInserimento);
            AddDetailRow(detailsTable, "Stato:", sottosezione.Stato);
            AddDetailRow(detailsTable, "Note:", sottosezione.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        private void ShowMontaggioDetails(Montaggio montaggio)
        {
            titleLabel.Text = $"Montaggio: {montaggio.CodiceMontaggio}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Codice Montaggio:", montaggio.CodiceMontaggio, true);
            AddDetailRow(detailsTable, "Descrizione:", montaggio.Descrizione, false, true);
            AddDetailRow(detailsTable, "Quantità:", montaggio.Quantita.ToString());
            AddDetailRow(detailsTable, "Revisione Inserimento:", montaggio.RevisioneInserimento);
            AddDetailRow(detailsTable, "Stato:", montaggio.Stato);
            AddDetailRow(detailsTable, "Note:", montaggio.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        private void ShowGruppoDetails(Gruppo gruppo)
        {
            titleLabel.Text = $"Gruppo: {gruppo.CodiceGruppo}";
            contentPanel.Controls.Clear();

            var detailsTable = CreateDetailsTable();

            AddDetailRow(detailsTable, "Codice Gruppo:", gruppo.CodiceGruppo, true);
            AddDetailRow(detailsTable, "Tipo Gruppo:", gruppo.TipoGruppo);
            AddDetailRow(detailsTable, "Descrizione:", gruppo.Descrizione, false, true);
            AddDetailRow(detailsTable, "Quantità:", gruppo.Quantita.ToString());
            AddDetailRow(detailsTable, "Revisione Inserimento:", gruppo.RevisioneInserimento);
            AddDetailRow(detailsTable, "Stato:", gruppo.Stato);
            AddDetailRow(detailsTable, "Note:", gruppo.Note ?? "Nessuna nota", false, true);

            contentPanel.Controls.Add(detailsTable);
        }

        /// <summary>
        /// Crea una tabella per i dettagli
        /// </summary>
        private TableLayoutPanel CreateDetailsTable()
        {
            var table = new TableLayoutPanel();
            table.Dock = DockStyle.Top;
            table.AutoSize = true;
            table.ColumnCount = 2;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            table.Margin = new Padding(0);
            table.Padding = new Padding(10);

            return table;
        }

        /// <summary>
        /// Aggiunge una riga di dettaglio alla tabella
        /// </summary>
        private void AddDetailRow(TableLayoutPanel table, string label, string value, bool bold = false, bool multiline = false)
        {
            int rowIndex = table.RowCount;
            table.RowCount++;
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Etichetta
            var lblLabel = new Label();
            lblLabel.Text = label;
            lblLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLabel.ForeColor = Color.FromArgb(55, 65, 81);
            lblLabel.AutoSize = true;
            lblLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblLabel.Margin = new Padding(0, 8, 15, 8);

            // Valore
            Control valueControl;
            if (multiline && !string.IsNullOrEmpty(value) && value.Length > 50)
            {
                var txtValue = new TextBox();
                txtValue.Text = value;
                txtValue.Font = new Font("Segoe UI", 9F, bold ? FontStyle.Bold : FontStyle.Regular);
                txtValue.ForeColor = bold ? Color.FromArgb(17, 24, 39) : Color.FromArgb(75, 85, 99);
                txtValue.ReadOnly = true;
                txtValue.Multiline = true;
                txtValue.BorderStyle = BorderStyle.None;
                txtValue.BackColor = Color.White;
                txtValue.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                txtValue.Margin = new Padding(0, 8, 0, 8);
                txtValue.Height = Math.Min(100, (value.Length / 50 + 1) * 20);
                valueControl = txtValue;
            }
            else
            {
                var lblValue = new Label();
                lblValue.Text = value;
                lblValue.Font = new Font("Segoe UI", 9F, bold ? FontStyle.Bold : FontStyle.Regular);
                lblValue.ForeColor = bold ? Color.FromArgb(17, 24, 39) : Color.FromArgb(75, 85, 99);
                lblValue.AutoSize = true;
                lblValue.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                lblValue.Margin = new Padding(0, 8, 0, 8);
                lblValue.MaximumSize = new Size(500, 0);
                valueControl = lblValue;
            }

            table.Controls.Add(lblLabel, 0, rowIndex);
            table.Controls.Add(valueControl, 1, rowIndex);
        }

        /// <summary>
        /// Aggiorna i dettagli se l'elemento è cambiato
        /// </summary>
        public void RefreshDetails()
        {
            if (currentElement != null)
            {
                var nodeData = new TreeNodeData(currentElementType, 0, currentElement);
                ShowElementDetails(nodeData);
            }
        }
    }
}