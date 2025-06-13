using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Controls
{
    /// <summary>
    /// Controllo per visualizzare la struttura del progetto in formato lista
    /// </summary>
    public partial class ProjectListViewControl : UserControl
    {
        private Repository repository;
        private Progetto currentProject;
        private ListView mainListView;
        private ToolStrip toolStrip;
        private ToolStripComboBox filterComboBox;
        private ToolStripTextBox searchTextBox;
        private ToolStripButton refreshButton;
        private ToolStripButton exportButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public event EventHandler<TreeNodeData> ElementSelected;

        public ProjectListViewControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Proprietà del controllo principale
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;

            // ToolStrip superiore
            toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.BackColor = Color.FromArgb(248, 250, 252);

            // Filtro per tipo
            var filterLabel = new ToolStripLabel("Filtro:");
            filterComboBox = new ToolStripComboBox();
            filterComboBox.Size = new Size(150, 25);
            filterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterComboBox.Items.AddRange(new string[] {
                "Tutti gli elementi",
                "Solo Parti Macchina",
                "Solo Sezioni",
                "Solo Sottosezioni",
                "Solo Montaggi",
                "Solo Gruppi"
            });
            filterComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndexChanged += FilterComboBox_SelectedIndexChanged;

            // Ricerca
            var searchLabel = new ToolStripLabel("Cerca:");
            searchTextBox = new ToolStripTextBox();
            searchTextBox.Size = new Size(200, 25);
            searchTextBox.TextChanged += SearchTextBox_TextChanged;

            // Pulsanti
            refreshButton = new ToolStripButton("Aggiorna");
            refreshButton.Click += RefreshButton_Click;

            exportButton = new ToolStripButton("Esporta Lista");
            exportButton.Click += ExportButton_Click;

            toolStrip.Items.AddRange(new ToolStripItem[] {
                filterLabel, filterComboBox,
                new ToolStripSeparator(),
                searchLabel, searchTextBox,
                new ToolStripSeparator(),
                refreshButton, exportButton
            });

            // ListView principale
            mainListView = new ListView();
            mainListView.Dock = DockStyle.Fill;
            mainListView.View = View.Details;
            mainListView.FullRowSelect = true;
            mainListView.GridLines = true;
            mainListView.MultiSelect = false;
            mainListView.HideSelection = false;
            mainListView.Font = new Font("Segoe UI", 9F);
            mainListView.BackColor = Color.White;

            // Colonne del ListView
            mainListView.Columns.Add("Tipo", 120);
            mainListView.Columns.Add("Codice", 150);
            mainListView.Columns.Add("Descrizione", 300);
            mainListView.Columns.Add("Quantità", 80);
            mainListView.Columns.Add("Revisione", 80);
            mainListView.Columns.Add("Stato", 80);
            mainListView.Columns.Add("Livello", 60);
            mainListView.Columns.Add("Percorso", 250);

            // Eventi ListView
            mainListView.DoubleClick += MainListView_DoubleClick;
            mainListView.SelectedIndexChanged += MainListView_SelectedIndexChanged;

            // StatusStrip
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.FromArgb(248, 250, 252);
            statusLabel = new ToolStripStatusLabel("Nessun progetto caricato");
            statusStrip.Items.Add(statusLabel);

            // Aggiungi controlli
            this.Controls.Add(mainListView);
            this.Controls.Add(toolStrip);
            this.Controls.Add(statusStrip);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Imposta il repository per accedere ai dati
        /// </summary>
        public void SetRepository(Repository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Carica la struttura del progetto
        /// </summary>
        public void LoadProject(Progetto project)
        {
            currentProject = project;
            if (project == null)
            {
                mainListView.Items.Clear();
                statusLabel.Text = "Nessun progetto caricato";
                return;
            }

            LoadProjectStructure();
        }

        private void LoadProjectStructure()
        {
            if (currentProject == null || repository == null)
                return;

            try
            {
                mainListView.BeginUpdate();
                mainListView.Items.Clear();

                var allElements = new List<ProjectElement>();

                // Carica tutte le parti macchina
                var partiMacchina = repository.GetPartiMacchinaByProgetto(currentProject.Id);
                foreach (var parte in partiMacchina)
                {
                    allElements.Add(new ProjectElement
                    {
                        Tipo = "PARTE_MACCHINA",
                        Id = parte.Id,
                        Codice = parte.CodiceParteMacchina,
                        Descrizione = parte.Descrizione,
                        Quantita = 1,
                        Revisione = parte.RevisioneInserimento,
                        Stato = parte.Stato,
                        Livello = 1,
                        Percorso = currentProject.NumeroCommessa,
                        Data = parte
                    });

                    // Carica sezioni per questa parte
                    LoadSezioniForParte(allElements, parte);
                }

                // Applica filtri e mostra elementi
                ApplyFiltersAndDisplay(allElements);

                statusLabel.Text = $"Caricati {allElements.Count} elementi dal progetto {currentProject.NumeroCommessa}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Errore durante il caricamento";
            }
            finally
            {
                mainListView.EndUpdate();
            }
        }

        private void LoadSezioniForParte(List<ProjectElement> elements, ParteMacchina parte)
        {
            var sezioni = repository.GetSezioniByParteMacchina(parte.Id);
            foreach (var sezione in sezioni)
            {
                elements.Add(new ProjectElement
                {
                    Tipo = "SEZIONE",
                    Id = sezione.Id,
                    Codice = sezione.CodiceSezione,
                    Descrizione = sezione.Descrizione,
                    Quantita = sezione.Quantita,
                    Revisione = sezione.RevisioneInserimento,
                    Stato = sezione.Stato,
                    Livello = 2,
                    Percorso = $"{currentProject.NumeroCommessa} > {parte.CodiceParteMacchina}",
                    Data = sezione
                });

                LoadSottosezioniForSezione(elements, sezione, parte);
            }
        }

        private void LoadSottosezioniForSezione(List<ProjectElement> elements, Sezione sezione, ParteMacchina parte)
        {
            var sottosezioni = repository.GetSottosezioniBySezione(sezione.Id);
            foreach (var sottosezione in sottosezioni)
            {
                elements.Add(new ProjectElement
                {
                    Tipo = "SOTTOSEZIONE",
                    Id = sottosezione.Id,
                    Codice = sottosezione.CodiceSottosezione,
                    Descrizione = sottosezione.Descrizione,
                    Quantita = sottosezione.Quantita,
                    Revisione = sottosezione.RevisioneInserimento,
                    Stato = sottosezione.Stato,
                    Livello = 3,
                    Percorso = $"{currentProject.NumeroCommessa} > {parte.CodiceParteMacchina} > {sezione.CodiceSezione}",
                    Data = sottosezione
                });

                LoadMontaggiForSottosezione(elements, sottosezione, sezione, parte);
            }
        }

        private void LoadMontaggiForSottosezione(List<ProjectElement> elements, Sottosezione sottosezione,
                                               Sezione sezione, ParteMacchina parte)
        {
            var montaggi = repository.GetMontaggiBySottosezione(sottosezione.Id);
            foreach (var montaggio in montaggi)
            {
                elements.Add(new ProjectElement
                {
                    Tipo = "MONTAGGIO",
                    Id = montaggio.Id,
                    Codice = montaggio.CodiceMontaggio,
                    Descrizione = montaggio.Descrizione,
                    Quantita = montaggio.Quantita,
                    Revisione = montaggio.RevisioneInserimento,
                    Stato = montaggio.Stato,
                    Livello = 4,
                    Percorso = $"{currentProject.NumeroCommessa} > {parte.CodiceParteMacchina} > {sezione.CodiceSezione} > {sottosezione.CodiceSottosezione}",
                    Data = montaggio
                });

                LoadGruppiForMontaggio(elements, montaggio, sottosezione, sezione, parte);
            }
        }

        private void LoadGruppiForMontaggio(List<ProjectElement> elements, Montaggio montaggio,
                                          Sottosezione sottosezione, Sezione sezione, ParteMacchina parte)
        {
            var gruppi = repository.GetGruppiByMontaggio(montaggio.Id);
            foreach (var gruppo in gruppi)
            {
                elements.Add(new ProjectElement
                {
                    Tipo = "GRUPPO",
                    Id = gruppo.Id,
                    Codice = gruppo.CodiceGruppo,
                    Descrizione = gruppo.Descrizione,
                    Quantita = gruppo.Quantita,
                    Revisione = gruppo.RevisioneInserimento,
                    Stato = gruppo.Stato,
                    Livello = 5,
                    Percorso = $"{currentProject.NumeroCommessa} > {parte.CodiceParteMacchina} > {sezione.CodiceSezione} > {sottosezione.CodiceSottosezione} > {montaggio.CodiceMontaggio}",
                    Data = gruppo,
                    TipoGruppo = gruppo.TipoGruppo
                });
            }
        }

        private void ApplyFiltersAndDisplay(List<ProjectElement> elements)
        {
            var filteredElements = elements.AsEnumerable();

            // Applica filtro tipo
            string selectedFilter = filterComboBox.SelectedItem?.ToString() ?? "Tutti gli elementi";
            if (selectedFilter != "Tutti gli elementi")
            {
                string filterType = selectedFilter.Replace("Solo ", "").Replace(" ", "_").ToUpper();
                if (filterType == "PARTI_MACCHINA") filterType = "PARTE_MACCHINA";
                filteredElements = filteredElements.Where(e => e.Tipo == filterType);
            }

            // Applica filtro ricerca
            string searchText = searchTextBox.Text?.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredElements = filteredElements.Where(e =>
                    e.Codice.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    e.Descrizione.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Ordina per livello e codice
            filteredElements = filteredElements.OrderBy(e => e.Livello).ThenBy(e => e.Codice);

            // Popola ListView
            foreach (var element in filteredElements)
            {
                var item = new ListViewItem(element.Tipo);
                item.SubItems.Add(element.Codice);
                item.SubItems.Add(element.Descrizione);
                item.SubItems.Add(element.Quantita.ToString());
                item.SubItems.Add(element.Revisione);
                item.SubItems.Add(element.Stato);
                item.SubItems.Add(element.Livello.ToString());
                item.SubItems.Add(element.Percorso);

                // Colora la riga in base al tipo
                item.BackColor = GetColorForType(element.Tipo);
                item.Tag = element;

                mainListView.Items.Add(item);
            }

            // Aggiorna conteggio
            int totalCount = elements.Count;
            int filteredCount = filteredElements.Count();

            if (totalCount == filteredCount)
                statusLabel.Text = $"{totalCount} elementi visualizzati";
            else
                statusLabel.Text = $"{filteredCount} di {totalCount} elementi visualizzati";
        }

        private Color GetColorForType(string tipo)
        {
            return tipo switch
            {
                "PARTE_MACCHINA" => Color.FromArgb(240, 253, 244),
                "SEZIONE" => Color.FromArgb(255, 251, 235),
                "SOTTOSEZIONE" => Color.FromArgb(252, 231, 243),
                "MONTAGGIO" => Color.FromArgb(245, 243, 255),
                "GRUPPO" => Color.FromArgb(254, 242, 242),
                _ => Color.White
            };
        }

        #region Event Handlers

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProjectStructure();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (currentProject != null)
            {
                LoadProjectStructure();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadProjectStructure();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        private void MainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                var selectedElement = (ProjectElement)mainListView.SelectedItems[0].Tag;
                var nodeData = new TreeNodeData(selectedElement.Tipo, selectedElement.Id, selectedElement.Data);
                ElementSelected?.Invoke(this, nodeData);
            }
        }

        private void MainListView_DoubleClick(object sender, EventArgs e)
        {
            if (mainListView.SelectedItems.Count > 0)
            {
                var selectedElement = (ProjectElement)mainListView.SelectedItems[0].Tag;
                var nodeData = new TreeNodeData(selectedElement.Tipo, selectedElement.Id, selectedElement.Data);

                // Potresti voler aprire un form di modifica qui
                MessageBox.Show($"Apertura dettagli per: {selectedElement.Codice}",
                              "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void ExportToCSV()
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "File CSV (*.csv)|*.csv";
                    saveDialog.FileName = $"Distinta_{currentProject?.NumeroCommessa}_{DateTime.Now:yyyyMMdd}.csv";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var lines = new List<string>();

                        // Header
                        lines.Add("Tipo;Codice;Descrizione;Quantità;Revisione;Stato;Livello;Percorso");

                        // Dati
                        foreach (ListViewItem item in mainListView.Items)
                        {
                            var values = new string[item.SubItems.Count];
                            for (int i = 0; i < item.SubItems.Count; i++)
                            {
                                values[i] = item.SubItems[i].Text;
                            }
                            lines.Add(string.Join(";", values));
                        }

                        System.IO.File.WriteAllLines(saveDialog.FileName, lines);

                        MessageBox.Show("Esportazione completata con successo!",
                                      "Esportazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'esportazione: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Rappresenta un elemento del progetto per la visualizzazione in lista
    /// </summary>
    internal class ProjectElement
    {
        public string Tipo { get; set; }
        public int Id { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public int Quantita { get; set; }
        public string Revisione { get; set; }
        public string Stato { get; set; }
        public int Livello { get; set; }
        public string Percorso { get; set; }
        public string TipoGruppo { get; set; }
        public object Data { get; set; }
    }
}