﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : FieldExtension.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 07.07.2017 22:10
// Last Revision : 08.07.2017 11:28
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Предоставляет набор методов расширения для работы с объектом реализующим интерфейс <see cref="IField"/>.
    /// </summary>
    public static class FieldExtension
    {
        #region Members
        /// <summary>
        /// Возвращает число живых соседей в окрестности Фон Неймана первого порядка для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Число живых соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static int GetLiveNeighborCountNeiman(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Convert.ToInt32(0 < source.GetCellAtDirection(x, y, Direction.North)) +
                Convert.ToInt32(0 < source.GetCellAtDirection(x, y, Direction.East)) +
                Convert.ToInt32(0 < source.GetCellAtDirection(x, y, Direction.South)) +
                Convert.ToInt32(0 < source.GetCellAtDirection(x, y, Direction.West));
        }

        /// <summary>
        /// Возвращает число живых соседей в окрестности Мура первого порядка для клетки с координатами.
        /// </summary>
        /// <param name="source">Объект <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Число живых соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static int GetLiveNeighborMooreNeighborhoodCount(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetNeighborMooreNeighborhoodCount(x, y, item => 0 < item).Count;
        }

        /// <summary>
        /// Возвращает список значений существующих соседей в окрестности Мура первого порядка для клетки с координатами.
        /// </summary>
        /// <param name="source">Объект для которого вычисляется <see cref="IField"/> значение.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Список значений существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static IEnumerable<int> GetNeighborsInAllDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetNeighborMooreNeighborhoodCount(x, y, item => item != -1);
        }

        /// <summary>
        /// Возвращает список значений существующих соседей в окрестности Фон Неймана первого порядка для клетки с заданными координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Список значений существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static IEnumerable<int> GetNeighborsInFourDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Direction.North),
                source.GetCellAtDirection(x, y, Direction.South),
                source.GetCellAtDirection(x, y, Direction.West),
                source.GetCellAtDirection(x, y, Direction.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Возвращает список значений существующих соседей в двух направлениях (на западе и востоке) для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <returns>Список значений существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static IEnumerable<int> GetNeighborsInTwoDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Direction.West),
                source.GetCellAtDirection(x, y, Direction.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Возвращает список соседей в окрестности Мура первого порядка для клетки с координатами удовлетворяющим условию.
        /// </summary>
        /// <param name="source">Объект <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата клетки по оси X.</param>
        /// <param name="y">Координата клетки по оси Y.</param>
        /// <param name="predicate">Условие в соответствии с которым происходит отбор соседей.</param>
        /// <returns>Список соседей в окрестности Мура первого порядка для клетки с координатами удовлетворяющим условию.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="predicate"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        private static ICollection<int> GetNeighborMooreNeighborhoodCount(this IField source, int x, int y, Func<int, bool> predicate)
        {
            Debug.Assert(source != null);
            Debug.Assert(predicate != null);

            LinkedList<int> result = new LinkedList<int>();

            int sourceWidth = source.Width;
            int sourceHeight = source.Height;
            int xStrart = (x == 0) ? 0 : (x - 1);
            int yStart = (y == 0) ? 0 : (y - 1);

            if ((sourceWidth <= xStrart) || (sourceHeight <= yStart))
                return result;

            int xEnd = (x < sourceWidth - 1) ? (x + 1) : x;
            int yEnd = (y < sourceHeight - 1) ? (y + 1) : y;

            for (int i = xStrart; i <= xEnd; i++)
            {
                for (int j = yStart; j <= yEnd; j++)
                {
                    if ((j == y) && (i == x))
                        continue;

                    int value = source[i, j];

                    if (predicate(value))
                        result.AddLast(value);
                }
            }

            return result;
        }
        #endregion
    }
}
