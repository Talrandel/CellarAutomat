#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : ConvertPointValueToColor.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 12:24
// Last Revision : 17.06.2017 12:24
// Description   : 
#endregion

using System.Drawing;

namespace CellularAutomaton.Core.Helpers.Recorder
{
    /// <summary>
    /// Делегат описания метода преобразования состояния клетки поля клеточного автомата в цвет.
    /// </summary>
    /// <param name="value">Состояние клетки поля.</param>
    /// <returns>Цвет соответствующий состоянию клетки.</returns>
    public delegate Color ConvertPointValueToColor(int value);
}
