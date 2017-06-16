#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IRule.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 12.06.2017 17:02
// Last Revision : 16.06.2017 12:48
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Интерфейс описания правила функционирования клеточного автомата.
    /// </summary>
    public interface IRule
    {
        #region Members
        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        int TransformCell(Field pastFiled, int x, int y, int statesCount);
        #endregion
    }
}
