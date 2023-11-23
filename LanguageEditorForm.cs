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
            dataGridView.Columns.Add("originalText", "Original Text");
            dataGridView.Columns.Add("translatedText", "Translated Text");
            toolTip.SetToolTip(dataGridView, "Double-click a cell to edit");

            btnLoadFile = new Button { Text = "Load File", Size = new Size(100, 30) };
            btnLoadFile.Click += BtnLoadFile_Click;
            toolTip.SetToolTip(btnLoadFile, "Load a .lng file");

            btnSaveFile = new Button { Text = "Save File", Size = new Size(100, 30) };
            btnSaveFile.Click += BtnSaveFile_Click;
            toolTip.SetToolTip(btnSaveFile, "Save the .lng file");

            lblTranslationProgress = new Label
            {
                Text = "Translation Progress",
                Size = new Size(200, 30)
            };

            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "lng",
                Filter = "Language files (*.lng)|*.lng"
            };

            FlowLayoutPanel bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.LeftToRight,
                Height = 50
            };
            bottomPanel.Controls.Add(btnLoadFile);
            bottomPanel.Controls.Add(btnSaveFile);
            bottomPanel.Controls.Add(lblTranslationProgress);

            Controls.Add(dataGridView);
            Controls.Add(bottomPanel);

            progressBar = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 20
            };
            Controls.Add(progressBar);
            
            btnLoadFile = new Button { Text = "Load File", Size = new Size(100, 30) };
            btnLoadFile.Click += BtnLoadFile_Click;
            StyleButton(btnLoadFile, Color.FromArgb(30, 144, 255), Color.FromArgb(65, 105, 225)); // DodgerBlue, RoyalBlue

            btnSaveFile = new Button { Text = "Save File", Size = new Size(100, 30) };
            btnSaveFile.Click += BtnSaveFile_Click;
            StyleButton(btnSaveFile, Color.FromArgb(50, 205, 50), Color.FromArgb(60, 179, 113)); // LimeGreen, MediumSeaGreen
        }


        private void InitializeTranslatorInfoFields()
        {
            lblTranslatorName = new Label { Text = "Translator Name:", AutoSize = true };
            txtTranslatorName = new TextBox { Width = 200 };

            lblTranslatorEmail = new Label { Text = "Translator Email:", AutoSize = true };
            txtTranslatorEmail = new TextBox { Width = 200 };

            lblTranslatorGitHub = new Label { Text = "Translator GitHub:", AutoSize = true };
            txtTranslatorGitHub = new TextBox { Width = 200 };

            lblTargetSoftware = new Label { Text = "Target Software:", AutoSize = true };
            cboTargetSoftware = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            PopulateTargetSoftwareComboBox();

            TableLayoutPanel topPanel = new TableLayoutPanel
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
            cboTargetSoftware.Items.AddRange(new object[] { "Software 1", "Software 2", "Software 3" });
            cboTargetSoftware.SelectedIndex = 0; // Set the default selected item
        }


        private void ApplyAdvancedTheming()
        {
            Color backgroundColor = Color.WhiteSmoke;
            Color primaryColor = Color.FromArgb(211, 211, 211); // Light gray

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

        private void StyleButton(Button button, Color backgroundColor, Color hoverColor)
        {
            // Base styling
            button.BackColor = backgroundColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;

            // Hover effects
            button.MouseEnter += (_, _) => button.BackColor = hoverColor;
            button.MouseLeave += (_, _) => button.BackColor = backgroundColor;
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
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("#"))
                {
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
                    int rowIndex = dataGridView.Rows.Add(parts[0], parts[1]);
                    UpdateRowColor(rowIndex, parts[0], parts[1]);
                }
            }
            UpdateTranslationProgress();
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
                    lines.Add(row.Cells[0].Value + "=" + row.Cells[1].Value);
                }
            }
            File.WriteAllLines(filePath, lines);
            statusLabel.Text = "File saved successfully.";
        }

        private void UpdateTranslationProgress()
        {
            int totalStrings = dataGridView.Rows.Count;
            int translatedStrings = dataGridView.Rows.Cast<DataGridViewRow>()
                .Count(row => row.Cells[0].Value?.ToString() != row.Cells[1].Value?.ToString());

            lblTranslationProgress.Text = $"{translatedStrings} of {totalStrings} strings translated " +
                                          $"({(totalStrings > 0 ? (int)(100 * translatedStrings / (float)totalStrings) : 0)}%)";
            statusLabel.Text = "Translation progress updated.";
        }

        private void UpdateRowColor(int rowIndex, string original, string translated)
        {
            Color textColor = original == translated ? Color.Red : Color.Green;
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
        
    }
}
