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
            if (disposing)
            {
                dragDropHandler?.Dispose();
                searchTimer?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.nuovoProgettoToolStripMenuItem = new ToolStripMenuItem();
            this.apriProgettoToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator = new ToolStripSeparator();
            this.salvaToolStripMenuItem = new ToolStripMenuItem();
            this.salvaComeToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.importaToolStripMenuItem = new ToolStripMenuItem();
            this.esportaToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.esciToolStripMenuItem = new ToolStripMenuItem();
            this.modificaToolStripMenuItem = new ToolStripMenuItem();
            this.copiaToolStripMenuItem = new ToolStripMenuItem();
            this.incollaToolStripMenuItem = new ToolStripMenuItem();
            this.duplicaToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.trovaToolStripMenuItem = new ToolStripMenuItem();
            this.strumentiToolStripMenuItem = new ToolStripMenuItem();
            this.generatoreCodiciToolStripMenuItem = new ToolStripMenuItem();
            this.validazioneDistintaToolStripMenuItem = new ToolStripMenuItem();
            this.opzioniToolStripMenuItem = new ToolStripMenuItem();
            this.aiutoToolStripMenuItem = new ToolStripMenuItem();
            this.informazioniToolStripMenuItem = new ToolStripMenuItem();
            this.toolStrip1 = new ToolStrip();
            this.nuovoToolStripButton = new ToolStripButton();
            this.apriToolStripButton = new ToolStripButton();
            this.salvaToolStripButton = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.aggiungiToolStripButton = new ToolStripButton();
            this.modificaToolStripButton = new ToolStripButton();
            this.eliminaToolStripButton = new ToolStripButton();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.toolStripLabel1 = new ToolStripLabel();
            this.cercaToolStripTextBox = new ToolStripTextBox();
            this.cercaToolStripButton = new ToolStripButton();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripProgressBar1 = new ToolStripProgressBar();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.splitContainer1 = new SplitContainer();
            this.projectTreeView = new TreeView();
            this.contextMenuTreeView = new ContextMenuStrip(this.components);
            this.apriToolStripMenuItem = new ToolStripMenuItem();
            this.aggiungiToolStripMenuItem = new ToolStripMenuItem();
            this.modificaToolStripMenuItem1 = new ToolStripMenuItem();
            this.eliminaToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.proprietàToolStripMenuItem = new ToolStripMenuItem();
            this.groupBoxProgetto = new GroupBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.label1 = new Label();
            this.txtCommessa = new TextBox();
            this.label2 = new Label();
            this.txtCliente = new TextBox();
            this.label3 = new Label();
            this.txtDisegnatore = new TextBox();
            this.label4 = new Label();
            this.txtRevisione = new TextBox();
            this.tabControl1 = new TabControl();
            this.tabPageDettagli = new TabPage();
            this.tabPageLista = new TabPage();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuTreeView.SuspendLayout();
            this.groupBoxProgetto.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.fileToolStripMenuItem, this.modificaToolStripMenuItem, this.strumentiToolStripMenuItem, this.aiutoToolStripMenuItem });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new Size(1225, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.nuovoProgettoToolStripMenuItem, this.apriProgettoToolStripMenuItem, this.toolStripSeparator, this.salvaToolStripMenuItem, this.salvaComeToolStripMenuItem, this.toolStripSeparator1, this.importaToolStripMenuItem, this.esportaToolStripMenuItem, this.toolStripSeparator2, this.esciToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // nuovoProgettoToolStripMenuItem
            // 
            this.nuovoProgettoToolStripMenuItem.Name = "nuovoProgettoToolStripMenuItem";
            this.nuovoProgettoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.nuovoProgettoToolStripMenuItem.Size = new Size(202, 22);
            this.nuovoProgettoToolStripMenuItem.Text = "&Nuovo Progetto";
            this.nuovoProgettoToolStripMenuItem.Click += this.NewProject_Click;
            // 
            // apriProgettoToolStripMenuItem
            // 
            this.apriProgettoToolStripMenuItem.Name = "apriProgettoToolStripMenuItem";
            this.apriProgettoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            this.apriProgettoToolStripMenuItem.Size = new Size(202, 22);
            this.apriProgettoToolStripMenuItem.Text = "&Apri Progetto";
            this.apriProgettoToolStripMenuItem.Click += this.OpenProject_Click;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new Size(199, 6);
            // 
            // salvaToolStripMenuItem
            // 
            this.salvaToolStripMenuItem.Enabled = false;
            this.salvaToolStripMenuItem.Name = "salvaToolStripMenuItem";
            this.salvaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            this.salvaToolStripMenuItem.Size = new Size(202, 22);
            this.salvaToolStripMenuItem.Text = "&Salva";
            this.salvaToolStripMenuItem.Click += this.SaveProject_Click;
            // 
            // salvaComeToolStripMenuItem
            // 
            this.salvaComeToolStripMenuItem.Enabled = false;
            this.salvaComeToolStripMenuItem.Name = "salvaComeToolStripMenuItem";
            this.salvaComeToolStripMenuItem.Size = new Size(202, 22);
            this.salvaComeToolStripMenuItem.Text = "Salva &Come...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(199, 6);
            // 
            // importaToolStripMenuItem
            // 
            this.importaToolStripMenuItem.Name = "importaToolStripMenuItem";
            this.importaToolStripMenuItem.Size = new Size(202, 22);
            this.importaToolStripMenuItem.Text = "&Importa...";
            this.importaToolStripMenuItem.Click += this.Import_Click;
            // 
            // esportaToolStripMenuItem
            // 
            this.esportaToolStripMenuItem.Enabled = false;
            this.esportaToolStripMenuItem.Name = "esportaToolStripMenuItem";
            this.esportaToolStripMenuItem.Size = new Size(202, 22);
            this.esportaToolStripMenuItem.Text = "&Esporta...";
            this.esportaToolStripMenuItem.Click += this.Export_Click;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(199, 6);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            this.esciToolStripMenuItem.Size = new Size(202, 22);
            this.esciToolStripMenuItem.Text = "E&sci";
            this.esciToolStripMenuItem.Click += this.Exit_Click;
            // 
            // modificaToolStripMenuItem
            // 
            this.modificaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.copiaToolStripMenuItem, this.incollaToolStripMenuItem, this.duplicaToolStripMenuItem, this.toolStripSeparator3, this.trovaToolStripMenuItem });
            this.modificaToolStripMenuItem.Name = "modificaToolStripMenuItem";
            this.modificaToolStripMenuItem.Size = new Size(66, 20);
            this.modificaToolStripMenuItem.Text = "&Modifica";
            // 
            // copiaToolStripMenuItem
            // 
            this.copiaToolStripMenuItem.Enabled = false;
            this.copiaToolStripMenuItem.Name = "copiaToolStripMenuItem";
            this.copiaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            this.copiaToolStripMenuItem.Size = new Size(156, 22);
            this.copiaToolStripMenuItem.Text = "&Copia";
            // 
            // incollaToolStripMenuItem
            // 
            this.incollaToolStripMenuItem.Enabled = false;
            this.incollaToolStripMenuItem.Name = "incollaToolStripMenuItem";
            this.incollaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            this.incollaToolStripMenuItem.Size = new Size(156, 22);
            this.incollaToolStripMenuItem.Text = "&Incolla";
            // 
            // duplicaToolStripMenuItem
            // 
            this.duplicaToolStripMenuItem.Enabled = false;
            this.duplicaToolStripMenuItem.Name = "duplicaToolStripMenuItem";
            this.duplicaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            this.duplicaToolStripMenuItem.Size = new Size(156, 22);
            this.duplicaToolStripMenuItem.Text = "&Duplica";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(153, 6);
            // 
            // trovaToolStripMenuItem
            // 
            this.trovaToolStripMenuItem.Name = "trovaToolStripMenuItem";
            this.trovaToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            this.trovaToolStripMenuItem.Size = new Size(156, 22);
            this.trovaToolStripMenuItem.Text = "&Trova...";
            this.trovaToolStripMenuItem.Click += this.Find_Click;
            // 
            // strumentiToolStripMenuItem
            // 
            this.strumentiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.generatoreCodiciToolStripMenuItem, this.validazioneDistintaToolStripMenuItem, this.opzioniToolStripMenuItem });
            this.strumentiToolStripMenuItem.Name = "strumentiToolStripMenuItem";
            this.strumentiToolStripMenuItem.Size = new Size(71, 20);
            this.strumentiToolStripMenuItem.Text = "&Strumenti";
            // 
            // generatoreCodiciToolStripMenuItem
            // 
            this.generatoreCodiciToolStripMenuItem.Name = "generatoreCodiciToolStripMenuItem";
            this.generatoreCodiciToolStripMenuItem.Size = new Size(185, 22);
            this.generatoreCodiciToolStripMenuItem.Text = "&Generatore Codici...";
            // 
            // validazioneDistintaToolStripMenuItem
            // 
            this.validazioneDistintaToolStripMenuItem.Name = "validazioneDistintaToolStripMenuItem";
            this.validazioneDistintaToolStripMenuItem.Size = new Size(185, 22);
            this.validazioneDistintaToolStripMenuItem.Text = "&Validazione Distinta...";
            // 
            // opzioniToolStripMenuItem
            // 
            this.opzioniToolStripMenuItem.Name = "opzioniToolStripMenuItem";
            this.opzioniToolStripMenuItem.Size = new Size(185, 22);
            this.opzioniToolStripMenuItem.Text = "&Opzioni...";
            // 
            // aiutoToolStripMenuItem
            // 
            this.aiutoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.informazioniToolStripMenuItem });
            this.aiutoToolStripMenuItem.Name = "aiutoToolStripMenuItem";
            this.aiutoToolStripMenuItem.Size = new Size(48, 20);
            this.aiutoToolStripMenuItem.Text = "&Aiuto";
            // 
            // informazioniToolStripMenuItem
            // 
            this.informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
            this.informazioniToolStripMenuItem.Size = new Size(150, 22);
            this.informazioniToolStripMenuItem.Text = "&Informazioni...";
            this.informazioniToolStripMenuItem.Click += this.About_Click;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new Size(20, 20);
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.nuovoToolStripButton, this.apriToolStripButton, this.salvaToolStripButton, this.toolStripSeparator6, this.aggiungiToolStripButton, this.modificaToolStripButton, this.eliminaToolStripButton, this.toolStripSeparator7, this.toolStripLabel1, this.cercaToolStripTextBox, this.cercaToolStripButton });
            this.toolStrip1.Location = new Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(1225, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // nuovoToolStripButton
            // 
            this.nuovoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.nuovoToolStripButton.Name = "nuovoToolStripButton";
            this.nuovoToolStripButton.Size = new Size(47, 22);
            this.nuovoToolStripButton.Text = "Nuovo";
            this.nuovoToolStripButton.Click += this.NewProject_Click;
            // 
            // apriToolStripButton
            // 
            this.apriToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.apriToolStripButton.Name = "apriToolStripButton";
            this.apriToolStripButton.Size = new Size(33, 22);
            this.apriToolStripButton.Text = "Apri";
            this.apriToolStripButton.Click += this.OpenProject_Click;
            // 
            // salvaToolStripButton
            // 
            this.salvaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.salvaToolStripButton.Enabled = false;
            this.salvaToolStripButton.Name = "salvaToolStripButton";
            this.salvaToolStripButton.Size = new Size(38, 22);
            this.salvaToolStripButton.Text = "Salva";
            this.salvaToolStripButton.Click += this.SaveProject_Click;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 25);
            // 
            // aggiungiToolStripButton
            // 
            this.aggiungiToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.aggiungiToolStripButton.Enabled = false;
            this.aggiungiToolStripButton.Name = "aggiungiToolStripButton";
            this.aggiungiToolStripButton.Size = new Size(60, 22);
            this.aggiungiToolStripButton.Text = "Aggiungi";
            this.aggiungiToolStripButton.Click += this.AddElement_Click;
            // 
            // modificaToolStripButton
            // 
            this.modificaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.modificaToolStripButton.Enabled = false;
            this.modificaToolStripButton.Name = "modificaToolStripButton";
            this.modificaToolStripButton.Size = new Size(58, 22);
            this.modificaToolStripButton.Text = "Modifica";
            this.modificaToolStripButton.Click += this.EditElement_Click;
            // 
            // eliminaToolStripButton
            // 
            this.eliminaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.eliminaToolStripButton.Enabled = false;
            this.eliminaToolStripButton.Name = "eliminaToolStripButton";
            this.eliminaToolStripButton.Size = new Size(50, 22);
            this.eliminaToolStripButton.Text = "Elimina";
            this.eliminaToolStripButton.Click += this.DeleteElement_Click;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(40, 22);
            this.toolStripLabel1.Text = "Cerca:";
            // 
            // cercaToolStripTextBox
            // 
            this.cercaToolStripTextBox.Name = "cercaToolStripTextBox";
            this.cercaToolStripTextBox.Size = new Size(176, 25);
            this.cercaToolStripTextBox.TextChanged += this.SearchTextBox_TextChanged;
            // 
            // cercaToolStripButton
            // 
            this.cercaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.cercaToolStripButton.Name = "cercaToolStripButton";
            this.cercaToolStripButton.Size = new Size(23, 22);
            this.cercaToolStripButton.Text = "🔍";
            this.cercaToolStripButton.Click += this.Search_Click;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new Size(20, 20);
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel1, this.toolStripProgressBar1, this.toolStripStatusLabel2 });
            this.statusStrip1.Location = new Point(0, 558);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new Size(1225, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(1106, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Pronto";
            this.toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new Size(88, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new Size(106, 17);
            this.toolStripStatusLabel2.Text = "DB: Non Connesso";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 49);
            this.splitContainer1.Margin = new Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.projectTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxProgetto);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new Size(1225, 509);
            this.splitContainer1.SplitterDistance = 306;
            this.splitContainer1.TabIndex = 3;
            // 
            // projectTreeView
            // 
            this.projectTreeView.ContextMenuStrip = this.contextMenuTreeView;
            this.projectTreeView.Dock = DockStyle.Fill;
            this.projectTreeView.FullRowSelect = true;
            this.projectTreeView.HideSelection = false;
            this.projectTreeView.Location = new Point(0, 90);
            this.projectTreeView.Margin = new Padding(3, 2, 3, 2);
            this.projectTreeView.Name = "projectTreeView";
            this.projectTreeView.ShowLines = false;
            this.projectTreeView.ShowPlusMinus = false;
            this.projectTreeView.Size = new Size(306, 419);
            this.projectTreeView.TabIndex = 1;
            this.projectTreeView.AfterSelect += this.ProjectTreeView_AfterSelect;
            this.projectTreeView.MouseClick += this.ProjectTreeView_MouseClick;
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.ImageScalingSize = new Size(20, 20);
            this.contextMenuTreeView.Items.AddRange(new ToolStripItem[] { this.apriToolStripMenuItem, this.aggiungiToolStripMenuItem, this.modificaToolStripMenuItem1, this.eliminaToolStripMenuItem, this.toolStripSeparator4, this.proprietàToolStripMenuItem });
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.Size = new Size(132, 120);
            this.contextMenuTreeView.Opening += this.ContextMenuTreeView_Opening;
            // 
            // apriToolStripMenuItem
            // 
            this.apriToolStripMenuItem.Name = "apriToolStripMenuItem";
            this.apriToolStripMenuItem.Size = new Size(131, 22);
            this.apriToolStripMenuItem.Text = "Apri";
            this.apriToolStripMenuItem.Click += this.OpenProject_Click;
            // 
            // aggiungiToolStripMenuItem
            // 
            this.aggiungiToolStripMenuItem.Name = "aggiungiToolStripMenuItem";
            this.aggiungiToolStripMenuItem.Size = new Size(131, 22);
            this.aggiungiToolStripMenuItem.Text = "Aggiungi";
            this.aggiungiToolStripMenuItem.Click += this.AddElement_Click;
            // 
            // modificaToolStripMenuItem1
            // 
            this.modificaToolStripMenuItem1.Name = "modificaToolStripMenuItem1";
            this.modificaToolStripMenuItem1.Size = new Size(131, 22);
            this.modificaToolStripMenuItem1.Text = "Modifica";
            this.modificaToolStripMenuItem1.Click += this.EditElement_Click;
            // 
            // eliminaToolStripMenuItem
            // 
            this.eliminaToolStripMenuItem.Name = "eliminaToolStripMenuItem";
            this.eliminaToolStripMenuItem.Size = new Size(131, 22);
            this.eliminaToolStripMenuItem.Text = "Elimina";
            this.eliminaToolStripMenuItem.Click += this.DeleteElement_Click;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(128, 6);
            // 
            // proprietàToolStripMenuItem
            // 
            this.proprietàToolStripMenuItem.Name = "proprietàToolStripMenuItem";
            this.proprietàToolStripMenuItem.Size = new Size(131, 22);
            this.proprietàToolStripMenuItem.Text = "Proprietà...";
            this.proprietàToolStripMenuItem.Click += this.Properties_Click;
            // 
            // groupBoxProgetto
            // 
            this.groupBoxProgetto.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxProgetto.Dock = DockStyle.Top;
            this.groupBoxProgetto.Location = new Point(0, 0);
            this.groupBoxProgetto.Margin = new Padding(3, 2, 3, 2);
            this.groupBoxProgetto.Name = "groupBoxProgetto";
            this.groupBoxProgetto.Padding = new Padding(3, 2, 3, 2);
            this.groupBoxProgetto.Size = new Size(306, 90);
            this.groupBoxProgetto.TabIndex = 0;
            this.groupBoxProgetto.TabStop = false;
            this.groupBoxProgetto.Text = "Informazioni Progetto";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCommessa, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCliente, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtDisegnatore, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtRevisione, 1, 3);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(3, 18);
            this.tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new Size(300, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new Size(86, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° Commessa:";
            // 
            // txtCommessa
            // 
            this.txtCommessa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCommessa.Location = new Point(95, 2);
            this.txtCommessa.Margin = new Padding(3, 2, 3, 2);
            this.txtCommessa.Name = "txtCommessa";
            this.txtCommessa.ReadOnly = true;
            this.txtCommessa.Size = new Size(202, 23);
            this.txtCommessa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtCliente.Location = new Point(95, 19);
            this.txtCliente.Margin = new Padding(3, 2, 3, 2);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new Size(202, 23);
            this.txtCliente.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(73, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Disegnatore:";
            // 
            // txtDisegnatore
            // 
            this.txtDisegnatore.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtDisegnatore.Location = new Point(95, 36);
            this.txtDisegnatore.Margin = new Padding(3, 2, 3, 2);
            this.txtDisegnatore.Name = "txtDisegnatore";
            this.txtDisegnatore.ReadOnly = true;
            this.txtDisegnatore.Size = new Size(202, 23);
            this.txtDisegnatore.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 53);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Revisione:";
            // 
            // txtRevisione
            // 
            this.txtRevisione.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtRevisione.Location = new Point(95, 53);
            this.txtRevisione.Margin = new Padding(3, 2, 3, 2);
            this.txtRevisione.Name = "txtRevisione";
            this.txtRevisione.ReadOnly = true;
            this.txtRevisione.Size = new Size(202, 23);
            this.txtRevisione.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDettagli);
            this.tabControl1.Controls.Add(this.tabPageLista);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Margin = new Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(915, 509);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += this.DetailsTabControl_SelectedIndexChanged;
            // 
            // tabPageDettagli
            // 
            this.tabPageDettagli.Location = new Point(4, 24);
            this.tabPageDettagli.Margin = new Padding(3, 2, 3, 2);
            this.tabPageDettagli.Name = "tabPageDettagli";
            this.tabPageDettagli.Padding = new Padding(3, 2, 3, 2);
            this.tabPageDettagli.Size = new Size(907, 481);
            this.tabPageDettagli.TabIndex = 0;
            this.tabPageDettagli.Text = "Dettagli";
            this.tabPageDettagli.UseVisualStyleBackColor = true;
            // 
            // tabPageLista
            // 
            this.tabPageLista.Location = new Point(4, 24);
            this.tabPageLista.Margin = new Padding(3, 2, 3, 2);
            this.tabPageLista.Name = "tabPageLista";
            this.tabPageLista.Padding = new Padding(3, 2, 3, 2);
            this.tabPageLista.Size = new Size(907, 481);
            this.tabPageLista.TabIndex = 1;
            this.tabPageLista.Text = "Vista Lista";
            this.tabPageLista.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1225, 580);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new Padding(3, 2, 3, 2);
            this.MinimumSize = new Size(1052, 535);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Distinta Tecnica - Gestionale";
            this.WindowState = FormWindowState.Maximized;
            this.FormClosing += this.MainForm_FormClosing;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuTreeView.ResumeLayout(false);
            this.groupBoxProgetto.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuovoProgettoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apriProgettoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem salvaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvaComeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esportaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incollaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem trovaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem strumentiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatoreCodiciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validazioneDistintaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opzioniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aiutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informazioniToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton nuovoToolStripButton;
        private System.Windows.Forms.ToolStripButton apriToolStripButton;
        private System.Windows.Forms.ToolStripButton salvaToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton aggiungiToolStripButton;
        private System.Windows.Forms.ToolStripButton modificaToolStripButton;
        private System.Windows.Forms.ToolStripButton eliminaToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox cercaToolStripTextBox;
        private System.Windows.Forms.ToolStripButton cercaToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView projectTreeView;
        private System.Windows.Forms.GroupBox groupBoxProgetto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommessa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDisegnatore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRevisione;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDettagli;
        private System.Windows.Forms.TabPage tabPageLista;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem apriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aggiungiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eliminaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem proprietàToolStripMenuItem;
    }
}