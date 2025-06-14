using System;
using System.Linq;
using System.Windows.Forms;
using DistintaTecnica.Data;
using DistintaTecnica.Models;

namespace DistintaTecnica.Forms
{
    public partial class ProjectForm : Form
    {
        private Repository repository;
        private Progetto currentProject;
        private bool isEditMode;

        public Progetto Project => currentProject;

        public ProjectForm() : this(null)
        {
        }

        public ProjectForm(Progetto progetto)
        {
            InitializeComponent();

            // Initialize repository
            var dbManager = new DatabaseManager();
            repository = new Repository(dbManager);

            currentProject = progetto;
            isEditMode = progetto != null;

            InitializeFormAfterDesigner();
            SetupEventHandlers();

            if (isEditMode)
            {
                LoadProjectData();
            }
        }

        #region Initialization

        private void InitializeFormAfterDesigner()
        {
            if (isEditMode)
            {
                this.Text = "Modifica Progetto";
                this.titleLabel.Text = "MODIFICA PROGETTO";
                this.subtitleLabel.Text = "Modifica le informazioni del progetto";
                this.okButton.Text = "Salva Modifiche";
            }
            else
            {
                this.Text = "Nuovo Progetto";
                this.titleLabel.Text = "NUOVO PROGETTO";
                this.subtitleLabel.Text = "Inserisci le informazioni base del progetto";
                this.okButton.Text = "Crea Progetto";
            }

            // Set default date and revision
            this.dtpDataInserimento.Value = DateTime.Now;
            this.cmbRevisione.SelectedIndex = 0; // Default to "A"

            // Set focus to first field
            this.txtCommessa.Focus();
        }

        private void SetupEventHandlers()
        {
            // Button events
            this.okButton.Click += OkButton_Click;
            this.cancelButton.Click += CancelButton_Click;

            // Validation events
            this.txtCommessa.TextChanged += Field_TextChanged;
            this.txtCliente.TextChanged += Field_TextChanged;
            this.txtDisegnatore.TextChanged += Field_TextChanged;

            // Enter key handling
            this.txtCommessa.KeyDown += TextBox_KeyDown;
            this.txtCliente.KeyDown += TextBox_KeyDown;
            this.txtDisegnatore.KeyDown += TextBox_KeyDown;

            // Form events
            this.Load += ProjectForm_Load;
        }

        private void LoadProjectData()
        {
            if (currentProject != null)
            {
                txtCommessa.Text = currentProject.NumeroCommessa;
                txtCliente.Text = currentProject.Cliente;
                dtpDataInserimento.Value = currentProject.DataInserimento;
                txtDisegnatore.Text = currentProject.NomeDisegnatore;

                // Set revision
                int revIndex = Array.IndexOf(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" },
                                           currentProject.LetteraRevisioneInserimento);
                if (revIndex >= 0)
                    cmbRevisione.SelectedIndex = revIndex;

                txtNote.Text = currentProject.Note ?? string.Empty;
            }
        }

        #endregion

        #region Event Handlers

        private void ProjectForm_Load(object sender, EventArgs e)
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

        private void Field_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
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

        #region Validation and Save

        private void ValidateForm()
        {
            bool isValid = true;
            string errorMessage = string.Empty;

            // Reset all field colors
            ResetFieldColors();

            // Required field validations
            if (string.IsNullOrWhiteSpace(txtCommessa.Text))
            {
                isValid = false;
                SetFieldError(txtCommessa, "Il numero commessa è obbligatorio");
            }
            else if (txtCommessa.Text.Length < 3)
            {
                isValid = false;
                SetFieldError(txtCommessa, "Il numero commessa deve essere almeno 3 caratteri");
            }

            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                isValid = false;
                SetFieldError(txtCliente, "Il cliente è obbligatorio");
            }

            if (string.IsNullOrWhiteSpace(txtDisegnatore.Text))
            {
                isValid = false;
                SetFieldError(txtDisegnatore, "Il nome disegnatore è obbligatorio");
            }

            // Check for duplicate project number (only in create mode or if number changed)
            if (!isEditMode || (isEditMode && currentProject.NumeroCommessa != txtCommessa.Text))
            {
                if (IsProjectNumberExists(txtCommessa.Text))
                {
                    isValid = false;
                    SetFieldError(txtCommessa, "Numero commessa già esistente");
                }
            }

            okButton.Enabled = isValid;
        }

        private bool ValidateAndSave()
        {
            try
            {
                // Final validation
                ValidateForm();

                if (!okButton.Enabled)
                {
                    MessageBox.Show("Correggi gli errori evidenziati prima di continuare.",
                                  "Errori di validazione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Create or update project
                if (isEditMode)
                {
                    UpdateProject();
                }
                else
                {
                    CreateProject();
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

        private void CreateProject()
        {
            currentProject = new Progetto
            {
                NumeroCommessa = txtCommessa.Text.Trim(),
                Cliente = txtCliente.Text.Trim(),
                DataInserimento = dtpDataInserimento.Value.Date,
                NomeDisegnatore = txtDisegnatore.Text.Trim(),
                LetteraRevisioneInserimento = cmbRevisione.SelectedItem.ToString(),
                Note = txtNote.Text.Trim()
            };

            int newId = repository.InsertProgetto(currentProject);
            currentProject.Id = newId;

            MessageBox.Show("Progetto creato con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateProject()
        {
            currentProject.NumeroCommessa = txtCommessa.Text.Trim();
            currentProject.Cliente = txtCliente.Text.Trim();
            currentProject.DataInserimento = dtpDataInserimento.Value.Date;
            currentProject.NomeDisegnatore = txtDisegnatore.Text.Trim();
            currentProject.LetteraRevisioneInserimento = cmbRevisione.SelectedItem.ToString();
            currentProject.Note = txtNote.Text.Trim();

            repository.UpdateProgetto(currentProject);

            MessageBox.Show("Progetto aggiornato con successo!",
                          "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsProjectNumberExists(string numeroCommessa)
        {
            if (string.IsNullOrWhiteSpace(numeroCommessa))
                return false;

            try
            {
                var progetti = repository.GetAllProgetti();
                return progetti.Any(p => p.NumeroCommessa.Equals(numeroCommessa, StringComparison.OrdinalIgnoreCase) &&
                                        (!isEditMode || p.Id != currentProject.Id));
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region UI Helper Methods

        private void ResetFieldColors()
        {
            txtCommessa.BackColor = System.Drawing.SystemColors.Window;
            txtCliente.BackColor = System.Drawing.SystemColors.Window;
            txtDisegnatore.BackColor = System.Drawing.SystemColors.Window;
        }

        private void SetFieldError(Control field, string message)
        {
            field.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);

            // You could also show tooltip with error message
            var toolTip = new ToolTip();
            toolTip.SetToolTip(field, message);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the form as a dialog for creating a new project
        /// </summary>
        public static Progetto ShowCreateDialog(IWin32Window owner = null)
        {
            using (var form = new ProjectForm())
            {
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                return result == DialogResult.OK ? form.Project : null;
            }
        }

        /// <summary>
        /// Shows the form as a dialog for editing an existing project
        /// </summary>
        public static Progetto ShowEditDialog(Progetto progetto, IWin32Window owner = null)
        {
            if (progetto == null) return null;

            using (var form = new ProjectForm(progetto))
            {
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);
                return result == DialogResult.OK ? form.Project : null;
            }
        }

        #endregion
    }
}