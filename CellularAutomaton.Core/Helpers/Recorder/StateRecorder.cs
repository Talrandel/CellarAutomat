#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : StateRecorder.cs
// Author        : Антипкин С.С.
// Created       : 14.06.2017 22:43
// Last Revision : 14.06.2017 22:51
// Description   : 
#endregion

namespace CellularAutomaton.Core.Helpers.Recorder
{
    /// <summary>
    /// Перечисление состояний регистратора.
    /// </summary>
    public enum StateRecorder
    {
        /// <summary>
        /// Запись остановлена.
        /// </summary>
        Stop,

        /// <summary>
        /// Ведётся запись.
        /// </summary>
        Record
    }
}
