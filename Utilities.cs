namespace Stardock_Software_Localization_Tool
{
    partial class LanguageEditorForm
    {
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

        private static string ExtractCommentData(string line)
        {
            return line[(line.IndexOf(':') + 1)..].Trim();
        }

        private static void SelectComboBoxItemByText(ComboBox comboBox, string text)
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