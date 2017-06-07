using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace FastBitmap
{
    /// <summary>
    /// Предоставляет методы расширеной обработки изображений.
    /// </summary>
    public static class BaseOperations
    {
        /// <summary>
        /// Выполняет передачу данных о цвете, соответствующих прямоугольной области пикселей, блоками битов с экрана на точечный рисунок System.Drawing.Bitmap.
        /// </summary>
        /// <param name="sourceX">Координата верхней левой точки области по Х.</param>
        /// <param name="sourceY">Координата верхней левой точки области по Y.</param>
        /// <param name="sizeScreen">Размер области.</param>
        /// <returns>Снимок заданной области.</returns>
        public static Bitmap PrintScreen(int sourceX, int sourceY, Size sizeScreen)
        {
            Bitmap img = null;

            const byte maxException = 5; // Максимальное число исключений.
            byte exception = 0; // Произошло исключений.
            Win32Exception win32Exception; // Информация о последнем произошедшем исключении.
            do
            {
                try
                {
                    img = new Bitmap(sizeScreen.Width, sizeScreen.Height);
                    Graphics.FromImage(img).CopyFromScreen(sourceX, sourceY, 0, 0, sizeScreen);

                    Bitmap newBitmap = null;

                    try
                    {
                        newBitmap = new Bitmap(img);
                    }
                    finally
                    {
                        newBitmap?.Dispose();
                    }

                    return newBitmap;
                }
                catch (Win32Exception e)
                {
                    exception++;
                    win32Exception = e;

                    Thread.Sleep(2000);
                }
                finally
                {
                    img?.Dispose();
                    img = null;
                }
            } while (exception < maxException); // Неудачная попытка?

            throw win32Exception;
        }

        /// <summary>
        /// Выполняет передачу данных о цвете, соответствующих прямоугольной области пикселей, блоками битов с экрана на точечный рисунок System.Drawing.Bitmap.
        /// </summary>
        /// <param name="source">Координата верхней левой точки области.</param>
        /// <param name="sizeScreen">Размер области.</param>
        /// <returns>Снимок заданной области.</returns>
        public static Bitmap PrintScreen(Point source, Size sizeScreen)
        {
            return PrintScreen(source.X, source.Y, sizeScreen);
        }

        /// <summary>
        /// Метод масштабирования изображения.
        /// </summary>
        /// <param name="img">Масштабируемое изображение.</param>
        /// <param name="scale">Коэффициент масштабирования.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="img"/> имеет значение <b>null</b>.</exception>
        /// <returns>Отмасштабированное изображение.</returns>
        public static Bitmap ImageScaling(Image img, byte scale)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));

            Image outImg = null;
            try
            {
                outImg = new Bitmap(img.Width * scale, img.Height * scale);
                using (Graphics gr = Graphics.FromImage(outImg))
                {
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.ScaleTransform(scale, scale);
                    gr.DrawImage(img, 0, 0, img.Width, img.Height);
                }

                return new Bitmap(outImg);
            }
            finally
            {
                outImg?.Dispose();
            }
        }

        /// <summary>
        /// Метод преобразования указанного цвета в оттенки серого.
        /// </summary>
        /// <param name="red">Красная компонента цвета.</param>
        /// <param name="green">Зелёная компонента цвета.</param>
        /// <param name="blue">Голубая компонента цвета.</param>
        /// <returns>Оттенок серого соответствующий заданному цвету.</returns>
        /// <remarks>Формула вычисления градации серого: 0.36 * Red + 0.53 * Green + 0.11 * Blue.</remarks>
        public static double ColorToGrayscale(byte red, byte green, byte blue)
        {
            return 0.36 * red + 0.53 * green + 0.11 * blue;
        }

        /// <summary>
        /// Метод преобразования указанного цвета в оттенки серого.
        /// </summary>
        /// <param name="color">Преобразуемый цвет.</param>
        /// <returns>Оттенок серого соответствующий заданному цвету.</returns>
        /// <remarks>Формула вычисления градации серого: 0.36 * Red + 0.53 * Green + 0.11 * Blue.</remarks>
        public static double ColorToGrayscale(Color color)
        {
            return ColorToGrayscale(color.R, color.G, color.B);
        }
    }
}