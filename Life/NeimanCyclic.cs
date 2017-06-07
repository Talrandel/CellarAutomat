namespace Life
{
    class NeimanCyclic : ITransform
    {
        private int StatesNumber;
        public int TransformCell(Field pastF, int x, int y)
        {
            int[] ExistingNeighbors = pastF.GetNeighborsInFourDirections(x, y);
            int nextState = (pastF.GetCell(x, y) + 1) % StatesNumber;
            foreach (int c in ExistingNeighbors)
            {
                if (c == nextState)
                    return nextState;
            }
            return pastF.GetCell(x, y);
        }
        public NeimanCyclic(int statesNumber)
        {
            StatesNumber = statesNumber;
        }
    }
}