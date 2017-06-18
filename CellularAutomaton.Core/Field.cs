#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Field.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 17.06.2017 16:31
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Представляет поле клеточного автомата.
    /// </summary>
    public class Field : IEquatable<Field>
    {
        #region Fields
        /// <summary>
        /// Поле.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional",
            MessageId = "Member")]
        private readonly int[,] _cells;

        /// <summary>
        /// Высота поля.
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// Ширина поля.
        /// </summary>
        private readonly int _width;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает ширину поля.
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Возвращает высоту поля.
        /// </summary>
        public int Height => _height;
        #endregion

        #region Indexers
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
            _cells = new int[height, width];
        }
        #endregion

        #region IEquatable<Field> Members
        /// <summary>
        /// Проверяет равенство заданного поля текущему.
        /// </summary>
        /// <param name="other">Сравниваемое поле.</param>
        /// <returns>True - поля идентичны, иначе false.</returns>
        public bool Equals(Field other)
        {
            if (other == null)
                return false;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                    if (this[i, j] != other[i, j])
                        return false;
            }

            return true;
        }
        #endregion

        #region Members
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

                    if ((0 <= x) && (y < _width))
                        state = this[x, y];
                    break;
                }
                case Directions.East:
                {
                    y++;

                    if (y < _width)
                        state = this[x, y];
                    break;
                }
                case Directions.SouthEast:
                {
                    x++;
                    y++;

                    if ((x < _height) && (y < _width))
                        state = this[x, y];
                    break;
                }
                case Directions.South:
                {
                    x++;

                    if (x < _height)
                        state = this[x, y];
                    break;
                }
                case Directions.SouthWest:
                {
                    x++;
                    y--;

                    if ((x < _height) && (0 <= y))
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
                    throw new ArgumentOutOfRangeException(nameof(direction), direction,
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Недопустимое_значение_параметра__0__, nameof(direction)));
            }

            return state;
        }

        /// <summary>
        /// Получить количество живых соседей в окрестности Мура для клетки с координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Количество живых соседей для выбранной клетки.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public int GetLiveNeighborCount(int x, int y)
        {
#warning TEST
            int count = 0;
            for (int i = y - 1; i <= y; i++)
            {
                for (int j = x - 1; j <= x; j++)
                {
                    if (i == y && j == x)
                        continue;

                    if (0 <= i && i <= _height && 0 <= j && j <= _width && _cells[i, j] != 0)
                        count++;
                }
            }

            return count;

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
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Количество живых соседей для выбранной клетки.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public int GetLiveNeighborCountNeiman(int x, int y)
        {
            int[] tempArray =
            {
                GetCellAtDirection(x, y, Directions.North),
                GetCellAtDirection(x, y, Directions.East),
                GetCellAtDirection(x, y, Directions.South),
                GetCellAtDirection(x, y, Directions.West)
            };

            return tempArray.Count(item => item > 0);
        }

        /// <summary>
        /// Получить список соседей в окрестности Мура для клетки с координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public IEnumerable<int> GetNeighborsInAllDirections(int x, int y)
        {
            int[] tempArray =
            {
                GetCellAtDirection(x, y, Directions.NorthWest),
                GetCellAtDirection(x, y, Directions.NorthEast),
                GetCellAtDirection(x, y, Directions.North),
                GetCellAtDirection(x, y, Directions.SouthWest),
                GetCellAtDirection(x, y, Directions.SouthEast),
                GetCellAtDirection(x, y, Directions.South),
                GetCellAtDirection(x, y, Directions.West),
                GetCellAtDirection(x, y, Directions.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Получить список соседей в окрестности Фон Неймана для клетки с координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public IEnumerable<int> GetNeighborsInFourDirections(int x, int y)
        {
            int[] tempArray =
            {
                GetCellAtDirection(x, y, Directions.North),
                GetCellAtDirection(x, y, Directions.South),
                GetCellAtDirection(x, y, Directions.West),
                GetCellAtDirection(x, y, Directions.East)
            };

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Получить список соседей в двух направлениях (на западе и востоке) для клетки с координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Список существующих соседей для клетки с заданными координатами.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public IEnumerable<int> GetNeighborsInTwoDirections(int x, int y)
        {
            int[] tempArray = {GetCellAtDirection(x, y, Directions.West), GetCellAtDirection(x, y, Directions.East)};

            return tempArray.Where(cell => cell != -1);
        }

        /// <summary>
        /// Осуществляет копирование текущего поля в заданное.
        /// </summary>
        /// <param name="other">Поле, в которое осуществляется копирование.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="other"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Проверить аргументы или открытые методы", MessageId = "0")]
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public void Copy(ref Field other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                    other[i, j] = this[i, j];
            }
        }

        /// <summary>
        /// Задаёт начальные состояния клеток на поле.
        /// </summary>
        /// <param name="statesNumber">Количество состояний клетки.</param>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        public void SetStartValues(int statesNumber, byte density)
        {
            if (100 < density)
                density = 100;

            Random rnd = new Random();

            // Сбросить состояние клеток.
            if (density == 0)
            {
                for (int i = 0; i < _height; i++)
                    for (int j = 0; j < _width; j++)
                        _cells[i, j] = 0;
            }
            else
            {
                for (int i = 0; i < _height; i++)
                {
                    for (int j = 0; j < _width; j++)
                        if (rnd.Next(0, 101) > density)
                            _cells[i, j] = rnd.Next(1, statesNumber);
                        else
                            _cells[i, j] = 0;
                    //cells[i, j] = rand.Next(0, statesNumber);
                }
            }
        }
        #endregion
    }
}
