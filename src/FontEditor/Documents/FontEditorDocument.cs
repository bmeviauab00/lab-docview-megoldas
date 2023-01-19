using FontEditor.DocView;

namespace FontEditor.Documents
{
    /// <summary>
    /// FontEditor dokumentum, a Document-ből származik. Tartalmazza a dokumentum adatait (a
    /// fontDefs tagváltozóban) és műveleteket ezek lekérdezéséhez és manipulációjához.
    /// </summary>
    public class FontEditorDocument : Document
    {
        /// <summary>
        /// Az egyes karakterek leírását tartalmazza egy hashtáblában. A kulcs a karakter,
        /// az érték a hozzá tartozó karakterdefíníció.
        /// </summary>
        private readonly Dictionary<char, CharDef> charDefs = new Dictionary<char, CharDef>();
        private readonly CharDef charDefForUnsupportedChars = new CharDef('�');

        public FontEditorDocument(string name)
            : base(name)
        {
            for (char c = 'a'; c <= 'z'; c++)
                charDefs[c] = new CharDef(c);

            charDefs[' '] = new CharDef(' '); // A space is támogatott.
            charDefs['!'] = new CharDef('!'); // A ! is támogatott.
        }

        /// <summary>
        /// Visszaadja az adott karakterhez tartozó karakterdefíníciót (a klónját).
        /// Ha nem találja, akkor a charDefForUnsupportedChars klónjával tér vissza.
        /// Enélkül pl. a magyar ékezetes karaktareknél elesne az alkalmazás.
        /// </summary>
        public CharDef GetCharDef(char c)
        {
            var charDef = GetCharDefCore(c);
            return charDef != null ? charDef.Clone() : charDefForUnsupportedChars.Clone();
        }

        /// <summary>
        /// Visszaadja az adott karakterhez tartozó karakterdefíníciót.
        /// Ha nincs az adott karakterhez tartozó definíció, nullal tér vissza.
        /// </summary>
        private CharDef? GetCharDefCore(char c)
        {
            return !charDefs.TryGetValue(c, out var f) ? null : f;
        }

        /// <summary>
        /// Invertálja az adott karakterhez tartozó karakterdefíníció x, y koordinátájának pixelét.
        /// </summary>
        public void InvertCharDefPixel(char c, int x, int y)
        {
            var charDef = GetCharDefCore(c);
            if (charDef == null)
                return;

            charDef.Pixels[x, y] = !charDef.Pixels[x, y];

            UpdateAllViews();
        }

        /// <summary>
        /// Az adott karakterhez tartozó karakterdefíníció x, y koordinátájának színét állítja/törli
        /// a val bool paraméter függvényében.
        /// </summary>
        public void SetCharDefPixel(char c, int x, int y, bool value)
        {
            var charDef = GetCharDefCore(c);
            if (charDef == null)
                return;

            charDef.Pixels[x, y] = value;

            UpdateAllViews();
        }

        /// <summary>
        /// Az adott karakterhez tartozó karakterdefíníció valamennyi pixelét törli.
        /// </summary>
        public void ClearCharDef(char c)
        {
            var charDef = GetCharDefCore(c);
            if (charDef == null)
                return;

            for (int y = 0; y < CharDef.Size.Height; y++)
                for (int x = 0; x < CharDef.Size.Width; x++)
                    charDef.Pixels[x, y] = false;

            UpdateAllViews();
        }
    }
}
