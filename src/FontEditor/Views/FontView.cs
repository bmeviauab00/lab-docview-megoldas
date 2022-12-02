using FontEditor.Documents;

namespace FontEditor.Views
{
    public partial class FontView : UserControl
    {
        private readonly CharDef charDef;

        public FontView(CharDef charDef)
        {
            InitializeComponent();

            this.charDef = charDef;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
        }
    }
}
