﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : IRecorder.cs
// Author        : Антипкин С.С.
// Created       : 13.06.2017 21:43
// Last Revision : 14.06.2017 22:50
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core.Helpers.Recorder
{
    /// <summary>
    /// Интерфейс регистратора, позволяющего записывать функциорирование клеточного автомата.
    /// </summary>
    public interface IRecorder
    {
        #region Properties
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
        /// Останавливает запись.
        /// </summary>
        void Stop();

        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        void Save(string fileName);

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
    }
}