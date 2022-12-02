using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        /// Az aktív dokumentum (melynek tabfüle ki van választva).
        /// </summary>
        private Document activeDocument;
        /// <summary>
        /// Az alkalmazáshoz tartozó dokumentumok listája.
        /// </summary>
        private List<Document> documents = new List<Document>();

        /// <summary>
        /// A főablakot tárolja.
        /// </summary>
        private MainForm mainForm;

        /// <summary>
        /// Az alkalmazásobjektum tárolására szolgál. 
        /// </summary>
        private static App theApp;
        
        /// <summary>
        /// Elérhetővé teszi mindenki számára az alkalmazásobjektumot (App.Instance-ként
        /// érhető el.)
        /// </summary>
        public static App Instance
        {
            get { return theApp; }
        }
        /// <summary>
        /// Létrehozza az alkalmazásobjektumot.
        /// </summary>
        public static void Initialize(MainForm form)
        {
            theApp = new App();
            theApp.mainForm = form;
        }

        /// <summary>
        /// A főablak.
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
        }
        
        /// <summary>
        /// Visszaadja az aktív dokumentumot.
        /// </summary>
        /// <returns></returns>
        public Document GetActiveDocument()
        {
            return activeDocument;
        }

        /// <summary>
        /// Megnyit egy dokumentumot. Nincs implementálva.
        /// </summary>
        public void OpenDocument()
        {
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
            if (activeDocument == null)
                return;

            // Útvonal bekérése a felhasználótól a SaveFileDialog segítségével.
            // ..
            string path = "";

            // A dokumentum adatainak elmentése.
            activeDocument.SaveDocument(path);
        }

        /// <summary>
        /// Bezárja az aktív dokumentumot.
        /// </summary>
        public void CloseActiveDocument()
        {
            if (mainForm.TabControl.TabPages.Count == 0)
                return;

            mainForm.TabControl.TabPages.RemoveByKey(activeDocument.Name);
            documents.Remove(activeDocument);
        }

        /// <summary>
        /// Létrehoz egy új dokumentumot.
        /// </summary>
        public void NewDocument()
        {
            // Bekérdezzük az új font típus (dokumentum) nevét a 
            // felhasználótól egy modális dialógs ablakban.
            NewDocForm form = new NewDocForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            // Új dokumentum objektum létrehozása és felvétele a 
            // dokumentum listába.
            Document doc = new FontEditorDocument(form.FontName);
            documents.Add(doc);
            // Az első paraméter egy kulcs, a második a tab falirata

            // Egy új tabra felteszi a dokumentumhoz tartozó felületelemeket.
            // Ezeket egy UserControl, a FontDocumentControl fogja össze.
            // Így csak ebből kell egy példányt az új tabpage-re feltenni.
            mainForm.TabControl.TabPages.Add(form.FontName, form.FontName);
            FontDocumentControl documentControl = new FontDocumentControl();
            TabPage tp = mainForm.TabControl.TabPages[form.FontName];
            tp.Controls.Add(documentControl);
            documentControl.Dock = DockStyle.Fill;

            // SampleTextView beregisztrálása a documentnél, hogy
            // értesüljön majd a dokumentum változásairól.
            documentControl.SampleTextView.AttachToDoc(doc);

            // Az új tab legyen a kiválasztott. 
            mainForm.TabControl.SelectTab(tp);

            // Az új tab lesz az aktív, az activeDocument
            // tagváltozót erre kell állítani.
            UpdateActiveDocument();
        }

        /// <summary>
        /// Frissíti az activeDocument változót, hogy az aktuálisan kiválasztott tabhoz tartozó dokumentumra 
        /// mutasson.
        /// </summary>
        public void UpdateActiveDocument()
        {
            if (mainForm.TabControl.TabPages.Count == 0)
                activeDocument = null;
            else
            {
                foreach (Document document in documents)
                    if (document.Name == mainForm.TabControl.SelectedTab.Name)
                        activeDocument = document;
            }
        }

        /// <summary>
        /// Létrehoz egy új FontEditorView-t az aktuális dokumentumhoz, és ezt be is regisztrálja a 
        /// dokumentumnál (hogy a jövőben étesüljön a válatozásairól).
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IView CreateFontEditorView(char c)
        {
            Document doc = GetActiveDocument();
            if (doc == null)
                return null;
            FontEditorView view = new FontEditorView(c, (FontEditorDocument)doc);
            doc.AttachView(view);
            return view;
        }
    }
}
