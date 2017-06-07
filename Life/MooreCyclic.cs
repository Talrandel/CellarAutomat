 namespace Life
{
    class MooreCyclic : ITransform
    {
        private int StatesNumber;
        public int TransformCell(Field pastF, int x, int y)
        {
            int[] ExistingNeighbors = pastF.GetNeighborsInAllDirections(x, y);
            int nextState = (pastF.GetCell(x, y) + 1) % StatesNumber;
            foreach (int c in ExistingNeighbors)
            {
                if (c == nextState)
                    return nextState;
            }
            return pastF.GetCell(x, y);
        }
        public MooreCyclic(int statesNumber)
        {
            StatesNumber = statesNumber;
        }
    }
}