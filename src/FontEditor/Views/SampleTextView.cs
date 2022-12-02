using FontEditor.Documents;
using FontEditor.DocView;

namespace FontEditor
{
    /// <summary>
    /// A karakterek mintaszöveg nézete. Egyrészt UserControl, másrészt nézet is.
    /// </summary>
    public partial class SampleTextView : UserControl, IView
    {
        /// <summary>
        /// Az aktuálisan megjelenített mintaszöveg karakterei.
        /// </summary>
        private string sampleText = "abc";

        /// <summary>
        /// A dokumentum, melynek adatait a nézet megjeleníti.
        /// </summary>
        private FontEditorDocument document = null!;

        public SampleTextView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Hozzácsatolja a nézetet az adott dokumentumhoz. Így a dokumentum változásairól
        /// a nézetünk is értesülni fog.
        /// </summary>
        public void AttachToDoc(FontEditorDocument doc)
        {
            doc.AttachView(this);
            document = doc;
        }

        /// <summary>
        /// Beállítja a mintaszöveget.
        /// </summary>
        public void SetSampleText(string text)
        {
            sampleText = text.ToLower();
            Update();
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
            // Design módban
            if (document == null)
                return;

            int offsetX = 0;
            int zoom = 2;
            foreach (char c in sampleText)
            {
                var charDef = document.GetCharDef(c);

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
                offsetX += CharDef.FontSize.Width * zoom + 1;
            }
        }
    }
}
