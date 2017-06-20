#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : IField.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 20:20
// Last Revision : 20.06.2017 21:14
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов.
    /// </summary>
    public interface IField
    {
        #region Indexers
        /// <summary>
        /// Возвращает или задаёт значение клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <returns>Значение клетки.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        int this[int x, int y] { get; set; }
        #endregion

        #region Members
        /// <summary>
        /// Задаёт начальные состояния клеток на поле.
        /// </summary>
        /// <param name="statesNumber">Количество состояний клетки.</param>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        /// <exception cref="ArgumentOutOfRangeException">Величина плотности распределения живых клеток на поле должна лежать в интервале [0; 100].</exception>
        void SetStartValues(int statesNumber, byte density);
        #endregion
    }
}
