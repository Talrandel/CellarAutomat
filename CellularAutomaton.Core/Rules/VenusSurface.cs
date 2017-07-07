#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : VenusSurface.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 20.06.2017 22:26
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

            int northWestCell = pastFiled.GetCellAtDirection(x, y, Directions.NorthWest);
            int northEastCell = pastFiled.GetCellAtDirection(x, y, Directions.NorthEast);
            int northCell = pastFiled.GetCellAtDirection(x, y, Directions.North);
            int southWestCell = pastFiled.GetCellAtDirection(x, y, Directions.SouthWest);
            int southEastCell = pastFiled.GetCellAtDirection(x, y, Directions.SouthEast);
            int southCell = pastFiled.GetCellAtDirection(x, y, Directions.South);
            int westCell = pastFiled.GetCellAtDirection(x, y, Directions.West);
            int eastCell = pastFiled.GetCellAtDirection(x, y, Directions.East);

            switch (pastFiled[x, y])
            {
                case 0:
                    return 2 * ((northWestCell % 2) ^ (northEastCell % 2)) + northCell % 2;
                case 1:
                    return 2 * ((northWestCell % 2) ^ (southWestCell % 2)) + westCell % 2;
                case 2:
                    return 2 * ((southWestCell % 2) ^ (southEastCell % 2)) + southCell % 2;
                default:
                    return 2 * ((southEastCell % 2) ^ (northEastCell % 2)) + eastCell % 2;
            }
        }
        #endregion
    }
}
