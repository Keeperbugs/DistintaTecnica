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
            this.mainPanel = new Panel();
            this.cancelButton = new Button();
            this.okButton = new Button();
            this.formTableLayout = new TableLayoutPanel();
            this.lblCommessa = new Label();
            this.txtCommessa = new TextBox();
            this.lblCliente = new Label();
            this.txtCliente = new TextBox();
            this.lblDataInserimento = new Label();
            this.dtpDataInserimento = new DateTimePicker();
            this.lblDisegnatore = new Label();
            this.txtDisegnatore = new TextBox();
            this.lblRevisione = new Label();
            this.cmbRevisione = new ComboBox();
            this.lblNote = new Label();
            this.txtNote = new TextBox();
            this.subtitleLabel = new Label();
            this.titleLabel = new Label();
            this.mainPanel.SuspendLayout();
            this.formTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = Color.White;
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.okButton);
            this.mainPanel.Controls.Add(this.formTableLayout);
            this.mainPanel.Controls.Add(this.subtitleLabel);
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 0);
            this.mainPanel.Margin = new Padding(3, 2, 3, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new Padding(18, 15, 18, 15);
            this.mainPanel.Size = new Size(525, 375);
            this.mainPanel.TabIndex = 0;
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
            this.cancelButton.Location = new Point(300, 328);
            this.cancelButton.Margin = new Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(70, 26);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Annulla";
            this.cancelButton.UseVisualStyleBackColor = false;
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
            this.okButton.Location = new Point(396, 328);
            this.okButton.Margin = new Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(105, 26);
            this.okButton.TabIndex = 12;
            this.okButton.Text = "Crea Progetto";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // formTableLayout
            // 
            this.formTableLayout.ColumnCount = 2;
            this.formTableLayout.ColumnStyles.Add(new ColumnStyle());
            this.formTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.formTableLayout.Controls.Add(this.lblCommessa, 0, 0);
            this.formTableLayout.Controls.Add(this.txtCommessa, 1, 0);
            this.formTableLayout.Controls.Add(this.lblCliente, 0, 1);
            this.formTableLayout.Controls.Add(this.txtCliente, 1, 1);
            this.formTableLayout.Controls.Add(this.lblDataInserimento, 0, 2);
            this.formTableLayout.Controls.Add(this.dtpDataInserimento, 1, 2);
            this.formTableLayout.Controls.Add(this.lblDisegnatore, 0, 3);
            this.formTableLayout.Controls.Add(this.txtDisegnatore, 1, 3);
            this.formTableLayout.Controls.Add(this.lblRevisione, 0, 4);
            this.formTableLayout.Controls.Add(this.cmbRevisione, 1, 4);
            this.formTableLayout.Controls.Add(this.lblNote, 0, 5);
            this.formTableLayout.Controls.Add(this.txtNote, 1, 5);
            this.formTableLayout.Location = new Point(20, 68);
            this.formTableLayout.Margin = new Padding(3, 2, 3, 2);
            this.formTableLayout.Name = "formTableLayout";
            this.formTableLayout.RowCount = 6;
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.RowStyles.Add(new RowStyle());
            this.formTableLayout.Size = new Size(472, 249);
            this.formTableLayout.TabIndex = 2;
            // 
            // lblCommessa
            // 
            this.lblCommessa.Anchor = AnchorStyles.Left;
            this.lblCommessa.AutoSize = true;
            this.lblCommessa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCommessa.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblCommessa.Location = new Point(3, 10);
            this.lblCommessa.Name = "lblCommessa";
            this.lblCommessa.Size = new Size(94, 15);
            this.lblCommessa.TabIndex = 0;
            this.lblCommessa.Text = "N° Commessa *:";
            // 
            // txtCommessa
            // 
            this.txtCommessa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCommessa.BorderStyle = BorderStyle.FixedSingle;
            this.txtCommessa.Font = new Font("Segoe UI", 9F);
            this.txtCommessa.Location = new Point(136, 6);
            this.txtCommessa.Margin = new Padding(7, 6, 0, 6);
            this.txtCommessa.MaxLength = 50;
            this.txtCommessa.Name = "txtCommessa";
            this.txtCommessa.Size = new Size(336, 23);
            this.txtCommessa.TabIndex = 1;
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = AnchorStyles.Left;
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCliente.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblCliente.Location = new Point(3, 45);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new Size(57, 15);
            this.lblCliente.TabIndex = 2;
            this.lblCliente.Text = "Cliente *:";
            // 
            // txtCliente
            // 
            this.txtCliente.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCliente.BorderStyle = BorderStyle.FixedSingle;
            this.txtCliente.Font = new Font("Segoe UI", 9F);
            this.txtCliente.Location = new Point(136, 41);
            this.txtCliente.Margin = new Padding(7, 6, 0, 6);
            this.txtCliente.MaxLength = 100;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new Size(336, 23);
            this.txtCliente.TabIndex = 3;
            // 
            // lblDataInserimento
            // 
            this.lblDataInserimento.Anchor = AnchorStyles.Left;
            this.lblDataInserimento.AutoSize = true;
            this.lblDataInserimento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDataInserimento.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblDataInserimento.Location = new Point(3, 80);
            this.lblDataInserimento.Name = "lblDataInserimento";
            this.lblDataInserimento.Size = new Size(107, 15);
            this.lblDataInserimento.TabIndex = 4;
            this.lblDataInserimento.Text = "Data Inserimento:";
            // 
            // dtpDataInserimento
            // 
            this.dtpDataInserimento.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.dtpDataInserimento.Font = new Font("Segoe UI", 9F);
            this.dtpDataInserimento.Format = DateTimePickerFormat.Short;
            this.dtpDataInserimento.Location = new Point(136, 76);
            this.dtpDataInserimento.Margin = new Padding(7, 6, 0, 6);
            this.dtpDataInserimento.Name = "dtpDataInserimento";
            this.dtpDataInserimento.Size = new Size(336, 23);
            this.dtpDataInserimento.TabIndex = 5;
            // 
            // lblDisegnatore
            // 
            this.lblDisegnatore.Anchor = AnchorStyles.Left;
            this.lblDisegnatore.AutoSize = true;
            this.lblDisegnatore.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDisegnatore.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblDisegnatore.Location = new Point(3, 115);
            this.lblDisegnatore.Name = "lblDisegnatore";
            this.lblDisegnatore.Size = new Size(123, 15);
            this.lblDisegnatore.TabIndex = 6;
            this.lblDisegnatore.Text = "Nome Disegnatore *:";
            // 
            // txtDisegnatore
            // 
            this.txtDisegnatore.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtDisegnatore.BorderStyle = BorderStyle.FixedSingle;
            this.txtDisegnatore.Font = new Font("Segoe UI", 9F);
            this.txtDisegnatore.Location = new Point(136, 111);
            this.txtDisegnatore.Margin = new Padding(7, 6, 0, 6);
            this.txtDisegnatore.MaxLength = 100;
            this.txtDisegnatore.Name = "txtDisegnatore";
            this.txtDisegnatore.Size = new Size(336, 23);
            this.txtDisegnatore.TabIndex = 7;
            // 
            // lblRevisione
            // 
            this.lblRevisione.Anchor = AnchorStyles.Left;
            this.lblRevisione.AutoSize = true;
            this.lblRevisione.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRevisione.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblRevisione.Location = new Point(3, 150);
            this.lblRevisione.Name = "lblRevisione";
            this.lblRevisione.Size = new Size(108, 15);
            this.lblRevisione.TabIndex = 8;
            this.lblRevisione.Text = "Lettera Revisione:";
            // 
            // cmbRevisione
            // 
            this.cmbRevisione.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.cmbRevisione.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRevisione.Font = new Font("Segoe UI", 9F);
            this.cmbRevisione.FormattingEnabled = true;
            this.cmbRevisione.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            this.cmbRevisione.Location = new Point(136, 146);
            this.cmbRevisione.Margin = new Padding(7, 6, 0, 6);
            this.cmbRevisione.Name = "cmbRevisione";
            this.cmbRevisione.Size = new Size(336, 23);
            this.cmbRevisione.TabIndex = 9;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNote.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblNote.Location = new Point(3, 175);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(38, 15);
            this.lblNote.TabIndex = 10;
            this.lblNote.Text = "Note:";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtNote.BorderStyle = BorderStyle.FixedSingle;
            this.txtNote.Font = new Font("Segoe UI", 9F);
            this.txtNote.Location = new Point(136, 181);
            this.txtNote.Margin = new Padding(7, 6, 0, 6);
            this.txtNote.MaxLength = 500;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = ScrollBars.Vertical;
            this.txtNote.Size = new Size(336, 60);
            this.txtNote.TabIndex = 11;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.subtitleLabel.Location = new Point(20, 39);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new Size(226, 15);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Inserisci le informazioni base del progetto";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            this.titleLabel.Location = new Point(20, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new Size(255, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "INFORMAZIONI PROGETTO";
            // 
            // ProjectForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(247, 250, 252);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new Size(525, 375);
            this.Controls.Add(this.mainPanel);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Margin = new Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Nuovo Progetto";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.formTableLayout.ResumeLayout(false);
            this.formTableLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

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
        private Button okButton;
        private Button cancelButton;
    }
}