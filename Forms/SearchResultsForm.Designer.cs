namespace DistintaTecnica.Forms
{
    partial class SearchResultsForm
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
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "SearchResultsForm";
            this.Text = "Risultati Ricerca";
            this.ShowIcon = false;

            this.InitializeControls();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeControls()
        {
            // Main panel
            this.mainPanel = new Panel();
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(16);
            this.mainPanel.BackColor = System.Drawing.Color.White;

            // Header panel
            this.headerPanel = new Panel();
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 80;
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.headerPanel.Padding = new Padding(16);

            // Title label
            this.titleLabel = new Label();
            this.titleLabel.Text = "RISULTATI RICERCA";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(45, 55, 72);
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new Point(16, 16);

            // Results count label
            this.resultsCountLabel = new Label();
            this.resultsCountLabel.Text = "0 risultati trovati";
            this.resultsCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.resultsCountLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.resultsCountLabel.AutoSize = true;
            this.resultsCountLabel.Location = new Point(16, 45);

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.resultsCountLabel);

            // Search panel
            this.searchPanel = new Panel();
            this.searchPanel.Dock = DockStyle.Top;
            this.searchPanel.Height = 60;
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Padding = new Padding(16, 8, 16, 8);

            // Search label
            this.searchLabel = new Label();
            this.searchLabel.Text = "Ricerca:";
            this.searchLabel.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new Point(16, 16);
            this.searchLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);

            // Search textbox
            this.searchTextBox = new TextBox();
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchTextBox.Location = new Point(80, 14);
            this.searchTextBox.Size = new Size(300, 27);
            this.searchTextBox.BorderStyle = BorderStyle.FixedSingle;

            // Search button
            this.searchButton = new Button();
            this.searchButton.Text = "🔍 Cerca";
            this.searchButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchButton.Location = new Point(390, 12);
            this.searchButton.Size = new Size(80, 30);
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.FlatStyle = FlatStyle.Flat;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.Cursor = Cursors.Hand;
            this.searchButton.UseVisualStyleBackColor = false;

            // Clear button
            this.clearButton = new Button();
            this.clearButton.Text = "Pulisci";
            this.clearButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.clearButton.Location = new Point(480, 12);
            this.clearButton.Size = new Size(70, 30);
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.clearButton.ForeColor = System.Drawing.Color.White;
            this.clearButton.FlatStyle = FlatStyle.Flat;
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.Cursor = Cursors.Hand;
            this.clearButton.UseVisualStyleBackColor = false;

            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.clearButton);

            // Results ListView
            this.resultsListView = new ListView();
            this.resultsListView.Dock = DockStyle.Fill;
            this.resultsListView.View = View.Details;
            this.resultsListView.FullRowSelect = true;
            this.resultsListView.GridLines = true;
            this.resultsListView.MultiSelect = false;
            this.resultsListView.HideSelection = false;
            this.resultsListView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.resultsListView.BackColor = System.Drawing.Color.White;
            this.resultsListView.BorderStyle = BorderStyle.FixedSingle;

            // ListView columns
            this.resultsListView.Columns.Add("Tipo", 100);
            this.resultsListView.Columns.Add("Codice", 120);
            this.resultsListView.Columns.Add("Descrizione", 300);
            this.resultsListView.Columns.Add("Progetto", 120);
            this.resultsListView.Columns.Add("Percorso", 200);

            // Results panel (contains ListView)
            this.resultsPanel = new Panel();
            this.resultsPanel.Dock = DockStyle.Fill;
            this.resultsPanel.Padding = new Padding(16, 8, 16, 16);
            this.resultsPanel.BackColor = System.Drawing.Color.White;
            this.resultsPanel.Controls.Add(this.resultsListView);

            // Buttons panel
            this.buttonsPanel = new Panel();
            this.buttonsPanel.Dock = DockStyle.Bottom;
            this.buttonsPanel.Height = 60;
            this.buttonsPanel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.buttonsPanel.Padding = new Padding(16);

            // Select button
            this.selectButton = new Button();
            this.selectButton.Text = "Seleziona";
            this.selectButton.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.selectButton.Size = new Size(100, 35);
            this.selectButton.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.selectButton.ForeColor = System.Drawing.Color.White;
            this.selectButton.FlatStyle = FlatStyle.Flat;
            this.selectButton.FlatAppearance.BorderSize = 0;
            this.selectButton.Cursor = Cursors.Hand;
            this.selectButton.UseVisualStyleBackColor = false;
            this.selectButton.Enabled = false;
            this.selectButton.DialogResult = DialogResult.OK;

            // View details button
            this.viewDetailsButton = new Button();
            this.viewDetailsButton.Text = "Dettagli";
            this.viewDetailsButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.viewDetailsButton.Size = new Size(80, 35);
            this.viewDetailsButton.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.viewDetailsButton.ForeColor = System.Drawing.Color.White;
            this.viewDetailsButton.FlatStyle = FlatStyle.Flat;
            this.viewDetailsButton.FlatAppearance.BorderSize = 0;
            this.viewDetailsButton.Cursor = Cursors.Hand;
            this.viewDetailsButton.UseVisualStyleBackColor = false;
            this.viewDetailsButton.Enabled = false;

            // Close button
            this.closeButton = new Button();
            this.closeButton.Text = "Chiudi";
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.closeButton.Size = new Size(80, 35);
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.FlatStyle = FlatStyle.Flat;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.Cursor = Cursors.Hand;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.DialogResult = DialogResult.Cancel;

            // Position buttons
            this.closeButton.Location = new Point(this.buttonsPanel.Width - 96, 12);
            this.viewDetailsButton.Location = new Point(this.buttonsPanel.Width - 186, 12);
            this.selectButton.Location = new Point(this.buttonsPanel.Width - 276, 12);

            // Anchor buttons to right
            this.closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.viewDetailsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.selectButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.buttonsPanel.Controls.Add(this.selectButton);
            this.buttonsPanel.Controls.Add(this.viewDetailsButton);
            this.buttonsPanel.Controls.Add(this.closeButton);

            // Context menu
            this.contextMenu = new ContextMenuStrip();
            this.openMenuItem = new ToolStripMenuItem("Apri");
            this.copyCodeMenuItem = new ToolStripMenuItem("Copia Codice");
            this.copyDescriptionMenuItem = new ToolStripMenuItem("Copia Descrizione");
            this.separator1 = new ToolStripSeparator();
            this.propertiesMenuItem = new ToolStripMenuItem("Proprietà...");

            this.contextMenu.Items.AddRange(new ToolStripItem[] {
                this.openMenuItem, this.copyCodeMenuItem, this.copyDescriptionMenuItem,
                this.separator1, this.propertiesMenuItem
            });

            this.resultsListView.ContextMenuStrip = this.contextMenu;

            // Add all panels to main panel
            this.mainPanel.Controls.Add(this.resultsPanel);
            this.mainPanel.Controls.Add(this.searchPanel);
            this.mainPanel.Controls.Add(this.headerPanel);
            this.mainPanel.Controls.Add(this.buttonsPanel);

            this.Controls.Add(this.mainPanel);

            // Set accept and cancel buttons
            this.AcceptButton = this.selectButton;
            this.CancelButton = this.closeButton;
        }

        #endregion

        // Component declarations
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