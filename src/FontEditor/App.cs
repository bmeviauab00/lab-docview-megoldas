using FontEditor.Documents;

namespace FontEditor
{
    /// <summary>
    /// Az alkalmazást reprezentálja. Egy példányt kell létrehozni belőle az Initialize 
    /// hívásával, ez lesz a "gyökérobjektum". Ez bármely osztály számára hozzáférhető az
    /// App.Instance propertyn keresztül.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Elérhetővé teszi mindenki számára az alkalmazásobjektumot (App.Instance-ként érhető el.)
        /// </summary>
        /// <remarks>
        /// Ez így nem szálbiztos, de ezzel most nem foglalkozunk
        /// </remarks>
        public static App Instance { get; private set; } = new App();

        /// <summary>
        /// Alapértelmezett konstruktor láthatóságának módosítása privátra
        /// </summary>
        private App()
        {
        }

        /// <summary>
        /// Az alkalmazáshoz tartozó dokumentumok listája.
        /// </summary>
        private readonly List<FontEditorDocument> documents = new List<FontEditorDocument>();

        /// <summary>
        /// Az aktív dokumentum (melynek tabfüle ki van választva).
        /// </summary>
        public FontEditorDocument? ActiveDocument { get; private set; }

        /// <summary>
        /// Megnyit egy dokumentumot. Nincs implementálva.
        /// </summary>
        public void OpenDocument()
        {
            throw new NotImplementedException();

            /* Lépések:
             * - Fájl útvonal megkérdezése a felhasználótól (OpenFileDialog).
             * - Új dokumentum objektum létrehozása 
             *      doc = new FontEditorDocument();
             * - Dokumentum tartalmának betöltése
             *      doc.LoadDocument(path);
             * - Az új dokumentum felvétele a megnyitott dokumentumok listájába
             *      documents.Add(doc);
             * - Új tab létrehozása a felhasználói felületen
             */
        }

        /// <summary>
        /// Elmenti az aktív dokumentum tartalmát. Nincs implementálva.
        /// </summary>
        public void SaveActiveDocument()
        {
            if (ActiveDocument == null)
                return;

            // TODO  Útvonal bekérése a felhasználótól a SaveFileDialog segítségével.
            string path = "";

            // A dokumentum adatainak elmentése.
            ActiveDocument.SaveDocument(path);
        }

        /// <summary>
        /// Bezárja az aktív dokumentumot.
        /// </summary>
        public void CloseActiveDocument()
        {
            if (ActiveDocument == null)
                return;

            documents.Remove(ActiveDocument);
        }

        /// <summary>
        /// Létrehoz egy új dokumentumot.
        /// </summary>
        public FontEditorDocument? NewDocument()
        {
            // Bekérdezzük az új font típus (dokumentum) nevét a 
            // felhasználótól egy modális dialógs ablakban.
            var form = new NewDocForm();
            if (form.ShowDialog() != DialogResult.OK)
                return null;

            // Új dokumentum objektum létrehozása és felvétele a dokumentum listába.
            var doc = new FontEditorDocument(form.FontName);
            documents.Add(doc);

            // Az új tab lesz az aktív, az activeDocument tagváltozót erre kell állítani.
            UpdateActiveDocument(doc.Name);

            return doc;
        }

        /// <summary>
        /// Frissíti az activeDocument változót,
        /// hogy az aktuálisan kiválasztott tabhoz tartozó dokumentumra mutasson.
        /// </summary>
        public void UpdateActiveDocument(string name)
        {
            if (name == null)
            {
                ActiveDocument = null;
            }
            else
            {
                foreach (var document in documents)
                {
                    if (document.Name == name)
                    {
                        ActiveDocument = document;
                    }
                }
            }
        }

        /// <summary>
        /// Létrehoz egy új FontEditorView-t az aktuális dokumentumhoz,
        /// és ezt be is regisztrálja a dokumentumnál (hogy a jövőben étesüljön a válatozásairól).
        /// </summary>
        public FontEditorView? CreateFontEditorView(char c)
        {
            if (ActiveDocument == null)
                return null;

            var view = new FontEditorView(c, ActiveDocument);
            ActiveDocument.AttachView(view);

            return view;
        }
    }
}
