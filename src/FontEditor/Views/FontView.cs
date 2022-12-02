using FontEditor.Documents;

namespace FontEditor.Views
{
    /// <summary>
    /// Helper osztály a rajzolás kódduplikációjának elkerülése érdekében
    /// </summary>
    public static class FontViewHelper
    {
        public static void DrawFont(Graphics g, CharDef charDef, int offsetX, int offsetY, int zoom)
        {
            for (int y = 0; y < CharDef.FontSize.Height; y++)
            {
                for (int x = 0; x < CharDef.FontSize.Width; x++)
                {
                    g.FillRectangle(
                        charDef.Pixels[x, y] ? Brushes.Yellow : Brushes.Black,
                        zoom * x + offsetX, zoom * y + offsetY, zoom, zoom);
                }
            }
        }
    }
}
