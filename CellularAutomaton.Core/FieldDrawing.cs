#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : FieldDrawing.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 10.07.2017 17:39
// Last Revision : 10.07.2017 22:15
// Description   : 
#endregion

using System;
using System.Drawing;
using System.Drawing.Imaging;

using FastBitmap;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Предоставляет метод раскрашивания полей клеточного автомата <see cref="IField"/>, <see cref="IReadOnlyField"/>.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public sealed class FieldDrawing
    {
        #region Fields
        /// <summary>
        /// Кешированное значение класса <see cref="BitmapData"/> используемое в методе <see cref="Drawing(IField)"/>.
        /// </summary>
        private readonly BitmapData _cachedBitmapData;

        /// <summary>
        /// Объект <see cref="FastBitmapFormat32bppArgb"/> предоставляющий методы работы с заблокированным в памяти объектом <see cref="Bitmap"/>.
        /// </summary>
        private readonly FastBitmapFormat32bppArgb _fastBitmap;

        /// <summary>
        /// Кешированное значение структуры <see cref="Rectangle"/> используемой в методе <see cref="Drawing(IField)"/>.
        /// </summary>
        private Rectangle _cachedFieldRect;

        /// <summary>
        /// Метод преобразования кода клетки поля в цвет.
        /// </summary>
        private ConvertPointValueToColor _colorize;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает или задаёт метод преобразования кода клетки поля в цвет.
        /// </summary>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="value"/> имеет значение <b>null</b>.</exception>
        // ReSharper disable once MemberCanBePrivate.Global
        public ConvertPointValueToColor Colorize
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get { return _colorize; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                _colorize = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FieldDrawing"/>.
        /// </summary>
        public FieldDrawing()
        {
            _cachedBitmapData = new BitmapData {PixelFormat = FastBitmapFormat32bppArgb.SupportedPixelFormat};
            _fastBitmap = new FastBitmapFormat32bppArgb(_cachedBitmapData);
            Colorize = PointValueToColor;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FieldDrawing"/>, заданной функцией раскрашивания.
        /// </summary>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="colorize"/> имеет значение <b>null</b>.</exception>
        // ReSharper disable once UnusedMember.Global
        public FieldDrawing(ConvertPointValueToColor colorize) : this()
        {
            Colorize = colorize;
        }
        #endregion

        #region Members
        /// <summary>
        /// Создаёт рисунок из поля клеточного автомата.
        /// </summary>
        /// <param name="field">Визуализируемое поле клеточного автомата.</param>
        /// <returns>Визуализированное поле.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="field"/> имеет значение <b>null</b>.</exception>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedMember.Global
        public Bitmap Drawing(IField field)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(field.Width, field.Height, PixelFormat.Format32bppArgb);
                return Drawing(field, bitmap);
            }
            catch
            {
                bitmap?.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Создаёт рисунок из поля клеточного автомата.
        /// </summary>
        /// <param name="field">Визуализируемое поле клеточного автомата.</param>
        /// <param name="bitmap">Объект <see cref="Bitmap"/> который будет содержать визуализацию поля.</param>
        /// <returns>Визуализированное поле.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <para>Параметр <paramref name="field"/> имеет значение <b>null</b>.</para>
        ///     <para>-- или --</para>
        ///     <para>Параметр <paramref name="bitmap"/> имеет значение <b>null</b>.</para>
        /// </exception>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once UnusedMethodReturnValue.Global
        public Bitmap Drawing(IField field, Bitmap bitmap)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));

            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));

            int fieldWidth = field.Width;
            int fieldHeight = field.Height;

            if ((_cachedFieldRect.Width != fieldWidth) || (_cachedFieldRect.Height != fieldHeight))
                _cachedFieldRect = new Rectangle(0, 0, fieldWidth, fieldHeight);

            bitmap.LockBits(_cachedFieldRect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb, _cachedBitmapData);

            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                    _fastBitmap.SetLockPixel(i, j, Colorize(field[i, j]));
            }

            bitmap.UnlockBits(_cachedBitmapData);

            return bitmap;
        }

        /// <summary>
        /// Преобразует состояние клетки поля клеточного автомата в цвет по умолчанию.
        /// </summary>
        /// <param name="value">Состояние клетки поля.</param>
        /// <returns>Цвет соответствующий состоянию клетки.</returns>
        private static Color PointValueToColor(int value)
        {
            // TODO: заменить на функцию задания цвета?
            switch (value)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.Black;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Pink;
                case 5:
                    return Color.DarkBlue;
                case 6:
                    return Color.Turquoise;
                case 7:
                    return Color.Orange;
                case 8:
                    return Color.GreenYellow;
                case 9:
                    return Color.MediumVioletRed;
                case 10:
                    return Color.BlueViolet;
                case 11:
                    return Color.PaleVioletRed;
                case 12:
                    return Color.LightGreen;
                case 13:
                    return Color.Purple;
                case 14:
                    return Color.PapayaWhip;
                case 15:
                    return Color.SaddleBrown;
                default:
                    return Color.Green;
            }
        }
        #endregion
    }
}
