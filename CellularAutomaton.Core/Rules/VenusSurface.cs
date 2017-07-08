#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : VenusSurface.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 08.07.2017 15:04
// Description   : 
#endregion

using System;

using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - поверхность Венеры.
    /// </summary>
    public class VenusSurface : IRule
    {
        #region IRule Members
        /// <summary>
        /// Возвращает название правила.
        /// </summary>
        public string Name => Resources.Rule_VenusSurface_Name;

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

            switch (pastFiled[x, y])
            {
                case 0:
                    return (((pastFiled.GetCellAtDirection(x, y, Direction.NorthWest) % 2) ^
                             (pastFiled.GetCellAtDirection(x, y, Direction.NorthEast) % 2)) << 1) +
                           pastFiled.GetCellAtDirection(x, y, Direction.North) % 2;
                case 1:
                    return (((pastFiled.GetCellAtDirection(x, y, Direction.NorthWest) % 2) ^
                             (pastFiled.GetCellAtDirection(x, y, Direction.SouthWest) % 2)) << 1) +
                           pastFiled.GetCellAtDirection(x, y, Direction.West) % 2;
                case 2:
                    return (((pastFiled.GetCellAtDirection(x, y, Direction.SouthWest) % 2) ^
                             (pastFiled.GetCellAtDirection(x, y, Direction.SouthEast) % 2)) << 1) +
                           pastFiled.GetCellAtDirection(x, y, Direction.South) % 2;
                default:
                    return (((pastFiled.GetCellAtDirection(x, y, Direction.SouthEast) % 2) ^
                             (pastFiled.GetCellAtDirection(x, y, Direction.NorthEast) % 2)) << 1) +
                           pastFiled.GetCellAtDirection(x, y, Direction.East) % 2;
            }
        }
        #endregion
    }
}
