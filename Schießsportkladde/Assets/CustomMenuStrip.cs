using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schießsportkladde.Assets
{
    public partial class CustomMenuStrip : UserControl
    {
        private readonly Color _highlightColor;
        public CustomMenuStrip(Color highlightColor)
        {
            InitializeComponent();
            _highlightColor = highlightColor;
        }              
    }
}
