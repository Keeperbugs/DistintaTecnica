namespace DistintaTecnica.Forms
{
    partial class ElementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ClientSize = new System.Drawing.Size(700, 600);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ElementForm";
            this.Text = "Nuovo Elemento";

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
            this.titleLabel.Text = "NUOVO ELEMENTO";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new Point(20, 20);

            // Subtitle label
            this.subtitleLabel = new Label();
            this.subtitleLabel.Text = "Inserisci le informazioni dell'elemento";
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Location = new Point(20, 50);

            // Main form panel
            this.formPanel = new Panel();
            this.formPanel.Location = new Point(20, 90);
            this.formPanel.Size = new Size(640, 420);
            this.formPanel.BackColor = System.Drawing.Color.White;

            // Initialize all field groups
            this.InitializeBasicFields();
            this.InitializeCodeGenerationPanel();
            this.InitializeQuantityAndRevisionFields();
            this.InitializeNotesField();

            // Buttons panel
            this.buttonsPanel = new Panel();
            this.buttonsPanel.Location = new Point(20, 520);
            this.buttonsPanel.Size = new Size(640, 50);
            this.buttonsPanel.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);

            // OK Button
            this.okButton = new Button();
            this.okButton.Text = "Salva";
            this.okButton.Size = new Size(100, 35);
            this.okButton.Location = new Point(530, 8);
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
            this.cancelButton.Location = new Point(440, 8);
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
            this.mainPanel.Controls.Add(this.formPanel);
            this.mainPanel.Controls.Add(this.buttonsPanel);

            this.Controls.Add(this.mainPanel);

            // Set accept and cancel buttons
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
        }

        private void InitializeBasicFields()
        {
            // Basic fields group
            this.basicFieldsGroup = new GroupBox();
            this.basicFieldsGroup.Text = "Informazioni Base";
            this.basicFieldsGroup.Location = new Point(0, 0);
            this.basicFieldsGroup.Size = new Size(640, 120);
            this.basicFieldsGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.basicFieldsGroup.BackColor = System.Drawing.Color.White;

            // Type selection (only for new items)
            this.lblTipo = new Label();
            this.lblTipo.Text = "Tipo Elemento:";
            this.lblTipo.Location = new Point(16, 28);
            this.lblTipo.Size = new Size(120, 23);
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.cmbTipo = new ComboBox();
            this.cmbTipo.Location = new Point(140, 25);
            this.cmbTipo.Size = new Size(180, 28);
            this.cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new System.Drawing.Font("Segoe UI", 9F);

            // SubProject selection (only for Parte Macchina)
            this.lblSottoProgetto = new Label();
            this.lblSottoProgetto.Text = "Sotto Progetto:";
            this.lblSottoProgetto.Location = new Point(340, 28);
            this.lblSottoProgetto.Size = new Size(120, 23);
            this.lblSottoProgetto.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSottoProgetto.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblSottoProgetto.Visible = false;

            this.cmbSottoProgetto = new ComboBox();
            this.cmbSottoProgetto.Location = new Point(460, 25);
            this.cmbSottoProgetto.Size = new Size(160, 28);
            this.cmbSottoProgetto.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSottoProgetto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSottoProgetto.Visible = false;

            // Machine Part Type (only for Parte Macchina)
            this.lblTipoParteMacchina = new Label();
            this.lblTipoParteMacchina.Text = "Tipo Parte:";
            this.lblTipoParteMacchina.Location = new Point(16, 65);
            this.lblTipoParteMacchina.Size = new Size(120, 23);
            this.lblTipoParteMacchina.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipoParteMacchina.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblTipoParteMacchina.Visible = false;

            this.cmbTipoParteMacchina = new ComboBox();
            this.cmbTipoParteMacchina.Location = new Point(140, 62);
            this.cmbTipoParteMacchina.Size = new Size(180, 28);
            this.cmbTipoParteMacchina.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoParteMacchina.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTipoParteMacchina.Visible = false;

            // Group Type (only for Gruppo)
            this.lblTipoGruppo = new Label();
            this.lblTipoGruppo.Text = "Tipo Gruppo:";
            this.lblTipoGruppo.Location = new Point(340, 65);
            this.lblTipoGruppo.Size = new Size(120, 23);
            this.lblTipoGruppo.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipoGruppo.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblTipoGruppo.Visible = false;

            this.cmbTipoGruppo = new ComboBox();
            this.cmbTipoGruppo.Location = new Point(460, 62);
            this.cmbTipoGruppo.Size = new Size(160, 28);
            this.cmbTipoGruppo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoGruppo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTipoGruppo.Visible = false;

            this.basicFieldsGroup.Controls.Add(this.lblTipo);
            this.basicFieldsGroup.Controls.Add(this.cmbTipo);
            this.basicFieldsGroup.Controls.Add(this.lblSottoProgetto);
            this.basicFieldsGroup.Controls.Add(this.cmbSottoProgetto);
            this.basicFieldsGroup.Controls.Add(this.lblTipoParteMacchina);
            this.basicFieldsGroup.Controls.Add(this.cmbTipoParteMacchina);
            this.basicFieldsGroup.Controls.Add(this.lblTipoGruppo);
            this.basicFieldsGroup.Controls.Add(this.cmbTipoGruppo);

            this.formPanel.Controls.Add(this.basicFieldsGroup);
        }

        private void InitializeCodeGenerationPanel()
        {
            // Code generation group
            this.codeGenerationGroup = new GroupBox();
            this.codeGenerationGroup.Text = "Codice e Descrizione";
            this.codeGenerationGroup.Location = new Point(0, 130);
            this.codeGenerationGroup.Size = new Size(640, 140);
            this.codeGenerationGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.codeGenerationGroup.BackColor = System.Drawing.Color.White;

            // Code field
            this.lblCodice = new Label();
            this.lblCodice.Text = "Codice *:";
            this.lblCodice.Location = new Point(16, 28);
            this.lblCodice.Size = new Size(80, 23);
            this.lblCodice.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCodice.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtCodice = new TextBox();
            this.txtCodice.Location = new Point(100, 25);
            this.txtCodice.Size = new Size(200, 27);
            this.txtCodice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCodice.BorderStyle = BorderStyle.FixedSingle;

            // Generate code button (for auto-generated codes)
            this.btnGeneraCodice = new Button();
            this.btnGeneraCodice.Text = "🔧 Genera";
            this.btnGeneraCodice.Location = new Point(310, 23);
            this.btnGeneraCodice.Size = new Size(80, 30);
            this.btnGeneraCodice.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.btnGeneraCodice.ForeColor = System.Drawing.Color.White;
            this.btnGeneraCodice.FlatStyle = FlatStyle.Flat;
            this.btnGeneraCodice.FlatAppearance.BorderSize = 0;
            this.btnGeneraCodice.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGeneraCodice.Cursor = Cursors.Hand;
            this.btnGeneraCodice.UseVisualStyleBackColor = false;

            // Code validation label
            this.lblValidazioneCodice = new Label();
            this.lblValidazioneCodice.Location = new Point(400, 28);
            this.lblValidazioneCodice.Size = new Size(220, 23);
            this.lblValidazioneCodice.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblValidazioneCodice.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.lblValidazioneCodice.Text = "";

            // Description field
            this.lblDescrizione = new Label();
            this.lblDescrizione.Text = "Descrizione *:";
            this.lblDescrizione.Location = new Point(16, 68);
            this.lblDescrizione.Size = new Size(100, 23);
            this.lblDescrizione.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDescrizione.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtDescrizione = new TextBox();
            this.txtDescrizione.Location = new Point(120, 65);
            this.txtDescrizione.Size = new Size(500, 27);
            this.txtDescrizione.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescrizione.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescrizione.MaxLength = 200;

            // Auto-description checkbox
            this.chkAutoDescrizione = new CheckBox();
            this.chkAutoDescrizione.Text = "Genera descrizione automatica";
            this.chkAutoDescrizione.Location = new Point(120, 100);
            this.chkAutoDescrizione.Size = new Size(200, 23);
            this.chkAutoDescrizione.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.chkAutoDescrizione.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.chkAutoDescrizione.UseVisualStyleBackColor = true;

            this.codeGenerationGroup.Controls.Add(this.lblCodice);
            this.codeGenerationGroup.Controls.Add(this.txtCodice);
            this.codeGenerationGroup.Controls.Add(this.btnGeneraCodice);
            this.codeGenerationGroup.Controls.Add(this.lblValidazioneCodice);
            this.codeGenerationGroup.Controls.Add(this.lblDescrizione);
            this.codeGenerationGroup.Controls.Add(this.txtDescrizione);
            this.codeGenerationGroup.Controls.Add(this.chkAutoDescrizione);

            this.formPanel.Controls.Add(this.codeGenerationGroup);
        }

        private void InitializeQuantityAndRevisionFields()
        {
            // Quantity and revision group
            this.quantityRevisionGroup = new GroupBox();
            this.quantityRevisionGroup.Text = "Quantità e Revisione";
            this.quantityRevisionGroup.Location = new Point(0, 280);
            this.quantityRevisionGroup.Size = new Size(640, 80);
            this.quantityRevisionGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.quantityRevisionGroup.BackColor = System.Drawing.Color.White;

            // Quantity field
            this.lblQuantita = new Label();
            this.lblQuantita.Text = "Quantità:";
            this.lblQuantita.Location = new Point(16, 35);
            this.lblQuantita.Size = new Size(80, 23);
            this.lblQuantita.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblQuantita.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.numQuantita = new NumericUpDown();
            this.numQuantita.Location = new Point(100, 32);
            this.numQuantita.Size = new Size(80, 27);
            this.numQuantita.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numQuantita.Minimum = 1;
            this.numQuantita.Maximum = 9999;
            this.numQuantita.Value = 1;

            // Revision field
            this.lblRevisione = new Label();
            this.lblRevisione.Text = "Revisione:";
            this.lblRevisione.Location = new Point(220, 35);
            this.lblRevisione.Size = new Size(80, 23);
            this.lblRevisione.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRevisione.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.cmbRevisione = new ComboBox();
            this.cmbRevisione.Location = new Point(300, 32);
            this.cmbRevisione.Size = new Size(80, 28);
            this.cmbRevisione.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRevisione.Font = new System.Drawing.Font("Segoe UI", 9F);

            // State field
            this.lblStato = new Label();
            this.lblStato.Text = "Stato:";
            this.lblStato.Location = new Point(420, 35);
            this.lblStato.Size = new Size(50, 23);
            this.lblStato.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStato.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            this.txtStato = new TextBox();
            this.txtStato.Location = new Point(470, 32);
            this.txtStato.Size = new Size(80, 27);
            this.txtStato.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStato.BorderStyle = BorderStyle.FixedSingle;
            this.txtStato.Text = "NEW";
            this.txtStato.ReadOnly = true;
            this.txtStato.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);

            this.quantityRevisionGroup.Controls.Add(this.lblQuantita);
            this.quantityRevisionGroup.Controls.Add(this.numQuantita);
            this.quantityRevisionGroup.Controls.Add(this.lblRevisione);
            this.quantityRevisionGroup.Controls.Add(this.cmbRevisione);
            this.quantityRevisionGroup.Controls.Add(this.lblStato);
            this.quantityRevisionGroup.Controls.Add(this.txtStato);

            this.formPanel.Controls.Add(this.quantityRevisionGroup);
        }

        private void InitializeNotesField()
        {
            // Notes group
            this.notesGroup = new GroupBox();
            this.notesGroup.Text = "Note";
            this.notesGroup.Location = new Point(0, 370);
            this.notesGroup.Size = new Size(640, 50);
            this.notesGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.notesGroup.BackColor = System.Drawing.Color.White;

            // Notes field
            this.txtNote = new TextBox();
            this.txtNote.Location = new Point(16, 22);
            this.txtNote.Size = new Size(604, 23);
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNote.BorderStyle = BorderStyle.FixedSingle;
            this.txtNote.MaxLength = 500;
            this.txtNote.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.notesGroup.Controls.Add(this.txtNote);
            this.formPanel.Controls.Add(this.notesGroup);
        }

        #endregion

        // Component declarations
        private Panel mainPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel formPanel;

        // Basic fields
        private GroupBox basicFieldsGroup;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblSottoProgetto;
        private ComboBox cmbSottoProgetto;
        private Label lblTipoParteMacchina;
        private ComboBox cmbTipoParteMacchina;
        private Label lblTipoGruppo;
        private ComboBox cmbTipoGruppo;

        // Code generation
        private GroupBox codeGenerationGroup;
        private Label lblCodice;
        private TextBox txtCodice;
        private Button btnGeneraCodice;
        private Label lblValidazioneCodice;
        private Label lblDescrizione;
        private TextBox txtDescrizione;
        private CheckBox chkAutoDescrizione;

        // Quantity and revision
        private GroupBox quantityRevisionGroup;
        private Label lblQuantita;
        private NumericUpDown numQuantita;
        private Label lblRevisione;
        private ComboBox cmbRevisione;
        private Label lblStato;
        private TextBox txtStato;

        // Notes
        private GroupBox notesGroup;
        private TextBox txtNote;

        // Buttons
        private Panel buttonsPanel;
        private Button okButton;
        private Button cancelButton;
    }
}