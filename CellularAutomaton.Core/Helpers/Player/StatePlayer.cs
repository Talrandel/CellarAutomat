#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : StatePlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 14.06.2017 22:51
// Last Revision : 16.06.2017 12:48
// Description   : 
#endregion

namespace CellularAutomaton.Core.Helpers.Player
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
