namespace DistintaTecnica.Forms
{
    partial class CodeSelectorForm
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
            this.selectionGroup = new GroupBox();
            this.radioCreateNew = new RadioButton();
            this.radioSelectExisting = new RadioButton();
            this.newCodeGroup = new GroupBox();
            this.lblNewCode = new Label();
            this.txtNewCode = new TextBox();
            this.btnGenerateCode = new Button();
            this.lblNewDescription = new Label();
            this.txtNewDescription = new TextBox();
            this.lblValidation = new Label();
            this.existingCodeGroup = new GroupBox();
            this.searchPanel = new Panel();
            this.lblSearch = new Label();
            this.txtSearch = new TextBox();
            this.btnClearSearch = new Button();
            this.codesListView = new ListView();
            this.colCodice = new ColumnHeader();
            this.colDescrizione = new ColumnHeader();
            this.colUtilizzi = new ColumnHeader();
            this.colUltimoUso = new ColumnHeader();
            this.detailsPanel = new Panel();
            this.lblSelectedCode = new Label();
            this.txtSelectedCode = new TextBox();
            this.lblSelectedDescription = new Label();
            this.txtSelectedDescription = new TextBox();
            this.lblUsageInfo = new Label();
            this.txtUsageInfo = new TextBox();
            this.btnShowHistory = new Button();
            this.buttonsPanel = new Panel();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.statusLabel = new Label();
            this.mainPanel.SuspendLayout();
            this.selectionGroup.SuspendLayout();
            this.newCodeGroup.SuspendLayout();
            this.existingCodeGroup.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.detailsPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = Color.White;
            this.mainPanel.Controls.Add(this.buttonsPanel);
            this.mainPanel.Controls.Add(this.existingCodeGroup);
            this.mainPanel.Controls.Add(this.newCodeGroup);
            this.mainPanel.Controls.Add(this.selectionGroup);
            this.mainPanel.Controls.Add(this.subtitleLabel);
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.statusLabel);
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.Size = new Size(900, 700);
            this.mainPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            this.titleLabel.Location = new Point(23, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new Size(182, 32);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "SELEZIONE CODICE";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.subtitleLabel.Location = new Point(23, 52);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new Size(287, 20);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Crea un nuovo codice o seleziona esistente";
            // 
            // selectionGroup
            // 
            this.selectionGroup.Controls.Add(this.radioSelectExisting);
            this.selectionGroup.Controls.Add(this.radioCreateNew);
            this.selectionGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.selectionGroup.Location = new Point(23, 85);
            this.selectionGroup.Name = "selectionGroup";
            this.selectionGroup.Size = new Size(854, 60);
            this.selectionGroup.TabIndex = 2;
            this.selectionGroup.TabStop = false;
            this.selectionGroup.Text = "Modalità Selezione";
            // 
            // radioCreateNew
            // 
            this.radioCreateNew.AutoSize = true;
            this.radioCreateNew.Checked = true;
            this.radioCreateNew.Font = new Font("Segoe UI", 9F);
            this.radioCreateNew.ForeColor = Color.FromArgb(55, 65, 81);
            this.radioCreateNew.Location = new Point(20, 28);
            this.radioCreateNew.Name = "radioCreateNew";
            this.radioCreateNew.Size = new Size(140, 24);
            this.radioCreateNew.TabIndex = 0;
            this.radioCreateNew.TabStop = true;
            this.radioCreateNew.Text = "Crea nuovo codice";
            this.radioCreateNew.UseVisualStyleBackColor = true;
            this.radioCreateNew.CheckedChanged += this.RadioCreateNew_CheckedChanged;
            // 
            // radioSelectExisting
            // 
            this.radioSelectExisting.AutoSize = true;
            this.radioSelectExisting.Font = new Font("Segoe UI", 9F);
            this.radioSelectExisting.ForeColor = Color.FromArgb(55, 65, 81);
            this.radioSelectExisting.Location = new Point(200, 28);
            this.radioSelectExisting.Name = "radioSelectExisting";
            this.radioSelectExisting.Size = new Size(189, 24);
            this.radioSelectExisting.TabIndex = 1;
            this.radioSelectExisting.Text = "Seleziona da libreria codici";
            this.radioSelectExisting.UseVisualStyleBackColor = true;
            this.radioSelectExisting.CheckedChanged += this.RadioSelectExisting_CheckedChanged;
            // 
            // newCodeGroup
            // 
            this.newCodeGroup.Controls.Add(this.lblValidation);
            this.newCodeGroup.Controls.Add(this.txtNewDescription);
            this.newCodeGroup.Controls.Add(this.lblNewDescription);
            this.newCodeGroup.Controls.Add(this.btnGenerateCode);
            this.newCodeGroup.Controls.Add(this.txtNewCode);
            this.newCodeGroup.Controls.Add(this.lblNewCode);
            this.newCodeGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.newCodeGroup.Location = new Point(23, 151);
            this.newCodeGroup.Name = "newCodeGroup";
            this.newCodeGroup.Size = new Size(854, 120);
            this.newCodeGroup.TabIndex = 3;
            this.newCodeGroup.TabStop = false;
            this.newCodeGroup.Text = "Nuovo Codice";
            // 
            // lblNewCode
            // 
            this.lblNewCode.AutoSize = true;
            this.lblNewCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNewCode.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblNewCode.Location = new Point(20, 31);
            this.lblNewCode.Name = "lblNewCode";
            this.lblNewCode.Size = new Size(68, 20);
            this.lblNewCode.TabIndex = 0;
            this.lblNewCode.Text = "Codice *:";
            // 
            // txtNewCode
            // 
            this.txtNewCode.BorderStyle = BorderStyle.FixedSingle;
            this.txtNewCode.Font = new Font("Segoe UI", 9F);
            this.txtNewCode.Location = new Point(100, 28);
            this.txtNewCode.MaxLength = 50;
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new Size(200, 27);
            this.txtNewCode.TabIndex = 1;
            this.txtNewCode.TextChanged += this.TxtNewCode_TextChanged;
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.BackColor = Color.FromArgb(16, 185, 129);
            this.btnGenerateCode.Cursor = Cursors.Hand;
            this.btnGenerateCode.FlatAppearance.BorderSize = 0;
            this.btnGenerateCode.FlatStyle = FlatStyle.Flat;
            this.btnGenerateCode.Font = new Font("Segoe UI", 8F);
            this.btnGenerateCode.ForeColor = Color.White;
            this.btnGenerateCode.Location = new Point(310, 26);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new Size(90, 30);
            this.btnGenerateCode.TabIndex = 2;
            this.btnGenerateCode.Text = "🔧 Genera";
            this.btnGenerateCode.UseVisualStyleBackColor = false;
            this.btnGenerateCode.Click += this.BtnGenerateCode_Click;
            // 
            // lblNewDescription
            // 
            this.lblNewDescription.AutoSize = true;
            this.lblNewDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNewDescription.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblNewDescription.Location = new Point(20, 71);
            this.lblNewDescription.Name = "lblNewDescription";
            this.lblNewDescription.Size = new Size(102, 20);
            this.lblNewDescription.TabIndex = 3;
            this.lblNewDescription.Text = "Descrizione *:";
            // 
            // txtNewDescription
            // 
            this.txtNewDescription.BorderStyle = BorderStyle.FixedSingle;
            this.txtNewDescription.Font = new Font("Segoe UI", 9F);
            this.txtNewDescription.Location = new Point(130, 68);
            this.txtNewDescription.MaxLength = 200;
            this.txtNewDescription.Name = "txtNewDescription";
            this.txtNewDescription.Size = new Size(500, 27);
            this.txtNewDescription.TabIndex = 4;
            this.txtNewDescription.TextChanged += this.TxtNewDescription_TextChanged;
            // 
            // lblValidation
            // 
            this.lblValidation.AutoSize = true;
            this.lblValidation.Font = new Font("Segoe UI", 8F);
            this.lblValidation.Location = new Point(410, 31);
            this.lblValidation.Name = "lblValidation";
            this.lblValidation.Size = new Size(0, 19);
            this.lblValidation.TabIndex = 5;
            // 
            // existingCodeGroup
            // 
            this.existingCodeGroup.Controls.Add(this.detailsPanel);
            this.existingCodeGroup.Controls.Add(this.codesListView);
            this.existingCodeGroup.Controls.Add(this.searchPanel);
            this.existingCodeGroup.Enabled = false;
            this.existingCodeGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.existingCodeGroup.Location = new Point(23, 277);
            this.existingCodeGroup.Name = "existingCodeGroup";
            this.existingCodeGroup.Size = new Size(854, 350);
            this.existingCodeGroup.TabIndex = 4;
            this.existingCodeGroup.TabStop = false;
            this.existingCodeGroup.Text = "Selezione da Libreria Codici";
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.btnClearSearch);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.lblSearch);
            this.searchPanel.Dock = DockStyle.Top;
            this.searchPanel.Location = new Point(3, 23);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new Padding(17, 8, 17, 8);
            this.searchPanel.Size = new Size(848, 50);
            this.searchPanel.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSearch.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblSearch.Location = new Point(17, 16);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(57, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Ricerca:";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Font = new Font("Segoe UI", 9F);
            this.txtSearch.Location = new Point(80, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Cerca per codice o descrizione...";
            this.txtSearch.Size = new Size(300, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += this.TxtSearch_TextChanged;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = Color.FromArgb(156, 163, 175);
            this.btnClearSearch.Cursor = Cursors.Hand;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.FlatStyle = FlatStyle.Flat;
            this.btnClearSearch.Font = new Font("Segoe UI", 8F);
            this.btnClearSearch.ForeColor = Color.White;
            this.btnClearSearch.Location = new Point(390, 11);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new Size(60, 30);
            this.btnClearSearch.TabIndex = 2;
            this.btnClearSearch.Text = "Pulisci";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += this.BtnClearSearch_Click;
            // 
            // codesListView
            // 
            this.codesListView.BorderStyle = BorderStyle.FixedSingle;
            this.codesListView.Columns.AddRange(new ColumnHeader[] { this.colCodice, this.colDescrizione, this.colUtilizzi, this.colUltimoUso });
            this.codesListView.Font = new Font("Segoe UI", 9F);
            this.codesListView.FullRowSelect = true;
            this.codesListView.GridLines = true;
            this.codesListView.HideSelection = false;
            this.codesListView.Location = new Point(20, 79);
            this.codesListView.MultiSelect = false;
            this.codesListView.Name = "codesListView";
            this.codesListView.Size = new Size(814, 180);
            this.codesListView.TabIndex = 1;
            this.codesListView.UseCompatibleStateImageBehavior = false;
            this.codesListView.View = View.Details;
            this.codesListView.SelectedIndexChanged += this.CodesListView_SelectedIndexChanged;
            this.codesListView.DoubleClick += this.CodesListView_DoubleClick;
            // 
            // colCodice
            // 
            this.colCodice.Text = "Codice";
            this.colCodice.Width = 150;
            // 
            // colDescrizione
            // 
            this.colDescrizione.Text = "Descrizione";
            this.colDescrizione.Width = 400;
            // 
            // colUtilizzi
            // 
            this.colUtilizzi.Text = "Utilizzi";
            this.colUtilizzi.TextAlign = HorizontalAlignment.Center;
            this.colUtilizzi.Width = 80;
            // 
            // colUltimoUso
            // 
            this.colUltimoUso.Text = "Ultimo Uso";
            this.colUltimoUso.TextAlign = HorizontalAlignment.Center;
            this.colUltimoUso.Width = 120;
            // 
            // detailsPanel
            // 
            this.detailsPanel.Controls.Add(this.btnShowHistory);
            this.detailsPanel.Controls.Add(this.txtUsageInfo);
            this.detailsPanel.Controls.Add(this.lblUsageInfo);
            this.detailsPanel.Controls.Add(this.txtSelectedDescription);
            this.detailsPanel.Controls.Add(this.lblSelectedDescription);
            this.detailsPanel.Controls.Add(this.txtSelectedCode);
            this.detailsPanel.Controls.Add(this.lblSelectedCode);
            this.detailsPanel.Location = new Point(20, 265);
            this.detailsPanel.Name = "detailsPanel";
            this.detailsPanel.Size = new Size(814, 75);
            this.detailsPanel.TabIndex = 2;
            // 
            // lblSelectedCode
            // 
            this.lblSelectedCode.AutoSize = true;
            this.lblSelectedCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSelectedCode.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblSelectedCode.Location = new Point(3, 8);
            this.lblSelectedCode.Name = "lblSelectedCode";
            this.lblSelectedCode.Size = new Size(126, 20);
            this.lblSelectedCode.TabIndex = 0;
            this.lblSelectedCode.Text = "Codice selezionato:";
            // 
            // txtSelectedCode
            // 
            this.txtSelectedCode.BackColor = Color.FromArgb(247, 250, 252);
            this.txtSelectedCode.BorderStyle = BorderStyle.FixedSingle;
            this.txtSelectedCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.txtSelectedCode.Location = new Point(135, 5);
            this.txtSelectedCode.Name = "txtSelectedCode";
            this.txtSelectedCode.ReadOnly = true;
            this.txtSelectedCode.Size = new Size(150, 27);
            this.txtSelectedCode.TabIndex = 1;
            // 
            // lblSelectedDescription
            // 
            this.lblSelectedDescription.AutoSize = true;
            this.lblSelectedDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSelectedDescription.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblSelectedDescription.Location = new Point(300, 8);
            this.lblSelectedDescription.Name = "lblSelectedDescription";
            this.lblSelectedDescription.Size = new Size(92, 20);
            this.lblSelectedDescription.TabIndex = 2;
            this.lblSelectedDescription.Text = "Descrizione:";
            // 
            // txtSelectedDescription
            // 
            this.txtSelectedDescription.BackColor = Color.FromArgb(247, 250, 252);
            this.txtSelectedDescription.BorderStyle = BorderStyle.FixedSingle;
            this.txtSelectedDescription.Font = new Font("Segoe UI", 9F);
            this.txtSelectedDescription.Location = new Point(398, 5);
            this.txtSelectedDescription.Name = "txtSelectedDescription";
            this.txtSelectedDescription.ReadOnly = true;
            this.txtSelectedDescription.Size = new Size(350, 27);
            this.txtSelectedDescription.TabIndex = 3;
            // 
            // lblUsageInfo
            // 
            this.lblUsageInfo.AutoSize = true;
            this.lblUsageInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblUsageInfo.ForeColor = Color.FromArgb(55, 65, 81);
            this.lblUsageInfo.Location = new Point(3, 43);
            this.lblUsageInfo.Name = "lblUsageInfo";
            this.lblUsageInfo.Size = new Size(129, 20);
            this.lblUsageInfo.TabIndex = 4;
            this.lblUsageInfo.Text = "Informazioni uso:";
            // 
            // txtUsageInfo
            // 
            this.txtUsageInfo.BackColor = Color.FromArgb(247, 250, 252);
            this.txtUsageInfo.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsageInfo.Font = new Font("Segoe UI", 9F);
            this.txtUsageInfo.Location = new Point(135, 40);
            this.txtUsageInfo.Name = "txtUsageInfo";
            this.txtUsageInfo.ReadOnly = true;
            this.txtUsageInfo.Size = new Size(480, 27);
            this.txtUsageInfo.TabIndex = 5;
            // 
            // btnShowHistory
            // 
            this.btnShowHistory.BackColor = Color.FromArgb(59, 130, 246);
            this.btnShowHistory.Cursor = Cursors.Hand;
            this.btnShowHistory.FlatAppearance.BorderSize = 0;
            this.btnShowHistory.FlatStyle = FlatStyle.Flat;
            this.btnShowHistory.Font = new Font("Segoe UI", 8F);
            this.btnShowHistory.ForeColor = Color.White;
            this.btnShowHistory.Location = new Point(630, 38);
            this.btnShowHistory.Name = "btnShowHistory";
            this.btnShowHistory.Size = new Size(80, 30);
            this.btnShowHistory.TabIndex = 6;
            this.btnShowHistory.Text = "Storico";
            this.btnShowHistory.UseVisualStyleBackColor = false;
            this.btnShowHistory.Click += this.BtnShowHistory_Click;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.btnCancel);
            this.buttonsPanel.Controls.Add(this.btnOK);
            this.buttonsPanel.Dock = DockStyle.Bottom;
            this.buttonsPanel.Location = new Point(20, 633);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new Padding(0, 10, 0, 0);
            this.buttonsPanel.Size = new Size(860, 47);
            this.buttonsPanel.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnOK.BackColor = Color.FromArgb(59, 130, 246);
            this.btnOK.Cursor = Cursors.Hand;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(750, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(100, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Seleziona";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += this.BtnOK_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnCancel.BackColor = Color.FromArgb(156, 163, 175);
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(640, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annulla";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new Font("Segoe UI", 8F);
            this.statusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.statusLabel.Location = new Point(23, 650);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(239, 19);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "Seleziona una modalità per continuare";
            // 
            // CodeSelectorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(247, 250, 252);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(900, 700);
            this.Controls.Add(this.mainPanel);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeSelectorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Selezione Codice";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.selectionGroup.ResumeLayout(false);
            this.selectionGroup.PerformLayout();
            this.newCodeGroup.ResumeLayout(false);
            this.newCodeGroup.PerformLayout();
            this.existingCodeGroup.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.detailsPanel.ResumeLayout(false);
            this.detailsPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private GroupBox selectionGroup;
        private RadioButton radioCreateNew;
        private RadioButton radioSelectExisting;
        private GroupBox newCodeGroup;
        private Label lblNewCode;
        private TextBox txtNewCode;
        private Button btnGenerateCode;
        private Label lblNewDescription;
        private TextBox txtNewDescription;
        private Label lblValidation;
        private GroupBox existingCodeGroup;
        private Panel searchPanel;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnClearSearch;
        private ListView codesListView;
        private ColumnHeader colCodice;
        private ColumnHeader colDescrizione;
        private ColumnHeader colUtilizzi;
        private ColumnHeader colUltimoUso;
        private Panel detailsPanel;
        private Label lblSelectedCode;
        private TextBox txtSelectedCode;
        private Label lblSelectedDescription;
        private TextBox txtSelectedDescription;
        private Label lblUsageInfo;
        private TextBox txtUsageInfo;
        private Button btnShowHistory;
        private Panel buttonsPanel;
        private Button btnOK;
        private Button btnCancel;
        private Label statusLabel;
    }
}