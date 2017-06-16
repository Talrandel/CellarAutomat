#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Directions.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 12.06.2017 17:05
// Last Revision : 16.06.2017 12:48
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
