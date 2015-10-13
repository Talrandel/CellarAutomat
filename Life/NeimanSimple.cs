
namespace Life
{
    class NeimanSimple : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int neighborCount = pastF.GetLiveNeighborCountNeiman(x, y);
            if (neighborCount == 1)
                return 1;
            else
                return pastF.GetCell(x, y);
        }
    }
}