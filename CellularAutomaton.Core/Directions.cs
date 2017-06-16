#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : Directions.cs
// Author        : Антипкин С.С.
// Created       : 12.06.2017 17:05
// Last Revision : 14.06.2017 21:38
// Description   : 
#endregion

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Перечисление направлений определяющих соседей клетки.
    /// </summary>
    public enum Directions
    {
        /// <summary>
        /// Северо-запад.
        /// </summary>
        NorthWest,

        /// <summary>
        /// Север.
        /// </summary>
        North,

        /// <summary>
        /// Северо-восток.
        /// </summary>
        NorthEast,

        /// <summary>
        /// Восток.
        /// </summary>
        East,

        /// <summary>
        /// Юго-восток.
        /// </summary>
        SouthEast,

        /// <summary>
        /// Юг.
        /// </summary>
        South,

        /// <summary>
        /// Юго-запад.
        /// </summary>
        SouthWest,

        /// <summary>
        /// Запад.
        /// </summary>
        West,

        /// <summary>
        /// Та же самая клетка.
        /// </summary>
        Center
    }
}
