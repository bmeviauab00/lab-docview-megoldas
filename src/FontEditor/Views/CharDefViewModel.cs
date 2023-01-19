using FontEditor.Documents;

namespace FontEditor.Views
{
    public static class CharDefViewModel
    {
        /// <summary>
        /// A megadott karakterre egy kétdimenziós bool tömb pixelreprezentációt állít elő a megadott méretben,
        /// egy beépített betűtípus alapján.
        /// </summary>
        public static bool[,] BuildCharDefPixels(char c, Size size)
        {
            var pixels = new bool[size.Width, size.Height];
            // Itt a Font, Bitmap és Graphics osztály nem a tényleges rajzoláshoz szükséges,
            // csak a dokumentum állapotát (pixeleket) inicializálja a megadott font alapján.

            using Font BuilderFont = new Font("Arial", 15);
            using var bmp = new Bitmap(size.Width, size.Height);
            var g = Graphics.FromImage(bmp);
            g.DrawString(c.ToString(), BuilderFont, Brushes.White, 0, 0);
            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    var color = bmp.GetPixel(x, y);
                    // Ez egy nagyon egyszerűsített font pixel reprezentáció
                    pixels[x, y] = color.R != 0 || color.G != 0 || color.B != 0;
                }
            }
            return pixels;
        }

        /// <summary>
        /// CharDef rajzoló logika (a rajzolás kódduplikációjának elkerülése érdekében).
        /// </summary>
        public static void DrawFont(Graphics g, CharDef charDef, int offsetX, int offsetY, int zoom)
        {
            for (int y = 0; y < CharDef.Size.Height; y++)
            {
                for (int x = 0; x < CharDef.Size.Width; x++)
                {
                    g.FillRectangle(
                        charDef.Pixels[x, y] ? Brushes.Yellow : Brushes.Black,
                        zoom * x + offsetX, zoom * y + offsetY, zoom, zoom);
                }
            }
        }
    }
}
