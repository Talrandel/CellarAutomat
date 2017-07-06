#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 06.07.2017 10:26
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core;

using FastBitmap;

namespace CellularAutomaton.Components.Recorder
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
        private readonly Core.CellularAutomaton _ca;

        /// <summary>
        /// Метод преобразования кода клетки поля в цвет.
        /// </summary>
        private readonly ConvertPointValueToColor _colorize;

        /// <summary>
        /// Источник сигнала отмены расчёта функционирования елеточного автомата <see cref="Record"/>.
        /// </summary>
        private readonly CancellationTokenSource _cts;

        /// <summary>
        /// Объект <see cref="LockImage"/> предоставляющий методы работы с заблокированным в памяти объектом <see cref="Bitmap"/>.
        /// </summary>
        private readonly Lazy<LockImage> _li;

        /// <summary>
        /// Объект блокировки.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Запись функционирования клеточного автомата.
        /// </summary>
        private readonly IRecord _record;

        /// <summary>
        /// <b>True</b>, если освобождение ресурсов осуществлялось, иначе <b>false</b>.
        /// </summary>
        private bool _disposed;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Recorder"/>.
        /// </summary>
        /// <param name="ca">Экземпляр класса <see cref="CellularAutomaton"/> функционирование которого регистрируется.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="ca"/> имеет значение <b>null</b>.</exception>
        public Recorder(Core.CellularAutomaton ca)
        {
            if (ca == null)
                throw new ArgumentNullException(nameof(ca));

            _ca = ca;
            _ca.GenerationChanged += CellularAutomatonGenerationChanged;

            _li = new Lazy<LockImage>(LazyThreadSafetyMode.None);

            _record = new Record(ca);
            _colorize = PointValueToColor;

            _cts = new CancellationTokenSource();
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
        public Recorder(Core.CellularAutomaton ca, ConvertPointValueToColor colorize) : this(ca)
        {
            if (colorize == null)
                throw new ArgumentNullException(nameof(colorize));

            _colorize = colorize;
        }
        #endregion

        #region IRecorder Members
        /// <summary>
        /// Освобождает все ресурсы занимаемые <see cref="Recorder"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Возвращает доступную только для чтения записанную запись.
        /// </summary>
        public IReadOnlyRecord GetRecord => (IReadOnlyRecord)_record;

        /// <summary>
        /// Возвращает состояние регистратора.
        /// </summary>
        public StateRecorder State { get; private set; } = StateRecorder.Stop;

        /// <summary>
        /// Начинает запись.
        /// </summary>
        public async void Record()
        {
            if (State != StateRecorder.Record)
            {
                Stop();
                RecordClear();
                State = StateRecorder.Record;

                OnStartRecord();
                try
                {
                    await _ca.ProcessingAsync(_cts.Token);
                }
                catch (OperationCanceledException)
                {
                    if (!_cts.IsCancellationRequested)
                        Stop();
                }
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
                _cts.Cancel();

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

        /// <summary>
        /// Происходит при записи очередного кадра.
        /// </summary>
        public event EventHandler FrameRecorded;

        /// <summary>
        /// Освобождает ресурсы занимаемые записанной записью.
        /// </summary>
        /// <exception cref="InvalidOperationException">Запись не остановлена.</exception>
        public void RecordClear()
        {
            if (State != StateRecorder.Stop)
                throw new InvalidOperationException(Resources.Ex__RecordingIsNotStopped);

            _record.Clear();
        }
        #endregion

        #region Members
        /// <summary>
        /// Создаёт рисунок из поля клеточного автомата.
        /// </summary>
        /// <param name="field">Визуализируемое поле клеточного автомата.</param>
        /// <returns>Визуализированное поле.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="field"/> имеет значение <b>null</b>.</exception>
        public Bitmap DrawingFromField(IReadOnlyField field)
        {
            lock (_lock)
            {
                if (field == null)
                    throw new ArgumentNullException(nameof(field));

                Bitmap bitmap = null;
                try
                {
                    bitmap = new Bitmap(field.Width, field.Height, PixelFormat.Format32bppArgb);

                    _li.Value.SetImage(bitmap, ImageLockMode.WriteOnly);

                    for (int i = 0; i < field.Width; i++)
                    {
                        for (int j = 0; j < field.Height; j++)
                            _li.Value.SetLockPixel(i, j, _colorize(field[i, j]));
                    }

                    _li.Value.UnlockImage();

                    return bitmap;
                }
                catch
                {
                    bitmap?.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые текущим объектом <see cref="Recorder"/>.
        /// </summary>
        /// <param name="disposing"><b>True</b> - освободить управляемые и неуправляемые ресурсы; <b>false</b> освободить только неуправляемые ресурсы.</param>
        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_cts")]
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            Stop();

            if (disposing)
            {
                _cts?.Dispose();
                _li?.Value.Dispose();
                RecordClear();
            }

            _disposed = true;
        }

        /// <summary>
        /// Вызывает событие <see cref="FrameRecorded"/>.
        /// </summary>
        protected virtual void OnFrameRecorded()
        {
            FrameRecorded?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// Обработчик события <see cref="Core.CellularAutomaton.GenerationChanged"/>. Создаёт и сохраняет новый кадр в записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void CellularAutomatonGenerationChanged(object sender, EventArgs e)
        {
            _record.Add(DrawingFromField(_ca.CurrentField));
            OnFrameRecorded();
        }

        /// <summary>
        /// Преобразует состояние клетки поля клеточного автомата в цвет.
        /// </summary>
        /// <param name="value">Состояние клетки поля.</param>
        /// <returns>Цвет соответствующий состоянию клетки.</returns>
        private static Color PointValueToColor(int value)
        {
            // TODO: заменить на функцию задания цвета?
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
        #endregion
    }
}
