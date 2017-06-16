﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : MooreSimple.cs
// Author        : Антипкин С.С.
// Created       : 12.06.2017 16:39
// Last Revision : 14.06.2017 21:38
// Description   : 
#endregion

using System;

namespace CellularAutomaton.Core.Rules
{
    /// <summary>
    /// Правило клеточного автомата - обычный, область Мура.
    /// </summary>
    public class MooreSimple : IRule
    {
        #region IRule Members
        /// <summary>
        /// Изменяет состояние клетки расположенной по заданным координатам.
        /// </summary>
        /// <param name="pastFiled">Поле на прошлой итерации клеточного автомата.</param>
        /// <param name="x">Координата по оси X клетки.</param>
        /// <param name="y">Координата по оси Y клетки.</param>
        /// <param name="statesCount"><b>Не используется.</b> Количество состояний клетки.</param>
        /// <returns>Новое состояние клетки.</returns>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pastFiled"/> имеет значение <b>null</b>.</exception>
        public int TransformCell(Field pastFiled, int x, int y, int statesCount)
        {
            if (pastFiled == null)
                throw new ArgumentNullException(nameof(pastFiled));

            return (pastFiled.GetLiveNeighborCount(x, y) == 1) ? 1 : pastFiled.GetCell(x, y);
        }
        #endregion
    }
}