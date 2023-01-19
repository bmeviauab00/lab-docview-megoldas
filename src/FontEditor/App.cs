using FontEditor.Documents;

namespace FontEditor
{
    /// <summary>
    /// Az alkalmazást reprezentálja. Egy példányt kell létrehozni belőle az Initialize
    /// hívásával, ez lesz a "gyökérobjektum". Ez bármely osztály számára hozzáférhető az
    /// App.Instance propertyn keresztül.
    /// Az egyszerűség érdekében egy osztály, de ideális esetben szét lenne szedve a pl. a következő
    /// osztályokra a felelősségi köröknek megfelelően:
    /// * DocumentManager: a megjelenítéstől függetlenül a dokumentumokat tárolná.
    /// * ViewManager: feladata a nézetek menedzselése, tabcontrolokhoz hozzáadása stb. lenne
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
        /// tabControl, ezen nyílnak meg a dokumentumok a felületen (minden dokumentumnak
        /// egy külön tab). A TabControl a MainForm-on található.
        /// </summary>
        private TabControl tabControl;

        public void Initialize(TabControl tabControl)
        {
            this.tabControl = tabControl;

            // Az eseménykezelőt lamda függvény formájában adjuk meg (de írhatnánk egy külün függvényt is)
            this.tabControl.SelectedIndexChanged += (sender, e) =>
                {
                    if (tabControl.TabCount != 0)
                        App.Instance.UpdateActiveDocument(tabControl.SelectedTab.Name);
                };
        }

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
            // Nincs egy dokumentum sem megnyitva
            if (ActiveDocument == null)
                return;

            tabControl.TabPages.RemoveByKey(ActiveDocument.Name);

            documents.Remove(ActiveDocument);
        }

        /// <summary>
        /// Létrehoz egy új dokumentumot.
        /// </summary>
        public void NewDocument()
        {
            // Bekérdezzük az új font típus (dokumentum) nevét a
            // felhasználótól egy modális dialógs ablakban.
            var form = new NewDocForm(GetDocumentNames());
            if (form.ShowDialog() != DialogResult.OK)
                return;

            // Új dokumentum objektum létrehozása és felvétele a dokumentum listába.
            var doc = new FontEditorDocument(form.FontName);
            documents.Add(doc);

            // Az új tab lesz az aktív, az activeDocument tagváltozót erre kell állítani.
            UpdateActiveDocument(doc.Name);

            CreateTabForNewDocument(doc);
        }

        /// <summary>
        /// Létrehoz egy tabot az dokumentumhoz.
        /// </summary>
        private void CreateTabForNewDocument(FontEditorDocument doc)
        {
            if (doc == null)
                return;

            var name = doc.Name;

            // Egy új tabra felteszi a dokumentumhoz tartozó felületelemeket.
            // Ezeket egy UserControl, a FontDocumentControl fogja össze.
            // Így csak ebből kell egy példányt az új tabpage-re feltenni.
            // Az első paraméter egy kulcs, a második a tab falirata
            tabControl.TabPages.Add(name, name);

            var documentControl = new FontDocumentControl();
            var tp = tabControl.TabPages[name];
            tp.Controls.Add(documentControl);
            documentControl.Dock = DockStyle.Fill;

            // SampleTextView beregisztrálása a documentnél,
            // hogy értesüljön majd a dokumentum változásairól.
            documentControl.SampleTextView.AttachToDoc(doc);

            // Az új tab legyen a kiválasztott.
            tabControl.SelectTab(tp);
        }

        /// <summary>
        /// Létrehoz egy új FontEditorView-t az aktuális dokumentumhoz,
        /// és ezt be is regisztrálja a dokumentumnál (hogy a jövőben értesüljön a válatozásairól).
        /// </summary>
        public FontEditorView? CreateFontEditorView(char c)
        {
            if (ActiveDocument == null)
                return null;

            var view = new FontEditorView(c, ActiveDocument);
            ActiveDocument.AttachView(view);

            return view;
        }

        /// <summary>
        /// Frissíti az activeDocument változót,
        /// hogy az aktuálisan kiválasztott tabhoz tartozó dokumentumra mutasson.
        /// </summary>
        private void UpdateActiveDocument(string name)
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
                        ActiveDocument = document;
                }
            }
        }

        /// <summary>
        /// Visszadja az összes dokumentum nevét egy tömbben.
        /// </summary>
        private string[] GetDocumentNames()
        {
            // Ezt nem tanuljuk a tárgy keretében, majd szakirányon lesz.
            // A lényege, hogy a Select-tel projekciót tudunk végrehajtani
            // egy gyűjtemény elemein (hasonlóan az adatbázisok select utasításához).
            // Egy lambda kifejezéssel adjuk meg mi a projekció kimenete.
            return documents
                .Select(document => document.Name).ToArray();
        }
    }
}
