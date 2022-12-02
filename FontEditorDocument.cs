using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FontEditor
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
        Dictionary<char, CharDef> charDefs = new Dictionary<char, CharDef>();
        CharDef charDefForUnsupportedChars = new CharDef('�');

        public FontEditorDocument(string name): base(name)
        {
            for (char c = 'a'; c <= 'z'; c++)
                charDefs[c] = new CharDef(c);
            charDefs[' '] = new CharDef(' '); // A space is támogatott.
            charDefs['!'] = new CharDef('!'); // A ! is támogatott.
        }

        /// <summary>
        /// Visszaadja az adott karakterhez tartozó karakterdefíníciót (a klónját).
        /// Ha nem találja, akkor a charDefForUnsupportedChars klónjával tér vissza.
        /// Enélkül a magyar ékezetes karaktareknél pl. elesne az alkalmazás.
        /// </summary>
        public CharDef GetCharDef(char c)
        {
            var charDef = getCharDef(c);
            return charDef != null ? charDef.Clone() : charDefForUnsupportedChars.Clone();
        }

        /// <summary>
        /// Visszaadja az adott karakterhez tartozó karakterdefíníciót.
        /// Ha nincs az adott karakterhez tartozó definíció, nullal tér vissza.
        /// </summary>
        private CharDef getCharDef(char c)
        {
            CharDef f;
            if (!charDefs.TryGetValue(c, out f))
                return null;
            return f;
        }

        /// <summary>
        /// Invertálja az adott karakterhez tartozó karakterdefíníció x, y koordinátájának
        /// pixelét.
        /// </summary>
        public void InvertCharDefPixel(char c, int x, int y)
        {
            CharDef fd = getCharDef(c);
            if (fd == null)
                return;
            fd.Pixels[x, y] = !fd.Pixels[x, y];

            UpdateAllViews();
        }

        /// <summary>
        /// Az adott karakterhez tartozó karakterdefíníció x, y koordinátájának színét állítja/törli
        /// a val bool paraméter függvényében.
        /// </summary>
        public void SetCharDefPixel(char c, int x, int y, bool val)
        {
            CharDef fd = getCharDef(c);
            if (fd == null)
                return;
            fd.Pixels[x, y] = val;
            UpdateAllViews();
        }

        /// <summary>
        /// Az adott karakterhez tartozó karakterdefíníció valamennyi pixelét törli.
        /// </summary>
        public void ClearCharDef(char c)
        {
            CharDef fd = getCharDef(c);
            for (int y = 0; y < CharDef.FontSize.Height; y++)
                for (int x = 0; x < CharDef.FontSize.Width; x++)
                    fd.Pixels[x, y] = false;
            UpdateAllViews();
        }
    }
}
