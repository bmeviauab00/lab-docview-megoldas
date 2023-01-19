using FontEditor.Views;

namespace FontEditor.Documents
{
    /// <summary>
    /// Egy adott betűtípus adott karakterének pixeljeit tartalmazza.
    /// Független mindennemű megjelenítéstől, a megjelenítéshez kapcsolódó ré
    /// </summary>
    public class CharDef
    {
        /// <summary>
        /// A karakter, melynek pixeljeit a CharDef tartalmazza.
        /// </summary>
        private readonly char character;

        /// <summary>
        /// A karakter pixeljeit inicializálja egy beépített Windows betűtípus alapján.
        /// </summary>
        public CharDef(char c): this(c, CharDefViewModel.BuildCharDefPixels(c, Size)) { }

        /// <summary>
        /// Klónozáshoz használható csak ebben az osztályban látható konstruktor.
        /// </summary>
        private CharDef(char c, bool[,] pixels)
        {
            character = c;
            Pixels = pixels;
        }

        /// <summary>
        /// A karakterek mérete.
        /// Egyszerűsítés: minden betű 15*20 pixeles.
        /// </summary>
        public static Size Size { get; } = new Size(15, 20);

        /// <summary>
        /// A betűdefiníció pixeljeit tartalmazza.
        /// </summary>
        public bool[,] Pixels { get; }

        public CharDef Clone()
        {
            // Klónozzuk a tömböt. Vigyázat, shallow copy-t készít, de mivel itt érték típusú
            // elemek vannak (bool), ezért ez jó nekünk.
            return new CharDef(character, (bool[,])Pixels.Clone());
        }
    }
}
