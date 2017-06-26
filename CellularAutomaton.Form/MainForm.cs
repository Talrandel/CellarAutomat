using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CellularAutomaton.Core.Rules;

namespace CellarAutomatForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cellularAutomatonRecorder1.Rules.Add(new Life());
        }
    }
}
