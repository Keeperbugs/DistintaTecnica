namespace DistintaTecnica.Forms
{
    partial class SearchResultsForm
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
            this.mainPanel = new Panel();
            this.resultsPanel = new Panel();
            this.resultsListView = new ListView();
            this.contextMenu = new ContextMenuStrip(this.components);
            this.openMenuItem = new ToolStripMenuItem();
            this.copyCodeMenuItem = new ToolStripMenuItem();
            this.copyDescriptionMenuItem = new ToolStripMenuItem();
            this.separator1 = new ToolStripSeparator();
            this.propertiesMenuItem = new ToolStripMenuItem();
            this.searchPanel = new Panel();
            this.clearButton = new Button();
            this.searchButton = new Button();
            this.searchTextBox = new TextBox();
            this.searchLabel = new Label();
            this.headerPanel = new Panel();
            this.resultsCountLabel = new Label();
            this.titleLabel = new Label();
            this.buttonsPanel = new Panel();
            this.closeButton = new Button();
            this.viewDetailsButton = new Button();
            this.selectButton = new Button();
            this.mainPanel.SuspendLayout();
            this.resultsPanel.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = Color.White;
            this.mainPanel.Controls.Add(this.resultsPanel);
            this.mainPanel.Controls.Add(this.searchPanel);
            this.mainPanel.Controls.Add(this.headerPanel);
            this.mainPanel.Controls.Add(this.buttonsPanel);
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new Padding(16);
            this.mainPanel.Size = new Size(900, 600);
            this.mainPanel.TabIndex = 0;
            // 
            // resultsPanel
            // 
            this.resultsPanel.BackColor = Color.White;
            this.resultsPanel.Controls.Add(this.resultsListView);
            this.resultsPanel.Dock = DockStyle.Fill;
            this.resultsPanel.Location = new Point(16, 156);
            this.resultsPanel.Name = "resultsPanel";
            this.resultsPanel.Padding = new Padding(16, 8, 16, 16);
            this.resultsPanel.Size = new Size(868, 368);
            this.resultsPanel.TabIndex = 0;
            // 
            // resultsListView
            // 
            this.resultsListView.BackColor = Color.White;
            this.resultsListView.BorderStyle = BorderStyle.FixedSingle;
            this.resultsListView.ContextMenuStrip = this.contextMenu;
            this.resultsListView.Dock = DockStyle.Fill;
            this.resultsListView.Font = new Font("Segoe UI", 9F);
            this.resultsListView.FullRowSelect = true;
            this.resultsListView.GridLines = true;
            this.resultsListView.HideSelection = false;
            this.resultsListView.Location = new Point(16, 8);
            this.resultsListView.MultiSelect = false;
            this.resultsListView.Name = "resultsListView";
            this.resultsListView.Size = new Size(836, 344);
            this.resultsListView.TabIndex = 0;
            this.resultsListView.UseCompatibleStateImageBehavior = false;
            this.resultsListView.View = View.Details;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new Size(20, 20);
            this.contextMenu.Items.AddRange(new ToolStripItem[] { this.openMenuItem, this.copyCodeMenuItem, this.copyDescriptionMenuItem, this.separator1, this.propertiesMenuItem });
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new Size(184, 106);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new Size(183, 24);
            this.openMenuItem.Text = "Apri";
            // 
            // copyCodeMenuItem
            // 
            this.copyCodeMenuItem.Name = "copyCodeMenuItem";
            this.copyCodeMenuItem.Size = new Size(183, 24);
            this.copyCodeMenuItem.Text = "Copia Codice";
            // 
            // copyDescriptionMenuItem
            // 
            this.copyDescriptionMenuItem.Name = "copyDescriptionMenuItem";
            this.copyDescriptionMenuItem.Size = new Size(183, 24);
            this.copyDescriptionMenuItem.Text = "Copia Descrizione";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new Size(180, 6);
            // 
            // propertiesMenuItem
            // 
            this.propertiesMenuItem.Name = "propertiesMenuItem";
            this.propertiesMenuItem.Size = new Size(183, 24);
            this.propertiesMenuItem.Text = "Proprietà...";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = Color.White;
            this.searchPanel.Controls.Add(this.clearButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = DockStyle.Top;
            this.searchPanel.Location = new Point(16, 96);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new Padding(16, 8, 16, 8);
            this.searchPanel.Size = new Size(868, 60);
            this.searchPanel.TabIndex = 1;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = Color.FromArgb(156, 163, 175);
            this.clearButton.Cursor = Cursors.Hand;
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.FlatStyle = FlatStyle.Flat;
            this.clearButton.Font = new Font("Segoe UI", 9F);
            this.clearButton.ForeColor = Color.White;
            this.clearButton.Location = new Point(480, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new Size(70, 30);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Pulisci";
            this.clearButton.UseVisualStyleBackColor = false;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = Color.FromArgb(59, 130, 246);
            this.searchButton.Cursor = Cursors.Hand;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = FlatStyle.Flat;
            this.searchButton.Font = new Font("Segoe UI", 9F);
            this.searchButton.ForeColor = Color.White;
            this.searchButton.Location = new Point(390, 12);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new Size(80, 30);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "🔍 Cerca";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.searchTextBox.Font = new Font("Segoe UI", 9F);
            this.searchTextBox.Location = new Point(80, 14);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new Size(300, 27);
            this.searchTextBox.TabIndex = 1;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.searchLabel.ForeColor = Color.FromArgb(55, 65, 81);
            this.searchLabel.Location = new Point(16, 16);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new Size(57, 20);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Ricerca:";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = Color.FromArgb(248, 250, 252);
            this.headerPanel.Controls.Add(this.resultsCountLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Location = new Point(16, 16);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new Padding(16);
            this.headerPanel.Size = new Size(868, 80);
            this.headerPanel.TabIndex = 2;
            // 
            // resultsCountLabel
            // 
            this.resultsCountLabel.AutoSize = true;
            this.resultsCountLabel.Font = new Font("Segoe UI", 9F);
            this.resultsCountLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this.resultsCountLabel.Location = new Point(16, 45);
            this.resultsCountLabel.Name = "resultsCountLabel";
            this.resultsCountLabel.Size = new Size(123, 20);
            this.resultsCountLabel.TabIndex = 1;
            this.resultsCountLabel.Text = "0 risultati trovati";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            this.titleLabel.Location = new Point(16, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new Size(199, 32);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "RISULTATI RICERCA";
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = Color.FromArgb(248, 250, 252);
            this.buttonsPanel.Controls.Add(this.closeButton);
            this.buttonsPanel.Controls.Add(this.viewDetailsButton);
            this.buttonsPanel.Controls.Add(this.selectButton);
            this.buttonsPanel.Dock = DockStyle.Bottom;
            this.buttonsPanel.Location = new Point(16, 524);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new Padding(16);
            this.buttonsPanel.Size = new Size(868, 60);
            this.buttonsPanel.TabIndex = 3;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.closeButton.BackColor = Color.FromArgb(156, 163, 175);
            this.closeButton.Cursor = Cursors.Hand;
            this.closeButton.DialogResult = DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = FlatStyle.Flat;
            this.closeButton.Font = new Font("Segoe UI", 9F);
            this.closeButton.ForeColor = Color.White;
            this.closeButton.Location = new Point(772, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new Size(80, 35);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Chiudi";
            this.closeButton.UseVisualStyleBackColor = false;
            // 
            // viewDetailsButton
            // 
            this.viewDetailsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.viewDetailsButton.BackColor = Color.FromArgb(59, 130, 246);
            this.viewDetailsButton.Cursor = Cursors.Hand;
            this.viewDetailsButton.Enabled = false;
            this.viewDetailsButton.FlatAppearance.BorderSize = 0;
            this.viewDetailsButton.FlatStyle = FlatStyle.Flat;
            this.viewDetailsButton.Font = new Font("Segoe UI", 9F);
            this.viewDetailsButton.ForeColor = Color.White;
            this.viewDetailsButton.Location = new Point(682, 12);
            this.viewDetailsButton.Name = "viewDetailsButton";
            this.viewDetailsButton.Size = new Size(80, 35);
            this.viewDetailsButton.TabIndex = 1;
            this.viewDetailsButton.Text = "Dettagli";
            this.viewDetailsButton.UseVisualStyleBackColor = false;
            // 
            // selectButton
            // 
            this.selectButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.selectButton.BackColor = Color.FromArgb(34, 197, 94);
            this.selectButton.Cursor = Cursors.Hand;
            this.selectButton.DialogResult = DialogResult.OK;
            this.selectButton.Enabled = false;
            this.selectButton.FlatAppearance.BorderSize = 0;
            this.selectButton.FlatStyle = FlatStyle.Flat;
            this.selectButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.selectButton.ForeColor = Color.White;
            this.selectButton.Location = new Point(572, 12);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new Size(100, 35);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "Seleziona";
            this.selectButton.UseVisualStyleBackColor = false;
            // 
            // SearchResultsForm
            // 
            this.AcceptButton = this.selectButton;
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(247, 250, 252);
            this.CancelButton = this.closeButton;
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.mainPanel);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(700, 400);
            this.Name = "SearchResultsForm";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Risultati Ricerca";
            this.mainPanel.ResumeLayout(false);
            this.resultsPanel.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Panel headerPanel;
        private Label titleLabel;
        private Label resultsCountLabel;
        private Panel searchPanel;
        private Label searchLabel;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button clearButton;
        private Panel resultsPanel;
        private ListView resultsListView;
        private Panel buttonsPanel;
        private Button selectButton;
        private Button viewDetailsButton;
        private Button closeButton;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem openMenuItem;
        private ToolStripMenuItem copyCodeMenuItem;
        private ToolStripMenuItem copyDescriptionMenuItem;
        private ToolStripSeparator separator1;
        private ToolStripMenuItem propertiesMenuItem;
    }
}