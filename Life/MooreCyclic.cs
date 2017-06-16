namespace CellarAutomat
{
    /// <summary>
    /// КА - циклический, окрестность Мура
    /// </summary>
    class MooreCyclic : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int[] ExistingNeighbors = pastF.GetNeighborsInAllDirections(x, y);
            int nextState = (pastF.GetCell(x, y) + 1) % StatesCount;
            foreach (int c in ExistingNeighbors)
            {
                if (c == nextState)
                    return nextState;
            }
            return pastF.GetCell(x, y);
        }

        public int StatesCount { get; set; }
        public MooreCyclic(int StatesCount)
        {
            this.StatesCount = StatesCount;
        }
    }
}