#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 13.06.2017 21:43
// Last Revision : 16.06.2017 12:48
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core.Helpers.Player
{
    /// <summary>
    /// Интерфейс проигрывателя записей работы клеточного автомата.
    /// </summary>
    public interface IPlayer
    {
        #region Properties
        /// <summary>
        /// Возвращает состояние проигрывателя.
        /// </summary>
        StatePlayer State { get; }

        /// <summary>
        /// Возвращает или задаёт скорость воспроизведения (кадры в минуту).
        /// </summary>
        byte FramesPerMinute { get; set; }

        /// <summary>
        /// Возвращает номер текущего кадра в записи.
        /// </summary>
        short CurrenFrame { get; }
        #endregion

        #region Members
        /// <summary>
        /// Начинает воспроизведение записи с текущей позиции записи.
        /// </summary>
        void Play();

        /// <summary>
        /// Приостанавливает воспроизведение записи.
        /// </summary>
        void Pause();

        /// <summary>
        /// Останавливает воспроизведение и переходит в начало записи.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop")]
        void Stop();

        /// <summary>
        /// Осуществляет переход к указаннному кадру записи.
        /// </summary>
        /// <param name="frame">Номер кадра.</param>
        void Rewind(short frame);

        /// <summary>
        /// Происходит при начале воспроизведения.
        /// </summary>
        event EventHandler StartPlay;

        /// <summary>
        /// Происходит при окончании воспроизведения.
        /// </summary>
        event EventHandler StopPlay;

        /// <summary>
        /// Происходит при приостановке воспроизведения.
        /// </summary>
        event EventHandler PausePlay;

        /// <summary>
        /// Загружает указанную запись в проигрыватель.
        /// </summary>
        /// <param name="rec">Запись для воспроизведения.</param>
        void Load(Record.Record rec);

        /// <summary>
        /// Загружает запись из указаннного файла.
        /// </summary>
        /// <param name="fileName">Имя файла содержащего запись для воспроизведения.</param>
        void Load(string fileName);
        #endregion
    }
}
