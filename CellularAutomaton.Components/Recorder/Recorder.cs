#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 10.07.2017 22:15
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Представляет регистратор функционирования клеточного автомата.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Recorder : IRecorder
    {
        #region Fields
        /// <summary>
        /// Регистрируемый клеточный автомат.
        /// </summary>
        private readonly Core.CellularAutomaton _ca;

        /// <summary>
        /// Источник сигнала отмены расчёта функционирования елеточного автомата <see cref="Record"/>.
        /// </summary>
        private readonly CancellationTokenSource _cts;

        /// <summary>
        /// Экземпляр класса <see cref="FieldDrawing"/> предоставляющий методы для раскрашивания поля.
        /// </summary>
        private readonly FieldDrawing _fieldDrawing;

        /// <summary>
        /// Запись функционирования клеточного автомата.
        /// </summary>
        private readonly IRecord _record;

        /// <summary>
        /// <b>True</b>, если освобождение ресурсов осуществлялось, иначе <b>false</b>.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Состояние регистратора.
        /// </summary>
        private StateRecorder _state = StateRecorder.Stop;
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

            _record = new Record(ca);
            _fieldDrawing = new FieldDrawing();
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
        // ReSharper disable once UnusedMember.Global
        public Recorder(Core.CellularAutomaton ca, ConvertPointValueToColor colorize) : this(ca)
        {
            _fieldDrawing.Colorize = colorize;
        }
        #endregion

        #region IRecorder Members
        /// <summary>
        /// Возвращает доступную только для чтения записанную запись.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        public IReadOnlyRecord GetRecord
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return (IReadOnlyRecord)_record;
            }
        }

        /// <summary>
        /// Возвращает состояние регистратора.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        public StateRecorder State
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return _state;
            }
            private set { _state = value; }
        }

        /// <summary>
        /// Начинает запись.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        public async void Record()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

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
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        public void Stop()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

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
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Save(string fileName)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

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
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        /// <exception cref="InvalidOperationException">Запись не остановлена.</exception>
        public void RecordClear()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

            if (State != StateRecorder.Stop)
                throw new InvalidOperationException(Resources.Ex__RecordingIsNotStopped);

            _record.Clear();
        }

        /// <summary>
        /// Освобождает все ресурсы занимаемые <see cref="Recorder"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Members
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
                RecordClear();
            }

            _disposed = true;
        }

        /// <summary>
        /// Вызывает событие <see cref="FrameRecorded"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        protected virtual void OnFrameRecorded()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

            FrameRecorded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StartRecord"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        protected virtual void OnStartRecord()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

            StartRecord?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StopRecord"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Ресурсы этого объекта были освобождены.</exception>
        protected virtual void OnStopRecord()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);

            StopRecord?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события <see cref="Core.CellularAutomaton.GenerationChanged"/>. Создаёт и сохраняет новый кадр в записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void CellularAutomatonGenerationChanged(object sender, EventArgs e)
        {
            _record.Add(_fieldDrawing.Drawing((IField)_ca.CurrentField));
            OnFrameRecorded();
        }
        #endregion
    }
}
