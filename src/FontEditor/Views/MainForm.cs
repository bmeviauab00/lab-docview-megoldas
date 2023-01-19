using FontEditor.Documents;

namespace FontEditor
{
    /// <summary>
    /// Az alkalmazás főablaka. A célunk az, hogy minél kevesebb logika legyen benne,
    /// inkább csak eseménykezelőket tartalmaz és a feladatokat detegálja a megfelelő
    /// felelősségű osztályok számára.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            App.Instance.Initialize(tcDocuments);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.NewDocument();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.OpenDocument();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.CloseActiveDocument();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.SaveActiveDocument();
        }
    }
}
