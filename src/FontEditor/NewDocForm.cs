using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FontEditor
{
    /// <summary>
    /// Új betűtípus paramétereinek (betűtípus neve) megadására szolgál.
    /// </summary>
    public partial class NewDocForm : Form
    {
        public NewDocForm()
        {
            InitializeComponent();
        }

        public string FontName
        {
            get { return tbFontName.Text; }
        }    
    }
}
