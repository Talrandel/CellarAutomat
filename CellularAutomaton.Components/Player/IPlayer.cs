#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : IPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:46
// Last Revision : 22.06.2017 15:48
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

using CellularAutomaton.Core;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Определеят методы управления проигрывателем записей работы клеточного автомата.
    /// </summary>
    public interface IPlayer
    {
        #region Events
        /// <summary>
        /// Происходит при приостановке воспроизведения.
        /// </summary>
        event EventHandler PausePlay;

        /// <summary>
        /// Происходит при смене кадра.
        /// </summary>
        event EventHandler<ChangeFrameEventArgs> ChangeFrame;

        /// <summary>
        /// Происходит при начале воспроизведения.
        /// </summary>
        event EventHandler StartPlay;

        /// <summary>
        /// Происходит при окончании воспроизведения.
        /// </summary>
        event EventHandler StopPlay;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает номер текущего кадра записи.
        /// </summary>
        short CurrenFrame { get; }

        /// <summary>
        /// Возвращает или задаёт скорость воспроизведения (кадры в минуту).
        /// </summary>
        byte FramesPerMinute { get; set; }

        /// <summary>
        /// Возвращает воспроизводимую запись.
        /// </summary>
        Record Record { get; }

        /// <summary>
        /// Возвращает состояние проигрывателя.
        /// </summary>
        StatePlayer State { get; }
        #endregion

        #region Members
        /// <summary>
        /// Загружает указанную запись в проигрыватель.
        /// </summary>
        /// <param name="rec">Запись для воспроизведения.</param>
        void Load(Record rec);

        /// <summary>
        /// Загружает запись из указаннного файла.
        /// </summary>
        /// <param name="fileName">Имя файла содержащего запись для воспроизведения.</param>
        void Load(string fileName);

        /// <summary>
        /// Приостанавливает воспроизведение записи.
        /// </summary>
        void Pause();

        /// <summary>
        /// Начинает воспроизведение записи с текущей позиции записи.
        /// </summary>
        void Play();

        /// <summary>
        /// Осуществляет переход к указаннному кадру записи.
        /// </summary>
        /// <param name="frame">Номер кадра.</param>
        void Rewind(short frame);

        /// <summary>
        /// Останавливает воспроизведение и переходит в начало записи.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop")]
        void Stop();
        #endregion
    }
}
