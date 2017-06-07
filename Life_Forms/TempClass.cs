using System;
using System.Drawing;

namespace CellarAutomatForm
{
    /// <summary>
    /// Класс - обертка для сериализации изображений автомата
    /// </summary>
    [Serializable]
    public class SerializableBitmap
    {
        public Bitmap[] BitmapArray { get; set; }
    }
}