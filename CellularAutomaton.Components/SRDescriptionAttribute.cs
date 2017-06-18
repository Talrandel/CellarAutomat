#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : SRDescriptionAttribute.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:11
// Last Revision : 18.06.2017 12:12
// Description   : 
#endregion

using System;
using System.ComponentModel;

namespace CellularAutomaton.Components
{
    /// <summary>
    /// Задает описание свойства или события.
    /// </summary>
    /// <remarks>От стандартного атрибута <see cref="DescriptionAttribute"/> отличается поиском в локальном хранилище локализованных строк.</remarks>
    [AttributeUsage(AttributeTargets.All)]
    // ReSharper disable once InconsistentNaming
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        #region Static Fields and Constants
        /// <summary>
        /// Суффикс, добавляемый к именам ресурсов с описанием свойств и событий.
        /// </summary>
        public const string Suffix = "Descr";
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает описание, хранящееся в данном атрибуте.
        /// </summary>
        public override string Description
        {
            get
            {
                // TODO: string Description
                return "";
                //throw new NotImplementedException();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SRDescriptionAttribute"/> с указанным описанием.
        /// </summary>
        /// <param name="description">Текст описания.</param>
        public SRDescriptionAttribute(string description) : base(description) { }
        #endregion
    }
}
