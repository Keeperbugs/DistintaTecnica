using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Forms
{
    public partial class SearchResultsForm : Form
    {
        private Repository repository;
        private List<SearchResult> allResults;
        private List<SearchResult> filteredResults;
        private SearchResult selectedResult;
        private System.Windows.Forms.Timer searchTimer;

        public SearchResult SelectedResult => selectedResult;

        public SearchResultsForm(List<SearchResult> results)
        {
            InitializeComponent();

            // Initialize repository
            var dbManager = new DatabaseManager();
            repository = new Repository(dbManager);

            allResults = results ?? new List<SearchResult>();
            filteredResults = new List<SearchResult>(allResults);

            InitializeForm();
            SetupEventHandlers();
            LoadResults();
        }

        #region Initialization

        private void InitializeForm()
        {
            // Initialize search timer
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300; // 300ms delay
            searchTimer.Tick += SearchTimer_Tick;

            // Set initial focus
            if (allResults.Count > 0)
            {
                resultsListView.Focus();
            }
            else
            {
                searchTextBox.Focus();
            }
        }

        private void SetupEventHandlers()
        {
            // Search events
            searchButton.Click += SearchButton_Click;
            clearButton.Click += ClearButton_Click;
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;

            // ListView events
            resultsListView.SelectedIndexChanged += ResultsListView_SelectedIndexChanged;
            resultsListView.DoubleClick += ResultsListView_DoubleClick;
            resultsListView.KeyDown += ResultsListView_KeyDown;

            // Button events
            selectButton.Click += SelectButton_Click;
            viewDetailsButton.Click += ViewDetailsButton_Click;
            closeButton.Click += CloseButton_Click;

            // Context menu events
            openMenuItem.Click += OpenMenuItem_Click;
            copyCodeMenuItem.Click += CopyCodeMenuItem_Click;
            copyDescriptionMenuItem.Click += CopyDescriptionMenuItem_Click;
            propertiesMenuItem.Click += PropertiesMenuItem_Click;

            // Form events
            this.Load += SearchResultsForm_Load;
            this.KeyDown += SearchResultsForm_KeyDown;
        }

        #endregion

        #region Data Loading

        private void LoadResults()
        {
            try
            {
                resultsListView.BeginUpdate();
                resultsListView.Items.Clear();

                foreach (var result in filteredResults)
                {
                    var item = new ListViewItem(result.Tipo);
                    item.SubItems.Add(result.Codice);
                    item.SubItems.Add(result.Descrizione);
                    item.SubItems.Add(result.Progetto);
                    item.SubItems.Add(result.PathCompleto);
                    item.Tag = result;

                    // Color coding by type
                    switch (result.Tipo.ToUpper())
                    {
                        case "PROGETTO":
                            item.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
                            break;
                        case "PARTE_MACCHINA":
                            item.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
                            break;
                        case "SEZIONE":
                            item.BackColor = System.Drawing.Color.FromArgb(255, 251, 235);
                            break;
                        case "SOTTOSEZIONE":
                            item.BackColor = System.Drawing.Color.FromArgb(252, 231, 243);
                            break;
                        case "MONTAGGIO":
                            item.BackColor = System.Drawing.Color.FromArgb(245, 243, 255);
                            break;
                        case "GRUPPO":
                            item.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
                            break;
                    }

                    resultsListView.Items.Add(item);
                }

                UpdateResultsCount();

                // Auto-select first item if available
                if (resultsListView.Items.Count > 0)
                {
                    resultsListView.Items[0].Selected = true;
                    resultsListView.Items[0].Focused = true;
                }
            }
            finally
            {
                resultsListView.EndUpdate();
            }
        }

        private void UpdateResultsCount()
        {
            int total = allResults.Count;
            int filtered = filteredResults.Count;

            if (total == filtered)
            {
                resultsCountLabel.Text = $"{total} risultati trovati";
            }
            else
            {
                resultsCountLabel.Text = $"{filtered} di {total} risultati mostrati";
            }
        }

        #endregion

        #region Search and Filter

        private void PerformSearch()
        {
            string searchTerm = searchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                filteredResults = new List<SearchResult>(allResults);
            }
            else
            {
                filteredResults = allResults.Where(r =>
                    r.Codice.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Descrizione.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Progetto.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Tipo.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }

            LoadResults();
        }

        #endregion

        #region Event Handlers

        private void SearchResultsForm_Load(object sender, EventArgs e)
        {
            // Auto-resize columns
            foreach (ColumnHeader column in resultsListView.Columns)
            {
                column.Width = -2; // Auto-size to content
            }
        }

        private void SearchResultsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyCode == Keys.F3 || (e.Control && e.KeyCode == Keys.F))
            {
                searchTextBox.Focus();
                searchTextBox.SelectAll();
                e.Handled = true;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            searchTextBox.Focus();
            PerformSearch();
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
            else if (e.KeyCode == Keys.Down && resultsListView.Items.Count > 0)
            {
                resultsListView.Focus();
                if (resultsListView.SelectedItems.Count == 0)
                {
                    resultsListView.Items[0].Selected = true;
                }
                e.Handled = true;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void ResultsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = resultsListView.SelectedItems.Count > 0;

            selectButton.Enabled = hasSelection;
            viewDetailsButton.Enabled = hasSelection;

            if (hasSelection)
            {
                selectedResult = (SearchResult)resultsListView.SelectedItems[0].Tag;
            }
            else
            {
                selectedResult = null;
            }
        }

        private void ResultsListView_DoubleClick(object sender, EventArgs e)
        {
            if (resultsListView.SelectedItems.Count > 0)
            {
                SelectButton_Click(sender, e);
            }
        }

        private void ResultsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && resultsListView.SelectedItems.Count > 0)
            {
                SelectButton_Click(sender, e);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedCode();
                e.Handled = true;
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (selectedResult != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ViewDetailsButton_Click(object sender, EventArgs e)
        {
            if (selectedResult != null)
            {
                ShowElementDetails(selectedResult);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Context Menu Handlers

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            SelectButton_Click(sender, e);
        }

        private void CopyCodeMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedCode();
        }

        private void CopyDescriptionMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedResult != null && !string.IsNullOrEmpty(selectedResult.Descrizione))
            {
                Clipboard.SetText(selectedResult.Descrizione);
                ShowStatusMessage("Descrizione copiata negli appunti");
            }
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            ViewDetailsButton_Click(sender, e);
        }

        #endregion

        #region Helper Methods

        private void CopySelectedCode()
        {
            if (selectedResult != null && !string.IsNullOrEmpty(selectedResult.Codice))
            {
                Clipboard.SetText(selectedResult.Codice);
                ShowStatusMessage("Codice copiato negli appunti");
            }
        }

        private void ShowElementDetails(SearchResult result)
        {
            string details = $"Tipo: {result.Tipo}\n" +
                           $"Codice: {result.Codice}\n" +
                           $"Descrizione: {result.Descrizione}\n" +
                           $"Progetto: {result.Progetto}\n" +
                           $"Percorso: {result.PathCompleto}\n" +
                           $"ID: {result.Id}";

            MessageBox.Show(details, "Dettagli Elemento",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowStatusMessage(string message)
        {
            // Show temporary status message
            var originalText = resultsCountLabel.Text;
            resultsCountLabel.Text = message;
            resultsCountLabel.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000; // 2 seconds
            timer.Tick += (s, e) =>
            {
                resultsCountLabel.Text = originalText;
                resultsCountLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the search results dialog
        /// </summary>
        public static SearchResult ShowDialog(List<SearchResult> results, IWin32Window owner = null)
        {
            using (var form = new SearchResultsForm(results))
            {
                var dialogResult = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                return dialogResult == DialogResult.OK ? form.SelectedResult : null;
            }
        }

        /// <summary>
        /// Performs a new search and shows the results
        /// </summary>
        public static SearchResult ShowSearchDialog(Repository repository, string searchTerm = "", IWin32Window owner = null)
        {
            try
            {
                var results = string.IsNullOrEmpty(searchTerm)
                    ? new List<SearchResult>()
                    : repository.SearchGlobal(searchTerm);

                using (var form = new SearchResultsForm(results))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        form.searchTextBox.Text = searchTerm;
                    }

                    var dialogResult = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                    return dialogResult == DialogResult.OK ? form.SelectedResult : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante la ricerca: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #endregion

        #region Cleanup

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                searchTimer?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}