using System;
using System.Drawing;
using System.Windows.Forms;
using DistintaTecnica.Models;
using DistintaTecnica.Export;

namespace DistintaTecnica.Export
{
    /// <summary>
    /// Form per selezionare le opzioni di esportazione
    /// </summary>
    public partial class ExportSelectionForm : Form
    {
        private Progetto progetto;
        private GroupBox formatGroup;
        private GroupBox contentGroup;
        private GroupBox optionsGroup;
        private RadioButton csvRadio, excelRadio, pdfRadio, textRadio;
        private CheckBox includeProjectInfo, includePartiMacchina, includeSezioni;
        private CheckBox includeSottosezioni, includeMontaggi, includeGruppi;
        private CheckBox includeNotes, includeEmptyElements;
        private Button okButton, cancelButton;
        private Label projectInfoLabel;

        public ExportType SelectedExportType { get; private set; }
        public ExportOptions SelectedOptions { get; private set; }

        public ExportSelectionForm(Progetto progetto)
        {
            this.progetto = progetto;
            InitializeComponent();
            LoadProjectInfo();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Size = new Size(500, 600);
            this.Text = "Opzioni Esportazione";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // Title
            var titleLabel = new Label();
            titleLabel.Text = "ESPORTAZIONE DISTINTA TECNICA";
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(450, 25);
            this.Controls.Add(titleLabel);

            // Project info label
            projectInfoLabel = new Label();
            projectInfoLabel.Location = new Point(20, 50);
            projectInfoLabel.Size = new Size(450, 30);
            projectInfoLabel.Font = new Font("Segoe UI", 9F);
            projectInfoLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.Controls.Add(projectInfoLabel);

            // Format selection group
            formatGroup = new GroupBox();
            formatGroup.Text = "Formato di esportazione";
            formatGroup.Location = new Point(20, 90);
            formatGroup.Size = new Size(450, 120);
            formatGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            csvRadio = new RadioButton();
            csvRadio.Text = "CSV (Excel compatibile)";
            csvRadio.Location = new Point(15, 25);
            csvRadio.Size = new Size(200, 23);
            csvRadio.Checked = true;

            excelRadio = new RadioButton();
            excelRadio.Text = "Excel (.xlsx)";
            excelRadio.Location = new Point(15, 50);
            excelRadio.Size = new Size(200, 23);
            excelRadio.Enabled = false; // Da implementare

            pdfRadio = new RadioButton();
            pdfRadio.Text = "PDF";
            pdfRadio.Location = new Point(15, 75);
            pdfRadio.Size = new Size(200, 23);
            pdfRadio.Enabled = false; // Da implementare

            textRadio = new RadioButton();
            textRadio.Text = "Testo (.txt)";
            textRadio.Location = new Point(220, 25);
            textRadio.Size = new Size(200, 23);

            formatGroup.Controls.AddRange(new Control[] { csvRadio, excelRadio, pdfRadio, textRadio });
            this.Controls.Add(formatGroup);

            // Content selection group
            contentGroup = new GroupBox();
            contentGroup.Text = "Contenuto da esportare";
            contentGroup.Location = new Point(20, 220);
            contentGroup.Size = new Size(450, 150);
            contentGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            includeProjectInfo = new CheckBox();
            includeProjectInfo.Text = "Informazioni progetto";
            includeProjectInfo.Location = new Point(15, 25);
            includeProjectInfo.Size = new Size(200, 23);
            includeProjectInfo.Checked = true;

            includePartiMacchina = new CheckBox();
            includePartiMacchina.Text = "Parti Macchina";
            includePartiMacchina.Location = new Point(15, 50);
            includePartiMacchina.Size = new Size(200, 23);
            includePartiMacchina.Checked = true;

            includeSezioni = new CheckBox();
            includeSezioni.Text = "Sezioni";
            includeSezioni.Location = new Point(15, 75);
            includeSezioni.Size = new Size(200, 23);
            includeSezioni.Checked = true;

            includeSottosezioni = new CheckBox();
            includeSottosezioni.Text = "Sottosezioni";
            includeSottosezioni.Location = new Point(15, 100);
            includeSottosezioni.Size = new Size(200, 23);
            includeSottosezioni.Checked = true;

            includeMontaggi = new CheckBox();
            includeMontaggi.Text = "Montaggi";
            includeMontaggi.Location = new Point(220, 50);
            includeMontaggi.Size = new Size(200, 23);
            includeMontaggi.Checked = true;

            includeGruppi = new CheckBox();
            includeGruppi.Text = "Gruppi";
            includeGruppi.Location = new Point(220, 75);
            includeGruppi.Size = new Size(200, 23);
            includeGruppi.Checked = true;

            contentGroup.Controls.AddRange(new Control[] {
                includeProjectInfo, includePartiMacchina, includeSezioni,
                includeSottosezioni, includeMontaggi, includeGruppi
            });
            this.Controls.Add(contentGroup);

            // Options group
            optionsGroup = new GroupBox();
            optionsGroup.Text = "Opzioni aggiuntive";
            optionsGroup.Location = new Point(20, 380);
            optionsGroup.Size = new Size(450, 80);
            optionsGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            includeNotes = new CheckBox();
            includeNotes.Text = "Includi note";
            includeNotes.Location = new Point(15, 25);
            includeNotes.Size = new Size(200, 23);
            includeNotes.Checked = true;

            includeEmptyElements = new CheckBox();
            includeEmptyElements.Text = "Includi elementi vuoti";
            includeEmptyElements.Location = new Point(15, 50);
            includeEmptyElements.Size = new Size(200, 23);
            includeEmptyElements.Checked = false;

            optionsGroup.Controls.AddRange(new Control[] { includeNotes, includeEmptyElements });
            this.Controls.Add(optionsGroup);

            // Buttons
            okButton = new Button();
            okButton.Text = "Esporta";
            okButton.Size = new Size(100, 35);
            okButton.Location = new Point(370, 480);
            okButton.BackColor = Color.FromArgb(59, 130, 246);
            okButton.ForeColor = Color.White;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderSize = 0;
            okButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            okButton.Click += OkButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Annulla";
            cancelButton.Size = new Size(80, 35);
            cancelButton.Location = new Point(280, 480);
            cancelButton.BackColor = Color.FromArgb(156, 163, 175);
            cancelButton.ForeColor = Color.White;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.Font = new Font("Segoe UI", 9F);
            cancelButton.Click += CancelButton_Click;

            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            // Set default button
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;

            this.ResumeLayout(false);
        }

        private void LoadProjectInfo()
        {
            if (progetto != null)
            {
                projectInfoLabel.Text = $"Progetto: {progetto.NumeroCommessa} - {progetto.Cliente}";
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Determina il tipo di export selezionato
                if (csvRadio.Checked)
                    SelectedExportType = ExportType.CSV;
                else if (excelRadio.Checked)
                    SelectedExportType = ExportType.Excel;
                else if (pdfRadio.Checked)
                    SelectedExportType = ExportType.PDF;
                else if (textRadio.Checked)
                    SelectedExportType = ExportType.Text;
                else
                    SelectedExportType = ExportType.CSV; // Default

                // Crea le opzioni di export
                SelectedOptions = new ExportOptions
                {
                    IncludeProjectInfo = includeProjectInfo.Checked,
                    IncludePartiMacchina = includePartiMacchina.Checked,
                    IncludeSezioni = includeSezioni.Checked,
                    IncludeSottosezioni = includeSottosezioni.Checked,
                    IncludeMontaggi = includeMontaggi.Checked,
                    IncludeGruppi = includeGruppi.Checked,
                    IncludeNotes = includeNotes.Checked,
                    IncludeEmptyElements = includeEmptyElements.Checked
                };

                // Valida le selezioni
                if (!ValidateSelections())
                    return;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nella configurazione dell'esportazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateSelections()
        {
            // Verifica che almeno un tipo di contenuto sia selezionato
            if (!includePartiMacchina.Checked && !includeSezioni.Checked &&
                !includeSottosezioni.Checked && !includeMontaggi.Checked &&
                !includeGruppi.Checked)
            {
                MessageBox.Show("Seleziona almeno un tipo di contenuto da esportare.",
                              "Selezione richiesta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Avvisa se sono selezionati formati non ancora implementati
            if (excelRadio.Checked)
            {
                var result = MessageBox.Show("Il formato Excel non è ancora completamente implementato.\n" +
                                           "Verrà generato un file CSV compatibile con Excel.\n\n" +
                                           "Vuoi continuare?",
                                           "Formato non implementato",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return false;
            }

            if (pdfRadio.Checked)
            {
                MessageBox.Show("Il formato PDF non è ancora implementato.\n" +
                              "Seleziona un altro formato.",
                              "Formato non disponibile",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Mostra il dialog di selezione export
        /// </summary>
        public static ExportSelectionForm ShowDialog(Progetto progetto, IWin32Window owner = null)
        {
            var form = new ExportSelectionForm(progetto);
            var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);

            if (result == DialogResult.OK)
                return form;
            else
                return null;
        }
    }
}