namespace DistintaTecnica
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            // Form properties
            this.SuspendLayout();

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.BackColor = System.Drawing.Color.FromArgb(240, 245, 251);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.Text = "Distinta Tecnica - Gestionale";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Initialize components
            this.InitializeMenuStrip();
            this.InitializeToolStrip();
            this.InitializeStatusStrip();
            this.InitializeMainLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeMenuStrip()
        {
            this.menuStrip = new MenuStrip();
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(45, 55, 72);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.menuStrip.Padding = new Padding(8, 4, 0, 4);

            // File Menu
            this.fileMenuItem = new ToolStripMenuItem("&File");
            this.newProjectMenuItem = new ToolStripMenuItem("&Nuovo Progetto...", null, null, Keys.Control | Keys.N);
            this.openProjectMenuItem = new ToolStripMenuItem("&Apri Progetto...", null, null, Keys.Control | Keys.O);
            this.saveProjectMenuItem = new ToolStripMenuItem("&Salva", null, null, Keys.Control | Keys.S);
            this.saveAsMenuItem = new ToolStripMenuItem("Salva &Come...");
            this.separator1 = new ToolStripSeparator();
            this.importMenuItem = new ToolStripMenuItem("&Importa...");
            this.exportMenuItem = new ToolStripMenuItem("&Esporta...");
            this.separator2 = new ToolStripSeparator();
            this.exitMenuItem = new ToolStripMenuItem("&Esci", null, null, Keys.Alt | Keys.F4);

            this.fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.newProjectMenuItem, this.openProjectMenuItem, this.saveProjectMenuItem,
                this.saveAsMenuItem, this.separator1, this.importMenuItem, this.exportMenuItem,
                this.separator2, this.exitMenuItem
            });

            // Edit Menu
            this.editMenuItem = new ToolStripMenuItem("&Modifica");
            this.copyMenuItem = new ToolStripMenuItem("&Copia", null, null, Keys.Control | Keys.C);
            this.pasteMenuItem = new ToolStripMenuItem("&Incolla", null, null, Keys.Control | Keys.V);
            this.duplicateMenuItem = new ToolStripMenuItem("&Duplica", null, null, Keys.Control | Keys.D);
            this.separator3 = new ToolStripSeparator();
            this.findMenuItem = new ToolStripMenuItem("&Trova...", null, null, Keys.Control | Keys.F);

            this.editMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.copyMenuItem, this.pasteMenuItem, this.duplicateMenuItem,
                this.separator3, this.findMenuItem
            });

            // Tools Menu
            this.toolsMenuItem = new ToolStripMenuItem("&Strumenti");
            this.codeGeneratorMenuItem = new ToolStripMenuItem("&Generatore Codici...");
            this.validationMenuItem = new ToolStripMenuItem("&Validazione Distinta...");
            this.optionsMenuItem = new ToolStripMenuItem("&Opzioni...");

            this.toolsMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.codeGeneratorMenuItem, this.validationMenuItem, this.optionsMenuItem
            });

            // Help Menu
            this.helpMenuItem = new ToolStripMenuItem("&Aiuto");
            this.aboutMenuItem = new ToolStripMenuItem("&Informazioni...");

            this.helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.aboutMenuItem
            });

            this.menuStrip.Items.AddRange(new ToolStripItem[] {
                this.fileMenuItem, this.editMenuItem, this.toolsMenuItem, this.helpMenuItem
            });

            this.MainMenuStrip = this.menuStrip;
            this.Controls.Add(this.menuStrip);
        }

        private void InitializeToolStrip()
        {
            this.toolStrip = new ToolStrip();
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(74, 85, 104);
            this.toolStrip.ForeColor = System.Drawing.Color.White;
            this.toolStrip.ImageScalingSize = new Size(24, 24);
            this.toolStrip.Padding = new Padding(8, 4, 8, 4);

            // Toolbar buttons
            this.newToolStripButton = new ToolStripButton("Nuovo");
            this.newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.newToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.openToolStripButton = new ToolStripButton("Apri");
            this.openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.openToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.saveToolStripButton = new ToolStripButton("Salva");
            this.saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.saveToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.toolStripSeparator1 = new ToolStripSeparator();

            this.addToolStripButton = new ToolStripButton("Aggiungi");
            this.addToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.addToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.editToolStripButton = new ToolStripButton("Modifica");
            this.editToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.editToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.deleteToolStripButton = new ToolStripButton("Elimina");
            this.deleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.deleteToolStripButton.TextImageRelation = TextImageRelation.ImageBeforeText;

            this.toolStripSeparator2 = new ToolStripSeparator();

            // Search box
            this.searchToolStripLabel = new ToolStripLabel("Cerca:");
            this.searchToolStripTextBox = new ToolStripTextBox();
            this.searchToolStripTextBox.Size = new Size(200, 27);
            this.searchToolStripTextBox.BorderStyle = BorderStyle.FixedSingle;

            this.searchToolStripButton = new ToolStripButton("🔍");

            this.toolStrip.Items.AddRange(new ToolStripItem[] {
                this.newToolStripButton, this.openToolStripButton, this.saveToolStripButton,
                this.toolStripSeparator1, this.addToolStripButton, this.editToolStripButton,
                this.deleteToolStripButton, this.toolStripSeparator2, this.searchToolStripLabel,
                this.searchToolStripTextBox, this.searchToolStripButton
            });

            this.Controls.Add(this.toolStrip);
        }

        private void InitializeStatusStrip()
        {
            this.statusStrip = new StatusStrip();
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(45, 55, 72);
            this.statusStrip.ForeColor = System.Drawing.Color.White;

            this.statusLabel = new ToolStripStatusLabel("Pronto");
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            this.progressBar = new ToolStripProgressBar();
            this.progressBar.Size = new Size(150, 18);
            this.progressBar.Visible = false;

            this.connectionStatusLabel = new ToolStripStatusLabel("DB: Connesso");
            this.connectionStatusLabel.ForeColor = System.Drawing.Color.LightGreen;

            this.statusStrip.Items.AddRange(new ToolStripItem[] {
                this.statusLabel, this.progressBar, this.connectionStatusLabel
            });

            this.Controls.Add(this.statusStrip);
        }

        private void InitializeMainLayout()
        {
            // Main container panel
            this.mainPanel = new Panel();
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.mainPanel.Padding = new Padding(8);

            // Split container for main layout
            this.mainSplitContainer = new SplitContainer();
            this.mainSplitContainer.Dock = DockStyle.Fill;
            this.mainSplitContainer.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.mainSplitContainer.SplitterWidth = 8;
            this.mainSplitContainer.SplitterDistance = 350;
            this.mainSplitContainer.Orientation = Orientation.Vertical;

            // Left panel for project tree and info
            this.leftPanel = new Panel();
            this.leftPanel.Dock = DockStyle.Fill;
            this.leftPanel.BackColor = System.Drawing.Color.White;
            this.leftPanel.Padding = new Padding(8);

            // Project info group
            this.projectInfoGroupBox = new GroupBox();
            this.projectInfoGroupBox.Text = "Informazioni Progetto";
            this.projectInfoGroupBox.Dock = DockStyle.Top;
            this.projectInfoGroupBox.Height = 120;
            this.projectInfoGroupBox.BackColor = System.Drawing.Color.White;
            this.projectInfoGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            this.InitializeProjectInfoControls();

            // Tree view for project structure
            this.projectTreeView = new TreeView();
            this.projectTreeView.Dock = DockStyle.Fill;
            this.projectTreeView.BackColor = System.Drawing.Color.White;
            this.projectTreeView.BorderStyle = BorderStyle.FixedSingle;
            this.projectTreeView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.projectTreeView.ShowLines = true;
            this.projectTreeView.ShowPlusMinus = true;
            this.projectTreeView.ShowRootLines = true;
            this.projectTreeView.HideSelection = false;
            this.projectTreeView.FullRowSelect = true;

            this.leftPanel.Controls.Add(this.projectTreeView);
            this.leftPanel.Controls.Add(this.projectInfoGroupBox);

            // Right panel for details
            this.rightPanel = new Panel();
            this.rightPanel.Dock = DockStyle.Fill;
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.Padding = new Padding(8);

            // Tab control for different views
            this.detailsTabControl = new TabControl();
            this.detailsTabControl.Dock = DockStyle.Fill;
            this.detailsTabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.detailsTabControl.Appearance = TabAppearance.FlatButtons;

            // Details tab
            this.detailsTabPage = new TabPage("Dettagli");
            this.detailsTabPage.BackColor = System.Drawing.Color.White;
            this.detailsTabPage.Padding = new Padding(8);

            // List view tab  
            this.listViewTabPage = new TabPage("Vista Lista");
            this.listViewTabPage.BackColor = System.Drawing.Color.White;
            this.listViewTabPage.Padding = new Padding(8);

            this.detailsTabControl.TabPages.Add(this.detailsTabPage);
            this.detailsTabControl.TabPages.Add(this.listViewTabPage);

            this.rightPanel.Controls.Add(this.detailsTabControl);

            // Add panels to split container
            this.mainSplitContainer.Panel1.Controls.Add(this.leftPanel);
            this.mainSplitContainer.Panel2.Controls.Add(this.rightPanel);

            this.mainPanel.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainPanel);
        }

        private void InitializeProjectInfoControls()
        {
            // Project info layout
            this.projectInfoTableLayout = new TableLayoutPanel();
            this.projectInfoTableLayout.Dock = DockStyle.Fill;
            this.projectInfoTableLayout.ColumnCount = 4;
            this.projectInfoTableLayout.RowCount = 2;
            this.projectInfoTableLayout.Padding = new Padding(8);

            this.projectInfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.projectInfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.projectInfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.projectInfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.projectInfoTableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.projectInfoTableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Labels and textboxes
            this.lblCommessa = new Label();
            this.lblCommessa.Text = "N° Commessa:";
            this.lblCommessa.AutoSize = true;
            this.lblCommessa.Anchor = AnchorStyles.Left;
            this.lblCommessa.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.txtCommessa = new TextBox();
            this.txtCommessa.ReadOnly = true;
            this.txtCommessa.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.txtCommessa.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.lblCliente = new Label();
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.AutoSize = true;
            this.lblCliente.Anchor = AnchorStyles.Left;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.txtCliente = new TextBox();
            this.txtCliente.ReadOnly = true;
            this.txtCliente.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.txtCliente.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.lblDisegnatore = new Label();
            this.lblDisegnatore.Text = "Disegnatore:";
            this.lblDisegnatore.AutoSize = true;
            this.lblDisegnatore.Anchor = AnchorStyles.Left;
            this.lblDisegnatore.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.txtDisegnatore = new TextBox();
            this.txtDisegnatore.ReadOnly = true;
            this.txtDisegnatore.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.txtDisegnatore.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.lblRevisione = new Label();
            this.lblRevisione.Text = "Revisione:";
            this.lblRevisione.AutoSize = true;
            this.lblRevisione.Anchor = AnchorStyles.Left;
            this.lblRevisione.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.txtRevisione = new TextBox();
            this.txtRevisione.ReadOnly = true;
            this.txtRevisione.BackColor = System.Drawing.Color.FromArgb(247, 250, 252);
            this.txtRevisione.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // Add controls to table layout
            this.projectInfoTableLayout.Controls.Add(this.lblCommessa, 0, 0);
            this.projectInfoTableLayout.Controls.Add(this.txtCommessa, 1, 0);
            this.projectInfoTableLayout.Controls.Add(this.lblCliente, 2, 0);
            this.projectInfoTableLayout.Controls.Add(this.txtCliente, 3, 0);
            this.projectInfoTableLayout.Controls.Add(this.lblDisegnatore, 0, 1);
            this.projectInfoTableLayout.Controls.Add(this.txtDisegnatore, 1, 1);
            this.projectInfoTableLayout.Controls.Add(this.lblRevisione, 2, 1);
            this.projectInfoTableLayout.Controls.Add(this.txtRevisione, 3, 1);

            this.projectInfoGroupBox.Controls.Add(this.projectInfoTableLayout);
        }

        #endregion

        // Component declarations
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem newProjectMenuItem;
        private ToolStripMenuItem openProjectMenuItem;
        private ToolStripMenuItem saveProjectMenuItem;
        private ToolStripMenuItem saveAsMenuItem;
        private ToolStripSeparator separator1;
        private ToolStripMenuItem importMenuItem;
        private ToolStripMenuItem exportMenuItem;
        private ToolStripSeparator separator2;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem editMenuItem;
        private ToolStripMenuItem copyMenuItem;
        private ToolStripMenuItem pasteMenuItem;
        private ToolStripMenuItem duplicateMenuItem;
        private ToolStripSeparator separator3;
        private ToolStripMenuItem findMenuItem;
        private ToolStripMenuItem toolsMenuItem;
        private ToolStripMenuItem codeGeneratorMenuItem;
        private ToolStripMenuItem validationMenuItem;
        private ToolStripMenuItem optionsMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;

        private ToolStrip toolStrip;
        private ToolStripButton newToolStripButton;
        private ToolStripButton openToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton addToolStripButton;
        private ToolStripButton editToolStripButton;
        private ToolStripButton deleteToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel searchToolStripLabel;
        private ToolStripTextBox searchToolStripTextBox;
        private ToolStripButton searchToolStripButton;

        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar progressBar;
        private ToolStripStatusLabel connectionStatusLabel;

        private Panel mainPanel;
        private SplitContainer mainSplitContainer;
        private Panel leftPanel;
        private Panel rightPanel;

        private GroupBox projectInfoGroupBox;
        private TableLayoutPanel projectInfoTableLayout;
        private Label lblCommessa;
        private TextBox txtCommessa;
        private Label lblCliente;
        private TextBox txtCliente;
        private Label lblDisegnatore;
        private TextBox txtDisegnatore;
        private Label lblRevisione;
        private TextBox txtRevisione;

        private TreeView projectTreeView;
        private TabControl detailsTabControl;
        private TabPage detailsTabPage;
        private TabPage listViewTabPage;
    }
}