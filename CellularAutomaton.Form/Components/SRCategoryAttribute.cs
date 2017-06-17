#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : SRCategoryAttribute.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 20:32
// Last Revision : 17.06.2017 20:47
// Description   : 
#endregion

using System;
using System.ComponentModel;

namespace CellarAutomatForm.Components
{
    /// <summary>
    /// Задает описание свойства или события.
    /// </summary>
    /// <remarks>От стандартного атрибута <see cref="CategoryAttribute"/> отличается поиском в локальном хранилище локализованных строк.</remarks>
    [AttributeUsage(AttributeTargets.All)]
    // ReSharper disable once InconsistentNaming
    internal sealed class SRCategoryAttribute : CategoryAttribute
    {
        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SRCategoryAttribute"/> заданным именем категории.
        /// </summary>
        /// <param name="category">Имя категории.</param>
        public SRCategoryAttribute(string category) : base(category) { }
        #endregion

        #region Members
        /// <summary>
        /// Выполняет поиск локализованной версии имени заданной категории.
        /// </summary>
        /// <param name="value">Идентификатор искомой категории.</param>
        /// <returns>Локализованное имя категории или значение null, если локализованное имя не существует.</returns>
        protected override string GetLocalizedString(string value)
        {
            // TODO: string GetLocalizedString(string value)
            return value;
            //throw new NotImplementedException();
        }
        #endregion
    }
}
