#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : OneDimentionalCyclic.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 20.06.2017 22:25
// Description   : 
#endregion

using System;

using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - циклический, одномерный.
    /// </summary>
    public class OneDimentionalCyclic : IRule
    {
        #region IRule Members
        /// <summary>
        /// Возвращает название правила.
        /// </summary>
        public string Name => Resources.Rule_OneDimentionalCyclic_Name;

        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        public int TransformCell(IReadOnlyField pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            int nextState = (pastFiled[x, y] + 1) % statesCount;

            foreach (int c in pastFiled.GetNeighborsInTwoDirections(x, y))
                if (c == nextState)
                    return nextState;

            return pastFiled[x, y];
        }
        #endregion
    }
}
