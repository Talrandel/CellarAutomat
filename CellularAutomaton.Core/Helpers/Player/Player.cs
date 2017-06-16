#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : Player.cs
// Author        : Антипкин С.С.
// Created       : 13.06.2017 21:43
// Last Revision : 16.06.2017 0:15
// Description   : 
#endregion

using System;
using System.Drawing;
using System.Linq;
using System.Timers;

namespace CellularAutomaton.Core.Helpers.Player
{
    /// <summary>
    /// Предоставляет методы для визуализации работы клеточного автомата.
    /// </summary>
    public class Player : IPlayer
    {
        #region Fields
        /// <summary>
        /// Объект <see cref="Timer"/> управляющий сменой кадров.
        /// </summary>
        private readonly Timer _timer;


        // https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bufferedgraphics?view=netframework-4.7
        // https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bufferedgraphicscontext.allocate?view=netframework-4.7

        private Buffered​Graphics _bg;

        private BufferedGraphicsContext _context;

        /// <summary>
        /// Число кадров в минуту.
        /// </summary>
        private byte _framesPerMinute;

        /// <summary>
        /// Воспроизводимая запись.
        /// </summary>
        private Record.Record _record;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает число кадров в записи.
        /// </summary>
        public int GetFrames => _record.Rec.Count;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Player"/>.
        /// </summary>
        public Player()
        {
            _timer = new Timer();
            _timer.Elapsed += Reproduce;
        }
        #endregion

        #region IPlayer Members
        /// <summary>
        /// Возвращает состояние проигрывателя.
        /// </summary>
        public StatePlayer State { get; private set; }

        /// <summary>
        /// Возвращает или задаёт число кадров в минуту.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Число кадров в минуту не может равной нулю величиной.</exception>
        public byte FramesPerMinute
        {
            get => _framesPerMinute;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                                                          "Число кадров в минуту не может быть равной нулю величиной.");
                }

                _framesPerMinute = value;

                //             1 second =  1 000 milliseconds
                // 1 minute = 60 second = 60 000 milliseconds
                _timer.Interval = 60000D / value;
            }
        }

        /// <summary>
        /// Возвращает номер текущего воспроизводимого кадра записи.
        /// </summary>
        public short CurrenFrame { get; private set; }

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
        public void Load(Record.Record rec)
        {
            Stop();
            _record = rec ?? throw new ArgumentNullException(nameof(rec));
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
        public void Play()
        {
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
        public void Rewind(short frame)
        {
            Stop();

            if (frame < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                                                      "Выполнена попытка перехода к кадру с отрицательным индексом.");
            }

            if (_record.Rec.Count < frame)
            {
                throw new ArgumentOutOfRangeException(nameof(frame), frame,
                                                      "Выполнена попытка перехода к кадру номер которого больше, чем кадров в текущей записи.");
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
                OnStopPlay();
                CurrenFrame = 0;

                _timer.Stop();
            }
        }
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="Timer.Elapsed"/>. Передаёт текущий кадр в буфер <see cref="_bg"/> для отображения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reproduce(object sender, ElapsedEventArgs e)
        {
            Bitmap bitmap = _record.Rec.Skip(CurrenFrame++).First();
            // TODO: передать кадр в выходной буфер.
            throw new NotImplementedException();
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
        /// Вызывает событие <see cref="PausePlay"/>.
        /// </summary>
        protected virtual void OnPausePlay()
        {
            PausePlay?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
