
namespace Life
{
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
    }
}