using System;

namespace Life
{
    /// <summary>
    /// Направления для определения соседей для клетки
    /// </summary>
    public enum Directions
    {
        /// <summary>
        /// Северо-запад
        /// </summary>
        NorthWest = 0,
        /// <summary>
        /// Север
        /// </summary>
        North,
        /// <summary>
        /// Северо-восток
        /// </summary>
        NorthEast,
        /// <summary>
        /// Восток
        /// </summary>
        East,
        /// <summary>
        /// Юго-восток
        /// </summary>
        SouthEast,
        /// <summary>
        /// Юг
        /// </summary>
        South,
        /// <summary>
        /// Юго-запад
        /// </summary>
        SouthWest,
        /// <summary>
        /// Запад
        /// </summary>
        West,
        /// <summary>
        /// Та же самая клетка
        /// </summary>
        Center
    }
    /// <summary>
    /// Класс поля КА
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Закрытый двумерный массив клеток поля
        /// </summary>
        private int[,] cells;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        private Random rand;

        /// <summary>
        /// Получить значение выбранной клетки
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Значение выбранной клетки</returns>
        public int GetCell(int x, int y)
        {
            return cells[x, y];
        }

        /// <summary>
        /// Установить значение выбранной клетки
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <param name="value">Значение, которое следует установить для выбранной клетки</param>
        public void SetCell(int x, int y, int value)
        {
            cells[x, y] = value;
        }

        /// <summary>
        /// Получить ширину поля
        /// </summary>
        /// <returns>Ширина поля</returns>
        public int GetWidth()
        {
            return cells.GetLength(1);
        }

        /// <summary>
        /// Получить высоту поля
        /// </summary>
        /// <returns>Высота поля</returns>
        public int GetHeight()
        {
            return cells.GetLength(0);
        }

        /// <summary>
        /// Получить клетку в выбранном направлении относительно заданной
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <param name="direction">Направление движения для получения соседа выбранной клетки</param>
        /// <returns></returns>
        public int GetCellAtDirection(int x, int y, Directions direction)
        {
            int state = -1;
            switch (direction)
            {
                case Directions.NorthWest:
                    if (x - 1 >= 0 && y - 1 >= 0)
                        state = GetCell(x - 1, y - 1);
                    break;
                case Directions.North:
                    if (x - 1 >= 0)
                        state = GetCell(x - 1, y);
                    break;
                case Directions.NorthEast:
                    if (x - 1 >= 0 && y + 1 < GetWidth())
                        state = GetCell(x - 1, y + 1);
                    break;
                case Directions.East:
                    if (y + 1 < GetWidth())
                        state = GetCell(x, y + 1);
                    break;
                case Directions.SouthEast:
                    if (x + 1 < GetHeight() && y + 1 < GetWidth())
                        state = GetCell(x + 1, y + 1);
                    break;
                case Directions.South:
                    if (x + 1 < GetHeight())
                        state = GetCell(x + 1, y);
                    break;
                case Directions.SouthWest:
                    if (x + 1 < GetHeight() && y - 1 >= 0)
                        state = GetCell(x + 1, y - 1);
                    break;
                case Directions.West:
                    if (y - 1 >= 0)
                        state = GetCell(x, y - 1);
                    break;
                case Directions.Center:
                        state = GetCell(x, y);
                    break;
            }
            return state;
        }

        /// <summary>
        /// Получить количество живых соседей в окрестности Мура
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Количество живых соседей для выбранной точки</returns>
        public int GetLiveNeighborCount(int x, int y)
        {
            int count = 0;
            for (int i = y - 1; i < y + 1; i++)
            {
                for (int j = x - 1; j < x + 1; j++)
                {
                    if (i == y && j == x)
                        continue;
                    if ((i >= 0 && i <= GetHeight()) && (j >= 0 && j <= GetWidth()) && cells[i, j] != 0)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Получить количество живых соседей в окрестности Неймана
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Количество живых соседей для выбранной точки</returns>
        public int GetLiveNeighborCountNeiman(int x, int y)
        {
            int count = 0;
            int[] NeimanCells = { GetCellAtDirection(x, y, Directions.North), GetCellAtDirection(x, y, Directions.East), GetCellAtDirection(x, y, Directions.South),  GetCellAtDirection(x, y, Directions.West) };
            for (int i = 0; i < NeimanCells.Length; i++)
            {
                if (NeimanCells[i] > 0)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Получить массив соседей в восьми направлениях (Окрестность Мура)
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Массив реально существующих соседей для выбранной клетки</returns>
        public int[] GetNeighborsInAllDirections(int x, int y)
        {
            int[] tempArray = { GetCellAtDirection(x, y, Directions.NorthWest), GetCellAtDirection(x, y, Directions.NorthEast), GetCellAtDirection(x, y, Directions.North), GetCellAtDirection(x, y, Directions.SouthWest), GetCellAtDirection(x, y, Directions.SouthEast), GetCellAtDirection(x, y, Directions.South), GetCellAtDirection(x, y, Directions.West), GetCellAtDirection(x, y, Directions.East) };
            System.Collections.Generic.List<int> tempList = new System.Collections.Generic.List<int>();
            foreach (int cell in tempArray)
            {
                if (cell != -1)
                {
                    tempList.Add(cell);
                }
            }
            return tempList.ToArray();
        }

        /// <summary>
        /// Получить массив соседей в четырех направлениях (окрестность Фон Неймана)
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Массив реально существующих соседей для выбранной клетки</returns>
        public int[] GetNeighborsInFourDirections(int x, int y)
        {
            int[] tempArray = { GetCellAtDirection(x, y, Directions.North), GetCellAtDirection(x, y, Directions.South), GetCellAtDirection(x, y, Directions.West), GetCellAtDirection(x, y, Directions.East) };
            System.Collections.Generic.List<int> tempList = new System.Collections.Generic.List<int>();
            foreach (int cell in tempArray)
            {
                if (cell != -1)
                {
                    tempList.Add(cell);
                }
            }
            return tempList.ToArray();
        }

        /// <summary>
        /// Получить массив соседей в двух направлениях
        /// </summary>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Массив реально существующих соседей для выбранной клетки</returns>
        public int[] GetNeighborsInTwoDirections(int x, int y)
        {
            int[] tempArray = { GetCellAtDirection(x, y, Directions.West), GetCellAtDirection(x, y, Directions.East) };
            System.Collections.Generic.List<int> tempList = new System.Collections.Generic.List<int>();
            foreach (int cell in tempArray)
            {
                if (cell != -1)
                {
                    tempList.Add(cell);
                }
            }
            return tempList.ToArray();
        }

        /// <summary>
        /// Копировать текущее поле в заданное
        /// </summary>
        /// <param name="secondField">Поле для копирования</param>
        public void CopyFieldToAnother(ref Field secondField)
        {
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    secondField.SetCell(i, j, GetCell(i, j));
                }
            }
        }

        /// <summary>
        /// Проверка соответствия текущего поля предыдущему (прошлому поколению КА)
        /// </summary>
        /// <param name="lastStateField">Ссылка на предыдущее поле</param>
        /// <returns>True = поля идентичны, False - нет</returns>
        public bool CheckIdentity(Field lastStateField)
        {
            int currentCell, pastCell;
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    currentCell = GetCell(i, j);
                    pastCell = lastStateField.GetCell(i, j);
                    if (currentCell == pastCell)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Установка начальных значений клеток на поле
        /// </summary>
        /// <param name="density">Плотность распределения живых клеток на поле</param>
        /// <param name="StatesNumber">Количество состояний клеток</param>
        public void SetStartValues(int StatesNumber, int density = 0)
        {
            // Обнулить все клетки на поле
            if (density == 0)
            {
                for (int i = 0; i < GetHeight(); i++)
                    for (int j = 0; j < GetWidth(); j++)
                        cells[i, j] = 0;
                return;
            }

            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    //if (rand.Next(1, 100) < density)
                    //    cells[i, j] = rand.Next(1, StatesNumber);
                    //else
                    //    cells[i, j] = 0;
                    cells[i, j] = rand.Next(0, StatesNumber);
                }
            }
        }

        /// <summary>
        /// Конструктор класса поля
        /// </summary>
        /// <param name="Width">Ширина поля</param>
        /// <param name="Height">Высота поля</param>
        public Field(int Width, int Height)
        {
            rand = new Random();
            cells = new int[Height, Width];
        }
    }
}