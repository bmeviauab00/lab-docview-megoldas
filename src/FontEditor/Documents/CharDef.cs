namespace FontEditor.Documents
{
    /// <summary>
    /// Egy adott betűtípus adott karakterének pixeljeit tartalmazza. 
    /// </summary>
    public class CharDef
    {
        /// <summary>
        /// A karakter, melynek pixeljeit a CharDef tartalmazza.
        /// </summary>
        private readonly char character;

        public CharDef(char c)
        {
            character = c;

            // Itt a Font, Bitmap és Graphics osztály nem a tényleges rajzoláshoz szükséges,
            // csak a dokumentum állapotát (pixeleket) inicializálja a megadott font alapján.
            var f = new Font("Arial", 15);
            var bmp = new Bitmap(FontSize.Width, FontSize.Height);
            var g = Graphics.FromImage(bmp);
            g.DrawString(c.ToString(), f, Brushes.White, 0, 0);
            for (int y = 0; y < FontSize.Height; y++)
            {
                for (int x = 0; x < FontSize.Width; x++)
                {
                    var color = bmp.GetPixel(x, y);
                    // Ez egy nagyon egyszerűsített font pixel reprezentáció
                    Pixels[x, y] = color.R != 0 || color.G != 0 || color.B != 0;
                }
            }
        }

        /// <summary>
        /// Klónozáshoz használható csak ebben az osztályban látható konstruktor
        /// </summary>
        private CharDef(char c, bool[,] pixels)
            : this(c)
        {
            Pixels = pixels;
        }

        /// <summary>
        /// A karakterek mérete.
        /// Egyszerűsítés: minden betű 15*20 pixeles.
        /// </summary>
        public static Size FontSize { get; } = new Size(15, 20);

        /// <summary>
        /// A betűdefiníció pixeljeit tartalmazza.
        /// </summary>
        public bool[,] Pixels { get; } = new bool[FontSize.Width, FontSize.Height];

        public CharDef Clone()
        {
            // Klónozzuk a tömböt. Vigyázat, shallow copy-t készít, de mivel itt érték típusú
            // elemek vannak (bool), ezért ez jó nekünk.
            return new CharDef(character, (bool[,])Pixels.Clone());
        }
    }
}
