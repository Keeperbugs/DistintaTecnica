using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Business;
using DistintaTecnica.Data;
using DistintaTecnica.Forms;

namespace DistintaTecnica.Forms
{
    /// <summary>
    /// Form per la selezione di codici dalla libreria o creazione di nuovi codici
    /// </summary>
    public partial class CodeSelectorForm : Form
    {
        private CodesLibraryManager libraryManager;
        private TipoElemento elementType;
        private string numeroCommessa;
        private System.Windows.Forms.Timer searchTimer;
        private CodiceBiblioteca selectedCode;
        private bool isNewCode;

        // Proprietà pubbliche per il risultato
        public string SelectedCodice { get; private set; } = string.Empty;
        public string SelectedDescrizione { get; private set; } = string.Empty;
        public bool IsNewCode => isNewCode;
        public CodiceBiblioteca SelectedLibraryCode => selectedCode;

        public CodeSelectorForm(TipoElemento tipoElemento, string numeroCommessa)
        {
            InitializeComponent();

            this.elementType = tipoElemento;
            this.numeroCommessa = numeroCommessa ?? string.Empty;
            this.libraryManager = new CodesLibraryManager();

            InitializeFormAfterDesigner();
            SetupEventHandlers();
            LoadInitialData();
        }

        #region Initialization

        private void InitializeFormAfterDesigner()
        {
            // Configura il titolo in base al tipo elemento
            string elementTypeName = GetElementTypeName();
            this.Text = $"Selezione Codice - {elementTypeName}";
            this.titleLabel.Text = $"SELEZIONE CODICE {elementTypeName.ToUpper()}";
            this.subtitleLabel.Text = $"Crea un nuovo codice {elementTypeName.ToLower()} o seleziona da libreria esistente";

            // Setup search timer
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 500; // 500ms delay
            searchTimer.Tick += SearchTimer_Tick;

            // Initial state
            UpdateUIState();
        }

        private void SetupEventHandlers()
        {
            // Form events
            this.Load += CodeSelectorForm_Load;
            this.FormClosing += CodeSelectorForm_FormClosing;

            // Already connected in designer
            // this.radioCreateNew.CheckedChanged += RadioCreateNew_CheckedChanged;
            // this.radioSelectExisting.CheckedChanged += RadioSelectExisting_CheckedChanged;
            // ... other events already connected
        }

        private void LoadInitialData()
        {
            try
            {
                // Load recent codes for this element type
                LoadCodesFromLibrary();
                UpdateStatusLabel("Pronto per la selezione", false);
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento: {ex.Message}", true);
            }
        }

        #endregion

        #region Event Handlers

        private void CodeSelectorForm_Load(object sender, EventArgs e)
        {
            // Set focus to appropriate control
            if (radioCreateNew.Checked)
                txtNewCode.Focus();
            else
                txtSearch.Focus();
        }

        private void CodeSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Stop();
            searchTimer?.Dispose();
        }

        private void RadioCreateNew_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCreateNew.Checked)
            {
                isNewCode = true;
                UpdateUIState();
                txtNewCode.Focus();
            }
        }

        private void RadioSelectExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSelectExisting.Checked)
            {
                isNewCode = false;
                UpdateUIState();
                txtSearch.Focus();
            }
        }

        private void TxtNewCode_TextChanged(object sender, EventArgs e)
        {
            ValidateNewCode();
            UpdateButtonState();
        }

        private void TxtNewDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void BtnGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateNewCode();
            }
            catch (Exception ex)
            {
                ShowValidationMessage($"Errore generazione: {ex.Message}", true);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            LoadCodesFromLibrary();
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCodesFromLibrary();
        }

        private void CodesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedCodeDetails();
            UpdateButtonState();
        }

        private void CodesListView_DoubleClick(object sender, EventArgs e)
        {
            if (codesListView.SelectedItems.Count > 0)
            {
                BtnOK_Click(sender, e);
            }
        }

        private void BtnShowHistory_Click(object sender, EventArgs e)
        {
            ShowCodeHistory();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateSelection())
            {
                PrepareResult();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region UI Management

        private void UpdateUIState()
        {
            // Enable/disable groups based on selection
            newCodeGroup.Enabled = radioCreateNew.Checked;
            existingCodeGroup.Enabled = radioSelectExisting.Checked;

            // Clear selections when switching modes
            if (radioCreateNew.Checked)
            {
                ClearExistingSelection();
                txtNewCode.Focus();
            }
            else
            {
                ClearNewCodeFields();
                txtSearch.Focus();
            }

            UpdateButtonState();
            UpdateStatusLabel();
        }

        private void UpdateButtonState()
        {
            bool canProceed = false;

            if (radioCreateNew.Checked)
            {
                // For new code: both code and description must be valid
                canProceed = !string.IsNullOrWhiteSpace(txtNewCode.Text) &&
                           !string.IsNullOrWhiteSpace(txtNewDescription.Text) &&
                           IsValidNewCode();
            }
            else if (radioSelectExisting.Checked)
            {
                // For existing code: must have selection
                canProceed = codesListView.SelectedItems.Count > 0;
            }

            btnOK.Enabled = canProceed;

            // Update button text based on mode
            if (radioCreateNew.Checked)
                btnOK.Text = "Crea e Seleziona";
            else
                btnOK.Text = "Seleziona";
        }

        private void UpdateStatusLabel(string message = null, bool isError = false)
        {
            if (!string.IsNullOrEmpty(message))
            {
                statusLabel.Text = message;
                statusLabel.ForeColor = isError ? Color.FromArgb(239, 68, 68) : Color.FromArgb(100, 116, 139);
            }
            else
            {
                if (radioCreateNew.Checked)
                {
                    statusLabel.Text = "Inserisci codice e descrizione per il nuovo elemento";
                }
                else
                {
                    int itemCount = codesListView.Items.Count;
                    statusLabel.Text = $"{itemCount} codici disponibili nella libreria";
                }
                statusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            }
        }

        #endregion

        #region New Code Management

        private void ValidateNewCode()
        {
            string code = txtNewCode.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                ShowValidationMessage("", false);
                return;
            }

            try
            {
                var validationResult = libraryManager.ValidaCodice(code, elementType, numeroCommessa);

                if (validationResult.IsValido)
                {
                    if (validationResult.EsisteInLibreria)
                    {
                        ShowValidationMessage($"⚠️ {validationResult.Messaggio}", false);
                        txtNewCode.BackColor = Color.FromArgb(255, 251, 235); // Yellow background

                        // Auto-fill description if exists
                        if (!string.IsNullOrEmpty(validationResult.DescrizioneEsistente))
                        {
                            txtNewDescription.Text = validationResult.DescrizioneEsistente;
                        }
                    }
                    else
                    {
                        ShowValidationMessage("✓ Codice valido", false);
                        txtNewCode.BackColor = Color.FromArgb(240, 253, 244); // Green background
                    }
                }
                else
                {
                    ShowValidationMessage($"✗ {validationResult.Messaggio}", true);
                    txtNewCode.BackColor = Color.FromArgb(254, 226, 226); // Red background
                }
            }
            catch (Exception ex)
            {
                ShowValidationMessage($"Errore validazione: {ex.Message}", true);
                txtNewCode.BackColor = SystemColors.Window;
            }
        }

        private bool IsValidNewCode()
        {
            string code = txtNewCode.Text.Trim();
            if (string.IsNullOrEmpty(code)) return false;

            try
            {
                var validationResult = libraryManager.ValidaCodice(code, elementType, numeroCommessa);
                return validationResult.IsValido;
            }
            catch
            {
                return false;
            }
        }

        private void GenerateNewCode()
        {
            // This would integrate with the existing CodeGenerator
            // For now, show a placeholder message
            MessageBox.Show(
                "Funzionalità di generazione automatica da implementare.\n\n" +
                "Sarà integrata con il CodeGenerator esistente per generare codici come:\n" +
                "- PRO1394C00 (Parti Macchina)\n" +
                "- FOR1687C12 (Sezioni)\n" +
                "- TFO1687D11 (Sottosezioni)",
                "Generazione Codice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void ShowValidationMessage(string message, bool isError)
        {
            lblValidation.Text = message;
            lblValidation.ForeColor = isError ?
                Color.FromArgb(239, 68, 68) :
                Color.FromArgb(34, 197, 94);
        }

        private void ClearNewCodeFields()
        {
            txtNewCode.Clear();
            txtNewDescription.Clear();
            txtNewCode.BackColor = SystemColors.Window;
            ShowValidationMessage("", false);
        }

        #endregion

        #region Existing Codes Management

        private void LoadCodesFromLibrary()
        {
            try
            {
                codesListView.BeginUpdate();
                codesListView.Items.Clear();

                string searchTerm = txtSearch.Text.Trim();
                var codes = libraryManager.CercaCodici(searchTerm, elementType, 100);

                foreach (var code in codes)
                {
                    var item = new ListViewItem(code.Codice);
                    item.SubItems.Add(code.Descrizione);
                    item.SubItems.Add(code.NumeroUtilizzi.ToString());
                    item.SubItems.Add(code.UltimoUtilizzo.ToString("dd/MM/yyyy"));
                    item.Tag = code;

                    // Color coding based on usage
                    if (code.NumeroUtilizzi > 10)
                        item.BackColor = Color.FromArgb(240, 253, 244); // Green for popular
                    else if (code.NumeroUtilizzi > 5)
                        item.BackColor = Color.FromArgb(255, 251, 235); // Yellow for moderate
                    else if ((DateTime.Now - code.UltimoUtilizzo).Days < 7)
                        item.BackColor = Color.FromArgb(239, 246, 255); // Blue for recent

                    codesListView.Items.Add(item);
                }

                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore caricamento libreria: {ex.Message}", true);
            }
            finally
            {
                codesListView.EndUpdate();
            }
        }

        private void UpdateSelectedCodeDetails()
        {
            if (codesListView.SelectedItems.Count == 0)
            {
                ClearSelectedCodeDetails();
                return;
            }

            try
            {
                selectedCode = (CodiceBiblioteca)codesListView.SelectedItems[0].Tag;

                txtSelectedCode.Text = selectedCode.Codice;
                txtSelectedDescription.Text = selectedCode.Descrizione;

                // Show usage information
                var commesse = selectedCode.GetCommesse();
                string usageInfo = $"Utilizzato {selectedCode.NumeroUtilizzi} volte";

                if (commesse.Length > 0)
                {
                    usageInfo += $" in {commesse.Length} commesse";
                    if (selectedCode.UtilizzatoInCommessa(numeroCommessa))
                    {
                        usageInfo += " (inclusa questa)";
                    }
                }

                txtUsageInfo.Text = usageInfo;
            }
            catch (Exception ex)
            {
                UpdateStatusLabel($"Errore dettagli: {ex.Message}", true);
            }
        }

        private void ClearSelectedCodeDetails()
        {
            selectedCode = null;
            txtSelectedCode.Clear();
            txtSelectedDescription.Clear();
            txtUsageInfo.Clear();
        }

        private void ClearExistingSelection()
        {
            codesListView.SelectedItems.Clear();
            ClearSelectedCodeDetails();
        }

        private void ShowCodeHistory()
        {
            if (selectedCode == null) return;

            try
            {
                var commesse = selectedCode.GetCommesse();
                string history = $"Storico utilizzi per: {selectedCode.Codice}\n\n";
                history += $"Descrizione: {selectedCode.Descrizione}\n";
                history += $"Totale utilizzi: {selectedCode.NumeroUtilizzi}\n";
                history += $"Ultimo utilizzo: {selectedCode.UltimoUtilizzo:dd/MM/yyyy HH:mm}\n\n";

                if (commesse.Length > 0)
                {
                    history += "Commesse dove è stato utilizzato:\n";
                    foreach (var commessa in commesse)
                    {
                        history += $"• {commessa}\n";
                    }
                }
                else
                {
                    history += "Nessuna informazione sulle commesse disponibile.";
                }

                MessageBox.Show(history, "Storico Codice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel recupero dello storico: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Validation and Result

        private bool ValidateSelection()
        {
            if (radioCreateNew.Checked)
            {
                string code = txtNewCode.Text.Trim();
                string description = txtNewDescription.Text.Trim();

                if (string.IsNullOrEmpty(code))
                {
                    MessageBox.Show("Il codice è obbligatorio.", "Validazione",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewCode.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("La descrizione è obbligatoria.", "Validazione",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewDescription.Focus();
                    return false;
                }

                if (!IsValidNewCode())
                {
                    MessageBox.Show("Il codice inserito non è valido.", "Validazione",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewCode.Focus();
                    return false;
                }
            }
            else if (radioSelectExisting.Checked)
            {
                if (selectedCode == null)
                {
                    MessageBox.Show("Seleziona un codice dalla libreria.", "Validazione",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void PrepareResult()
        {
            if (radioCreateNew.Checked)
            {
                SelectedCodice = txtNewCode.Text.Trim();
                SelectedDescrizione = txtNewDescription.Text.Trim();
                isNewCode = true;

                try
                {
                    // Register the new code in the library
                    libraryManager.RegistraCodice(SelectedCodice, SelectedDescrizione, numeroCommessa);
                }
                catch (Exception ex)
                {
                    // Log error but don't fail the selection
                    System.Diagnostics.Debug.WriteLine($"Error registering code: {ex.Message}");
                }
            }
            else if (radioSelectExisting.Checked && selectedCode != null)
            {
                SelectedCodice = selectedCode.Codice;
                SelectedDescrizione = selectedCode.Descrizione;
                isNewCode = false;

                try
                {
                    // Increment usage for existing code
                    libraryManager.RegistraCodice(SelectedCodice, SelectedDescrizione, numeroCommessa);
                }
                catch (Exception ex)
                {
                    // Log error but don't fail the selection
                    System.Diagnostics.Debug.WriteLine($"Error updating code usage: {ex.Message}");
                }
            }
        }

        #endregion

        #region Helper Methods

        private string GetElementTypeName()
        {
            return elementType switch
            {
                TipoElemento.ParteMacchina => "Parte Macchina",
                TipoElemento.Sezione => "Sezione",
                TipoElemento.Sottosezione => "Sottosezione",
                TipoElemento.Montaggio => "Montaggio",
                TipoElemento.Gruppo => "Gruppo",
                _ => "Elemento"
            };
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Mostra il dialog per la selezione di un codice
        /// </summary>
        public static CodeSelectorForm ShowDialog(TipoElemento tipoElemento, string numeroCommessa, IWin32Window owner = null)
        {
            var form = new CodeSelectorForm(tipoElemento, numeroCommessa);
            var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);

            if (result == DialogResult.OK)
                return form;
            else
            {
                form.Dispose();
                return null;
            }
        }

        /// <summary>
        /// Mostra il dialog e restituisce il risultato della selezione
        /// </summary>
        public static CodeSelectionResult SelectCode(TipoElemento tipoElemento, string numeroCommessa, IWin32Window owner = null)
        {
            using (var form = new CodeSelectorForm(tipoElemento, numeroCommessa))
            {
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);

                if (result == DialogResult.OK)
                {
                    return new CodeSelectionResult
                    {
                        Success = true,
                        Codice = form.SelectedCodice,
                        Descrizione = form.SelectedDescrizione,
                        IsNewCode = form.IsNewCode,
                        LibraryCode = form.SelectedLibraryCode
                    };
                }
                else
                {
                    return new CodeSelectionResult { Success = false };
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Risultato della selezione codice
    /// </summary>
    public class CodeSelectionResult
    {
        public bool Success { get; set; }
        public string Codice { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public bool IsNewCode { get; set; }
        public CodiceBiblioteca? LibraryCode { get; set; }
    }
}