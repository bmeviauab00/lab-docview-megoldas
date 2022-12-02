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
    /// A karakterek mintaszöveg nézete. Egyrészt UserControl, másrészt nézet is.
    /// </summary>
    public partial class SampleTextView : UserControl, IView
    {
        public SampleTextView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A dokumentum, melynek adatait a nézet megjeleníti.
        /// </summary>
        private FontEditorDocument document;

        /// <summary>
        /// A dokumentum, melynek adatait a nézet megjeleníti.
        /// </summary>
        protected Document Document
        {
            get { return document; }
        }

        /// <summary>
        /// Az aktuálisan megjelenített mintaszöveg karakterei.
        /// </summary>
        private string sampleText = "abc";

        /// <summary>
        /// Hozzácsatolja a nézetet az adott dokumentumhoz. Így a dokumentum változásairól
        /// a nézetünk is értesülni fog.
        /// </summary>
        public void AttachToDoc(Document doc)
        {
            doc.AttachView(this);
            document = (FontEditorDocument)doc;
        }

        /// <summary>
        /// Beállítja a mintaszöveget.
        /// </summary>
        public void SetSampleText(String text)
        {
            sampleText = text.ToLower();
            Update();
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
            // Design módban
            if (document == null)
                return;

            int offsetX = 0;
            int zoom = 2;
            foreach(char c in sampleText)
            {
                CharDef charDef = document.GetCharDef(c);

                // A nem támogatott karaktereket ugorjuk át.
                if (charDef == null)
                    continue;

                for (int y = 0; y < CharDef.FontSize.Height; y++)
                {
                    for (int x = 0; x < CharDef.FontSize.Width; x++)
                    {
                        e.Graphics.FillRectangle(
                            charDef.Pixels[x, y] ? Brushes.Yellow : Brushes.Black,
                            zoom * x + offsetX, zoom * y, zoom, zoom);
                    }
                }
                offsetX += CharDef.FontSize.Width*zoom + 1;
            }
        }
    }
}
