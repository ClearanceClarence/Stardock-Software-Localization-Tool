using System.Xml.Linq;
using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Stardock_Software_Localization_Tool
{
    public partial class LanguageEditorForm : Form
    {
        private DataGridView dataGridView;
        private Button btnLoadFile, btnSaveFile, btnExportToXml, btnImportFromXml,  btnCloseFile;
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
        private bool isFileLoaded = false;

        public LanguageEditorForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            InitializeTranslatorInfoFields();
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "Assets", "Images", "icon.ico"));

            dataGridView!.CellValueChanged += DataGridView_CellValueChanged;
            UpdateButtonStates();
        }
        }
}