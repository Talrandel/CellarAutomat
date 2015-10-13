using System;

namespace Life
{
    public enum Directions
    {
        NorthWest = 0, North, NorthEast, East, SouthEast, South, SouthWest, West, Center
    }
    public class Field
    {
        private int[,] cells;
        public int GetCell(int x, int y)
        {
            return cells[x, y];
        }
        public void SetCell(int x, int y, int value)
        {
            cells[x, y] = value;
        }
        public int GetWidth()
        {
            return cells.GetLength(1);
        }
        public int GetHeight()
        {
            return cells.GetLength(0);
        }
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
                        state = GetCell(x + 1, y);
                    break;
                case Directions.West:
                    if (y - 1 >= 0)
                        state = GetCell(x + 1, y);
                    break;
                case Directions.Center:
                        state = GetCell(x, y);
                    break;
            }
            return state;
        }
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
        public void CopyFieldToAnother(ref Field lastStateField)
        {
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    lastStateField.SetCell(i, j, GetCell(i, j));
                }
            }
        }
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
        public void SetStartValues(int density)
        {
            if (density == 0)
            {
                for (int i = 0; i < GetHeight(); i++)
                    for (int j = 0; j < GetWidth(); j++)
                        cells[i, j] = 0;
                return;
            }
            Random rand = new Random();
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    //if (rand.Next(1, 100) < density)
                    //    cells[i, j] = 1;
                    //else
                    //    cells[i, j] = 0;
                    cells[i, j] = rand.Next(0, 2);
                }
            }
        }
        public void PrintField()
        {
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int j = 0; j < GetWidth(); j++)
                {
                    //if (GetCell(i, j) != 0)
                        Console.Write(GetCell(i, j));
                }
                Console.WriteLine();
            }
        }

        public Field(int Width, int Height)
        {
            cells = new int[Height, Width];
        }
    }
}