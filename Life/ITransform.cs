namespace Life
{
    /// <summary>
    /// Основной интерфейс 
    /// </summary>
    interface ITransform
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pastF"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int TransformCell(Field pastF, int x, int y);
    }
}