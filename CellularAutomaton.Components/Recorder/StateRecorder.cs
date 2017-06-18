#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : StateRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:44
// Last Revision : 18.06.2017 12:45
// Description   : 
#endregion

namespace CellularAutomaton.Components.Recorder
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
