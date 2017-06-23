#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : FieldExtension.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 22:09
// Last Revision : 23.06.2017 13:29
// Description   : 
#endregion

using System;
using System.Collections.Generic;
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
        /// Получить количество живых соседей в окрестности Мура для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Количество живых соседей для выбранной клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static int GetLiveNeighborCount(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int count = 0;
            for (int i = y - 1; i <= y; i++)
            {
                for (int j = x - 1; j <= x; j++)
                {
                    if (i == y && j == x)
                        continue;

                    if (0 <= i && i <= source.Height && 0 <= j && j <= source.Width && source[i, j] != 0)
                        count++;
                }
            }

            return count;

            // BUG: Выяснить почему не получается аналогичный результат.
            //int[] tempArray =
            //{
            //    GetCellAtDirection(x, y, Directions.NorthWest),
            //    GetCellAtDirection(x, y, Directions.NorthEast),
            //    GetCellAtDirection(x, y, Directions.North),
            //    GetCellAtDirection(x, y, Directions.SouthWest),
            //    GetCellAtDirection(x, y, Directions.SouthEast),
            //    GetCellAtDirection(x, y, Directions.South),
            //    GetCellAtDirection(x, y, Directions.West),
            //    GetCellAtDirection(x, y, Directions.East)
            //};

            //int count2 = tempArray.Count(item => item != 0);

            //return count2;
        }

        /// <summary>
        /// Получить число живых соседей в окрестности Фон Неймана для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Количество живых соседей для выбранной клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static int GetLiveNeighborCountNeiman(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Directions.North),
                source.GetCellAtDirection(x, y, Directions.East),
                source.GetCellAtDirection(x, y, Directions.South),
                source.GetCellAtDirection(x, y, Directions.West)
            };

            return tempArray.Count(item => item > 0);
        }

        /// <summary>
        /// Получить список соседей в окрестности Мура для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static IEnumerable<int> GetNeighborsInAllDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Directions.NorthWest),
                source.GetCellAtDirection(x, y, Directions.NorthEast),
                source.GetCellAtDirection(x, y, Directions.North),
                source.GetCellAtDirection(x, y, Directions.SouthWest),
                source.GetCellAtDirection(x, y, Directions.SouthEast),
                source.GetCellAtDirection(x, y, Directions.South),
                source.GetCellAtDirection(x, y, Directions.West),
                source.GetCellAtDirection(x, y, Directions.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Получить список соседей в окрестности Фон Неймана для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static IEnumerable<int> GetNeighborsInFourDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Directions.North),
                source.GetCellAtDirection(x, y, Directions.South),
                source.GetCellAtDirection(x, y, Directions.West),
                source.GetCellAtDirection(x, y, Directions.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Получить список соседей в двух направлениях (на западе и востоке) для клетки с координатами.
        /// </summary>
        /// <param name="source">Объек <see cref="IField"/> в котором производится вычисление характеристики.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static IEnumerable<int> GetNeighborsInTwoDirections(this IField source, int x, int y)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int[] tempArray =
            {
                source.GetCellAtDirection(x, y, Directions.West),
                source.GetCellAtDirection(x, y, Directions.East)
            };

            return tempArray.Where(cell => cell != -1);
        }
        #endregion
    }
}
