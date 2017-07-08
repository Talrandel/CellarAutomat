#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IReadOnlyField.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 08.07.2017 11:31
// Description   : 
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов доступными только для чтения.
    /// </summary>
    public interface IReadOnlyField : IEquatable<IField>, ICloneable
    {
        #region Properties
        /// <summary>
        /// Возвращает высоту поля.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Возвращает ширину поля.
        /// </summary>
        int Width { get; }
        #endregion

        #region Indexers
        /// <summary>
        /// Возвращает значение клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Значение клетки.</returns>
        [SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        int this[int x, int y] { get; }
        #endregion

        #region Members
        /// <summary>
        /// Осуществляет копирование текущего поля в заданное.
        /// </summary>
        /// <param name="other">Поле, в которое осуществляется копирование.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="other"/> имеет значение <b>null</b>.</exception>
        void CopyTo(IField other);

        /// <summary>
        /// Возвращает состояние клетки расположенной в заданном направлении относительно клетки заданной координатами.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="direction">Направление движения.</param>
        /// <returns>Состояние клетки.  Значение -1, если клетка по указанным координатам лежит вне поля.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Недопустимое значение параметра <paramref name="direction"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        int GetCellAtDirection(int x, int y, Direction direction);
        #endregion
    }
}
