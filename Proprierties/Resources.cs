using System.Drawing;

namespace DistintaTecnica.Properties
{
    /// <summary>
    /// Classe statica per gestire le risorse dell'applicazione
    /// </summary>
    internal static class Resources
    {
        // Icone placeholder per la toolbar
        // In un'applicazione reale, queste sarebbero caricate da file di risorse

        public static Image New => CreatePlaceholderIcon("N", Color.Green);
        public static Image Open => CreatePlaceholderIcon("O", Color.Blue);
        public static Image Save => CreatePlaceholderIcon("S", Color.Orange);
        public static Image Add => CreatePlaceholderIcon("+", Color.LimeGreen);
        public static Image Delete => CreatePlaceholderIcon("-", Color.Red);
        public static Image Edit => CreatePlaceholderIcon("E", Color.Purple);
        public static Image Search => CreatePlaceholderIcon("🔍", Color.Gray);
        public static Image Export => CreatePlaceholderIcon("📤", Color.DarkBlue);
        public static Image Import => CreatePlaceholderIcon("📥", Color.DarkGreen);

        /// <summary>
        /// Crea un'icona placeholder con testo e colore specificati
        /// </summary>
        /// <param name="text">Testo da visualizzare nell'icona</param>
        /// <param name="backgroundColor">Colore di sfondo</param>
        /// <returns>Immagine dell'icona</returns>
        private static Image CreatePlaceholderIcon(string text, Color backgroundColor)
        {
            int size = 16;
            var bitmap = new Bitmap(size, size);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Riempie lo sfondo
                graphics.Clear(backgroundColor);

                // Disegna il testo
                using (var brush = new SolidBrush(Color.White))
                using (var font = new Font("Arial", 8, FontStyle.Bold))
                {
                    var textSize = graphics.MeasureString(text, font);
                    var x = (size - textSize.Width) / 2;
                    var y = (size - textSize.Height) / 2;

                    graphics.DrawString(text, font, brush, x, y);
                }

                // Disegna un bordo
                using (var pen = new Pen(Color.DarkGray, 1))
                {
                    graphics.DrawRectangle(pen, 0, 0, size - 1, size - 1);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Icone per il TreeView basate sui tipi di elemento
        /// </summary>
        public static class TreeViewIcons
        {
            public static Image Progetto => CreatePlaceholderIcon("📋", Color.Blue);
            public static Image ParteMacchina => CreatePlaceholderIcon("⚙️", Color.DarkBlue);
            public static Image Sezione => CreatePlaceholderIcon("📁", Color.Orange);
            public static Image Sottosezione => CreatePlaceholderIcon("📂", Color.Gold);
            public static Image Montaggio => CreatePlaceholderIcon("🔧", Color.Green);
            public static Image Gruppo => CreatePlaceholderIcon("🔩", Color.Purple);
            public static Image Variante => CreatePlaceholderIcon("🔀", Color.Teal);
            public static Image Particolare => CreatePlaceholderIcon("🔧", Color.Brown);
            public static Image Commerciale => CreatePlaceholderIcon("💰", Color.DarkGreen);
        }

        /// <summary>
        /// Icone per gli stati degli elementi
        /// </summary>
        public static class StatusIcons
        {
            public static Image New => CreatePlaceholderIcon("N", Color.Green);
            public static Image Modified => CreatePlaceholderIcon("M", Color.Orange);
            public static Image Deleted => CreatePlaceholderIcon("D", Color.Red);
            public static Image Approved => CreatePlaceholderIcon("✓", Color.LimeGreen);
            public static Image Warning => CreatePlaceholderIcon("⚠", Color.Gold);
            public static Image Error => CreatePlaceholderIcon("✗", Color.Red);
        }

        /// <summary>
        /// Ottiene l'icona appropriata per un tipo di elemento
        /// </summary>
        /// <param name="tipoElemento">Tipo di elemento</param>
        /// <returns>Immagine dell'icona</returns>
        public static Image GetIconForElementType(string tipoElemento)
        {
            return tipoElemento?.ToUpper() switch
            {
                "PROGETTO" => TreeViewIcons.Progetto,
                "PARTE_MACCHINA" => TreeViewIcons.ParteMacchina,
                "SEZIONE" => TreeViewIcons.Sezione,
                "SOTTOSEZIONE" => TreeViewIcons.Sottosezione,
                "MONTAGGIO" => TreeViewIcons.Montaggio,
                "GRUPPO" => TreeViewIcons.Gruppo,
                "VARIANTE" => TreeViewIcons.Variante,
                "PARTICOLARE" => TreeViewIcons.Particolare,
                "COMMERCIALE" => TreeViewIcons.Commerciale,
                _ => TreeViewIcons.Gruppo
            };
        }

        /// <summary>
        /// Ottiene l'icona appropriata per uno stato
        /// </summary>
        /// <param name="stato">Stato dell'elemento</param>
        /// <returns>Immagine dell'icona</returns>
        public static Image GetIconForStatus(string stato)
        {
            return stato?.ToUpper() switch
            {
                "NEW" => StatusIcons.New,
                "MODIFIED" => StatusIcons.Modified,
                "DELETED" => StatusIcons.Deleted,
                "APPROVED" => StatusIcons.Approved,
                _ => StatusIcons.New
            };
        }
    }
}