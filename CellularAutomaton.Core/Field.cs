﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Field.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 29.06.2017 14:28
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Представляет поле клеточного автомата.
    /// </summary>
    public class Field : IField, IReadOnlyField
    {
        #region Fields
        /// <summary>
        /// Внутреннее представление поля.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member")]
        private readonly int[,] _cells;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Field"/> с заданными размерами.
        /// </summary>
        /// <param name="width">Ширина поля.</param>
        /// <param name="height">Высота поля.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <para>Ширина поля <paramref name="width"/> меньше нуля.</para>
        ///     <para>-- или --</para>
        ///     <para>Высота поля <paramref name="height"/> меньше нуля.</para>
        /// </exception>
        public Field(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width),
                    string.Format(CultureInfo.CurrentCulture, Resources.Ex__Ширина_поля__0__меньше_нуля_, nameof(width)));
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width),
                    string.Format(CultureInfo.CurrentCulture, Resources.Ex__Высота_поля__0__меньше_нуля_, nameof(height)));
            }

            Width = width;
            Height = height;
            _cells = new int[height, width];
        }
        #endregion

        #region IField Members
        /// <summary>
        /// Проверяет равенство заданного поля текущему.
        /// </summary>
        /// <param name="other">Сравниваемое поле.</param>
        /// <returns>True - поля идентичны, иначе false.</returns>
        public bool Equals(IField other)
        {
            if (other == null)
                return false;
            // TODO: Попробовать применить перечисление для ускорения вычислений.
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    if (this[i, j] != other[i, j])
                        return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает или задаёт значение клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Значение клетки.</returns>
        [SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public int this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }

        /// <summary>
        /// Возвращает ширину поля.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Возвращает высоту поля.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Возвращает состояние клетки расположенной в заданном направлении относительно клетки заданной координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="direction">Направление движения.</param>
        /// <returns>Состояние клетки.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Недопустимое значение параметра <paramref name="direction"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public int GetCellAtDirection(int x, int y, Directions direction)
        {
            int state = -1;
            switch (direction)
            {
                case Directions.NorthWest:
                {
                    x--;
                    y--;

                    if ((0 <= x) && (0 <= y))
                        state = this[x, y];
                    break;
                }
                case Directions.North:
                {
                    x--;

                    if (0 <= x)
                        state = this[x, y];
                    break;
                }
                case Directions.NorthEast:
                {
                    x--;
                    y++;

                    if ((0 <= x) && (y < Width))
                        state = this[x, y];
                    break;
                }
                case Directions.East:
                {
                    y++;

                    if (y < Width)
                        state = this[x, y];
                    break;
                }
                case Directions.SouthEast:
                {
                    x++;
                    y++;

                    if ((x < Height) && (y < Width))
                        state = this[x, y];
                    break;
                }
                case Directions.South:
                {
                    x++;

                    if (x < Height)
                        state = this[x, y];
                    break;
                }
                case Directions.SouthWest:
                {
                    x++;
                    y--;

                    if ((x < Height) && (0 <= y))
                        state = this[x, y];
                    break;
                }
                case Directions.West:
                {
                    y--;

                    if (0 <= y)
                        state = this[x, y];
                    break;
                }
                case Directions.Center:
                {
                    state = this[x, y];
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(direction),
                        direction,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Недопустимое_значение_параметра__0__,
                            nameof(direction)));
            }

            return state;
        }

        /// <summary>
        /// Осуществляет копирование текущего поля в заданное.
        /// </summary>
        /// <param name="other">Поле, в которое осуществляется копирование.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="other"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Проверить аргументы или открытые методы", MessageId = "0")]
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public void Copy(ref IField other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    other[i, j] = this[i, j];
            }
        }

        /// <summary>
        /// Устанавливает состояние клеток поля по умолчанию.
        /// </summary>
        public void Reset()
        {
            _cells.Initialize();
        }

        /// <summary>
        /// Задаёт начальное состояние клеток на поле.
        /// </summary>
        /// <param name="statesCountMin">Минимальное число состояний клетки.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        /// <exception cref="ArgumentOutOfRangeException">Величина плотности распределения живых клеток на поле должна лежать в интервале [0; 100].</exception>
        public void Initialize(int statesCountMin, int statesCount, byte density)
        {
            if (100 < density)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(density),
                    density,
                    Resources.Ex__DensityOutOfRange);
            }

            if (density == 0)
                Reset();
            else
            {
                Random rnd = new Random();
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                        if (density < rnd.Next(1, 101))
                            _cells[i, j] = rnd.Next(statesCountMin, statesCount);
                        else
                            _cells[i, j] = 0;
                }
            }
        }
        #endregion
    }
}
