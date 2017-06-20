﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : SRDescriptionAttribute.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:11
// Last Revision : 20.06.2017 22:55
// Description   : 
#endregion

using System;
using System.ComponentModel;

using CellularAutomaton.Components.Properties;

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

        #region Fields
        /// <summary>
        /// <b>True</b> - локализованная строка загружена из ресурсов, иначе <b>false</b>.
        /// </summary>
        private bool _isLoaded;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает описание, хранящееся в данном атрибуте.
        /// </summary>
        /// <returns>Локализованное описание или значение <b>null</b>, если локализованное описание не существует.</returns>
        public override string Description
        {
            get
            {
                if (!_isLoaded) // Локализованная строка не загружена из ресурсов?
                {
                    _isLoaded = true;
                    DescriptionValue = Resources.ResourceManager.GetString(base.Description);
                }

                return DescriptionValue;
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
