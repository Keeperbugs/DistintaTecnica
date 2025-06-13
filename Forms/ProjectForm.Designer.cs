namespace DistintaTecnica.Forms
{
    partial class ProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.SuspendLayout();

            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ProjectForm";
            this.Text = "Nuovo Progetto";

            this.InitializeControls();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeControls()
        {
            // Main panel
            this.mainPanel = new Panel();
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.BackColor = System.Drawing.Color.White;

            // Title label
            this.titleLabel = new Label();
            this.titleLabel.Text = "INFORMAZIONI PROGETTO";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new Point(20, 20);

            // Subtitle label
            this.subtitleLabel = new Label();
            this.subtitleLabel.Text = "Inserisci le informazioni base del progetto";
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Location = new Point(20, 50);

            // Form table layout
            this.formTableLayout = new TableLayoutPanel();
            this.formTableLayout.Location = new Point(20, 90);
            this.formTableLayout.Size = new Size(540, 320);
            this.formTableLayout.ColumnCount = 2;
            this.formTableLayout.RowCount = 6;
            this.formTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            // Column styles
            this.formTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.formTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Row styles
            for (int i = 0; i < 6; i++)
            {
                this.formTableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            // Initialize form fields
            this.InitializeFormFields();

            // Buttons panel
            this.buttonsPanel = new Panel();
            this.buttonsPanel.Location = new Point(20, 420);
            this.buttonsPanel.Size = new Size(540, 50);
            this.buttonsPanel.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);

            // OK Button
            this.okButton = new Button();
            this.okButton.Text = "Crea Progetto";
            this.okButton.Size = new Size(120, 35);
            this.okButton.Location = new Point(410, 8);
            this.okButton.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.FlatStyle = FlatStyle.Flat;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.okButton.Cursor = Cursors.Hand;
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.DialogResult = DialogResult.OK;

            // Cancel Button
            this.cancelButton = new Button();
            this.cancelButton.Text = "Annulla";
            this.cancelButton.Size = new Size(80, 35);
            this.cancelButton.Location = new Point(320, 8);
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.FlatStyle = FlatStyle.Flat;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cancelButton.Cursor = Cursors.Hand;
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.DialogResult = DialogResult.Cancel;

            this.buttonsPanel.Controls.Add(this.okButton);
            this.buttonsPanel.Controls.Add(this.cancelButton);

            // Add controls to main panel
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.subtitleLabel);
            this.mainPanel.Controls.Add(this.formTableLayout);
            this.mainPanel.Controls.Add(this.buttonsPanel);

            this.Controls.Add(this.mainPanel);

            // Set accept and cancel buttons
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
        }

        private void InitializeFormFields()
        {
            int currentRow = 0;

            // N° Commessa
            this.lblCommessa = this.CreateLabel("N° Commessa *:", currentRow);
            this.txtCommessa = this.CreateTextBox(currentRow);
            this.txtCommessa.MaxLength = 50;
            currentRow++;

            // Cliente
            this.lblCliente = this.CreateLabel("Cliente *:", currentRow);
            this.txtCliente = this.CreateTextBox(currentRow);
            this.txtCliente.MaxLength = 100;
            currentRow++;

            // Data Inserimento
            this.lblDataInserimento = this.CreateLabel("Data Inserimento:", currentRow);
            this.dtpDataInserimento = new DateTimePicker();
            this.dtpDataInserimento.Format = DateTimePickerFormat.Short;
            this.dtpDataInserimento.Value = DateTime.Now;
            this.dtpDataInserimento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.dtpDataInserimento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDataInserimento.Margin = new Padding(8, 8, 0, 8);
            this.formTableLayout.Controls.Add(this.dtpDataInserimento, 1, currentRow);
            currentRow++;

            // Nome Disegnatore
            this.lblDisegnatore = this.CreateLabel("Nome Disegnatore *:", currentRow);
            this.txtDisegnatore = this.CreateTextBox(currentRow);
            this.txtDisegnatore.MaxLength = 100;
            currentRow++;

            // Lettera Revisione
            this.lblRevisione = this.CreateLabel("Lettera Revisione:", currentRow);
            this.cmbRevisione = new ComboBox();
            this.cmbRevisione.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRevisione.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbRevisione.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbRevisione.Margin = new Padding(8, 8, 0, 8);

            // Populate revision combo
            string[] revisioni = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            this.cmbRevisione.Items.AddRange(revisioni);
            this.cmbRevisione.SelectedIndex = 0; // Default to "A"

            this.formTableLayout.Controls.Add(this.cmbRevisione, 1, currentRow);
            currentRow++;

            // Note
            this.lblNote = this.CreateLabel("Note:", currentRow);
            this.txtNote = new TextBox();
            this.txtNote.Multiline = true;
            this.txtNote.Height = 80;
            this.txtNote.ScrollBars = ScrollBars.Vertical;
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNote.BorderStyle = BorderStyle.FixedSingle;
            this.txtNote.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.txtNote.Margin = new Padding(8, 8, 0, 8);
            this.txtNote.MaxLength = 500;
            this.formTableLayout.Controls.Add(this.txtNote, 1, currentRow);

            // Add labels to table layout
            this.formTableLayout.Controls.Add(this.lblCommessa, 0, 0);
            this.formTableLayout.Controls.Add(this.lblCliente, 0, 1);
            this.formTableLayout.Controls.Add(this.lblDataInserimento, 0, 2);
            this.formTableLayout.Controls.Add(this.lblDisegnatore, 0, 3);
            this.formTableLayout.Controls.Add(this.lblRevisione, 0, 4);
            this.formTableLayout.Controls.Add(this.lblNote, 0, 5);
        }

        private Label CreateLabel(string text, int row)
        {
            var label = new Label();
            label.Text = text;
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            label.Anchor = AnchorStyles.Left;
            label.Margin = new Padding(0, 12, 8, 8);
            return label;
        }

        private TextBox CreateTextBox(int row)
        {
            var textBox = new TextBox();
            textBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox.Margin = new Padding(8, 8, 0, 8);
            this.formTableLayout.Controls.Add(textBox, 1, row);
            return textBox;
        }

        #endregion

        // Component declarations
        private Panel mainPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TableLayoutPanel formTableLayout;

        private Label lblCommessa;
        private TextBox txtCommessa;
        private Label lblCliente;
        private TextBox txtCliente;
        private Label lblDataInserimento;
        private DateTimePicker dtpDataInserimento;
        private Label lblDisegnatore;
        private TextBox txtDisegnatore;
        private Label lblRevisione;
        private ComboBox cmbRevisione;
        private Label lblNote;
        private TextBox txtNote;

        private Panel buttonsPanel;
        private Button okButton;
        private Button cancelButton;
    }
}