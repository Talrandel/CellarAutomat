#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Field.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 07.07.2017 22:10
// Last Revision : 08.07.2017 11:26
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

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
        private readonly int[][] _cells;

        /// <summary>
        /// Высота поля.
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// Ширина поля.
        /// </summary>
        private readonly int _width;
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

            _width = width;
            _height = height;
            _cells = new int[width][];
            for (int i = 0; i < width; i++)
                _cells[i] = new int[height];
        }
        #endregion

        #region IField Members
        /// <summary>
        /// Проверяет равенство заданного поля текущему.
        /// </summary>
        /// <param name="other">Сравниваемое поле.</param>
        /// <returns><b>True</b> - поля идентичны, иначе <b>false</b>.</returns>
        public bool Equals(IField other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if ((_width != other.Width) || (_height != other.Height))
                return false;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
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
            get { return _cells[x][y]; }
            set { _cells[x][y] = value; }
        }

        /// <summary>
        /// Возвращает ширину поля.
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Возвращает высоту поля.
        /// </summary>
        public int Height => _height;

        /// <summary>
        /// Возвращает состояние клетки расположенной в заданном направлении относительно клетки заданной координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="direction">Направление движения.</param>
        /// <returns>Состояние клетки. Значение -1, если клетка по указанным координатам лежит вне поля.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Недопустимое значение параметра <paramref name="direction"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public int GetCellAtDirection(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.SouthWest:
                {
                    x--;
                    y--;

                    if ((0 <= x) && (0 <= y))
                        return this[x, y];

                    break;
                }
                case Direction.South:
                {
                    x--;

                    if (0 <= x)
                        return this[x, y];

                    break;
                }
                case Direction.SouthEast:
                {
                    x--;
                    y++;

                    if ((0 <= x) && (y < _width))
                        return this[x, y];

                    break;
                }
                case Direction.East:
                {
                    y++;

                    if (y < _width)
                        return this[x, y];

                    break;
                }
                case Direction.NorthEast:
                {
                    x++;
                    y++;

                    if ((x < _height) && (y < _width))
                        return this[x, y];

                    break;
                }
                case Direction.North:
                {
                    x++;

                    if (x < _height)
                        return this[x, y];

                    break;
                }
                case Direction.NorthWest:
                {
                    x++;
                    y--;

                    if ((x < _height) && (0 <= y))
                        return this[x, y];

                    break;
                }
                case Direction.West:
                {
                    y--;

                    if (0 <= y)
                        return this[x, y];

                    break;
                }
                case Direction.Center:
                {
                    return this[x, y];
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

            return -1;
        }

        /// <summary>
        /// Осуществляет копирование текущего поля в заданное.
        /// </summary>
        /// <param name="other">Поле, в которое осуществляется копирование.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="other"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Не совпадают размеры текущего и заданного полей.</exception>
        public void CopyTo(IField other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if ((_width != other.Width) || (_height != other.Height))
                throw new ArgumentOutOfRangeException(nameof(other), Resources.Ex__Not_match_sizes_fields);

            InnerCopyTo((Field)other);
        }

        /// <summary>
        /// Устанавливает состояние клеток поля по умолчанию.
        /// </summary>
        public void Reset()
        {
            foreach (int[] row in _cells)
                Array.Clear(row, 0, _height);
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
#if DEBUG || PERFORMANCE
                Random rnd = new Random(0);
#else
                Random rnd = new Random();
#endif
                for (int i = 0; i < _width; i++)
                {
                    for (int j = 0; j < _height; j++)
                        if (density < rnd.Next(1, 101))
                            this[i, j] = rnd.Next(statesCountMin, statesCount);
                        else
                            this[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Создает новый объект, являющийся копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
        public object Clone()
        {
            Field newField = new Field(_width, _height);
            InnerCopyTo(newField);
            return newField;
        }
        #endregion

        #region Members
        /// <summary>
        /// <b>Внутренний метод.</b> Осуществляет копирование внутреннего представления текущего поля <see cref="_cells"/> в заданное.
        /// </summary>
        /// <param name="destinationField">Поле, в которое осуществляется копирование.</param>
        private void InnerCopyTo(Field destinationField)
        {
            for (int i = 0; i < Width; i++)
                Array.Copy(_cells[i], 0, destinationField._cells[i], 0, Height);
        }
        #endregion
    }
}
