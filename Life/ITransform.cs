namespace CellarAutomat
{
    /// <summary>
    /// Основной интерфейс для логики клеточного автомата
    /// </summary>
    interface ITransform
    {
        /// <summary>
        /// Изменение состояния клетки
        /// </summary>
        /// <param name="pastF">Поле на прошлой итерации КА, на основе которого считается состояние клетки</param>
        /// <param name="x">X координата выбранной клетки</param>
        /// <param name="y">Y координата выбранной клетки</param>
        /// <returns>Новое состояние клетки</returns>
        int TransformCell(Field pastF, int x, int y);

        /// <summary>
        /// Количество состояний клеток для выбранного КА
        /// </summary>
        int StatesCount { get; set; }
    }
}