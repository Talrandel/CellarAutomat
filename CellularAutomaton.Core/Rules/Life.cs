#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Life.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 17.06.2017 12:12
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - жизнь.
    /// </summary>
    public class Life : IRule
    {
        #region IRule Members
        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount"><b>Не используется.</b> Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        public int TransformCell(Field pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            switch (pastFiled.GetLiveNeighborCount(x, y))
            {
                case 3: return 1;
                case 2: return pastFiled[x, y];
                default: return 0;
            }
        }
        #endregion
    }
}
