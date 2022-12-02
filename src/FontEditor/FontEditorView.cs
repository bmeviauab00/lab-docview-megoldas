using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FontEditor
{
    /// <summary>
    /// A karakterek szerkesztőnézete. Egyrészt UserControl, másrészt nézet is.
    /// </summary>
    public partial class FontEditorView : UserControl, IView
    {
        public FontEditorView()
        {
            InitializeComponent();
        }

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
        const int offsetY = 20;

        public FontEditorView(char editedChar, FontEditorDocument document)
        {
            InitializeComponent();
            lblEditedChar.Text = editedChar.ToString();
            this.editedChar = editedChar;
            this.document = document;
        }

        /// <summary>
        /// A View interfész Update műveletánek implementációja.
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

            CharDef editedCharDef = document.GetCharDef(editedChar);

            for (int y = 0; y < CharDef.FontSize.Height; y++)
            {
                for (int x = 0; x < CharDef.FontSize.Width; x++)
                {
                    e.Graphics.FillRectangle(
                        editedCharDef.Pixels[x,y] ? Brushes.Yellow: Brushes.Black,
                        zoom * x, offsetY + zoom * y, zoom, zoom);
                }
            }
        }

        /// <summary>
        /// A zoom in gomb eseménykezelője.
        /// </summary>
        private void bZoomIn_Click(object sender, EventArgs e)
        {
            zoom++;
            Invalidate();
        }

        /// <summary>
        /// A zoom out gomb eseménykezelője.
        /// </summary>
        private void bZoomOut_Click(object sender, EventArgs e)
        {
            if (zoom == 0)
                return;
            zoom--;
            Invalidate();
        }

        /// <summary>
        /// Egérkattintás eseménykezelője. A felhasználó egy adott pixelen az egérrel kattintva
        /// invertálhatja a színt.
        /// </summary>
        private void FontEditorView_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X/zoom;
            int y = (e.Y - offsetY)/zoom;
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
