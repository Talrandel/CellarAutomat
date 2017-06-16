namespace CellarAutomat
{
    /// <summary>
    /// КА - Жизнь
    /// </summary>
    class BaseLife : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int neighborCount = pastF.GetLiveNeighborCount(x, y);
            if (neighborCount == 3)
                return 1;
            else if (neighborCount == 2)
                return pastF.GetCell(x, y);
            else
                return 0;
        }

        public int StatesCount { get { return 2; } set { value = 2; } }

        public BaseLife(int StatesCount)
        {
            this.StatesCount = StatesCount;
        }
    }
}