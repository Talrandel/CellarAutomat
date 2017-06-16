namespace CellarAutomat
{
    /// <summary>
    /// КА - циклический, одномерный
    /// </summary>
    class OneDimentionalCyclic : ITransform
    {
        public int StatesCount { get; set; }
        public int TransformCell(Field pastF, int x, int y)
        {
            int[] ExistingNeighbors = pastF.GetNeighborsInTwoDirections(x, y);
            int nextState = (pastF.GetCell(x, y) + 1) % StatesCount;
            foreach (int c in ExistingNeighbors)
            {
                if (c == nextState)
                    return nextState;
            }
            return pastF.GetCell(x, y);
        }
        public OneDimentionalCyclic(int StatesCount)
        {
            this.StatesCount = StatesCount;
        }
    }
}