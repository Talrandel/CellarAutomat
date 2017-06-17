#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 13:14
// Last Revision : 17.06.2017 16:30
// Description   : 
#endregion

using System;
using System.Drawing;

namespace CellularAutomaton.Core.Helpers.Recorder
{
    /// <summary>
    /// Представляет регистратор функционирования клеточного автомата.
    /// </summary>
    public class Recorder : IRecorder
    {
        #region Fields
        /// <summary>
        /// Регистрируемый клеточный автомат.
        /// </summary>
        private readonly CellularAutomaton _ca;

        /// <summary>
        /// Метод преобразования кода клетки поля в цвет.
        /// </summary>
        private readonly ConvertPointValueToColor _colorize;

        /// <summary>
        /// Запись функционирования клеточного автомата.
        /// </summary>
        private readonly Record.Record _record;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Recorder"/>.
        /// </summary>
        /// <param name="ca">Экземпляр класса <see cref="CellularAutomaton"/> функционирование которого регистрируется.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="ca"/> имеет значение <b>null</b>.</exception>
        public Recorder(CellularAutomaton ca)
        {
            if (ca == null)
                throw new ArgumentNullException(nameof(ca));

            _ca = ca;
            _ca.GenerationChanged += CellularAutomatonGenerationChanged;

            _record = new Record.Record();
            _colorize = PointValueToColor;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Recorder"/>, заданной функцией раскрашивания результатов функционирования.
        /// </summary>
        /// <param name="ca">Экземпляр класса <see cref="CellularAutomaton"/> функционирование которого регистрируется.</param>
        /// <param name="colorize">Функция раскрашивания визуализируемого клеточного автомата.</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>Параметр <paramref name="ca"/> имеет значение <b>null</b>.</para>
        ///     <para>-- или --</para>
        ///     <para>Параметр <paramref name="colorize"/> имеет значение <b>null</b>.</para>
        /// </exception>
        public Recorder(CellularAutomaton ca, ConvertPointValueToColor colorize) : this(ca)
        {
            if (colorize == null)
                throw new ArgumentNullException(nameof(colorize));

            _colorize = colorize;
        }
        #endregion

        #region IRecorder Members
        /// <summary>
        /// Возвращает состояние регистратора.
        /// </summary>
        public StateRecorder State { get; private set; } = StateRecorder.Stop;

        /// <summary>
        /// Начинает запись.
        /// </summary>
        public void Record()
        {
            if (State != StateRecorder.Record)
            {
                State = StateRecorder.Record;
                _ca.Stop = false;

                OnStartRecord();

                _ca.Processing();
            }
        }

        /// <summary>
        /// Останавливает запись.
        /// </summary>
        public void Stop()
        {
            if (State != StateRecorder.Stop)
            {
                State = StateRecorder.Stop;
                _ca.Stop = true;

                OnStopRecord();
            }
        }

        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Save(string fileName)
        {
            Stop();
            _record.Save(fileName);
        }

        /// <summary>
        /// Происходит при начале записи.
        /// </summary>
        public event EventHandler StartRecord;

        /// <summary>
        /// Происходит при окончании записи.
        /// </summary>
        public event EventHandler StopRecord;
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="CellularAutomaton.GenerationChanged"/>. Создаёт и сохраняет новый кадр в записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void CellularAutomatonGenerationChanged(object sender, EventArgs e)
        {
            _record.Rec.Add(DrawingFromField(_ca.CurrentField));
        }

        /// <summary>
        /// Создаёт рисунок из поля клеточного автомата.
        /// </summary>
        /// <param name="field">Визуализируемое поле клеточного автомата.</param>
        /// <returns>Визуализированное поле.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="field"/> имеет значение <b>null</b>.</exception>
        public Bitmap DrawingFromField(Field field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));

            int width = field.Width;
            int height = field.Height;

            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(width, height);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                        bitmap.SetPixel(i, j, _colorize(field[i, j]));
                }

                return bitmap;
            }
            catch
            {
                bitmap?.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Преобразует состояние клетки поля клеточного автомата в цвет.
        /// </summary>
        /// <param name="value">Состояние клетки поля.</param>
        /// <returns>Цвет соответствующий состоянию клетки.</returns>
        private static Color PointValueToColor(int value)
        {
            switch (value)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Pink;
                case 5:
                    return Color.DarkBlue;
                case 6:
                    return Color.White;
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
                    return Color.Black;
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="StartRecord"/>.
        /// </summary>
        protected virtual void OnStartRecord()
        {
            StartRecord?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StopRecord"/>.
        /// </summary>
        protected virtual void OnStopRecord()
        {
            StopRecord?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
