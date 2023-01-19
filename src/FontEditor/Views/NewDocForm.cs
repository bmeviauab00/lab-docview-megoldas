namespace FontEditor
{
    /// <summary>
    /// Új betűtípus paramétereinek (betűtípus neve) megadására szolgál.
    /// </summary>
    public partial class NewDocForm : Form
    {
        readonly string[] docNames;

        public NewDocForm(string[] docNames)
        {
            InitializeComponent();
            this.docNames = docNames;

        }

        public string FontName
        {
            get { return tbFontName.Text; }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (docNames.Contains(tbFontName.Text))
            {
                MessageBox.Show("Már létezik ilyen nevű dokumentum");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
