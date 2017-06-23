#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : BelousovZhabotinskyReaction.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 20.06.2017 22:25
// Description   : 
#endregion

using System;
using System.Linq;

using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - реакция Белоусова-Жаботинского.
    /// </summary>
    public class BelousovZhabotinskyReaction : IRule
    {
        #region IRule Members
        /// <summary>
        /// Возвращает название правила.
        /// </summary>
        public string Name => Resources.BelousovZhabotinskyReaction_Name;

        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount"><b>Не используется.</b> Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        public int TransformCell(IField pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            int centerCell = pastFiled[x, y];
            int sum = pastFiled.GetNeighborsInAllDirections(x, y).Sum();

            if (centerCell == 0)
            {
                if (sum < 5)
                    return 0;

                return sum < 100 ? 2 : 3;
            }

            return (centerCell == pastFiled[x, y] - 1) ? 0 : Math.Min(sum / 8 + 5, pastFiled[x, y] - 1);
        }
        #endregion
    }
}
