#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : StatePlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:46
// Last Revision : 18.06.2017 12:46
// Description   : 
#endregion

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Перечисление состояний проигрывателя.
    /// </summary>
    public enum StatePlayer
    {
        /// <summary>
        /// Воспроизведение остановлено.
        /// </summary>
        Stop,

        /// <summary>
        /// Запись воспроизводится.
        /// </summary>
        Play,

        /// <summary>
        /// Воспроизведение приостановлено.
        /// </summary>
        Pause
    }
}
