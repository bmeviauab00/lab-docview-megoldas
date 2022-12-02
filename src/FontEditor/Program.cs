namespace FontEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // A legjobb megk�zel�t�s a SystemAware lenne (alap�rtelmezett), de azzal nem j�l sk�l�zza
            // a rajzokat nagy DPI-n.
            Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);

            var mainForm = new MainForm();
            App.Initialize(mainForm);
            Application.Run(mainForm);
        }
    }
}