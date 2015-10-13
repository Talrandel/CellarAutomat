
namespace Life
{
    abstract class LastCellState : ITransformation
    {
        public Field pastField;

        public abstract int TransformCell(Field f, int x, int y);
    }
}