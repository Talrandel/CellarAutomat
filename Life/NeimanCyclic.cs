namespace CellarAutomat
{
    /// <summary>
    /// КА - циклический, окрестность Неймана
    /// </summary>
    class NeimanCyclic : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int[] ExistingNeighbors = pastF.GetNeighborsInFourDirections(x, y);
            int nextState = (pastF.GetCell(x, y) + 1) % StatesCount;
            foreach (int c in ExistingNeighbors)
            {
                if (c == nextState)
                    return nextState;
            }
            return pastF.GetCell(x, y);
        }

        public int StatesCount { get; set; }
        public NeimanCyclic(int StatesCount)
        {
            this.StatesCount = StatesCount;
        }
    }
}