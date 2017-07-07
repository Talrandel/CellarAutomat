#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Direction.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 07.07.2017 14:46
// Description   : 
#endregion

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Перечисление направлений определяющих соседей клетки.
    /// </summary>
    public enum Direction
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
