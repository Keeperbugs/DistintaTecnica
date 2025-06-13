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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'inizializzazione: {ex.Message}",
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

        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = projectTreeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    projectTreeView.SelectedNode = node;
                    // Il context menu è già associato al TreeView nel designer
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
            if (tabControl1.SelectedTab == tabPageLista)
            {
                LoadListView();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Dispose();
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
                // TODO: Implement delete functionality
                MessageBox.Show("Funzionalità di eliminazione da implementare",
                              "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'eliminazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Methods

        private bool CanDeleteSelectedNode()
        {
            var selectedNode = projectTreeView.SelectedNode;
            return selectedNode?.Tag is TreeNodeData nodeData && nodeData.Tipo != "PROGETTO";
        }

        private void LoadElementDetails(TreeNodeData nodeData)
        {
            // TODO: Load element details in the details tab
        }

        private void LoadListView()
        {
            // TODO: Load list view with project structure
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
                        LoadSezioni(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "SEZIONE":
                        LoadSottosezioni(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "SOTTOSEZIONE":
                        LoadMontaggi(node, nodeData.Id);
                        node.Expand();
                        break;
                    case "MONTAGGIO":
                        LoadGruppi(node, nodeData.Id);
                        node.Expand();
                        break;
                }
            }
        }

        #endregion
    }
}