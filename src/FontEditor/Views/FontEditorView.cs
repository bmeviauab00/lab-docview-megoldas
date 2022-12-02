using FontEditor.Documents;
using FontEditor.DocView;
using FontEditor.Views;

namespace FontEditor
{
    /// <summary>
    /// A karakterek szerkesztőnézete. Egyrészt UserControl, másrészt nézet is.
    /// </summary>
    public partial class FontEditorView : UserControl, IView
    {
        /// <summary>
        /// A dokumentum, melynek adatait a nézet megjeleníti.
        /// </summary>
        private FontEditorDocument document;

        /// <summary>
        /// Az adott nézetben szerkesztett karakter
        /// </summary>
        private char editedChar;

        /// <summary>
        /// Az adott nézet nagyítása.
        /// </summary>
        private int zoom = 5;
        private const int offsetY = 20;

        public FontEditorView(char editedChar, FontEditorDocument document)
        {
            InitializeComponent();

            lblEditedChar.Text = editedChar.ToString();
            this.editedChar = editedChar;
            this.document = document;
        }

        /// <summary>
        /// A IView interfész Update műveletánek implementációja.
        /// </summary>
        public void Update()
        {
            Invalidate();
        }

        /// <summary>
        /// A UserControl.Paint felüldefiniálása, ebben rajzolunk.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var editedCharDef = document.GetCharDef(editedChar);

            FontViewHelper.DrawFont(e.Graphics, editedCharDef, 0, offsetY, zoom);
        }

        /// <summary>
        /// A zoom in gomb eseménykezelője.
        /// </summary>
        private void bZoomIn_Click(object sender, EventArgs e)
        {
            zoom++;
            Update();
        }

        /// <summary>
        /// A zoom out gomb eseménykezelője.
        /// </summary>
        private void bZoomOut_Click(object sender, EventArgs e)
        {
            if (zoom == 0)
                return;

            zoom--;
            Update();
        }

        /// <summary>
        /// Egérkattintás eseménykezelője.
        /// A felhasználó egy adott pixelen az egérrel kattintva invertálhatja a színt.
        /// </summary>
        private void FontEditorView_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / zoom;
            int y = (e.Y - offsetY) / zoom;
            if (x >= CharDef.FontSize.Width)
                return;

            document.InvertCharDefPixel(editedChar, x, y);
        }

        /// <summary>
        /// A Clear gomb eseménykezelője.
        /// </summary>
        private void bClear_Click(object sender, EventArgs e)
        {
            document.ClearCharDef(editedChar);
        }
    }
}
