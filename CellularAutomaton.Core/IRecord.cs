#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IRecord.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 23.06.2017 10:56
// Last Revision : 24.06.2017 18:54
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
        /// Возвращает размеры поля клеточного автомата.
        /// </summary>
        Size FieldSize { get; }

        /// <summary>
        /// Возвращает число поколений.
        /// </summary>
        int Generation { get; }

        /// <summary>
        /// Возвращает название правила поведения клеточного автомата.
        /// </summary>
        string Rule { get; }

        /// <summary>
        /// Возвращает количество состояний клетки клеточного автомата.
        /// </summary>
        int StatesCount { get; }
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
