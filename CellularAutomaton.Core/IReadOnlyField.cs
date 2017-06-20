#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IReadOnlyField.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 20:21
// Last Revision : 20.06.2017 20:44
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов доступными только для чтения.
    /// </summary>
    public interface IReadOnlyField
    {
        #region Properties
        /// <summary>
        /// Возвращает ширину поля.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Возвращает высоту поля.
        /// </summary>
        int Height { get; }
        #endregion

        #region Indexers
        /// <summary>
        /// Возвращает значение клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Значение клетки.</returns>
        int this[int x, int y] { get; }
        #endregion

        #region Members
        /// <summary>
        /// Возвращает состояние клетки расположенной в заданном направлении относительно клетки заданной координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="direction">Направление движения.</param>
        /// <returns>Состояние клетки.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Недопустимое значение параметра <paramref name="direction"/>.</exception>
        int GetCellAtDirection(int x, int y, Directions direction);

        /// <summary>
        /// Осуществляет копирование текущего поля в заданное.
        /// </summary>
        /// <param name="other">Поле, в которое осуществляется копирование.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="other"/> имеет значение <b>null</b>.</exception>
        void Copy(ref Field other);
        #endregion
    }
}
