using System.Xml.Linq;

namespace Stardock_Software_Localization_Tool
{
    partial class LanguageEditorForm
    {
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
            isFileLoaded = true;
            UpdateButtonStates();
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

        private static string AddQuotationMarks(string? input)
        {
            return $"\"{input}\"";
        }

        private static string RemoveQuotationMarks(string input)
        {
            return input.Trim('"');
        }
    }
}