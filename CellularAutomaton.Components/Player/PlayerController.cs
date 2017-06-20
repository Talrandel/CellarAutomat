#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PlayerController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 23:22
// Last Revision : 20.06.2017 23:54
// Description   : 
#endregion

using System.Windows.Forms;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     <b>При странном поведении разметки компонента в первую очередь попробовать отключить свойство <see cref="ButtonBase.AutoSize"/>.</b>
    /// </remarks>
    public partial class PlayerController : UserControl
    {
        #region Constructors
        public PlayerController()
        {
            InitializeComponent();
        }
        #endregion
    }
}
