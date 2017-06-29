﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : IRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:44
// Last Revision : 24.06.2017 12:58
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Определяет методы управления регистратором, позволяющим записывать функционирование клеточного автомата.
    /// </summary>
    public interface IRecorder : IDisposable
    {
        #region Events
        ///// <summary>
        ///// Сохраняет записанную запись в указанный поток.
        ///// </summary>
        ///// <param name="stream">Поток для сохранения записи.</param>
        //void Save(Stream stream);

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
        /// Возвращает число кадров в записи.
        /// </summary>
        int RecordCount { get; }

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
    }
}
