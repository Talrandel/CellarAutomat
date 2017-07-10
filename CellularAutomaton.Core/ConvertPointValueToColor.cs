#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : ConvertPointValueToColor.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 10.07.2017 17:51
// Last Revision : 10.07.2017 17:52
// Description   : 
#endregion

using System.Drawing;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Делегат описания метода преобразования состояния клетки поля клеточного автомата в цвет.
    /// </summary>
    /// <param name="value">Состояние клетки поля.</param>
    /// <returns>Цвет соответствующий состоянию клетки.</returns>
    public delegate Color ConvertPointValueToColor(int value);
}
