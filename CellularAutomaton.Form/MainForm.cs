﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : MainForm.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 21:01
// Last Revision : 27.06.2017 21:04
// Description   : 
#endregion

using System.Windows.Forms;

using CellularAutomaton.Core.Rules;

namespace CellarAutomatForm
{
    public partial class MainForm : Form
    {
        #region Constructors
        public MainForm()
        {
            InitializeComponent();

            cellularAutomatonRecorder1.Rules.Add(new Life());
            cellularAutomatonRecorder1.Rules.Add(new BelousovZhabotinskyReaction());
            cellularAutomatonRecorder1.Rules.Add(new MooreCyclic());
            cellularAutomatonRecorder1.Rules.Add(new MooreSimple());
            cellularAutomatonRecorder1.Rules.Add(new NeimanCyclic());
            cellularAutomatonRecorder1.Rules.Add(new NeimanSimple());
            cellularAutomatonRecorder1.Rules.Add(new OneDimentionalCyclic());
            cellularAutomatonRecorder1.Rules.Add(new VenusSurface());
        }
        #endregion
    }
}
