#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : OneDimentionalCyclic.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 07.07.2017 19:52
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
        public int TransformCell(IField pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            int centerCell = pastFiled[x, y];
            int nextState = (centerCell + 1) % statesCount;

            if ((pastFiled.GetCellAtDirection(x, y, Direction.West) == nextState) ||
                (pastFiled.GetCellAtDirection(x, y, Direction.East) == nextState))
                return nextState;

            return centerCell;
        }
        #endregion
    }
}
