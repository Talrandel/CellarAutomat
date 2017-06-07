using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]

namespace FastBitmap
{
    /// <summary>
    /// Предоставляет методы работы с заблокированным объектом System.Drawing.Bitmap в системной памяти.
    /// </summary>
    public class LockImage
    {
        /// <summary>
        /// Сведения об операции блокировки.
        /// </summary>
        private BitmapData _bitmapData;

        /// <summary>
        /// Число элементов в массиве представляющем заблокированное изображение.
        /// </summary>
        private int _length;

        /// <summary>
        /// Массив байт представляющий заблокированное изображение.
        /// </summary>
        private byte[] _lockImage;

        /// <summary>
        /// Заблокированное изображение.
        /// </summary>
        private Bitmap _img;

        /// <summary>
        /// Размеры изображения.
        /// </summary>
        private Size _size;

        /// <summary>
        /// Инициализирует новый экземпляр класса LockImage.
        /// </summary>
        public LockImage()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса LockImage заданным изображением.
        /// </summary>
        /// <param name="img">Блокируемое изображение.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="img"/> имеет значение <b>null</b>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public LockImage(ref Bitmap img)
        {
            SetImage(ref img);
        }

        /// <summary>
        /// Блокирует изображение в системной памяти.
        /// </summary>
        /// <param name="img">Блокируемое изображение.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="img"/> имеет значение <b>null</b>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public void SetImage(ref Bitmap img)
        {
            if (img == null)
            {
                throw new ArgumentNullException(nameof(img));
            }

            _img = img;

            Rectangle rect = new Rectangle(0, 0, _img.Width, _img.Height);
            _bitmapData = _img.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            if ((_lockImage == null) || (_size != _img.Size)) // Память не выделена?
            {
                _size = _img.Size;
                _length = _bitmapData.Stride * _bitmapData.Height;
                _lockImage = new byte[_length];
            }

            Marshal.Copy(_bitmapData.Scan0, _lockImage, 0, _length);
        }

        /// <summary>
        /// Разблокирует это изображение System.Drawing.Bitmap из системной памяти.
        /// </summary>
        public void UnlockImage()
        {
            _img.UnlockBits(_bitmapData);
        }

        /// <summary>
        /// Возвращает цвет указанного пикселя.
        /// </summary>
        /// <param name="x">Возвращаемая координата пикселя по оси X.</param>
        /// <param name="y">Возвращаемая координата пикселя по оси Y.</param>
        /// <param name="alpha">Альфа-компонента цвета пиксела.</param>
        /// <param name="red">Красная компонента цвета пиксела.</param>
        /// <param name="green">Зелёная компонента цвета пиксела.</param>
        /// <param name="blue">Голубая компонента цвета пиксела.</param>
        /// <returns>Структура System.Drawing.Color, представляющая цвет указанного пикселя.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "x*4"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "5#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "4#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        public void GetLockPixel(int x, int y, out byte alpha, out byte red, out byte green, out byte blue)
        {
            int i = y * _bitmapData.Stride + x * 4;
            blue = _lockImage[i++];
            green = _lockImage[i++];
            red = _lockImage[i++];
            alpha = _lockImage[i];
        }

        /// <summary>
        /// Возвращает цвет указанного пикселя.
        /// </summary>
        /// <param name="x">Возвращаемая координата пикселя по оси X.</param>
        /// <param name="y">Возвращаемая координата пикселя по оси Y.</param>
        /// <param name="red">Красная компонента цвета пиксела.</param>
        /// <param name="green">Зелёная компонента цвета пиксела.</param>
        /// <param name="blue">Голубая компонента цвета пиксела.</param>
        /// <returns>Структура System.Drawing.Color, представляющая цвет указанного пикселя.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "4#"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        public void GetLockPixel(int x, int y, out byte red, out byte green, out byte blue)
        {
            byte alpha;
            GetLockPixel(x, y, out alpha, out red, out green, out blue);
        }

        /// <summary>
        /// Возвращает цвет указанного пикселя.
        /// </summary>
        /// <param name="x">Возвращаемая координата пикселя по оси X.</param>
        /// <param name="y">Возвращаемая координата пикселя по оси Y.</param>
        /// <returns>Структура System.Drawing.Color, представляющая цвет указанного пикселя.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public Color GetLockPixel(int x, int y)
        {
            byte alpha;
            byte red;
            byte green;
            byte blue;

            GetLockPixel(x, y, out alpha, out red, out green, out blue);

            return Color.FromArgb(alpha, red, green, blue);
        }

        /// <summary>
        /// Установить цвет указанного пикселя.
        /// </summary>
        /// <param name="x">Устанавливаемая координата пикселя по оси X.</param>
        /// <param name="y">Устанавливаемая координата пикселя по оси Y.</param>
        /// <param name="c"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c")]
        public void SetLockPixel(int x, int y, Color c)
        {
            int i = y * _bitmapData.Stride + x * 4;
            _lockImage[i++] = c.B;
            _lockImage[i++] = c.G;
            _lockImage[i++] = c.R;
            _lockImage[i] = c.A;
        }
    }
}