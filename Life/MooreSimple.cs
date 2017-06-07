namespace Life
{
    class MooreSimple : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int neighborCount = pastF.GetLiveNeighborCount(x, y);
            if (neighborCount == 1)
                return 1;
            else
                return pastF.GetCell(x, y);
        }
    }
}