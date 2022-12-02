using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FontEditor
{
    /// <summary>
    /// Egy adott betűtípus adott karakterének pixeljeit tartalmazza. 
    /// </summary>
    public class CharDef
    {
        /// <summary>
        /// A karakterek mérete.
        /// Egyszerűsítés: minden betű 15*20 pixeles.
        /// </summary>
        public static readonly Size FontSize = new Size(15, 20);

        /// <summary>
        /// A betűdefiníció pixeljeit tartalmazza.
        /// </summary>
        public Boolean[,] Pixels = new bool[FontSize.Width, FontSize.Height];

        /// <summary>
        /// A karakter, melynek pixeljeit a CharDef tartalmazza.
        /// </summary>
        private char character;

        public CharDef(char c)
        {
            character = c;

            Font f = new Font("Arial", 15);
            Bitmap bmp = new Bitmap(FontSize.Width, FontSize.Height, App.Instance.MainForm.CreateGraphics());
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString(c.ToString(), f, Brushes.White, 0, 0);
            for (int y = 0; y < FontSize.Height; y++)
            {
                for (int x = 0; x < FontSize.Width; x++)
                {
                    Color col =  bmp.GetPixel(x, y);
                    Pixels[x, y] = col.R != 0 || col.G != 0 || col.B != 0;
                    //Console.WriteLine(col.ToString());
                }
            }
        }

        public CharDef Clone()
        {
            CharDef charDef = new CharDef(this.character);
            // Klónozzuk a tömböt. Vigyázat, shallow copy-t készít, de mivel itt érték típusú
            // elemek vannak (bool), ezért ez jó nekünk.
            charDef.Pixels = (bool[,])Pixels.Clone();
            return charDef;
        }
    }
}
