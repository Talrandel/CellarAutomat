#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : NeimanCyclic.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 12.06.2017 16:39
// Last Revision : 16.06.2017 12:48
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - циклический, окрестность Неймана.
    /// </summary>
    public class NeimanCyclic : IRule
    {
        #region IRule Members
        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        public int TransformCell(Field pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            int nextState = (pastFiled.GetCell(x, y) + 1) % statesCount;

            foreach (int c in pastFiled.GetNeighborsInFourDirections(x, y))
                if (c == nextState)
                    return nextState;

            return pastFiled.GetCell(x, y);
        }
        #endregion
    }
}
