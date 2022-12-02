namespace FontEditor
{
    /// <summary>
    /// Új betűtípus paramétereinek (betűtípus neve) megadására szolgál.
    /// </summary>
    public partial class NewDocForm : Form
    {
        public NewDocForm()
        {
            InitializeComponent();
        }

        public string FontName => tbFontName.Text;
    }
}
