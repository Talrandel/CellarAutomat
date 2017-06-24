#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IReadOnlyRecord.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 22.06.2017 23:25
// Last Revision : 24.06.2017 18:54
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов доступными только для чтения.
    /// </summary>
    public interface IReadOnlyRecord : IReadOnlyCollection<Bitmap>, ICloneable
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
        /// <exception cref="ArgumentOutOfRangeException">Количество состояний клетки клеточного автомата должно лежать в интервале [<see cref="CellularAutomaton.StatesNumberMin"/>; <see cref="CellularAutomaton.StatesNumberMax"/>].</exception>
        int StatesCount { get; }
        #endregion

        #region Members
        /// <summary>
        /// Определяет содержит ли запись указанный кадр.
        /// </summary>
        /// <param name="item">Кадр, который требуется найти в записи.</param>
        /// <returns>Значение <b>true</b>, если кадр найден в записи, иначе - <b>false</b>.</returns>
        bool Contains(Bitmap item);

        /// <summary>
        /// Копирует элементы <see cref="Record"/> в массив <see cref="Bitmap"/>, начиная с указанного индекса.
        /// </summary>
        /// <param name="array">Одномерный массив <see cref="Bitmap"/>, в который копируются элементы из записи. Массив должен иметь индексацию, начинающуюся с нуля.</param>
        /// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, указывающий начало копирования.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="array"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
        /// <exception cref="ArgumentException">Количество элементов в записи превышает доступное место, начиная с индекса <paramref name="arrayIndex"/> до конца массива назначения <paramref name="array"/>.</exception>
        void CopyTo(Bitmap[] array, int arrayIndex);
        #endregion
    }
}
