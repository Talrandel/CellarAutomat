#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : Player.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:46
// Last Revision : 23.06.2017 11:58
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
        #region Fields
        /// <summary>
        /// Графический буфер на который осуществляется вывод изображения.
        /// </summary>
        private readonly Buffered​Graphics _bufGr;

        /// <summary>
        /// Экземпляр класса <see cref="BufferedGraphicsContext"/> управляющий <see cref="_bufGr"/>.
        /// </summary>
        private readonly BufferedGraphicsContext _bufGrContext;

        /// <summary>
        /// Объект <see cref="Timer"/> управляющий сменой кадров.
        /// </summary>
        private readonly Timer _timer;

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
        private byte _framesPerMinute;

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
        /// <param name="rec">Размеры области на которую осуществяется вывод изображения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="e"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentException">Значение высоты или ширины размера меньше или равно нулю.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e")]
        public Player(Graphics e, Rectangle rec)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            if ((rec.Width == 0) || (rec.Height == 0))
            {
                throw new ArgumentException(
                    Resources.Ex__Значение_высоты_или_ширины_размера_меньше_или_равно_нулю_, nameof(rec));
            }

            _bufGrContext = new BufferedGraphicsContext();
            _bufGr = _bufGrContext.Allocate(e, rec);
            _bufGrContext.MaximumBuffer = rec.Size;

            _timer = new Timer();
            _timer.Elapsed += TimerElapsed;

            FramesPerMinute = Convert.ToByte(Resources.Player__FramesPerMinuteDefValue, CultureInfo.CurrentCulture);
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
        /// <exception cref="ArgumentOutOfRangeException">Число кадров в минуту не может быть равной нулю величиной.</exception>
        /// <remarks>
        ///     <b>Значение по умолчанию - 2.</b>
        /// </remarks>
        public byte FramesPerMinute
        {
            get { return _framesPerMinute; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        Resources.Ex__Число_кадров_в_минуту_не_может_быть_равной_нулю_величиной_);
                }

                _framesPerMinute = value;

                //             1 second =  1 000 milliseconds
                // 1 minute = 60 second = 60 000 milliseconds
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
        /// <param name="rec">Запись для воспроизведения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="rec"/> имеет значение <b>null</b>.</exception>
        public void Load(Record rec)
        {
            Stop();
            if (rec == null)
                throw new ArgumentNullException(nameof(rec));

            _record = rec;
            GetRecordEnumerator();
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
            GetRecordEnumerator();
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
        /// <exception cref="ArgumentException">Не загружена запись для воспроизведения.</exception>
        public void Play()
        {
            if (_record == null)
                throw new ArgumentException(Resources.Ex__RecordNotLoaded, nameof(Record));

            if (State != StatePlayer.Play)
            {
                State = StatePlayer.Play;
                OnStartPlay();

                _timer.Start();
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
            bool isFastRewind = (frame - CurrenFrame == 1) && (frame <= _record.Count); // Возможна быстрая перемотка?
            bool isNotRewind = (frame == CurrenFrame); // Нет необходимости в перемотке?

            if (isFastRewind || isNotRewind)
                StopNoReset();
            else
                Stop();

            // TODO: Может быть стоит сначала проверять?
            if (frame < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                    Resources.Ex__Выполнена_попытка_перехода_к_кадру_с_отрицательным_индексом_);
            }

            if (_record.Count < frame)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                    Resources.Ex__Выполнена_попытка_перехода_к_кадру_номер_которого_больше__чем_кадров_в_текущей_записи_);
            }

            CurrenFrame = frame;

            if (!isNotRewind)
            {
                if (isFastRewind)
                    _recordEnumerator.MoveNext();
                else
                {
                    for (int i = 0; i < CurrenFrame; i++) // Перейти к указанному кадру.
                        _recordEnumerator.MoveNext();
                }
            }
        }

        /// <summary>
        /// Останавливает воспроизведение и переходит в начало записи.
        /// </summary>
        public void Stop()
        {
            if (State != StatePlayer.Stop)
            {
                State = StatePlayer.Stop;
                OnStopPlay();
                CurrenFrame = 0;
                _recordEnumerator.Reset();
                _timer.Stop();
            }
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

            if (disposing)
            {
                _bufGr?.Dispose();
                _bufGrContext?.Dispose();
                _timer?.Close();
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
        /// Возвращает перечислитель для записи <see cref="Record"/>.
        /// </summary>
        private void GetRecordEnumerator()
        {
            _recordEnumerator = Record.GetEnumerator();
        }

        /// <summary>
        /// Останавливает воспроизведение без перемотки в начало записи.
        /// </summary>
        private void StopNoReset()
        {
            if (State != StatePlayer.Stop)
            {
                State = StatePlayer.Stop;
                OnStopPlay();
                _timer.Stop();
            }
        }

        /// <summary>
        /// Обработчик события <see cref="Timer.Elapsed"/>. Передаёт текущий кадр в буфер <see cref="_bufGr"/> для отображения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии <see cref="Timer.Elapsed"/>.</param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _bufGr.Graphics.DrawImage(_recordEnumerator.Current, _bufGrContext.MaximumBuffer.Width, _bufGrContext.MaximumBuffer.Height);
            _bufGr.Render();

            CurrenFrame++;

            if (!_recordEnumerator.MoveNext()) // Не достигнут конец записи?
                Stop();
        }
        #endregion
    }
}
