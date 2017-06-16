namespace CellarAutomat
{
    /// <summary>
    /// КА - Поверхность Венеры
    /// </summary>
    class VenusSurface : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int centerCell = pastF.GetCell(x, y);
            // Possible check for -1 value of cell in case of addition all cell's neighbors to array
            //int[] Neighbors = f.GetNeighborsInAllDirections(x, y);
            int northWestCell = pastF.GetCellAtDirection(x, y, Directions.NorthWest);
            int northEastCell = pastF.GetCellAtDirection(x, y, Directions.NorthEast);
            int northCell = pastF.GetCellAtDirection(x, y, Directions.North);
            int southWestCell = pastF.GetCellAtDirection(x, y, Directions.SouthWest);
            int southEastCell = pastF.GetCellAtDirection(x, y, Directions.SouthEast);
            int southCell = pastF.GetCellAtDirection(x, y, Directions.South);
            int westCell = pastF.GetCellAtDirection(x, y, Directions.West);
            int eastCell = pastF.GetCellAtDirection(x, y, Directions.East);

            int[] Neighbors = { northWestCell, northCell, northEastCell, eastCell, southEastCell, southCell, southWestCell, westCell };
            for (int i = 0; i < Neighbors.Length; i++)
            {
                if (Neighbors[i] < 0)
                    Neighbors[i] = 0;
            }

            if (centerCell == 0)
                return 2 * ((northWestCell % 2) ^ (northEastCell % 2)) + northCell % 2;
            else if (centerCell == 1)
                return 2 * ((northWestCell % 2) ^ (southWestCell % 2)) + westCell % 2;
            else if (centerCell == 2)
                return 2 * ((southWestCell % 2) ^ (southEastCell % 2)) + southCell % 2;
            else
                return 2 * ((southEastCell % 2) ^ (northEastCell % 2)) + eastCell % 2;
        }

        public int StatesCount { get; set; }

        public VenusSurface(int StatesCount)
        {
            this.StatesCount = StatesCount;
        }
    }
}