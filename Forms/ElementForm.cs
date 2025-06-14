using System;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;
using DistintaTecnica.Extensions;

namespace DistintaTecnica.Forms
{
    public enum ElementType
    {
        ParteMacchina,
        Sezione,
        Sottosezione,
        Montaggio,
        Gruppo
    }

    public partial class ElementForm : Form
    {
        private Repository repository;
        private CodeGenerator codeGenerator;
        private ElementType elementType;
        private int parentId;
        private object currentElement;
        private bool isEditMode;
        private string parentCode;

        public object Element => currentElement;

        public ElementForm(ElementType type, int parentId, string parentCode = null, object existingElement = null)
        {
            InitializeComponent();

            // Initialize dependencies
            var dbManager = new DatabaseManager();
            repository = new Repository(dbManager);
            codeGenerator = repository.GetCodeGenerator();

            this.elementType = type;
            this.parentId = parentId;
            this.parentCode = parentCode ?? string.Empty;
            this.currentElement = existingElement;
            this.isEditMode = existingElement != null;

            InitializeFormAfterDesigner();
            SetupEventHandlers();
            LoadComboBoxData();

            if (isEditMode)
            {
                LoadElementData();
            }
        }

        #region Initialization

        private void InitializeFormAfterDesigner()
        {
            // Set form title and labels based on element type
            string elementTypeName = GetElementTypeName();

            if (isEditMode)
            {
                this.Text = $"Modifica {elementTypeName}";
                this.titleLabel.Text = $"MODIFICA {elementTypeName.ToUpper()}";
                this.subtitleLabel.Text = $"Modifica le informazioni del {elementTypeName.ToLower()}";
                this.okButton.Text = "Salva Modifiche";
            }
            else
            {
                this.Text = $"Nuovo {elementTypeName}";
                this.titleLabel.Text = $"NUOVO {elementTypeName.ToUpper()}";
                this.subtitleLabel.Text = $"Inserisci le informazioni del {elementTypeName.ToLower()}";
                this.okButton.Text = "Crea";
            }

            // Configure UI based on element type
            ConfigureUIForElementType();

            // Set focus to first available field
            SetInitialFocus();
        }

        private string GetElementTypeName()
        {
            return elementType switch
            {
                ElementType.ParteMacchina => "Parte Macchina",
                ElementType.Sezione => "Sezione",
                ElementType.Sottosezione => "Sottosezione",
                ElementType.Montaggio => "Montaggio",
                ElementType.Gruppo => "Gruppo",
                _ => "Elemento"
            };
        }

        private void ConfigureUIForElementType()
        {
            // Hide type selector in edit mode
            if (isEditMode)
            {
                lblTipo.Visible = false;
                cmbTipo.Visible = false;
            }
            else
            {
                // Populate type combo for new elements
                cmbTipo.Items.Clear();
                cmbTipo.Items.Add(GetElementTypeName());
                cmbTipo.SelectedIndex = 0;
                cmbTipo.Enabled = false; // Type is fixed based on context
            }

            // Show/hide fields based on element type
            switch (elementType)
            {
                case ElementType.ParteMacchina:
                    ShowParteMacchinaFields();
                    break;
                case ElementType.Sezione:
                    ShowSezioneFields();
                    break;
                case ElementType.Sottosezione:
                    ShowSottosezioneFields();
                    break;
                case ElementType.Montaggio:
                    ShowMontaggioFields();
                    break;
                case ElementType.Gruppo:
                    ShowGruppoFields();
                    break;
            }

            // Configure code generation based on element type
            ConfigureCodeGeneration();
        }

        private void ShowParteMacchinaFields()
        {
            lblSottoProgetto.Visible = true;
            cmbSottoProgetto.Visible = true;
            lblTipoParteMacchina.Visible = true;
            cmbTipoParteMacchina.Visible = true;

            lblQuantita.Visible = false;
            numQuantita.Visible = false;
        }

        private void ShowSezioneFields()
        {
            lblQuantita.Visible = true;
            numQuantita.Visible = true;
        }

        private void ShowSottosezioneFields()
        {
            lblQuantita.Visible = true;
            numQuantita.Visible = true;
        }

        private void ShowMontaggioFields()
        {
            lblQuantita.Visible = true;
            numQuantita.Visible = true;

            // Montaggi are not auto-generated
            btnGeneraCodice.Visible = false;
            chkAutoDescrizione.Visible = false;
        }

        private void ShowGruppoFields()
        {
            lblTipoGruppo.Visible = true;
            cmbTipoGruppo.Visible = true;
            lblQuantita.Visible = true;
            numQuantita.Visible = true;

            // Gruppi are not auto-generated
            btnGeneraCodice.Visible = false;
            chkAutoDescrizione.Visible = false;
        }

        private void ConfigureCodeGeneration()
        {
            // Auto-generation is available only for ParteMacchina, Sezione, Sottosezione
            bool canAutoGenerate = elementType == ElementType.ParteMacchina ||
                                 elementType == ElementType.Sezione ||
                                 elementType == ElementType.Sottosezione;

            btnGeneraCodice.Visible = canAutoGenerate && !isEditMode;
            chkAutoDescrizione.Visible = canAutoGenerate;

            if (!canAutoGenerate)
            {
                lblCodice.Text = "Codice *:";
                if (elementType == ElementType.Montaggio)
                {
                    // Aggiungi placeholder per montaggi
                    txtCodice.PlaceholderText = "es: 82509A1";
                }
                else
                {
                    // Placeholder per gruppi
                    txtCodice.PlaceholderText = "es: 51152M, 123456, 1234567";
                }
            }
        }

        private void SetInitialFocus()
        {
            if (btnGeneraCodice.Visible)
            {
                btnGeneraCodice.Focus();
            }
            else if (txtCodice.Visible)
            {
                txtCodice.Focus();
            }
            else
            {
                txtDescrizione.Focus();
            }
        }

        private void SetupEventHandlers()
        {
            // Button events
            this.okButton.Click += OkButton_Click;
            this.cancelButton.Click += CancelButton_Click;
            this.btnGeneraCodice.Click += GenerateCode_Click;

            // Validation events
            this.txtCodice.TextChanged += Code_TextChanged;
            this.txtDescrizione.TextChanged += Description_TextChanged;
            this.chkAutoDescrizione.CheckedChanged += AutoDescription_CheckedChanged;

            // ComboBox events
            this.cmbSottoProgetto.SelectedIndexChanged += SottoProgetto_SelectedIndexChanged;
            this.cmbTipoParteMacchina.SelectedIndexChanged += TipoParteMacchina_SelectedIndexChanged;
            this.cmbTipoGruppo.SelectedIndexChanged += TipoGruppo_SelectedIndexChanged;

            // Enter key handling
            this.txtCodice.KeyDown += TextBox_KeyDown;
            this.txtDescrizione.KeyDown += TextBox_KeyDown;
            this.txtNote.KeyDown += TextBox_KeyDown;

            // Form events
            this.Load += ElementForm_Load;
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load SottoProgetto items
                if (cmbSottoProgetto.Visible)
                {
                    var sottoProgetti = EnumExtensions.ToComboBoxItems<SottoProgetto>();
                    cmbSottoProgetto.DataSource = sottoProgetti;
                    cmbSottoProgetto.DisplayMember = "Text";
                    cmbSottoProgetto.ValueMember = "Value";
                }

                // Load TipoParteMacchina items
                if (cmbTipoParteMacchina.Visible)
                {
                    var tipiParte = EnumExtensions.ToComboBoxItems<TipoParteMacchina>();
                    cmbTipoParteMacchina.DataSource = tipiParte;
                    cmbTipoParteMacchina.DisplayMember = "Text";
                    cmbTipoParteMacchina.ValueMember = "Value";
                }

                // Load TipoGruppo items
                if (cmbTipoGruppo.Visible)
                {
                    var tipiGruppo = EnumExtensions.ToComboBoxItems<TipoGruppo>();
                    cmbTipoGruppo.DataSource = tipiGruppo;
                    cmbTipoGruppo.DisplayMember = "Text";
                    cmbTipoGruppo.ValueMember = "Value";
                }

                // Load revision items
                string[] revisioni = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                cmbRevisione.Items.AddRange(revisioni);
                cmbRevisione.SelectedIndex = 0; // Default to "A"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel caricamento dati: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadElementData()
        {
            try
            {
                switch (elementType)
                {
                    case ElementType.ParteMacchina:
                        LoadParteMacchinaData();
                        break;
                    case ElementType.Sezione:
                        LoadSezioneData();
                        break;
                    case ElementType.Sottosezione:
                        LoadSottosezioneData();
                        break;
                    case ElementType.Montaggio:
                        LoadMontaggioData();
                        break;
                    case ElementType.Gruppo:
                        LoadGruppoData();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel caricamento dati elemento: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadParteMacchinaData()
        {
            var parte = (ParteMacchina)currentElement;

            // Set SottoProgetto
            var sottoProgettoItem = cmbSottoProgetto.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(x => x.Text.Contains(parte.SottoProgetto));
            if (sottoProgettoItem != null)
                cmbSottoProgetto.SelectedItem = sottoProgettoItem;

            // Set TipoParteMacchina
            var tipoParteItem = cmbTipoParteMacchina.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(x => x.Text.Contains(parte.TipoParteMacchina));
            if (tipoParteItem != null)
                cmbTipoParteMacchina.SelectedItem = tipoParteItem;

            txtCodice.Text = parte.CodiceParteMacchina;
            txtDescrizione.Text = parte.Descrizione;
            cmbRevisione.Text = parte.RevisioneInserimento;
            txtStato.Text = parte.Stato;
            txtNote.Text = parte.Note ?? string.Empty;
        }

        private void LoadSezioneData()
        {
            var sezione = (Sezione)currentElement;

            txtCodice.Text = sezione.CodiceSezione;
            txtDescrizione.Text = sezione.Descrizione;
            numQuantita.Value = sezione.Quantita;
            cmbRevisione.Text = sezione.RevisioneInserimento;
            txtStato.Text = sezione.Stato;
            txtNote.Text = sezione.Note ?? string.Empty;
        }

        private void LoadSottosezioneData()
        {
            var sottosezione = (Sottosezione)currentElement;

            txtCodice.Text = sottosezione.CodiceSottosezione;
            txtDescrizione.Text = sottosezione.Descrizione;
            numQuantita.Value = sottosezione.Quantita;
            cmbRevisione.Text = sottosezione.RevisioneInserimento;
            txtStato.Text = sottosezione.Stato;
            txtNote.Text = sottosezione.Note ?? string.Empty;
        }

        private void LoadMontaggioData()
        {
            var montaggio = (Montaggio)currentElement;

            txtCodice.Text = montaggio.CodiceMontaggio;
            txtDescrizione.Text = montaggio.Descrizione;
            numQuantita.Value = montaggio.Quantita;
            cmbRevisione.Text = montaggio.RevisioneInserimento;
            txtStato.Text = montaggio.Stato;
            txtNote.Text = montaggio.Note ?? string.Empty;
        }

        private void LoadGruppoData()
        {
            var gruppo = (Gruppo)currentElement;

            // Set TipoGruppo
            var tipoGruppoItem = cmbTipoGruppo.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(x => x.Text.Contains(gruppo.TipoGruppo));
            if (tipoGruppoItem != null)
                cmbTipoGruppo.SelectedItem = tipoGruppoItem;

            txtCodice.Text = gruppo.CodiceGruppo;
            txtDescrizione.Text = gruppo.Descrizione;
            numQuantita.Value = gruppo.Quantita;
            cmbRevisione.Text = gruppo.RevisioneInserimento;
            txtStato.Text = gruppo.Stato;
            txtNote.Text = gruppo.Note ?? string.Empty;
        }

        #endregion

        #region Event Handlers

        private void ElementForm_Load(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidateAndSave())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void GenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                string generatedCode = GenerateCodeBasedOnType();
                if (!string.IsNullOrEmpty(generatedCode))
                {
                    txtCodice.Text = generatedCode;

                    if (chkAutoDescrizione.Checked)
                    {
                        GenerateAutoDescription();
                    }

                    ValidateCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nella generazione del codice: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Code_TextChanged(object sender, EventArgs e)
        {
            ValidateCode();
            ValidateForm();
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void AutoDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoDescrizione.Checked && !string.IsNullOrEmpty(txtCodice.Text))
            {
                GenerateAutoDescription();
            }
        }

        private void SottoProgetto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAutoDescrizione.Checked)
            {
                GenerateAutoDescription();
            }
        }

        private void TipoParteMacchina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAutoDescrizione.Checked)
            {
                GenerateAutoDescription();
            }
        }

        private void TipoGruppo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAutoDescrizione.Checked)
            {
                GenerateAutoDescription();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        #endregion

        #region Code Generation and Validation

        private string GenerateCodeBasedOnType()
        {
            switch (elementType)
            {
                case ElementType.ParteMacchina:
                    return GenerateParteMacchinaCode();
                case ElementType.Sezione:
                    return GenerateSezioneCode();
                case ElementType.Sottosezione:
                    return GenerateSottosezioneCode();
                default:
                    return string.Empty;
            }
        }

        private string GenerateParteMacchinaCode()
        {
            if (cmbTipoParteMacchina.SelectedItem is ComboBoxItem selectedItem)
            {
                string prefix = GetPrefixFromTipoParteMacchina(selectedItem.Value);

                // For now, use default values - in real implementation, you might want a dialog
                int diametro = 139; // Default diameter
                int spessore = 4;   // Default thickness

                return codeGenerator.GeneraCodiceParteMacchina(prefix, diametro, spessore);
            }
            return string.Empty;
        }

        private string GenerateSezioneCode()
        {
            if (!string.IsNullOrEmpty(parentCode))
            {
                // Use FOR as default prefix - in real implementation, you might want a selector
                return codeGenerator.GeneraCodiceSezione("FOR", parentCode);
            }
            return string.Empty;
        }

        private string GenerateSottosezioneCode()
        {
            if (!string.IsNullOrEmpty(parentCode))
            {
                // Use TFO as default prefix - in real implementation, you might want a selector
                return codeGenerator.GeneraCodiceSottosezione("TFO", parentCode);
            }
            return string.Empty;
        }

        private string GetPrefixFromTipoParteMacchina(string tipoValue)
        {
            return tipoValue switch
            {
                "Profila" => "PRO",
                "Aspo" => "ASP",
                "PinchRoll" => "PIN",
                "Intestatrice" => "INT",
                "Preintestatrice" => "PRE",
                "Floop" => "FLO",
                "Accumulatore" => "ACC",
                "Taglio" => "TAG",
                "ViaRulli" => "VRU",
                "Lancianastro" => "LAN",
                _ => "PRO"
            };
        }

        private void ValidateCode()
        {
            string code = txtCodice.Text.Trim();
            lblValidazioneCodice.Text = "";

            if (string.IsNullOrEmpty(code))
            {
                txtCodice.BackColor = System.Drawing.SystemColors.Window;
                return;
            }

            try
            {
                string elementTypeString = elementType.ToString().ToUpper();
                var validation = repository.ValidateAndCheckCode(code, elementTypeString);

                if (validation.IsValid)
                {
                    txtCodice.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
                    lblValidazioneCodice.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
                    lblValidazioneCodice.Text = "✓ Codice valido";

                    if (validation.Warnings.Count > 0)
                    {
                        lblValidazioneCodice.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
                        lblValidazioneCodice.Text = "⚠ " + validation.Warnings.First();
                    }
                }
                else
                {
                    txtCodice.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                    lblValidazioneCodice.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
                    lblValidazioneCodice.Text = "✗ " + validation.Errors.First();
                }
            }
            catch (Exception)
            {
                txtCodice.BackColor = System.Drawing.SystemColors.Window;
                lblValidazioneCodice.Text = "";
            }
        }

        private void GenerateAutoDescription()
        {
            try
            {
                string description = "";

                switch (elementType)
                {
                    case ElementType.ParteMacchina:
                        description = GenerateParteMacchinaDescription();
                        break;
                    case ElementType.Sezione:
                        description = GenerateSezioneDescription();
                        break;
                    case ElementType.Sottosezione:
                        description = GenerateSottosezioneDescription();
                        break;
                }

                if (!string.IsNullOrEmpty(description))
                {
                    txtDescrizione.Text = description;
                }
            }
            catch (Exception)
            {
                // Ignore errors in auto-description generation
            }
        }

        private string GenerateParteMacchinaDescription()
        {
            var tipoItem = cmbTipoParteMacchina.SelectedItem as ComboBoxItem;
            var sottoProgettoItem = cmbSottoProgetto.SelectedItem as ComboBoxItem;

            if (tipoItem != null)
            {
                string base_desc = tipoItem.Text;
                string codice = txtCodice.Text;

                return $"{base_desc} {codice}";
            }

            return string.Empty;
        }

        private string GenerateSezioneDescription()
        {
            string codice = txtCodice.Text;
            return $"SEZ. {codice}";
        }

        private string GenerateSottosezioneDescription()
        {
            string codice = txtCodice.Text;
            return $"SOTTOSEZ. {codice}";
        }

        #endregion

        #region Validation and Save

        private void ValidateForm()
        {
            bool isValid = true;

            // Reset field colors
            ResetFieldColors();

            // Required field validations
            if (string.IsNullOrWhiteSpace(txtCodice.Text))
            {
                isValid = false;
                SetFieldError(txtCodice, "Il codice è obbligatorio");
            }

            if (string.IsNullOrWhiteSpace(txtDescrizione.Text))
            {
                isValid = false;
                SetFieldError(txtDescrizione, "La descrizione è obbligatoria");
            }

            okButton.Enabled = isValid;
        }

        private bool ValidateAndSave()
        {
            try
            {
                ValidateForm();

                if (!okButton.Enabled)
                {
                    MessageBox.Show("Correggi gli errori evidenziati prima di continuare.",
                                  "Errori di validazione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (isEditMode)
                {
                    UpdateElement();
                }
                else
                {
                    CreateElement();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il salvataggio: {ex.Message}",
                              "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CreateElement()
        {
            switch (elementType)
            {
                case ElementType.ParteMacchina:
                    CreateParteMacchina();
                    break;
                case ElementType.Sezione:
                    CreateSezione();
                    break;
                case ElementType.Sottosezione:
                    CreateSottosezione();
                    break;
                case ElementType.Montaggio:
                    CreateMontaggio();
                    break;
                case ElementType.Gruppo:
                    CreateGruppo();
                    break;
            }
        }

        private void CreateParteMacchina()
        {
            var sottoProgettoItem = (ComboBoxItem)cmbSottoProgetto.SelectedItem;
            var tipoParteItem = (ComboBoxItem)cmbTipoParteMacchina.SelectedItem;

            var parte = new ParteMacchina
            {
                ProgettoId = parentId,
                SottoProgetto = sottoProgettoItem?.Text ?? string.Empty,
                TipoParteMacchina = tipoParteItem?.Text ?? string.Empty,
                CodiceParteMacchina = txtCodice.Text.Trim(),
                Descrizione = txtDescrizione.Text.Trim(),
                RevisioneInserimento = cmbRevisione.Text,
                Stato = txtStato.Text,
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertParteMacchina(parte);
            parte.Id = newId;
            currentElement = parte;

            MessageBox.Show("Parte macchina creata con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateSezione()
        {
            var sezione = new Sezione
            {
                ParteMacchinaId = parentId,
                CodiceSezione = txtCodice.Text.Trim(),
                Descrizione = txtDescrizione.Text.Trim(),
                Quantita = (int)numQuantita.Value,
                RevisioneInserimento = cmbRevisione.Text,
                Stato = txtStato.Text,
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertSezione(sezione);
            sezione.Id = newId;
            currentElement = sezione;

            MessageBox.Show("Sezione creata con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateSottosezione()
        {
            var sottosezione = new Sottosezione
            {
                SezioneId = parentId,
                CodiceSottosezione = txtCodice.Text.Trim(),
                Descrizione = txtDescrizione.Text.Trim(),
                Quantita = (int)numQuantita.Value,
                RevisioneInserimento = cmbRevisione.Text,
                Stato = txtStato.Text,
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertSottosezione(sottosezione);
            sottosezione.Id = newId;
            currentElement = sottosezione;

            MessageBox.Show("Sottosezione creata con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateMontaggio()
        {
            var montaggio = new Montaggio
            {
                SottosezioneId = parentId,
                CodiceMontaggio = txtCodice.Text.Trim(),
                Descrizione = txtDescrizione.Text.Trim(),
                Quantita = (int)numQuantita.Value,
                RevisioneInserimento = cmbRevisione.Text,
                Stato = txtStato.Text,
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertMontaggio(montaggio);
            montaggio.Id = newId;
            currentElement = montaggio;

            MessageBox.Show("Montaggio creato con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateGruppo()
        {
            var tipoGruppoItem = (ComboBoxItem)cmbTipoGruppo.SelectedItem;

            var gruppo = new Gruppo
            {
                MontaggioId = parentId,
                CodiceGruppo = txtCodice.Text.Trim(),
                TipoGruppo = tipoGruppoItem?.Value ?? "GRUPPO",
                Descrizione = txtDescrizione.Text.Trim(),
                Quantita = (int)numQuantita.Value,
                RevisioneInserimento = cmbRevisione.Text,
                Stato = txtStato.Text,
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertGruppo(gruppo);
            gruppo.Id = newId;
            currentElement = gruppo;

            MessageBox.Show("Gruppo creato con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateElement()
        {
            // Update logic would be implemented here for edit mode
            MessageBox.Show("Funzionalità di modifica da implementare",
                          "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetFieldColors()
        {
            txtCodice.BackColor = System.Drawing.SystemColors.Window;
            txtDescrizione.BackColor = System.Drawing.SystemColors.Window;
        }

        private void SetFieldError(Control field, string message)
        {
            field.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);

            var toolTip = new ToolTip();
            toolTip.SetToolTip(field, message);
        }

        #endregion

        #region Public Static Methods

        public static object ShowCreateDialog(ElementType type, int parentId, string parentCode = null, IWin32Window owner = null)
        {
            using (var form = new ElementForm(type, parentId, parentCode))
            {
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                return result == DialogResult.OK ? form.Element : null;
            }
        }

        public static object ShowEditDialog(ElementType type, object element, IWin32Window owner = null)
        {
            if (element == null) return null;

            // Extract parent ID based on element type
            int parentId = GetParentIdFromElement(element);

            using (var form = new ElementForm(type, parentId, null, element))
            {
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                return result == DialogResult.OK ? form.Element : null;
            }
        }

        private static int GetParentIdFromElement(object element)
        {
            return element switch
            {
                ParteMacchina pm => pm.ProgettoId,
                Sezione s => s.ParteMacchinaId,
                Sottosezione ss => ss.SezioneId,
                Montaggio m => m.SottosezioneId,
                Gruppo g => g.MontaggioId,
                _ => 0
            };
        }

        #endregion
    }
}