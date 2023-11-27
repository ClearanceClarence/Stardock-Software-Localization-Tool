namespace Stardock_Software_Localization_Tool
{
    partial class LanguageEditorForm
    {
        private void BtnLoadFile_Click(object? sender, EventArgs e)
        {
            openFileDialog.Filter = "Language files (*.lng)|*.lng|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadLanguageFile(openFileDialog.FileName);
                statusLabel.Text = "File loaded: " + openFileDialog.FileName;
                isFileLoaded = true;
                UpdateButtonStates();
            }
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

        private void BtnCloseFile_Click(object? sender, EventArgs e)
        {
            CloseFile();
        }

        private void CloseFile()
        {
            dataGridView.Rows.Clear();
            ResetTranslatorInfoFields();
            progressBar.Value = 0;
            lblTranslationProgress.Text = "Translation Progress: ";
            statusLabel.Text = "No file loaded.";
            isFileLoaded = false;
            UpdateButtonStates();
        }
    }
}