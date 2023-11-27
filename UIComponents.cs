using Newtonsoft.Json;

namespace Stardock_Software_Localization_Tool
{
    partial class LanguageEditorForm
    {
        
        private ProgressBar translationProgressBar;
        private void InitializeCustomComponents()
        {
            toolTip = new ToolTip();

            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusStrip.Items.Add(statusLabel);
            Controls.Add(statusStrip);

            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var originalTextColumn = new DataGridViewTextBoxColumn
            {
                Name = "originalText",
                HeaderText = "Original Text",
                ReadOnly = true
            };
            dataGridView.Columns.Add(originalTextColumn);

            var translatedTextColumn = new DataGridViewTextBoxColumn
            {
                Name = "translatedText",
                HeaderText = "Translated Text"
            };
            dataGridView.Columns.Add(translatedTextColumn);

            toolTip.SetToolTip(dataGridView, "Double-click a cell to edit");

            // Buttons
            btnLoadFile = new Button { Text = "Load File", Size = new Size(100, 30) };
            btnLoadFile.Click += BtnLoadFile_Click;
            toolTip.SetToolTip(btnLoadFile, "Load a .lng file");

            btnSaveFile = new Button { Text = "Save File", Size = new Size(100, 30) };
            btnSaveFile.Click += BtnSaveFile_Click;
            toolTip.SetToolTip(btnSaveFile, "Save the .lng file");

            btnExportToXml = new Button { Text = "Export to XML", Size = new Size(120, 30) };
            btnExportToXml.Click += BtnExportToXml_Click;
            toolTip.SetToolTip(btnExportToXml, "Export data to a XML file");

            btnImportFromXml = new Button { Text = "Import from XML", Size = new Size(120, 30) };
            btnImportFromXml.Click += BtnImportFromXml_Click;
            toolTip.SetToolTip(btnImportFromXml, "Import data from a XML file");
            
            btnCloseFile = new Button { Text = "Close File", Size = new Size(100, 30), Visible = false };
            btnCloseFile.Click += BtnCloseFile_Click;
            toolTip.SetToolTip(btnCloseFile, "Close the current file");


            // Button panel
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 5, 0, 0)
            };
            
            translationProgressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                BackColor = Color.Gray,
                ForeColor = Color.Green,
                Height = 20,
                Style = ProgressBarStyle.Continuous
            };

            buttonPanel.Controls.Add(btnLoadFile);
            buttonPanel.Controls.Add(btnSaveFile);
            buttonPanel.Controls.Add(btnExportToXml);
            buttonPanel.Controls.Add(btnImportFromXml);
            buttonPanel.Controls.Add(btnCloseFile);

            lblTranslationProgress = new Label
            {
                Text = "Translation Progress: ",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Bottom
            };

            openFileDialog = new OpenFileDialog
            {
                Filter = "Language files (*.lng)|*.lng|All files (*.*)|*.*"
            };

            saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "lng",
                Filter = "Language files (*.lng)|*.lng"
            };

            // Add controls to the form
            Controls.Add(dataGridView);
            Controls.Add(buttonPanel);
            Controls.Add(lblTranslationProgress);
            Controls.Add(translationProgressBar);
            Controls.Add(statusStrip);
        }

private void InitializeTranslatorInfoFields()
        {
            lblTranslatorName = new Label
            {
                Text = "Translator Name:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtTranslatorName = new TextBox { Width = 200 };

            lblTranslatorEmail = new Label
            {
                Text = "Translator Email:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtTranslatorEmail = new TextBox { Width = 200 };

            lblTranslatorGitHub = new Label
            {
                Text = "Translator GitHub:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            txtTranslatorGitHub = new TextBox { Width = 200 };

            lblTargetSoftware = new Label
            {
                Text = "Target Software:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            cboTargetSoftware = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            PopulateTargetSoftwareComboBox();

            var topPanel = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 2,
                Dock = DockStyle.Top,
                AutoSize = true
            };
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            topPanel.Controls.Add(lblTranslatorName, 0, 0);
            topPanel.Controls.Add(txtTranslatorName, 1, 0);
            topPanel.Controls.Add(lblTranslatorEmail, 2, 0);
            topPanel.Controls.Add(txtTranslatorEmail, 3, 0);
            topPanel.Controls.Add(lblTranslatorGitHub, 0, 1);
            topPanel.Controls.Add(txtTranslatorGitHub, 1, 1);
            topPanel.Controls.Add(lblTargetSoftware, 2, 1);
            topPanel.Controls.Add(cboTargetSoftware, 3, 1);

            Controls.Add(topPanel);
        }

        private void UpdateButtonStates()
        {
            btnSaveFile.Enabled = isFileLoaded;
            btnExportToXml.Enabled = isFileLoaded;
            btnImportFromXml.Enabled = isFileLoaded;
            btnCloseFile.Visible = isFileLoaded;
        }
        
        private void PopulateTargetSoftwareComboBox()
        {
            try
            {
                var filePath = Path.Combine(Application.StartupPath, "Resources", "Assets", "Files", "software.json");
                var jsonData = File.ReadAllText(filePath);
                var softwareList = JsonConvert.DeserializeObject<List<string>>(jsonData);

                cboTargetSoftware.Items.Clear();
                foreach (var software in softwareList!)
                {
                    cboTargetSoftware.Items.Add(software);
                }

                if (cboTargetSoftware.Items.Count > 0)
                    cboTargetSoftware.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading software list: {ex.Message}");
            }
        }
    }
}