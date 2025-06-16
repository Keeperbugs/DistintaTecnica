using DistintaTecnica.Data;
using DistintaTecnica.Forms;
using DistintaTecnica.Models;
using DistintaTecnica.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Controls;
using DistintaTecnica.Export;
using System.IO;
using DistintaTecnica.Business;

namespace DistintaTecnica
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbManager;
        private Repository repository;
        private Progetto currentProject;
        private System.Windows.Forms.Timer searchTimer;
        private DeleteOperations deleteOperations;
        private ElementDetailsControl detailsControl;
        private ProjectListViewControl listViewControl;
        private ExportManager exportManager;
        private DragDropHandler dragDropHandler;

        public MainForm()
        {
            InitializeComponent();
            InitializeApplication();
            LoadProjectList();
        }

        #region Initialization

        private void InitializeApplication()
        {
            try
            {
                dbManager = new DatabaseManager();
                repository = new Repository(dbManager);
                deleteOperations = new DeleteOperations(repository);
                exportManager = new ExportManager(repository);

                // Test database connection
                if (dbManager.TestConnection())
                {
                    UpdateStatusLabel("Database connesso con successo", true);
                    toolStripStatusLabel2.Text = "DB: Connesso";
                    toolStripStatusLabel2.ForeColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    UpdateStatusLabel("Errore connessione database", false);
                    toolStripStatusLabel2.Text = "DB: Errore";
                    toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
                }

                // Initialize search timer
                searchTimer = new System.Windows.Forms.Timer();
                searchTimer.Interval = 500;
                searchTimer.Tick += SearchTimer_Tick;

                UpdateUI();
                InitializeTabControls();
                InitializeDragDrop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'inizializzazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void InitializeTabControls()
        {
            try
            {
                // Inizializza controllo dettagli
                detailsControl = new ElementDetailsControl();
                detailsControl.Dock = DockStyle.Fill;
                tabPageDettagli.Controls.Add(detailsControl);

                // Inizializza controllo lista
                listViewControl = new ProjectListViewControl();
                listViewControl.Dock = DockStyle.Fill;
                listViewControl.SetRepository(repository);
                listViewControl.ElementSelected += ListViewControl_ElementSelected;
                tabPageLista.Controls.Add(listViewControl);

                UpdateStatusLabel("Controlli tab inizializzati", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'inizializzazione dei controlli tab: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeDragDrop()
        {
            try
            {
                dragDropHandler = new DragDropHandler(projectTreeView);
                dragDropHandler.ElementMoved += DragDropHandler_ElementMoved;

                UpdateStatusLabel("Drag & Drop abilitato per il TreeView", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'inizializzazione del Drag & Drop: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region UI Updates

        private void UpdateUI()
        {
            bool hasProject = currentProject != null;
            bool hasSelection = projectTreeView.SelectedNode != null;

            // Update menu items
            salvaToolStripMenuItem.Enabled = hasProject;
            salvaComeToolStripMenuItem.Enabled = hasProject;
            esportaToolStripMenuItem.Enabled = hasProject;

            // Update toolbar buttons
            salvaToolStripButton.Enabled = hasProject;
            aggiungiToolStripButton.Enabled = hasProject;
            modificaToolStripButton.Enabled = hasSelection;
            eliminaToolStripButton.Enabled = hasSelection && CanDeleteSelectedNode();

            // Update project info
            if (hasProject)
            {
                UpdateProjectInfo();
            }
            else
            {
                ClearProjectInfo();
            }
        }

        private void UpdateProjectInfo()
        {
            if (currentProject != null)
            {
                txtCommessa.Text = currentProject.NumeroCommessa;
                txtCliente.Text = currentProject.Cliente;
                txtDisegnatore.Text = currentProject.NomeDisegnatore;
                txtRevisione.Text = currentProject.LetteraRevisioneInserimento;
            }
        }

        private void ClearProjectInfo()
        {
            txtCommessa.Clear();
            txtCliente.Clear();
            txtDisegnatore.Clear();
            txtRevisione.Clear();
        }

        private void UpdateStatusLabel(string message, bool success = true)
        {
            toolStripStatusLabel1.Text = message;
            toolStripStatusLabel1.ForeColor = success ?
                System.Drawing.Color.Black : System.Drawing.Color.Red;
        }

        #endregion

        #region Project Management

        private void LoadProjectList()
        {
            try
            {
                projectTreeView.Nodes.Clear();

                var progetti = repository.GetAllProgetti();

                foreach (var progetto in progetti)
                {
                    var projectNode = new TreeNode($"{progetto.NumeroCommessa} - {progetto.Cliente}")
                    {
                        Tag = new TreeNodeData("PROGETTO", progetto.Id, progetto),
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };

                    projectTreeView.Nodes.Add(projectNode);
                }

                UpdateStatusLabel($"Caricati {progetti.Count} progetti");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento dei progetti: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProjectDetails(int projectId)
        {
            try
            {
                currentProject = repository.GetProgettoById(projectId);

                if (currentProject != null)
                {
                    LoadProjectStructure();
                    UpdateUI();
                    UpdateStatusLabel($"Progetto {currentProject.NumeroCommessa} caricato");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento del progetto: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProjectStructure()
        {
            if (currentProject == null) return;

            try
            {
                var selectedNode = projectTreeView.SelectedNode;
                if (selectedNode?.Tag is TreeNodeData nodeData && nodeData.Tipo == "PROGETTO")
                {
                    selectedNode.Nodes.Clear();

                    var partiMacchina = repository.GetPartiMacchinaByProgetto(currentProject.Id);

                    foreach (var parte in partiMacchina)
                    {
                        var parteNode = new TreeNode($"{parte.CodiceParteMacchina} - {parte.Descrizione}")
                        {
                            Tag = new TreeNodeData("PARTE_MACCHINA", parte.Id, parte),
                            ImageIndex = 1,
                            SelectedImageIndex = 1
                        };

                        LoadSezioni(parteNode, parte.Id);
                        selectedNode.Nodes.Add(parteNode);
                    }

                    selectedNode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento della struttura: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSezioni(TreeNode parentNode, int parteMacchinaId)
        {
            try
            {
                var sezioni = repository.GetSezioniByParteMacchina(parteMacchinaId);

                foreach (var sezione in sezioni)
                {
                    var sezioneNode = new TreeNode($"{sezione.CodiceSezione} - {sezione.Descrizione} (Q.tà: {sezione.Quantita})")
                    {
                        Tag = new TreeNodeData("SEZIONE", sezione.Id, sezione),
                        ImageIndex = 2,
                        SelectedImageIndex = 2
                    };

                    LoadSottosezioni(sezioneNode, sezione.Id);
                    parentNode.Nodes.Add(sezioneNode);
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento sezioni: {ex.Message}", false);
            }
        }

        private void LoadSottosezioni(TreeNode parentNode, int sezioneId)
        {
            try
            {
                var sottosezioni = repository.GetSottosezioniBySezione(sezioneId);

                foreach (var sottosezione in sottosezioni)
                {
                    var sottosezioneNode = new TreeNode($"{sottosezione.CodiceSottosezione} - {sottosezione.Descrizione} (Q.tà: {sottosezione.Quantita})")
                    {
                        Tag = new TreeNodeData("SOTTOSEZIONE", sottosezione.Id, sottosezione),
                        ImageIndex = 3,
                        SelectedImageIndex = 3
                    };

                    LoadMontaggi(sottosezioneNode, sottosezione.Id);
                    parentNode.Nodes.Add(sottosezioneNode);
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento sottosezioni: {ex.Message}", false);
            }
        }

        private void LoadMontaggi(TreeNode parentNode, int sottosezioneId)
        {
            try
            {
                var montaggi = repository.GetMontaggiBySottosezione(sottosezioneId);

                foreach (var montaggio in montaggi)
                {
                    var montaggioNode = new TreeNode($"{montaggio.CodiceMontaggio} - {montaggio.Descrizione} (Q.tà: {montaggio.Quantita})")
                    {
                        Tag = new TreeNodeData("MONTAGGIO", montaggio.Id, montaggio),
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    };

                    LoadGruppi(montaggioNode, montaggio.Id);
                    parentNode.Nodes.Add(montaggioNode);
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento montaggi: {ex.Message}", false);
            }
        }

        private void LoadGruppi(TreeNode parentNode, int montaggioId)
        {
            try
            {
                var gruppi = repository.GetGruppiByMontaggio(montaggioId);

                foreach (var gruppo in gruppi)
                {
                    var gruppoNode = new TreeNode($"{gruppo.CodiceGruppo} ({gruppo.TipoGruppo}) - {gruppo.Descrizione} (Q.tà: {gruppo.Quantita})")
                    {
                        Tag = new TreeNodeData("GRUPPO", gruppo.Id, gruppo),
                        ImageIndex = 5,
                        SelectedImageIndex = 5
                    };

                    parentNode.Nodes.Add(gruppoNode);
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento gruppi: {ex.Message}", false);
            }
        }

        #endregion

        #region Event Handlers

        private void LibreriaCodici_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostra il form di gestione libreria codici
                ShowLibreriaCodiciManager();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'apertura della libreria codici: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Errore libreria codici: {ex.Message}", false);
            }
        }

        private void NewProject_Click(object sender, EventArgs e)
        {
            try
            {
                var result = ProjectForm.ShowCreateDialog(this);
                if (result != null)
                {
                    LoadProjectList();
                    UpdateStatusLabel("Nuovo progetto creato con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante la creazione del progetto: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (projectTreeView.SelectedNode?.Tag is TreeNodeData nodeData && nodeData.Tipo == "PROGETTO")
            {
                LoadProjectDetails(nodeData.Id);
            }
            else
            {
                MessageBox.Show("Seleziona un progetto dalla lista",
                              "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveProject_Click(object sender, EventArgs e)
        {
            if (currentProject != null)
            {
                try
                {
                    repository.UpdateProgetto(currentProject);
                    UpdateStatusLabel("Progetto salvato con successo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante il salvataggio: {ex.Message}",
                                  "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddElement_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedNode = projectTreeView.SelectedNode;
                if (selectedNode?.Tag is TreeNodeData nodeData)
                {
                    switch (nodeData.Tipo)
                    {
                        case "PROGETTO":
                            AddParteMacchina(nodeData.Id);
                            break;
                        case "PARTE_MACCHINA":
                            AddSezione(nodeData.Id);
                            break;
                        case "SEZIONE":
                            AddSottosezione(nodeData.Id);
                            break;
                        case "SOTTOSEZIONE":
                            AddMontaggio(nodeData.Id);
                            break;
                        case "MONTAGGIO":
                            AddGruppo(nodeData.Id);
                            break;
                        default:
                            MessageBox.Show("Impossibile aggiungere elementi a questo livello",
                                          "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditElement_Click(object sender, EventArgs e)
        {
            var selectedNode = projectTreeView.SelectedNode;
            if (selectedNode?.Tag is TreeNodeData nodeData)
            {
                EditElement(nodeData);
            }
        }

        private void DeleteElement_Click(object sender, EventArgs e)
        {
            var selectedNode = projectTreeView.SelectedNode;
            if (selectedNode?.Tag is TreeNodeData nodeData)
            {
                if (MessageBox.Show($"Sei sicuro di voler eliminare l'elemento selezionato?",
                                  "Conferma eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DeleteElement(nodeData);
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void Find_Click(object sender, EventArgs e)
        {
            cercaToolStripTextBox.Focus();
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Distinta Tecnica v1.0\nSistema di gestione distinte tecniche\n\n© 2024",
                          "Informazioni", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Properties_Click(object sender, EventArgs e)
        {
            // Mostra proprietà dell'elemento selezionato
            var selectedNode = projectTreeView.SelectedNode;
            if (selectedNode?.Tag is TreeNodeData nodeData)
            {
                ShowElementProperties(nodeData);
            }
        }

        private void ProjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is TreeNodeData nodeData)
            {
                LoadElementDetails(nodeData);
                UpdateUI();
            }
        }

        // Aggiorna il metodo ProjectTreeView_MouseClick esistente:
        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = projectTreeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    projectTreeView.SelectedNode = node;

                    // Configura il context menu in base al tipo di nodo
                    if (node.Tag is TreeNodeData nodeData)
                    {
                        ConfigureContextMenu(nodeData);
                    }
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void DetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPageLista)
                {
                    LoadListView();
                }
                else if (tabControl1.SelectedTab == tabPageDettagli)
                {
                    // Aggiorna i dettagli con l'elemento attualmente selezionato
                    if (projectTreeView.SelectedNode?.Tag is TreeNodeData nodeData)
                    {
                        detailsControl?.ShowElementDetails(nodeData);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore cambio tab: {ex.Message}", false);
            }
        }

        // Metodo per aggiornare entrambi i controlli quando il progetto cambia
        private void RefreshAllViews()
        {
            try
            {
                // Aggiorna la vista lista se è il tab attivo
                if (tabControl1.SelectedTab == tabPageLista)
                {
                    LoadListView();
                }

                // Aggiorna i dettagli se c'è un elemento selezionato
                if (projectTreeView.SelectedNode?.Tag is TreeNodeData nodeData)
                {
                    detailsControl?.ShowElementDetails(nodeData);
                }

                UpdateStatusLabel("Tutte le viste aggiornate");
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore aggiornamento viste: {ex.Message}", false);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Dispose delle risorse personalizzate
                dragDropHandler?.Dispose();
                searchTimer?.Dispose();
            }
            catch (Exception ex)
            {
                // Log dell'errore ma non bloccare la chiusura
                System.Diagnostics.Debug.WriteLine($"Errore durante cleanup: {ex.Message}");
            }
        }

        #endregion

        #region Add Methods Implementation

        private void AddParteMacchina(int progettoId)
        {
            try
            {
                var result = ElementForm.ShowCreateDialog(ElementType.ParteMacchina, progettoId, null, this);
                if (result != null)
                {
                    LoadProjectStructure();
                    UpdateStatusLabel("Parte macchina aggiunta con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta della parte macchina: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSezione(int parteMacchinaId)
        {
            try
            {
                string parentCode = GetParteMacchinaCode(parteMacchinaId);
                var result = ElementForm.ShowCreateDialog(ElementType.Sezione, parteMacchinaId, parentCode, this);
                if (result != null)
                {
                    RefreshTreeNode(projectTreeView.SelectedNode);
                    UpdateStatusLabel("Sezione aggiunta con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta della sezione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSottosezione(int sezioneId)
        {
            try
            {
                string parentCode = GetSezioneCode(sezioneId);
                var result = ElementForm.ShowCreateDialog(ElementType.Sottosezione, sezioneId, parentCode, this);
                if (result != null)
                {
                    RefreshTreeNode(projectTreeView.SelectedNode);
                    UpdateStatusLabel("Sottosezione aggiunta con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta della sottosezione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddMontaggio(int sottosezioneId)
        {
            try
            {
                var result = ElementForm.ShowCreateDialog(ElementType.Montaggio, sottosezioneId, null, this);
                if (result != null)
                {
                    RefreshTreeNode(projectTreeView.SelectedNode);
                    UpdateStatusLabel("Montaggio aggiunto con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta del montaggio: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddGruppo(int montaggioId)
        {
            try
            {
                var result = ElementForm.ShowCreateDialog(ElementType.Gruppo, montaggioId, null, this);
                if (result != null)
                {
                    RefreshTreeNode(projectTreeView.SelectedNode);
                    UpdateStatusLabel("Gruppo aggiunto con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'aggiunta del gruppo: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditElement(TreeNodeData nodeData)
        {
            try
            {
                object result = null;

                switch (nodeData.Tipo.ToUpper())
                {
                    case "PARTE_MACCHINA":
                        result = ElementForm.ShowEditDialog(ElementType.ParteMacchina, nodeData.Data, this);
                        break;
                    case "SEZIONE":
                        result = ElementForm.ShowEditDialog(ElementType.Sezione, nodeData.Data, this);
                        break;
                    case "SOTTOSEZIONE":
                        result = ElementForm.ShowEditDialog(ElementType.Sottosezione, nodeData.Data, this);
                        break;
                    case "MONTAGGIO":
                        result = ElementForm.ShowEditDialog(ElementType.Montaggio, nodeData.Data, this);
                        break;
                    case "GRUPPO":
                        result = ElementForm.ShowEditDialog(ElementType.Gruppo, nodeData.Data, this);
                        break;
                    case "PROGETTO":
                        var progetto = ProjectForm.ShowEditDialog((Progetto)nodeData.Data, this);
                        if (progetto != null)
                        {
                            currentProject = progetto;
                            LoadProjectList();
                            UpdateUI();
                        }
                        return;
                }

                if (result != null)
                {
                    RefreshTreeNode(projectTreeView.SelectedNode);
                    UpdateStatusLabel("Elemento modificato con successo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante la modifica: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteElement(TreeNodeData nodeData)
        {
            try
            {
                // Verifica che l'elemento non sia un progetto (i progetti si eliminano diversamente)
                if (nodeData.Tipo.ToUpper() == "PROGETTO")
                {
                    MessageBox.Show(
                        "Per eliminare un progetto, utilizzare il menu File > Elimina Progetto",
                        "Operazione non permessa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Esegui l'eliminazione
                bool success = deleteOperations.DeleteElement(nodeData, this);

                if (success)
                {
                    // Rimuovi il nodo dal TreeView
                    var selectedNode = projectTreeView.SelectedNode;
                    if (selectedNode != null)
                    {
                        // Se il nodo eliminato ha un parent, refresh del parent
                        var parentNode = selectedNode.Parent;
                        selectedNode.Remove();

                        if (parentNode != null)
                        {
                            projectTreeView.SelectedNode = parentNode;
                            RefreshTreeNode(parentNode);
                        }
                        else
                        {
                            // Se era un nodo root, ricarica tutta la lista progetti
                            LoadProjectList();
                        }
                    }

                    UpdateStatusLabel($"{nodeData.Tipo} eliminato con successo");
                    UpdateUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Errore durante l'eliminazione: {ex.Message}",
                    "Errore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                UpdateStatusLabel($"Errore durante l'eliminazione: {ex.Message}", false);
            }
        }

        #endregion

        #region Helper Methods

        private void ShowLibreriaCodiciManager()
        {
            try
            {
                // Per ora mostra un form di test del CodeSelector
                // In futuro questo aprirà il form di gestione completa della libreria

                using (var testForm = new TestCodeSelectorForm())
                {
                    testForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form temporaneo per testare il CodeSelector:
        public partial class TestCodeSelectorForm : Form
        {
            private ComboBox cmbTipoElemento;
            private TextBox txtCommessa;
            private Button btnTest;
            private Label lblRisultato;

            public TestCodeSelectorForm()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.Size = new Size(500, 300);
                this.Text = "Test Code Selector";
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                // ComboBox tipo elemento
                var lblTipo = new Label();
                lblTipo.Text = "Tipo Elemento:";
                lblTipo.Location = new Point(20, 20);
                lblTipo.Size = new Size(100, 23);
                this.Controls.Add(lblTipo);

                cmbTipoElemento = new ComboBox();
                cmbTipoElemento.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTipoElemento.Location = new Point(130, 20);
                cmbTipoElemento.Size = new Size(200, 23);
                cmbTipoElemento.Items.AddRange(new string[] {
            "ParteMacchina",
            "Sezione",
            "Sottosezione",
            "Montaggio",
            "Gruppo"
        });
                cmbTipoElemento.SelectedIndex = 0;
                this.Controls.Add(cmbTipoElemento);

                // TextBox commessa
                var lblCommessa = new Label();
                lblCommessa.Text = "N° Commessa:";
                lblCommessa.Location = new Point(20, 60);
                lblCommessa.Size = new Size(100, 23);
                this.Controls.Add(lblCommessa);

                txtCommessa = new TextBox();
                txtCommessa.Location = new Point(130, 60);
                txtCommessa.Size = new Size(200, 23);
                txtCommessa.Text = "2024-001"; // Default
                this.Controls.Add(txtCommessa);

                // Button test
                btnTest = new Button();
                btnTest.Text = "Apri Code Selector";
                btnTest.Location = new Point(130, 100);
                btnTest.Size = new Size(150, 35);
                btnTest.BackColor = Color.FromArgb(59, 130, 246);
                btnTest.ForeColor = Color.White;
                btnTest.FlatStyle = FlatStyle.Flat;
                btnTest.Click += BtnTest_Click;
                this.Controls.Add(btnTest);

                // Label risultato
                lblRisultato = new Label();
                lblRisultato.Location = new Point(20, 150);
                lblRisultato.Size = new Size(450, 100);
                lblRisultato.Text = "Clicca il pulsante per testare il CodeSelector";
                lblRisultato.BorderStyle = BorderStyle.FixedSingle;
                lblRisultato.BackColor = Color.FromArgb(247, 250, 252);
                this.Controls.Add(lblRisultato);
            }

            private void BtnTest_Click(object sender, EventArgs e)
            {
                try
                {
                    // Converti la selezione in enum
                    if (!Enum.TryParse<TipoElemento>(cmbTipoElemento.SelectedItem.ToString(), out TipoElemento tipo))
                    {
                        MessageBox.Show("Seleziona un tipo elemento valido", "Errore");
                        return;
                    }

                    string commessa = txtCommessa.Text.Trim();
                    if (string.IsNullOrEmpty(commessa))
                    {
                        MessageBox.Show("Inserisci un numero commessa", "Errore");
                        return;
                    }

                    // Apri il CodeSelector
                    var result = CodeSelectorForm.SelectCode(tipo, commessa, this);

                    // Mostra il risultato
                    if (result.Success)
                    {
                        lblRisultato.Text = $"✅ SELEZIONE COMPLETATA\n\n" +
                                          $"Codice: {result.Codice}\n" +
                                          $"Descrizione: {result.Descrizione}\n" +
                                          $"Nuovo: {(result.IsNewCode ? "Sì" : "No")}\n" +
                                          $"Tipo: {tipo}";
                        lblRisultato.ForeColor = Color.FromArgb(34, 197, 94);
                    }
                    else
                    {
                        lblRisultato.Text = "❌ Selezione annullata dall'utente";
                        lblRisultato.ForeColor = Color.FromArgb(239, 68, 68);
                    }
                }
                catch (Exception ex)
                {
                    lblRisultato.Text = $"❌ ERRORE: {ex.Message}";
                    lblRisultato.ForeColor = Color.FromArgb(239, 68, 68);
                }
            }
        }

        private bool CanDeleteSelectedNode()
        {
            var selectedNode = projectTreeView.SelectedNode;
            if (selectedNode?.Tag is TreeNodeData nodeData)
            {
                // I progetti non si eliminano dal context menu/toolbar
                if (nodeData.Tipo.ToUpper() == "PROGETTO")
                    return false;

                // Verifica se l'elemento può essere eliminato dal database
                try
                {
                    // Per ora permettiamo sempre l'eliminazione
                    // Il controllo dettagliato viene fatto nella DeleteOperations
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        private void LoadElementDetails(TreeNodeData nodeData)
        {
            try
            {
                // Aggiorna il controllo dettagli
                detailsControl?.ShowElementDetails(nodeData);

                // Se il tab corrente è "Vista Lista", aggiorna anche quella
                if (tabControl1.SelectedTab == tabPageLista)
                {
                    // Evidenzia l'elemento nella lista se presente
                    HighlightElementInList(nodeData);
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento dettagli: {ex.Message}", false);
            }
        }

        private void LoadListView()
        {
            try
            {
                if (currentProject != null)
                {
                    listViewControl?.LoadProject(currentProject);
                    UpdateStatusLabel("Vista lista aggiornata");
                }
                else
                {
                    listViewControl?.LoadProject(null);
                    UpdateStatusLabel("Nessun progetto selezionato");
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento vista lista: {ex.Message}", false);
            }
        }

        // Nuovo metodo per evidenziare un elemento nella lista
        private void HighlightElementInList(TreeNodeData nodeData)
        {
            // Implementazione per evidenziare l'elemento selezionato nella vista lista
            // Questo sarà utile per sincronizzare TreeView e ListView
        }

        // Event handler per quando un elemento viene selezionato nella vista lista
        private void ListViewControl_ElementSelected(object sender, TreeNodeData nodeData)
        {
            try
            {
                // Trova e seleziona il nodo corrispondente nel TreeView
                SelectNodeInTreeView(nodeData);

                // Aggiorna i dettagli
                detailsControl?.ShowElementDetails(nodeData);

                UpdateStatusLabel($"Selezionato: {GetElementDisplayText(nodeData)}");
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore selezione elemento: {ex.Message}", false);
            }
        }

        // Metodo per selezionare un nodo nel TreeView basandosi sui dati
        private void SelectNodeInTreeView(TreeNodeData targetNodeData)
        {
            try
            {
                foreach (TreeNode projectNode in projectTreeView.Nodes)
                {
                    var foundNode = FindNodeRecursive(projectNode, targetNodeData);
                    if (foundNode != null)
                    {
                        projectTreeView.SelectedNode = foundNode;
                        foundNode.EnsureVisible();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Errore nella ricerca del nodo, non critico
                System.Diagnostics.Debug.WriteLine($"Errore ricerca nodo: {ex.Message}");
            }
        }

        // Metodo ricorsivo per trovare un nodo nel TreeView
        private TreeNode FindNodeRecursive(TreeNode parentNode, TreeNodeData targetData)
        {
            // Controlla il nodo corrente
            if (parentNode.Tag is TreeNodeData nodeData &&
                nodeData.Tipo == targetData.Tipo &&
                nodeData.Id == targetData.Id)
            {
                return parentNode;
            }

            // Cerca nei nodi figli
            foreach (TreeNode childNode in parentNode.Nodes)
            {
                var foundNode = FindNodeRecursive(childNode, targetData);
                if (foundNode != null)
                    return foundNode;
            }

            return null;
        }

        // Metodo helper per ottenere testo di visualizzazione di un elemento
        private string GetElementDisplayText(TreeNodeData nodeData)
        {
            switch (nodeData.Data)
            {
                case Progetto p:
                    return $"Progetto {p.NumeroCommessa}";
                case ParteMacchina pm:
                    return $"Parte {pm.CodiceParteMacchina}";
                case Sezione s:
                    return $"Sezione {s.CodiceSezione}";
                case Sottosezione ss:
                    return $"Sottosezione {ss.CodiceSottosezione}";
                case Montaggio m:
                    return $"Montaggio {m.CodiceMontaggio}";
                case Gruppo g:
                    return $"Gruppo {g.CodiceGruppo}";
                default:
                    return "Elemento sconosciuto";
            }
        }

        private void PerformSearch()
        {
            string searchTerm = cercaToolStripTextBox.Text;
            if (string.IsNullOrWhiteSpace(searchTerm)) return;

            try
            {
                var results = repository.SearchGlobal(searchTerm);
                var selectedResult = SearchResultsForm.ShowDialog(results, this);
                if (selectedResult != null)
                {
                    // Navigate to selected result
                    UpdateStatusLabel($"Trovato: {selectedResult.Codice}");
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore durante la ricerca: {ex.Message}", false);
            }
        }

        private void ShowElementProperties(TreeNodeData nodeData)
        {
            string details = $"Tipo: {nodeData.Tipo}\nID: {nodeData.Id}";

            switch (nodeData.Data)
            {
                case Progetto p:
                    details += $"\nCommessa: {p.NumeroCommessa}\nCliente: {p.Cliente}";
                    break;
                case ParteMacchina pm:
                    details += $"\nCodice: {pm.CodiceParteMacchina}\nDescrizione: {pm.Descrizione}";
                    break;
                case Sezione s:
                    details += $"\nCodice: {s.CodiceSezione}\nDescrizione: {s.Descrizione}\nQuantità: {s.Quantita}";
                    break;
                case Sottosezione ss:
                    details += $"\nCodice: {ss.CodiceSottosezione}\nDescrizione: {ss.Descrizione}\nQuantità: {ss.Quantita}";
                    break;
                case Montaggio m:
                    details += $"\nCodice: {m.CodiceMontaggio}\nDescrizione: {m.Descrizione}\nQuantità: {m.Quantita}";
                    break;
                case Gruppo g:
                    details += $"\nCodice: {g.CodiceGruppo}\nTipo: {g.TipoGruppo}\nDescrizione: {g.Descrizione}\nQuantità: {g.Quantita}";
                    break;
            }

            MessageBox.Show(details, "Proprietà Elemento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetParteMacchinaCode(int parteMacchinaId)
        {
            try
            {
                var partiMacchina = repository.GetPartiMacchinaByProgetto(currentProject?.Id ?? 0);
                var parte = partiMacchina.FirstOrDefault(p => p.Id == parteMacchinaId);
                return parte?.CodiceParteMacchina ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetSezioneCode(int sezioneId)
        {
            try
            {
                var selectedNode = projectTreeView.SelectedNode;
                if (selectedNode?.Tag is TreeNodeData nodeData && nodeData.Tipo == "SEZIONE")
                {
                    var sezione = (Sezione)nodeData.Data;
                    return sezione.CodiceSezione;
                }

                return FindSezioneCodeInProject(sezioneId);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string FindSezioneCodeInProject(int sezioneId)
        {
            try
            {
                if (currentProject == null) return string.Empty;

                var partiMacchina = repository.GetPartiMacchinaByProgetto(currentProject.Id);
                foreach (var parte in partiMacchina)
                {
                    var sezioni = repository.GetSezioniByParteMacchina(parte.Id);
                    var sezione = sezioni.FirstOrDefault(s => s.Id == sezioneId);
                    if (sezione != null)
                    {
                        return sezione.CodiceSezione;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        // Aggiorna il metodo RefreshTreeNode per aggiornare anche gli altri controlli
        private void RefreshTreeNode(TreeNode node)
        {
            if (node?.Tag is TreeNodeData nodeData)
            {
                switch (nodeData.Tipo.ToUpper())
                {
                    case "PROGETTO":
                        LoadProjectStructure();
                        break;
                    case "PARTE_MACCHINA":
                        node.Nodes.Clear();
                        LoadSezioni(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "SEZIONE":
                        node.Nodes.Clear();
                        LoadSottosezioni(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "SOTTOSEZIONE":
                        node.Nodes.Clear();
                        LoadMontaggi(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "MONTAGGIO":
                        node.Nodes.Clear();
                        LoadGruppi(node, nodeData.Id);
                        node.Expand();
                        break;
                }

                // Aggiorna anche gli altri controlli
                RefreshAllViews();
            }
        }

        #endregion
        // Aggiungi questi metodi al MainForm.cs per gestire meglio il context menu

        #region Context Menu Management

        /// <summary>
        /// Configura il context menu in base al tipo di elemento selezionato
        /// </summary>
        private void ConfigureContextMenu(TreeNodeData nodeData)
        {
            if (contextMenuTreeView == null) return;

            // Reset di tutti gli item (solo ToolStripMenuItem, non i separator)
            foreach (ToolStripItem item in contextMenuTreeView.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = true;
                    menuItem.Visible = true;
                }
            }

            switch (nodeData.Tipo.ToUpper())
            {
                case "PROGETTO":
                    ConfigureProjectContextMenu();
                    break;
                case "PARTE_MACCHINA":
                    ConfigureParteMacchinaContextMenu();
                    break;
                case "SEZIONE":
                    ConfigureSezioneContextMenu();
                    break;
                case "SOTTOSEZIONE":
                    ConfigureSottosezioneContextMenu();
                    break;
                case "MONTAGGIO":
                    ConfigureMontaggioContextMenu();
                    break;
                case "GRUPPO":
                    ConfigureGruppoContextMenu();
                    break;
            }
        }

        private void ConfigureProjectContextMenu()
        {
            // Per i progetti: Apri, Modifica, Elimina (se vuoto), Proprietà
            apriToolStripMenuItem.Text = "Apri Progetto";
            apriToolStripMenuItem.Enabled = true;

            aggiungiToolStripMenuItem.Text = "Aggiungi Parte Macchina";
            aggiungiToolStripMenuItem.Enabled = true;

            modificaToolStripMenuItem1.Text = "Modifica Progetto";
            modificaToolStripMenuItem1.Enabled = true;

            // I progetti non si eliminano dal context menu
            eliminaToolStripMenuItem.Enabled = false;
            eliminaToolStripMenuItem.Text = "Elimina (non disponibile)";
        }

        private void ConfigureParteMacchinaContextMenu()
        {
            apriToolStripMenuItem.Text = "Espandi";
            apriToolStripMenuItem.Enabled = true;

            aggiungiToolStripMenuItem.Text = "Aggiungi Sezione";
            aggiungiToolStripMenuItem.Enabled = true;

            modificaToolStripMenuItem1.Text = "Modifica Parte";
            modificaToolStripMenuItem1.Enabled = true;

            eliminaToolStripMenuItem.Text = "Elimina Parte Macchina";
            eliminaToolStripMenuItem.Enabled = true;
        }

        private void ConfigureSezioneContextMenu()
        {
            apriToolStripMenuItem.Text = "Espandi";
            apriToolStripMenuItem.Enabled = true;

            aggiungiToolStripMenuItem.Text = "Aggiungi Sottosezione";
            aggiungiToolStripMenuItem.Enabled = true;

            modificaToolStripMenuItem1.Text = "Modifica Sezione";
            modificaToolStripMenuItem1.Enabled = true;

            eliminaToolStripMenuItem.Text = "Elimina Sezione";
            eliminaToolStripMenuItem.Enabled = true;
        }

        private void ConfigureSottosezioneContextMenu()
        {
            apriToolStripMenuItem.Text = "Espandi";
            apriToolStripMenuItem.Enabled = true;

            aggiungiToolStripMenuItem.Text = "Aggiungi Montaggio";
            aggiungiToolStripMenuItem.Enabled = true;

            modificaToolStripMenuItem1.Text = "Modifica Sottosezione";
            modificaToolStripMenuItem1.Enabled = true;

            eliminaToolStripMenuItem.Text = "Elimina Sottosezione";
            eliminaToolStripMenuItem.Enabled = true;
        }

        private void ConfigureMontaggioContextMenu()
        {
            apriToolStripMenuItem.Text = "Espandi";
            apriToolStripMenuItem.Enabled = true;

            aggiungiToolStripMenuItem.Text = "Aggiungi Gruppo";
            aggiungiToolStripMenuItem.Enabled = true;

            modificaToolStripMenuItem1.Text = "Modifica Montaggio";
            modificaToolStripMenuItem1.Enabled = true;

            eliminaToolStripMenuItem.Text = "Elimina Montaggio";
            eliminaToolStripMenuItem.Enabled = true;
        }

        private void ConfigureGruppoContextMenu()
        {
            apriToolStripMenuItem.Text = "Visualizza";
            apriToolStripMenuItem.Enabled = true;

            // I gruppi non possono avere sotto-elementi
            aggiungiToolStripMenuItem.Enabled = false;
            aggiungiToolStripMenuItem.Text = "Aggiungi (non disponibile)";

            modificaToolStripMenuItem1.Text = "Modifica Gruppo";
            modificaToolStripMenuItem1.Enabled = true;

            eliminaToolStripMenuItem.Text = "Elimina Gruppo";
            eliminaToolStripMenuItem.Enabled = true;
        }

        #endregion

        // Aggiungi questi event handler per gestire meglio il context menu:
        private void ContextMenuTreeView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Verifica che ci sia un nodo selezionato
            if (projectTreeView.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }

            // Configura il menu in base al nodo selezionato
            if (projectTreeView.SelectedNode.Tag is TreeNodeData nodeData)
            {
                ConfigureContextMenu(nodeData);
            }
        }

        // Aggiungi questo event handler per il menu Esporta
        private void Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentProject == null)
                {
                    MessageBox.Show("Nessun progetto aperto per l'esportazione.",
                                  "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                exportManager.ShowExportDialog(currentProject, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'esportazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Errore esportazione: {ex.Message}", false);
            }
        }

        // Nel costruttore o InitializeComponent, aggiungi:
        // contextMenuTreeView.Opening += ContextMenuTreeView_Opening;

        // Implementa anche una funzionalità di Import base
        private void Import_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "La funzionalità di importazione è in fase di sviluppo.\n\n" +
                    "Attualmente supporta l'importazione di:\n" +
                    "• File CSV con struttura predefinita\n" +
                    "• Backup di database SQLite\n\n" +
                    "Vuoi procedere con l'importazione di un file CSV?",
                    "Importazione",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    ImportFromCSV();
                }
                else if (result == DialogResult.No)
                {
                    ImportDatabaseBackup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'importazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromCSV()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "File CSV (*.csv)|*.csv|Tutti i file (*.*)|*.*";
                openDialog.Title = "Seleziona file CSV da importare";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Per ora mostra solo un messaggio informativo
                        var preview = System.IO.File.ReadAllLines(openDialog.FileName)
                            .Take(5)
                            .Aggregate((a, b) => a + "\n" + b);

                        MessageBox.Show(
                            $"File selezionato: {openDialog.FileName}\n\n" +
                            "Prime righe del file:\n" +
                            preview + "\n\n" +
                            "Funzionalità di importazione CSV da implementare completamente.",
                            "Anteprima Importazione",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore nella lettura del file CSV: {ex.Message}",
                                      "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ImportDatabaseBackup()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Database SQLite (*.db)|*.db|Backup files (*.bak)|*.bak|Tutti i file (*.*)|*.*";
                openDialog.Title = "Seleziona database di backup da importare";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show(
                        $"Vuoi importare il database:\n{openDialog.FileName}\n\n" +
                        "ATTENZIONE: Questa operazione sostituirà tutti i dati attuali!\n\n" +
                        "È consigliabile fare un backup prima di procedere.\n" +
                        "Vuoi continuare?",
                        "Conferma Importazione Database",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Crea backup del database attuale
                            CreateDatabaseBackup();

                            // Importa il nuovo database (implementazione da completare)
                            MessageBox.Show(
                                "Funzionalità di importazione database da implementare.\n\n" +
                                "È stato creato un backup del database corrente.",
                                "Importazione Database",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Errore durante l'importazione del database: {ex.Message}",
                                          "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void CreateDatabaseBackup()
        {
            try
            {
                string backupPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Backups"
                );

                if (!Directory.Exists(backupPath))
                    Directory.CreateDirectory(backupPath);

                string backupFileName = $"DistintaTecnica_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                string backupFullPath = Path.Combine(backupPath, backupFileName);

                string currentDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DistintaTecnica.db");

                if (File.Exists(currentDbPath))
                {
                    File.Copy(currentDbPath, backupFullPath);
                    UpdateStatusLabel($"Backup creato: {backupFileName}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore nella creazione del backup: {ex.Message}");
            }
        }

        // Aggiungi anche una funzionalità per creare backup manuali
        private void CreateBackup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Database SQLite (*.db)|*.db|Backup files (*.bak)|*.bak";
                    saveDialog.FileName = $"DistintaTecnica_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                    saveDialog.Title = "Salva backup database";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string currentDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DistintaTecnica.db");

                        if (File.Exists(currentDbPath))
                        {
                            File.Copy(currentDbPath, saveDialog.FileName, true);

                            MessageBox.Show($"Backup salvato con successo in:\n{saveDialog.FileName}",
                                          "Backup Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            UpdateStatusLabel("Backup database creato con successo");
                        }
                        else
                        {
                            MessageBox.Show("Database non trovato per la creazione del backup.",
                                          "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante la creazione del backup: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Errore backup: {ex.Message}", false);
            }
        }

        // Migliora anche la gestione del menu File per abilitare/disabilitare export
        private void UpdateFileMenu()
        {
            bool hasProject = currentProject != null;

            // Abilita/disabilita le opzioni del menu File
            salvaToolStripMenuItem.Enabled = hasProject;
            salvaComeToolStripMenuItem.Enabled = hasProject;
            esportaToolStripMenuItem.Enabled = hasProject;

            // Aggiorna il testo del menu in base al progetto corrente
            if (hasProject)
            {
                this.Text = $"Distinta Tecnica - Gestionale - {currentProject.NumeroCommessa}";
            }
            else
            {
                this.Text = "Distinta Tecnica - Gestionale";
            }
        }

        // Event handler per quando un elemento viene spostato
        private void DragDropHandler_ElementMoved(object sender, DragDropEventArgs e)
        {
            try
            {
                // Per ora mostra solo un messaggio informativo
                // In futuro qui implementeremo l'aggiornamento del database

                UpdateStatusLabel($"Spostato: {GetElementDisplayText(e.MovedElement)} -> {GetElementDisplayText(e.NewParent)}");

                // Potresti voler implementare qui l'aggiornamento del database
                // UpdateElementParentInDatabase(e.MovedElement, e.NewParent);

                // Aggiorna le altre viste
                RefreshAllViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel gestire lo spostamento: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel($"Errore spostamento: {ex.Message}", false);
            }
        }

        // Placeholder per l'aggiornamento del database dopo il drag & drop
        private void UpdateElementParentInDatabase(TreeNodeData movedElement, TreeNodeData newParent)
        {
            // TODO: Implementare l'aggiornamento del database
            // Questo richiederà nuovi metodi nel Repository per aggiornare le foreign key

            MessageBox.Show(
                "Aggiornamento database dopo Drag & Drop non ancora implementato.\n\n" +
                "Lo spostamento è stato effettuato solo nell'interfaccia.\n" +
                "Per renderlo permanente, ricarica il progetto.",
                "Funzionalità da completare",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}