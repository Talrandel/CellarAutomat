#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : SRDescriptionAttribute.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 20:40
// Last Revision : 17.06.2017 20:49
// Description   : 
#endregion

using System;
using System.ComponentModel;

namespace CellarAutomatForm.Components
{
    /// <summary>
    /// Задает описание свойства или события.
    /// </summary>
    /// <remarks>От стандартного атрибута <see cref="DescriptionAttribute"/> отличается поиском в локальном хранилище локализованных строк.</remarks>
    [AttributeUsage(AttributeTargets.All)]
    // ReSharper disable once InconsistentNaming
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
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
