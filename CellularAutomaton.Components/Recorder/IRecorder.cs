#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : IRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 29.06.2017 17:55
// Last Revision : 03.07.2017 12:27
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

using CellularAutomaton.Core;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Определяет методы управления регистратором, позволяющим записывать функционирование клеточного автомата.
    /// </summary>
    public interface IRecorder : IDisposable
    {
        #region Events
        /// <summary>
        /// Происходит при записи очередного кадра.
        /// </summary>
        event EventHandler FrameRecorded;

        /// <summary>
        /// Происходит при начале записи.
        /// </summary>
        event EventHandler StartRecord;

        /// <summary>
        /// Происходит при окончании записи.
        /// </summary>
        event EventHandler StopRecord;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает доступную только для чтения записанную запись.
        /// </summary>
        IReadOnlyRecord GetRecord { get; }

        /// <summary>
        /// Возвращает состояние регистратора.
        /// </summary>
        StateRecorder State { get; }
        #endregion

        #region Members
        /// <summary>
        /// Начинает запись.
        /// </summary>
        void Record();

        /// <summary>
        /// Освобождает ресурсы занимаемые записанной записью.
        /// </summary>
        /// <exception cref="InvalidOperationException">Запись не остановлена.</exception>
        void RecordClear();

        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        void Save(string fileName);

        /// <summary>
        /// Останавливает запись.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop")]
        void Stop();
        #endregion

        //void Save(Stream stream);
        ///// <param name="stream">Поток для сохранения записи.</param>
        ///// </summary>
        ///// Сохраняет записанную запись в указанный поток.

        ///// <summary>
    }
}
