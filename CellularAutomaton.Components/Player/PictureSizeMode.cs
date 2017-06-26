#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PictureSizeMode.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 26.06.2017 23:02
// Last Revision : 26.06.2017 23:17
// Description   : 
#endregion

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Указывает, как изображение размещается в пределах области отображения.
    /// </summary>
    public enum PictureSizeMode
    {
        /// <summary>
        /// Изображение размещается в левом верхнем углу области отображения. Изображение обрезается если оно больше области отображения.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Размер изображения растягивается или сжимается до размеров области отображения.
        /// </summary>
        StretchImage = 1,

        /// <summary>
        /// Размер области отображения равен размеру изображения.
        /// </summary>
        AutoSize = 2,

        /// <summary>
        /// Изображение выводится по центру, если область отображения больше изображения. Если изображение больше области отображения, тогда изображение размещается в центре области отображения и края обрезаются.
        /// </summary>
        CenterImage = 3,

        /// <summary>
        /// Размер изображения растягивается или сжимается до размеров области отображения с сохранением пропорций.
        /// </summary>
        Zoom = 4
    }
}
