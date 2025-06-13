using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;
using DistintaTecnica.Forms;

namespace DistintaTecnica
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbManager;
        private Repository repository;
        private Progetto currentProject;
        private System.Windows.Forms.Timer searchTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeApplication();
            SetupEventHandlers();
            LoadProjectList();
        }

        #region Initialization

        private void InitializeApplication()
        {
            try
            {
                dbManager = new DatabaseManager();
                repository = new Repository(dbManager);

                // Test database connection
                if (dbManager.TestConnection())
                {
                    UpdateStatusLabel("Database connesso con successo", true);
                }
                else
                {
                    UpdateStatusLabel("Errore connessione database", false);
                    connectionStatusLabel.Text = "DB: Errore";
                    connectionStatusLabel.ForeColor = System.Drawing.Color.Red;
                }

                // Initialize search timer
                searchTimer = new System.Windows.Forms.Timer();
                searchTimer.Interval = 500; // 500ms delay
                searchTimer.Tick += SearchTimer_Tick;

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'inizializzazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            // Menu events
            newProjectMenuItem.Click += NewProject_Click;
            openProjectMenuItem.Click += OpenProject_Click;
            saveProjectMenuItem.Click += SaveProject_Click;
            exitMenuItem.Click += Exit_Click;

            copyMenuItem.Click += Copy_Click;
            pasteMenuItem.Click += Paste_Click;
            duplicateMenuItem.Click += Duplicate_Click;
            findMenuItem.Click += Find_Click;

            codeGeneratorMenuItem.Click += CodeGenerator_Click;
            validationMenuItem.Click += Validation_Click;
            optionsMenuItem.Click += Options_Click;

            aboutMenuItem.Click += About_Click;

            // Toolbar events
            newToolStripButton.Click += NewProject_Click;
            openToolStripButton.Click += OpenProject_Click;
            saveToolStripButton.Click += SaveProject_Click;
            addToolStripButton.Click += AddElement_Click;
            editToolStripButton.Click += EditElement_Click;
            deleteToolStripButton.Click += DeleteElement_Click;
            searchToolStripButton.Click += Search_Click;

            // TreeView events
            projectTreeView.AfterSelect += ProjectTreeView_AfterSelect;
            projectTreeView.MouseClick += ProjectTreeView_MouseClick;

            // Search events
            searchToolStripTextBox.TextChanged += SearchTextBox_TextChanged;
            searchToolStripTextBox.KeyDown += SearchTextBox_KeyDown;

            // Tab control events
            detailsTabControl.SelectedIndexChanged += DetailsTabControl_SelectedIndexChanged;

            // Form events
            this.FormClosing += MainForm_FormClosing;
        }

        #endregion

        #region UI Updates

        private void UpdateUI()
        {
            bool hasProject = currentProject != null;
            bool hasSelection = projectTreeView.SelectedNode != null;

            // Update menu items
            saveProjectMenuItem.Enabled = hasProject;
            saveAsMenuItem.Enabled = hasProject;
            exportMenuItem.Enabled = hasProject;

            // Update toolbar buttons
            saveToolStripButton.Enabled = hasProject;
            addToolStripButton.Enabled = hasProject;
            editToolStripButton.Enabled = hasSelection;
            deleteToolStripButton.Enabled = hasSelection && CanDeleteSelectedNode();

            // Update project info
            if (hasProject)
            {
                txtCommessa.Text = currentProject.NumeroCommessa;
                txtCliente.Text = currentProject.Cliente;
                txtDisegnatore.Text = currentProject.NomeDisegnatore;
                txtRevisione.Text = currentProject.LetteraRevisioneInserimento;
            }
            else
            {
                ClearProjectInfo();
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
            statusLabel.Text = message;
            statusLabel.ForeColor = success ?
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

        private void NewProject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new ProjectForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadProjectList();
                        UpdateStatusLabel("Nuovo progetto creato con successo");
                    }
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

        private void ProjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is TreeNodeData nodeData)
            {
                LoadElementDetails(nodeData);
                UpdateUI();
            }
        }

        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = projectTreeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    projectTreeView.SelectedNode = node;
                    ShowContextMenu(e.Location);
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchTimer.Stop();
                PerformSearch();
                e.Handled = true;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void DetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (detailsTabControl.SelectedTab == listViewTabPage)
            {
                LoadListView();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cleanup resources
            searchTimer?.Dispose();
        }

        #endregion

        #region Helper Methods

        private bool CanDeleteSelectedNode()
        {
            var selectedNode = projectTreeView.SelectedNode;
            return selectedNode?.Tag is TreeNodeData nodeData && nodeData.Tipo != "PROGETTO";
        }

        private void ShowContextMenu(Point location)
        {
            // Context menu implementation will be added later
        }

        private void LoadElementDetails(TreeNodeData nodeData)
        {
            // Details loading implementation will be added later
        }

        private void LoadListView()
        {
            // List view implementation will be added later
        }

        private void PerformSearch()
        {
            string searchTerm = searchToolStripTextBox.Text;
            if (string.IsNullOrWhiteSpace(searchTerm)) return;

            try
            {
                var results = repository.SearchGlobal(searchTerm);
                // Search results handling will be implemented in a separate form
                using (var searchForm = new SearchResultsForm(results))
                {
                    searchForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore durante la ricerca: {ex.Message}", false);
            }
        }

        #endregion

        #region Add Methods Stubs

        private void AddParteMacchina(int progettoId)
        {
            // Implementation will be in separate form
        }

        private void AddSezione(int parteMacchinaId)
        {
            // Implementation will be in separate form
        }

        private void AddSottosezione(int sezioneId)
        {
            // Implementation will be in separate form
        }

        private void AddMontaggio(int sottosezioneId)
        {
            // Implementation will be in separate form
        }

        private void AddGruppo(int montaggioId)
        {
            // Implementation will be in separate form
        }

        private void EditElement(TreeNodeData nodeData)
        {
            // Implementation will be in separate form
        }

        private void DeleteElement(TreeNodeData nodeData)
        {
            // Implementation will be in repository
        }

        #endregion

        #region Menu Event Stubs

        private void Copy_Click(object sender, EventArgs e)
        {
            // Copy implementation
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            // Paste implementation
        }

        private void Duplicate_Click(object sender, EventArgs e)
        {
            // Duplicate implementation
        }

        private void Find_Click(object sender, EventArgs e)
        {
            searchToolStripTextBox.Focus();
        }

        private void CodeGenerator_Click(object sender, EventArgs e)
        {
            // Code generator form will be implemented separately
        }

        private void Validation_Click(object sender, EventArgs e)
        {
            // Validation form will be implemented separately
        }

        private void Options_Click(object sender, EventArgs e)
        {
            // Options form will be implemented separately
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Distinta Tecnica v1.0\nSistema di gestione distinte tecniche\n\n© 2024",
                          "Informazioni", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}