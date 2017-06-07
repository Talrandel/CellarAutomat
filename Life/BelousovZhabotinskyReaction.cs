using System;

namespace Life
{
    class BelousovZhabotinskyReaction : ITransform
    {
        public int TransformCell(Field pastF, int x, int y)
        {
            int centerCell = pastF.GetCell(x, y);
            int[] ExistingNeighbors = pastF.GetNeighborsInAllDirections(x, y);
            int sum = 0;
            for (int i = 0; i < ExistingNeighbors.Length; i++)
                sum += ExistingNeighbors[i];
            if (centerCell == 0)
            {
                if (sum < 5)
                    return 0;
                else if (sum < 100)
                    return 2;
                else
                    return 3;
            }
            else if (centerCell == pastF.GetCell(x, y) - 1)
                return 0;
            else
                return Math.Min(sum / 8 + 5, pastF.GetCell(x, y) - 1);
        }
    }
}