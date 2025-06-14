namespace DistintaTecnica.Forms
{
    partial class ElementForm
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
            this.mainPanel = new Panel();
            this.titleLabel = new Label();
            this.subtitleLabel = new Label();
            this.basicFieldsGroup = new GroupBox();
            this.lblTipo = new Label();
            this.cmbTipo = new ComboBox();
            this.lblSottoProgetto = new Label();
            this.cmbSottoProgetto = new ComboBox();
            this.lblTipoParteMacchina = new Label();
            this.cmbTipoParteMacchina = new ComboBox();
            this.lblTipoGruppo = new Label();
            this.cmbTipoGruppo = new ComboBox();
            this.codeGenerationGroup = new GroupBox();
            this.lblCodice = new Label();
            this.txtCodice = new TextBox();
            this.btnGeneraCodice = new Button();
            this.lblValidazioneCodice = new Label();
            this.lblDescrizione = new Label();
            this.txtDescrizione = new TextBox();
            this.chkAutoDescrizione = new CheckBox();
            this.quantityRevisionGroup = new GroupBox();
            this.lblQuantita = new Label();
            this.numQuantita = new NumericUpDown();
            this.lblRevisione = new Label();
            this.cmbRevisione = new ComboBox();
            this.lblStato = new Label();
            this.txtStato = new TextBox();
            this.notesGroup = new GroupBox();
            this.txtNote = new TextBox();
            this.okButton = new Button();
            this.cancelButton = new Button();
            this.mainPanel.SuspendLayout();
            this.basicFieldsGroup.SuspendLayout();
            this.codeGenerationGroup.SuspendLayout();
            this.quantityRevisionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantita)).BeginInit();
            this.notesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = Color.White;
            this.mainPanel.Controls.Add(this.notesGroup);
            this.mainPanel.Controls.Add(this.quantityRevisionGroup);
            this.mainPanel.Controls.Add(this.codeGenerationGroup);
            this.mainPanel.Controls.Add(this.basicFieldsGroup);
            this.mainPanel.Controls.Add(this.subtitleLabel);
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.okButton);
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.Size = new Size(700, 600);
            this.mainPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            this.titleLabel.Location = new Point(23, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new Size(186, 32);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "NUOVO ELEMENTO";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.subtitleLabel.Location = new Point(23, 52);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new Size(250, 20);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Inserisci le informazioni dell'elemento";
            // 
            // basicFieldsGroup
            // 
            this.basicFieldsGroup.Controls.Add(this.lblTipoGruppo);
            this.basicFieldsGroup.Controls.Add(this.cmbTipoGruppo);
            this.basicFieldsGroup.Controls.Add(this.lblTipoParteMacchina);
            this.basicFieldsGroup.Controls.Add(this.cmbTipoParteMacchina);
            this.basicFieldsGroup.Controls.Add(this.lblSottoProgetto);
            this.basicFieldsGroup.Controls.Add(this.cmbSottoProgetto);
            this.basicFieldsGroup.Controls.Add(this.lblTipo);
            this.basicFieldsGroup.Controls.Add(this.cmbTipo);
            this.basicFieldsGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.basicFieldsGroup.Location = new Point(23, 85);
            this.basicFieldsGroup.Name = "basicFieldsGroup";
            this.basicFieldsGroup.Size = new Size(640, 120);
            this.basicFieldsGroup.TabIndex = 2;
            this.basicFieldsGroup.TabStop = false;
            this.basicFieldsGroup.Text = "Informazioni Base";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipo.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblTipo.Location = new Point(16, 31);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new Size(110, 20);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo Elemento:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new Font("Segoe UI", 9F);
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new Point(140, 28);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new Size(180, 28);
            this.cmbTipo.TabIndex = 1;
            // 
            // lblSottoProgetto
            // 
            this.lblSottoProgetto.AutoSize = true;
            this.lblSottoProgetto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSottoProgetto.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblSottoProgetto.Location = new Point(340, 31);
            this.lblSottoProgetto.Name = "lblSottoProgetto";
            this.lblSottoProgetto.Size = new Size(114, 20);
            this.lblSottoProgetto.TabIndex = 2;
            this.lblSottoProgetto.Text = "Sotto Progetto:";
            this.lblSottoProgetto.Visible = false;
            // 
            // cmbSottoProgetto
            // 
            this.cmbSottoProgetto.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSottoProgetto.Font = new Font("Segoe UI", 9F);
            this.cmbSottoProgetto.FormattingEnabled = true;
            this.cmbSottoProgetto.Location = new Point(460, 28);
            this.cmbSottoProgetto.Name = "cmbSottoProgetto";
            this.cmbSottoProgetto.Size = new Size(160, 28);
            this.cmbSottoProgetto.TabIndex = 3;
            this.cmbSottoProgetto.Visible = false;
            // 
            // lblTipoParteMacchina
            // 
            this.lblTipoParteMacchina.AutoSize = true;
            this.lblTipoParteMacchina.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipoParteMacchina.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblTipoParteMacchina.Location = new Point(16, 68);
            this.lblTipoParteMacchina.Name = "lblTipoParteMacchina";
            this.lblTipoParteMacchina.Size = new Size(86, 20);
            this.lblTipoParteMacchina.TabIndex = 4;
            this.lblTipoParteMacchina.Text = "Tipo Parte:";
            this.lblTipoParteMacchina.Visible = false;
            // 
            // cmbTipoParteMacchina
            // 
            this.cmbTipoParteMacchina.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoParteMacchina.Font = new Font("Segoe UI", 9F);
            this.cmbTipoParteMacchina.FormattingEnabled = true;
            this.cmbTipoParteMacchina.Location = new Point(140, 65);
            this.cmbTipoParteMacchina.Name = "cmbTipoParteMacchina";
            this.cmbTipoParteMacchina.Size = new Size(180, 28);
            this.cmbTipoParteMacchina.TabIndex = 5;
            this.cmbTipoParteMacchina.Visible = false;
            // 
            // lblTipoGruppo
            // 
            this.lblTipoGruppo.AutoSize = true;
            this.lblTipoGruppo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipoGruppo.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblTipoGruppo.Location = new Point(340, 68);
            this.lblTipoGruppo.Name = "lblTipoGruppo";
            this.lblTipoGruppo.Size = new Size(102, 20);
            this.lblTipoGruppo.TabIndex = 6;
            this.lblTipoGruppo.Text = "Tipo Gruppo:";
            this.lblTipoGruppo.Visible = false;
            // 
            // cmbTipoGruppo
            // 
            this.cmbTipoGruppo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipoGruppo.Font = new Font("Segoe UI", 9F);
            this.cmbTipoGruppo.FormattingEnabled = true;
            this.cmbTipoGruppo.Location = new Point(460, 65);
            this.cmbTipoGruppo.Name = "cmbTipoGruppo";
            this.cmbTipoGruppo.Size = new Size(160, 28);
            this.cmbTipoGruppo.TabIndex = 7;
            this.cmbTipoGruppo.Visible = false;
            // 
            // codeGenerationGroup
            // 
            this.codeGenerationGroup.Controls.Add(this.chkAutoDescrizione);
            this.codeGenerationGroup.Controls.Add(this.txtDescrizione);
            this.codeGenerationGroup.Controls.Add(this.lblDescrizione);
            this.codeGenerationGroup.Controls.Add(this.lblValidazioneCodice);
            this.codeGenerationGroup.Controls.Add(this.btnGeneraCodice);
            this.codeGenerationGroup.Controls.Add(this.txtCodice);
            this.codeGenerationGroup.Controls.Add(this.lblCodice);
            this.codeGenerationGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.codeGenerationGroup.Location = new Point(23, 211);
            this.codeGenerationGroup.Name = "codeGenerationGroup";
            this.codeGenerationGroup.Size = new Size(640, 140);
            this.codeGenerationGroup.TabIndex = 3;
            this.codeGenerationGroup.TabStop = false;
            this.codeGenerationGroup.Text = "Codice e Descrizione";
            // 
            // lblCodice
            // 
            this.lblCodice.AutoSize = true;
            this.lblCodice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCodice.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblCodice.Location = new Point(16, 31);
            this.lblCodice.Name = "lblCodice";
            this.lblCodice.Size = new Size(68, 20);
            this.lblCodice.TabIndex = 0;
            this.lblCodice.Text = "Codice *:";
            // 
            // txtCodice
            // 
            this.txtCodice.BorderStyle = BorderStyle.FixedSingle;
            this.txtCodice.Font = new Font("Segoe UI", 9F);
            this.txtCodice.Location = new Point(100, 28);
            this.txtCodice.Name = "txtCodice";
            this.txtCodice.Size = new Size(200, 27);
            this.txtCodice.TabIndex = 1;
            // 
            // btnGeneraCodice
            // 
            this.btnGeneraCodice.BackColor = Color.FromArgb(16, 185, 129);
            this.btnGeneraCodice.Cursor = Cursors.Hand;
            this.btnGeneraCodice.FlatAppearance.BorderSize = 0;
            this.btnGeneraCodice.FlatStyle = FlatStyle.Flat;
            this.btnGeneraCodice.Font = new Font("Segoe UI", 8F);
            this.btnGeneraCodice.ForeColor = Color.White;
            this.btnGeneraCodice.Location = new Point(310, 26);
            this.btnGeneraCodice.Name = "btnGeneraCodice";
            this.btnGeneraCodice.Size = new Size(80, 30);
            this.btnGeneraCodice.TabIndex = 2;
            this.btnGeneraCodice.Text = "🔧 Genera";
            this.btnGeneraCodice.UseVisualStyleBackColor = false;
            // 
            // lblValidazioneCodice
            // 
            this.lblValidazioneCodice.AutoSize = true;
            this.lblValidazioneCodice.Font = new Font("Segoe UI", 8F);
            this.lblValidazioneCodice.ForeColor = Color.FromArgb(34, 197, 94);
            this.lblValidazioneCodice.Location = new Point(400, 31);
            this.lblValidazioneCodice.Name = "lblValidazioneCodice";
            this.lblValidazioneCodice.Size = new Size(0, 19);
            this.lblValidazioneCodice.TabIndex = 3;
            // 
            // lblDescrizione
            // 
            this.lblDescrizione.AutoSize = true;
            this.lblDescrizione.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDescrizione.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblDescrizione.Location = new Point(16, 71);
            this.lblDescrizione.Name = "lblDescrizione";
            this.lblDescrizione.Size = new Size(102, 20);
            this.lblDescrizione.TabIndex = 4;
            this.lblDescrizione.Text = "Descrizione *:";
            // 
            // txtDescrizione
            // 
            this.txtDescrizione.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescrizione.Font = new Font("Segoe UI", 9F);
            this.txtDescrizione.Location = new Point(120, 68);
            this.txtDescrizione.MaxLength = 200;
            this.txtDescrizione.Name = "txtDescrizione";
            this.txtDescrizione.Size = new Size(500, 27);
            this.txtDescrizione.TabIndex = 5;
            // 
            // chkAutoDescrizione
            // 
            this.chkAutoDescrizione.AutoSize = true;
            this.chkAutoDescrizione.Font = new Font("Segoe UI", 8F);
            this.chkAutoDescrizione.ForeColor = Color.FromArgb(100, 116, 139);
            this.chkAutoDescrizione.Location = new Point(120, 103);
            this.chkAutoDescrizione.Name = "chkAutoDescrizione";
            this.chkAutoDescrizione.Size = new Size(224, 23);
            this.chkAutoDescrizione.TabIndex = 6;
            this.chkAutoDescrizione.Text = "Genera descrizione automatica";
            this.chkAutoDescrizione.UseVisualStyleBackColor = true;
            // 
            // quantityRevisionGroup
            // 
            this.quantityRevisionGroup.Controls.Add(this.txtStato);
            this.quantityRevisionGroup.Controls.Add(this.lblStato);
            this.quantityRevisionGroup.Controls.Add(this.cmbRevisione);
            this.quantityRevisionGroup.Controls.Add(this.lblRevisione);
            this.quantityRevisionGroup.Controls.Add(this.numQuantita);
            this.quantityRevisionGroup.Controls.Add(this.lblQuantita);
            this.quantityRevisionGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.quantityRevisionGroup.Location = new Point(23, 357);
            this.quantityRevisionGroup.Name = "quantityRevisionGroup";
            this.quantityRevisionGroup.Size = new Size(640, 80);
            this.quantityRevisionGroup.TabIndex = 4;
            this.quantityRevisionGroup.TabStop = false;
            this.quantityRevisionGroup.Text = "Quantità e Revisione";
            // 
            // lblQuantita
            // 
            this.lblQuantita.AutoSize = true;
            this.lblQuantita.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblQuantita.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblQuantita.Location = new Point(16, 38);
            this.lblQuantita.Name = "lblQuantita";
            this.lblQuantita.Size = new Size(75, 20);
            this.lblQuantita.TabIndex = 0;
            this.lblQuantita.Text = "Quantità:";
            // 
            // numQuantita
            // 
            this.numQuantita.Font = new Font("Segoe UI", 9F);
            this.numQuantita.Location = new Point(100, 35);
            this.numQuantita.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.numQuantita.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantita.Name = "numQuantita";
            this.numQuantita.Size = new Size(80, 27);
            this.numQuantita.TabIndex = 1;
            this.numQuantita.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblRevisione
            // 
            this.lblRevisione.AutoSize = true;
            this.lblRevisione.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRevisione.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblRevisione.Location = new Point(220, 38);
            this.lblRevisione.Name = "lblRevisione";
            this.lblRevisione.Size = new Size(78, 20);
            this.lblRevisione.TabIndex = 2;
            this.lblRevisione.Text = "Revisione:";
            // 
            // cmbRevisione
            // 
            this.cmbRevisione.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRevisione.Font = new Font("Segoe UI", 9F);
            this.cmbRevisione.FormattingEnabled = true;
            this.cmbRevisione.Location = new Point(300, 35);
            this.cmbRevisione.Name = "cmbRevisione";
            this.cmbRevisione.Size = new Size(80, 28);
            this.cmbRevisione.TabIndex = 3;
            // 
            // lblStato
            // 
            this.lblStato.AutoSize = true;
            this.lblStato.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStato.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblStato.Location = new Point(420, 38);
            this.lblStato.Name = "lblStato";
            this.lblStato.Size = new Size(48, 20);
            this.lblStato.TabIndex = 4;
            this.lblStato.Text = "Stato:";
            // 
            // txtStato
            // 
            this.txtStato.BackColor = Color.FromArgb(247, 250, 252);
            this.txtStato.BorderStyle = BorderStyle.FixedSingle;
            this.txtStato.Font = new Font("Segoe UI", 9F);
            this.txtStato.Location = new Point(470, 35);
            this.txtStato.Name = "txtStato";
            this.txtStato.ReadOnly = true;
            this.txtStato.Size = new Size(80, 27);
            this.txtStato.TabIndex = 5;
            this.txtStato.Text = "NEW";
            // 
            // notesGroup
            // 
            this.notesGroup.Controls.Add(this.txtNote);
            this.notesGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.notesGroup.Location = new Point(23, 443);
            this.notesGroup.Name = "notesGroup";
            this.notesGroup.Size = new Size(640, 70);
            this.notesGroup.TabIndex = 5;
            this.notesGroup.TabStop = false;
            this.notesGroup.Text = "Note";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtNote.BorderStyle = BorderStyle.FixedSingle;
            this.txtNote.Font = new Font("Segoe UI", 9F);
            this.txtNote.Location = new Point(16, 25);
            this.txtNote.MaxLength = 500;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new Size(604, 35);
            this.txtNote.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.okButton.BackColor = Color.FromArgb(59, 130, 246);
            this.okButton.Cursor = Cursors.Hand;
            this.okButton.DialogResult = DialogResult.OK;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatStyle = FlatStyle.Flat;
            this.okButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.okButton.ForeColor = Color.White;
            this.okButton.Location = new Point(550, 535);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(100, 35);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Salva";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.cancelButton.BackColor = Color.FromArgb(156, 163, 175);
            this.cancelButton.Cursor = Cursors.Hand;
            this.cancelButton.DialogResult = DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = FlatStyle.Flat;
            this.cancelButton.Font = new Font("Segoe UI", 9F);
            this.cancelButton.ForeColor = Color.White;
            this.cancelButton.Location = new Point(460, 535);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(80, 35);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Annulla";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // ElementForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(247, 250, 252);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new Size(700, 600);
            this.Controls.Add(this.mainPanel);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElementForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Nuovo Elemento";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.basicFieldsGroup.ResumeLayout(false);
            this.basicFieldsGroup.PerformLayout();
            this.codeGenerationGroup.ResumeLayout(false);
            this.codeGenerationGroup.PerformLayout();
            this.quantityRevisionGroup.ResumeLayout(false);
            this.quantityRevisionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantita)).EndInit();
            this.notesGroup.ResumeLayout(false);
            this.notesGroup.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private GroupBox basicFieldsGroup;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblSottoProgetto;
        private ComboBox cmbSottoProgetto;
        private Label lblTipoParteMacchina;
        private ComboBox cmbTipoParteMacchina;
        private Label lblTipoGruppo;
        private ComboBox cmbTipoGruppo;
        private GroupBox codeGenerationGroup;
        private Label lblCodice;
        private TextBox txtCodice;
        private Button btnGeneraCodice;
        private Label lblValidazioneCodice;
        private Label lblDescrizione;
        private TextBox txtDescrizione;
        private CheckBox chkAutoDescrizione;
        private GroupBox quantityRevisionGroup;
        private Label lblQuantita;
        private NumericUpDown numQuantita;
        private Label lblRevisione;
        private ComboBox cmbRevisione;
        private Label lblStato;
        private TextBox txtStato;
        private GroupBox notesGroup;
        private TextBox txtNote;
        private Button okButton;
        private Button cancelButton;
    }
}