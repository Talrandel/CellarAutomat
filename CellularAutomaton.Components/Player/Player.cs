#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : Player.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 29.06.2017 22:50
// Last Revision : 01.07.2017 23:06
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Timers;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Предоставляет методы для визуализации работы клеточного автомата.
    /// </summary>
    public class Player : IPlayer, IDisposable
    {
        #region Static Fields and Constants
        /// <summary>
        /// Максимальное число кадров в минуту.
        /// </summary>
        public const short FramesPerMinuteMaxDefValue = 3600;

        /// <summary>
        /// Минимальное число кадров в минуту.
        /// </summary>
        public const short FramesPerMinuteMinDefValue = 1;

        /// <summary>
        /// Значение числа кадров в минуту по умолчанию.
        /// </summary>
        public const short FramesPerMinuteValueDefValue = 60;
        #endregion

        #region Fields
        /// <summary>
        /// Экземпляр класса <see cref="BufferedGraphicsContext"/> управляющий <see cref="_bufGr"/>.
        /// </summary>
        private readonly BufferedGraphicsContext _bufGrContext;

        /// <summary>
        /// Поверхность рисования GDI+ на которую осуществляется вывод изображения.
        /// </summary>
        private readonly Graphics _e;

        /// <summary>
        /// Объект <see cref="Timer"/> управляющий сменой кадров.
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// Графический буфер на который осуществляется вывод изображения.
        /// </summary>
        private Buffered​Graphics _bufGr;

        /// <summary>
        /// Номер текущего кадра записи.
        /// </summary>
        private int _currenFrame;

        /// <summary>
        /// True, если освобождение ресурсов осуществлялось, иначе false.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Число кадров в минуту.
        /// </summary>
        private short _framesPerMinute;

        /// <summary>
        /// Воспроизводимая запись.
        /// </summary>
        private IRecord _record;

        /// <summary>
        /// Перечислитель записи.
        /// </summary>
        private IEnumerator<Bitmap> _recordEnumerator;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Player"/>.
        /// </summary>
        /// <param name="e">Поверхность рисования GDI+ на которую осуществляется вывод изображения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="e"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e")]
        public Player(Graphics e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            _e = e;

            _bufGrContext = new BufferedGraphicsContext();

            _timer = new Timer();
            _timer.Elapsed += TimerElapsed;

            FramesPerMinute = Convert.ToInt16(Resources.Player__FramesPerMinuteDefValue, CultureInfo.CurrentCulture);

            Load(new Record());
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Освобождает все ресурсы занимаемые <see cref="Player"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IPlayer Members
        /// <summary>
        /// Происходит при смене кадра.
        /// </summary>
        public event EventHandler<ChangeFrameEventArgs> ChangeFrame;

        /// <summary>
        /// Возвращает воспроизводимую запись.
        /// </summary>
        public IReadOnlyRecord Record => (IReadOnlyRecord)_record;

        /// <summary>
        /// Возвращает состояние проигрывателя.
        /// </summary>
        public StatePlayer State { get; private set; }

        /// <summary>
        /// Возвращает или задаёт число кадров в минуту.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Значение <see name="FramesPerMinute"/> должно лежать в диапазоне от <see cref="FramesPerMinuteMinDefValue"/> до <see name="FramesPerMinuteMaxDefValue"/>.</exception>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see name="FramesPerMinuteValueDefValue"/>.</b>
        /// </remarks>
        public short FramesPerMinute
        {
            get { return _framesPerMinute; }
            set
            {
                if ((value < FramesPerMinuteMinDefValue) ||
                    (FramesPerMinuteMaxDefValue < value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinute),
                            FramesPerMinuteMinDefValue,
                            FramesPerMinuteMaxDefValue));
                }

                _framesPerMinute = value;

                //             1 second  =  1 000 milliseconds
                // 1 minute = 60 seconds = 60 000 milliseconds
                _timer.Interval = 60000D / value;
            }
        }

        /// <summary>
        /// Возвращает номер текущего кадра записи.
        /// </summary>
        public int CurrenFrame
        {
            get { return _currenFrame; }
            private set
            {
                _currenFrame = value;
                OnChangeFrame(new ChangeFrameEventArgs(value));
            }
        }

        /// <summary>
        /// Происходит при начале воспроизведения.
        /// </summary>
        public event EventHandler StartPlay;

        /// <summary>
        /// Происходит при окончании воспроизведения.
        /// </summary>
        public event EventHandler StopPlay;

        /// <summary>
        /// Происходит при приостановке воспроизведения.
        /// </summary>
        public event EventHandler PausePlay;

        /// <summary>
        /// Устанавливает указанную запись в проигрыватель.
        /// </summary>
        /// <param name="rec">Объект реализующий интерфейс <see cref="IRecord"/>.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="rec"/> имеет значение <b>null</b>.</exception>
        public void Load(IRecord rec)
        {
            Stop();
            if (rec == null)
                throw new ArgumentNullException(nameof(rec));

            RecordClear();
            _record = rec;
            InitializeNewRecord();
        }

        /// <summary>
        /// Освобождает ресурсы занимаемые воспроизводимой записью.
        /// </summary>
        /// <exception cref="InvalidOperationException">Воспроизведение не остановлено.</exception>
        public void RecordClear()
        {
            if (State != StatePlayer.Stop)
                throw new InvalidOperationException(Resources.Ex__PlaybackIsNotStopped);

            _record?.Clear();
        }

        /// <summary>
        /// Загружает запись из указаннного файла.
        /// </summary>
        /// <param name="fileName">Имя файла содержащего запись для воспроизведения.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Load(string fileName)
        {
            Stop();
            _record.Load(fileName);
            InitializeNewRecord();
        }

        /// <summary>
        /// Приостанавливает воспроизведение записи.
        /// </summary>
        public void Pause()
        {
            if (State == StatePlayer.Play)
            {
                State = StatePlayer.Pause;
                OnPausePlay();

                _timer.Stop();
            }
        }

        /// <summary>
        /// Начинает воспроизведение записи с текущей позиции записи.
        /// </summary>
        /// <exception cref="Invalid​Operation​Exception">Не загружена запись для воспроизведения.</exception>
        public void Play()
        {
            if (_record == null)
                throw new Invalid​Operation​Exception(Resources.Ex__RecordNotLoaded);

            if (0 < _record.Count) // Есть кадры для воспроизведения?
            {
                if (State != StatePlayer.Play)
                {
                    State = StatePlayer.Play;
                    OnStartPlay();

                    _timer.Start();
                }
            }
        }

        /// <summary>
        /// Осуществляет переход к указаннному кадру записи.
        /// </summary>
        /// <param name="frame">Номер кадра.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <para>Выполнена попытка перехода к кадру с отрицательным индексом.</para>
        ///     <para>-- или --</para>
        ///     <para>Выполнена попытка перехода к кадру номер которого больше, чем кадров в текущей записи.</para>
        /// </exception>
        public void Rewind(int frame)
        {
            Pause();

            if (frame < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                    Resources.Ex__You_are_attempting_to_go_to_frame_with_a_negative_index_);
            }

            if (_record.Count < frame)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                    Resources.Ex__You_are_attempting_jump_to_frame_number_that_is_larger_than_count_frames_in_current_record_);
            }

            if (frame != CurrenFrame) // Необходима перемотка?
            {
                if (frame < CurrenFrame) // Необходимо перемотать на предыдущий кадр?
                    Reset();

                for (int i = CurrenFrame; i < frame; i++) // Перейти к указанному кадру.
                    MoveNext();
            }

            CurrenFrame = frame;
        }

        /// <summary>
        /// Останавливает воспроизведение и переходит в начало записи.
        /// </summary>
        public void Stop()
        {
            if (State != StatePlayer.Stop)
            {
                State = StatePlayer.Stop;
                _timer.Stop();
                OnStopPlay();
                Reset();
                DrawCurrentFrame();
            }
        }

        /// <summary>
        /// Воспроизводит текущий кадр.
        /// </summary>
        public void Paint()
        {
            _bufGr.Render();
        }
        #endregion

        #region Members
        /// <summary>
        /// Освобождает все ресурсы, используемые текущим объектом <see cref="Player"/>.
        /// </summary>
        /// <param name="disposing">True - освободить управляемые и неуправляемые ресурсы; false освободить только неуправляемые ресурсы.</param>
        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_timer")]
        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_bufGrContext")]
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            Stop();

            if (disposing)
            {
                _timer?.Close();
                _e?.Dispose();
                _bufGr?.Dispose();
                _bufGrContext?.Dispose();
                RecordClear();
            }

            _disposed = true;
        }

        /// <summary>
        /// Вызывает событие <see cref="ChangeFrame"/>.
        /// </summary>
        /// <param name="e">Объект <see cref="ChangeFrameEventArgs"/> представляющий данные о событии.</param>
        protected virtual void OnChangeFrame(ChangeFrameEventArgs e)
        {
            ChangeFrame?.Invoke(this, e);
        }

        /// <summary>
        /// Вызывает событие <see cref="PausePlay"/>.
        /// </summary>
        protected virtual void OnPausePlay()
        {
            PausePlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StartPlay"/>.
        /// </summary>
        protected virtual void OnStartPlay()
        {
            StartPlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StopPlay"/>.
        /// </summary>
        protected virtual void OnStopPlay()
        {
            StopPlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Рисует и воспроизводит текущий кадр.
        /// </summary>
        private void DrawCurrentFrame()
        {
            _bufGr.Graphics.DrawImage(_recordEnumerator.Current, 0, 0);
            Paint();
        }

        /// <summary>
        /// Подготавливает <see cref="Recorder.Recorder"/> к воспроизведению при установке новой записи <see cref="Core.Record"/>.
        /// </summary>
        private void InitializeNewRecord()
        {
            _recordEnumerator = Record.GetEnumerator();
            MoveNext();

            if ((_record.FieldSize.Width != 0) &&
                (_record.FieldSize.Height != 0))
                _bufGrContext.MaximumBuffer = _record.FieldSize;

            _bufGr?.Dispose();
            _bufGr = _bufGrContext.Allocate(_e, new Rectangle(0, 0, _record.FieldSize.Width, _record.FieldSize.Height));
        }

        /// <summary>
        /// Перемещяет перечислитель к следующему кадру.
        /// </summary>
        /// <returns><b>True</b>, если перечислитель был успешно перемещен к следующему элементу, иначе <b>false</b>, если был достигнут конец записи.</returns>
        private bool MoveNext()
        {
            return _recordEnumerator.MoveNext();
        }

        /// <summary>
        /// Устанавливает перечислитель записи на первый кадр.
        /// </summary>
        private void Reset()
        {
            CurrenFrame = 0;
            _recordEnumerator.Reset();
            MoveNext();
        }

        /// <summary>
        /// Обработчик события <see cref="Timer.Elapsed"/>. Передаёт текущий кадр в буфер <see cref="_bufGr"/> для отображения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии <see cref="Timer.Elapsed"/>.</param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            DrawCurrentFrame();

            if (!MoveNext()) // Достигнут конец записи?
                Stop();
            else
                CurrenFrame++;
        }
        #endregion
    }
}
