using System;
using System.Windows.Forms;

namespace DistintaTecnica
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Abilita gli stili visivi di Windows
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Avvia l'applicazione con la MainForm
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore critico nell'avvio dell'applicazione:\n{ex.Message}\n\nDettagli:\n{ex.StackTrace}",
                    "Errore Critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}