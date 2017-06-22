#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : ChangeFrameEventArgs.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 22.06.2017 16:44
// Last Revision : 22.06.2017 17:47
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Предоставляет данные для события <see cref="IPlayer.ChangeFrame"/>.
    /// </summary>
    public class ChangeFrameEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Возвращает номер отображаемого кадра.
        /// </summary>
        public short ReproducedFrame { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChangeFrameEventArgs"/> заданным номером кадра.
        /// </summary>
        /// <param name="frame">Номер воспроизводимого кадра.</param>
        internal ChangeFrameEventArgs(short frame)
        {
            ReproducedFrame = frame;
        }
        #endregion
    }
}
