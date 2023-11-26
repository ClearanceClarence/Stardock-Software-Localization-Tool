using System.Xml.Linq;
using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Stardock_Software_Localization_Tool
{
    public partial class LanguageEditorForm : Form
    {
        private DataGridView dataGridView;
        private Button btnLoadFile, btnSaveFile;
        private Label lblTranslationProgress;
        private ProgressBar progressBar;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private ToolTip toolTip;
        private TextBox txtTranslatorName, txtTranslatorEmail, txtTranslatorGitHub;
        private ComboBox cboTargetSoftware;
        private Label lblTranslatorName, lblTranslatorEmail, lblTranslatorGitHub, lblTargetSoftware;

        public LanguageEditorForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            InitializeTranslatorInfoFields();
            ApplyAdvancedTheming();
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "Assets", "Images", "icon.ico"));

            dataGridView!.CellValueChanged += DataGridView_CellValueChanged;
        }

        private void DataGridView_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            // Update the row color for the changed cell
            if (e.ColumnIndex == dataGridView.Columns["translatedText"]!.Index)
            {
                var originalText = dataGridView.Rows[e.RowIndex].Cells["originalText"].Value?.ToString();
                var translatedText = dataGridView.Rows[e.RowIndex].Cells["translatedText"].Value?.ToString();
                UpdateRowColor(e.RowIndex, originalText!, translatedText!);
            }

            // Update the translation progress
            UpdateTranslationProgress();
        }

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

            Button btnExportToXml = new Button { Text = "Export to XML", Size = new Size(120, 30) };
            btnExportToXml.Click += BtnExportToXml_Click; // Ensure this event handler is implemented

            Button btnImportFromXml = new Button { Text = "Import from XML", Size = new Size(120, 30) };
            btnImportFromXml.Click += BtnImportFromXml_Click; // Ensure this event handler is implemented

            // Button panel
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                Height = 40,
                Padding = new Padding(0, 5, 0, 0)
            };

            buttonPanel.Controls.Add(btnLoadFile);
            buttonPanel.Controls.Add(btnSaveFile);
            buttonPanel.Controls.Add(btnExportToXml);
            buttonPanel.Controls.Add(btnImportFromXml);

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

            progressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 20
            };

            // Add controls to the form
            Controls.Add(dataGridView);
            Controls.Add(buttonPanel);
            Controls.Add(lblTranslationProgress);
            Controls.Add(progressBar);
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


        private void ApplyAdvancedTheming()
        {
            var backgroundColor = Color.WhiteSmoke;
            var primaryColor = Color.FromArgb(211, 211, 211); // Light gray

            BackColor = backgroundColor;
            Font = new Font("Segoe UI", 9);

            dataGridView.BackgroundColor = backgroundColor;
            dataGridView.GridColor = primaryColor;
            dataGridView.DefaultCellStyle.BackColor = backgroundColor;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView.EnableHeadersVisualStyles = false; // To apply custom styles

            statusStrip.BackColor = primaryColor;
            statusStrip.ForeColor = Color.Black;
            statusLabel.Font = new Font("Segoe UI", 9);
        }


        private void BtnLoadFile_Click(object? sender, EventArgs e)
        {
            openFileDialog.Filter = "Language files (*.lng)|*.lng|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadLanguageFile(openFileDialog.FileName);
                statusLabel.Text = "File loaded: " + openFileDialog.FileName;
            }
        }
        
        private void LoadLanguageFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            dataGridView.Rows.Clear();
            ResetTranslatorInfoFields();

            foreach (var line in lines)
            {
                // Skip empty lines, lines with specific empty key-value pairs, or comment lines
                if (string.IsNullOrWhiteSpace(line) 
                    || line.Trim().Equals("\"  \"=\"  \"") 
                    || line.Trim().Equals("\" \"=\" \"")
                    || line.StartsWith(";"))
                    continue;

                if (line.StartsWith("#"))
                {
                    // Handle comment lines
                    if (line.Contains("Translator Name:"))
                        txtTranslatorName.Text = ExtractCommentData(line);
                    else if (line.Contains("Translator Email:"))
                        txtTranslatorEmail.Text = ExtractCommentData(line);
                    else if (line.Contains("Translator GitHub:"))
                        txtTranslatorGitHub.Text = ExtractCommentData(line);
                    else if (line.Contains("Target Software:"))
                        SelectComboBoxItemByText(cboTargetSoftware, ExtractCommentData(line));

                    continue;
                }

                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    var originalText = RemoveQuotationMarks(parts[0]);
                    var translatedText = RemoveQuotationMarks(parts[1]);

                    // Check if both original and translated text are not empty
                    if (!string.IsNullOrWhiteSpace(originalText) && !string.IsNullOrWhiteSpace(translatedText))
                    {
                        var rowIndex = dataGridView.Rows.Add(originalText, translatedText);
                        UpdateRowColor(rowIndex, originalText, translatedText);
                    }
                }
            }
            UpdateTranslationProgress();
        }


        private string RemoveQuotationMarks(string input)
        {
            return input.Trim('"');
        }

        private void BtnSaveFile_Click(object? sender, EventArgs e)
        {
            saveFileDialog.Filter = "Language files (*.lng)|*.lng|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLanguageFile(saveFileDialog.FileName);
                statusLabel.Text = "File saved: " + saveFileDialog.FileName;
            }
        }


        private void SaveLanguageFile(string filePath)
        {
            var lines = new List<string>
            {
                $"# Translator Name: {txtTranslatorName.Text}",
                $"# Translator Email: {txtTranslatorEmail.Text}",
                $"# Translator GitHub: {txtTranslatorGitHub.Text}",
                $"# Target Software: {cboTargetSoftware.SelectedItem}",
                ""
            };

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var originalText = AddQuotationMarks(row.Cells[0].Value.ToString());
                    var translatedText = AddQuotationMarks(row.Cells[1].Value.ToString());

                    lines.Add($"{originalText}={translatedText}");
                }
            }

            File.WriteAllLines(filePath, lines);
            statusLabel.Text = "File saved successfully.";
        }

        private string AddQuotationMarks(string? input)
        {
            return $"\"{input}\"";
        }

        private void UpdateTranslationProgress()
        {
            var totalStrings = dataGridView.Rows.Count;
            var translatedStrings = dataGridView.Rows.Cast<DataGridViewRow>()
                .Count(row => row.Cells[0].Value?.ToString() != row.Cells[1].Value?.ToString());

            lblTranslationProgress.Text = $"{translatedStrings} of {totalStrings} strings translated " +
                                          $"({(totalStrings > 0 ? (int)(100 * translatedStrings / (float)totalStrings) : 0)}%)";
            statusLabel.Text = "Translation progress updated.";
        }

        private void UpdateRowColor(int rowIndex, string original, string translated)
        {
            var textColor = original == translated ? Color.Red : Color.Green;
            dataGridView.Rows[rowIndex].Cells[1].Style.ForeColor = textColor;
        }

        private string ExtractCommentData(string line)
        {
            return line.Substring(line.IndexOf(':') + 1).Trim();
        }

        private void SelectComboBoxItemByText(ComboBox comboBox, string text)
        {
            comboBox.SelectedIndex = comboBox.FindStringExact(text);
        }

        private void ResetTranslatorInfoFields()
        {
            txtTranslatorName.Clear();
            txtTranslatorEmail.Clear();
            txtTranslatorGitHub.Clear();
            cboTargetSoftware.SelectedIndex = -1;
        }

        private void ExportToXml(string filePath)
        {
            var xDocument = new XDocument();
            var root = new XElement("Translations");

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                    continue;

                var originalText = row.Cells[0].Value.ToString();
                var translatedText = row.Cells[1].Value.ToString();

                var translationElement = new XElement("Translation",
                    new XElement("Original", originalText),
                    new XElement("Translated", translatedText));

                root.Add(translationElement);
            }

            xDocument.Add(root);
            xDocument.Save(filePath);
        }

        private void ImportFromXml(string filePath)
        {
            var xDocument = XDocument.Load(filePath);
            var translations = xDocument.Descendants("Translation");

            dataGridView.Rows.Clear();

            foreach (var translation in translations)
            {
                var originalText = translation.Element("Original")?.Value.Trim();
                var translatedText = translation.Element("Translated")?.Value.Trim();

                // Skip empty translations or trivial key-value pairs
                if (string.IsNullOrWhiteSpace(originalText) 
                    || string.IsNullOrWhiteSpace(translatedText) 
                    || (originalText == " " && translatedText == " ")
                    || (originalText == "  " && translatedText == "  "))
                    continue;

                dataGridView.Rows.Add(originalText, translatedText);
            }

            UpdateTranslationProgress();
        }


        private void BtnExportToXml_Click(object? sender, EventArgs e)
        {
            SaveFileDialog saveXmlDialog = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                DefaultExt = "xml",
                AddExtension = true
            };

            if (saveXmlDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToXml(saveXmlDialog.FileName);
            }
        }

        private void BtnImportFromXml_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openXmlDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                DefaultExt = "xml"
            };

            if (openXmlDialog.ShowDialog() == DialogResult.OK)
            {
                ImportFromXml(openXmlDialog.FileName);
            }
        }
    }
}