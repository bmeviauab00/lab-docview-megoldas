namespace FontEditor.DocView
{
    /// <summary>
    /// Az egyes dokumentum típusok ősosztálya. Bár esetünkben csak egy dokumentum típus
    /// (FontEditorDocument) létezik, a későbbi bővíthetőség miatt célszerű külön választani.
    /// Tartalmazza a nézetek listáját, melyek a dokumentumot megjelenítik. 
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// A nézetek listája, melyek a dokumentumot megjelenítik. 
        /// </summary>
        private readonly List<IView> views = new List<IView>();

        public Document(string name)
        {
            Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// Egy nézetet beregisztrál a dokumentumhoz.
        /// </summary>
        /// <param name="v"></param>
        public void AttachView(IView v)
        {
            views.Add(v);
            v.Update();
        }

        /// <summary>
        /// Kiregisztrál egy nézetet.
        /// </summary>
        /// <param name="v"></param>
        public void DetachView(IView v)
        {
            views.Remove(v);
        }

        /// <summary>
        /// Frissíti az összes, dokumentumhoz tartozó nézetet.
        /// </summary>
        protected void UpdateAllViews()
        {
            foreach (var view in views)
            {
                view.Update();
            }
        }

        /// <summary>
        /// Betölti a dokumentum tartalmát
        /// </summary>
        public void LoadDocument(string filePath)
        {
            // ...
        }

        /// <summary>
        /// Elmenti a dokumentum tartalmát.
        /// </summary>
        public void SaveDocument(string filePath)
        {
            // ...
        }
    }
}
