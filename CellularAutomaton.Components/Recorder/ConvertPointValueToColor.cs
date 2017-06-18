#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : ConvertPointValueToColor.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:44
// Last Revision : 18.06.2017 12:45
// Description   : 
#endregion

using System.Drawing;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Делегат описания метода преобразования состояния клетки поля клеточного автомата в цвет.
    /// </summary>
    /// <param name="value">Состояние клетки поля.</param>
    /// <returns>Цвет соответствующий состоянию клетки.</returns>
    public delegate Color ConvertPointValueToColor(int value);
}
