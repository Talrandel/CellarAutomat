#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IRecord.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 23.06.2017 10:56
// Last Revision : 23.06.2017 11:04
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов.
    /// </summary>
    public interface IRecord : ICollection<Bitmap>, ICloneable
    {
        #region Properties
        /// <summary>
        /// Возвращает или задаёт название правила поведения клеточного автомата.
        /// </summary>
        string Rule { get; set; }

        /// <summary>
        /// Возвращает или задаёт количество состояний клетки клеточного автомата.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Количество состояний клетки клеточного автомата должно лежать в интервале [<see cref="CellularAutomaton.StatesNumberMin"/>; <see cref="CellularAutomaton.StatesNumberMax"/>].</exception>
        int StatesCount { get; set; }
        #endregion

        #region Members
        /// <summary>
        /// Загружает запись из указанного файла.
        /// </summary>
        /// <param name="fileName">Имя файла для содержащего запись.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        void Load(string fileName);

        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        void Save(string fileName);
        #endregion
    }
}
