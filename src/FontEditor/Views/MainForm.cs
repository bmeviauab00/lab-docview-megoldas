using FontEditor.Documents;

namespace FontEditor
{
    /// <summary>
    /// Az alkalmazás főablaka.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fontEditorDocument = App.Instance.NewDocument();
            if (fontEditorDocument == null)
                return;

            var name = fontEditorDocument.Name;

            // Egy új tabra felteszi a dokumentumhoz tartozó felületelemeket.
            // Ezeket egy UserControl, a FontDocumentControl fogja össze.
            // Így csak ebből kell egy példányt az új tabpage-re feltenni.
            // Az első paraméter egy kulcs, a második a tab falirata
            tcDocuments.TabPages.Add(name, name);

            var documentControl = new FontDocumentControl();
            var tp = tcDocuments.TabPages[name];
            tp.Controls.Add(documentControl);
            documentControl.Dock = DockStyle.Fill;

            // SampleTextView beregisztrálása a documentnél,
            // hogy értesüljön majd a dokumentum változásairól.
            documentControl.SampleTextView.AttachToDoc(fontEditorDocument);

            // Az új tab legyen a kiválasztott. 
            tcDocuments.SelectTab(tp);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.OpenDocument();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tcDocuments.TabCount == 0 || App.Instance.ActiveDocument == null)
                return;

            tcDocuments.TabPages.RemoveByKey(App.Instance.ActiveDocument.Name);

            App.Instance.CloseActiveDocument();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.SaveActiveDocument();
        }

        private void tcDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcDocuments.TabCount == 0)
                return;

            App.Instance.UpdateActiveDocument(tcDocuments.SelectedTab.Name);
        }
    }
}
