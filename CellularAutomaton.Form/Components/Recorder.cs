#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 20:30
// Last Revision : 18.06.2017 10:50
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using CellularAutomaton.Core.Rules;

namespace CellarAutomatForm.Components
{
    /// <summary>
    /// Представляет регистратор функционирования клеточного автомата описываемого <see cref="CellularAutomaton.Core.CellularAutomaton"/>.
    /// </summary>
    public partial class Recorder : UserControl
    {
        #region Properties
        /// <summary>
        /// Возвращает или задаёт имя файла в который осуществляется сохранение записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("CatData")]
        [SRDescription(nameof(FileName) + SRDescriptionAttribute.Suffix)]
        public string FileName { get; set; }

        /// <summary>
        /// Возвращает или задаёт минимальную ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldWidthMin"/>' должно лежать в диапазоне от 0 до '<see cref="SizeFieldWidthMax"/>'.</exception>
        [DefaultValue(50)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldWidth")]
        [SRDescription(nameof(SizeFieldWidthMin) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthMin
        {
            get { return Convert.ToInt16(nUDWidth.Minimum); }
            set
            {
                if (value < 0 ||
                    SizeFieldWidthMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldWidthMin),
                        value,
                        $"Значение '{nameof(SizeFieldWidthMin)}' должно лежать в диапазоне от 0 до '{nameof(SizeFieldWidthMax)}'.");
                }

                nUDWidth.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 500.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldWidthMax"/>' не должно быть меньше '<see cref="SizeFieldWidthMin"/>'.</exception>
        [DefaultValue(500)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldWidth")]
        [SRDescription(nameof(SizeFieldWidthMax) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthMax
        {
            get { return Convert.ToInt16(nUDWidth.Maximum); }
            set
            {
                if (value < SizeFieldWidthMin)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldWidthMax),
                        value,
                        $"Значение '{nameof(SizeFieldWidthMax)}' не должно быть меньше '{nameof(SizeFieldWidthMin)}'.");
                }

                nUDWidth.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightValue"/>' должно лежать в диапазоне от '<see cref="SizeFieldHeightMin"/>' до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(100)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldWidth")]
        [SRDescription(nameof(SizeFieldWidthValue) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthValue
        {
            get { return Convert.ToInt16(nUDWidth.Value); }
            set
            {
                if (value < SizeFieldWidthMin ||
                    SizeFieldWidthMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldWidthValue),
                        value,
                        $"Значение '{nameof(SizeFieldWidthValue)}' должно лежать в диапазоне от '{nameof(SizeFieldWidthMin)}' до '{nameof(SizeFieldWidthMax)}'.");
                }

                nUDWidth.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightMin"/>' должно лежать в диапазоне от 0 до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(50)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldHeight")]
        [SRDescription(nameof(SizeFieldHeightMin) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightMin
        {
            get { return Convert.ToInt16(nUDHeight.Minimum); }
            set
            {
                if (value < 0 ||
                    SizeFieldHeightMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldHeightMin),
                        value,
                        $"Значение '{nameof(SizeFieldHeightMin)}' должно лежать в диапазоне от 0 до '{nameof(SizeFieldHeightMax)}'.");
                }

                nUDHeight.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 500.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightMax"/>' не должно быть меньше '<see cref="SizeFieldHeightMin"/>'.</exception>
        [DefaultValue(500)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldHeight")]
        [SRDescription(nameof(SizeFieldHeightMax) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightMax
        {
            get { return Convert.ToInt16(nUDHeight.Maximum); }
            set
            {
                if (value < SizeFieldHeightMin)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldHeightMax),
                        value,
                        $"Значение '{nameof(SizeFieldHeightMax)}' не должно быть меньше '{nameof(SizeFieldHeightMin)}'.");
                }

                nUDHeight.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">'<see cref="SizeFieldHeightValue"/>' должно лежать в диапазоне от '<see cref="SizeFieldHeightMin"/>' до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(100)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("SizeFieldHeight")]
        [SRDescription(nameof(SizeFieldHeightValue) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightValue
        {
            get { return Convert.ToInt16(nUDHeight.Value); }
            set
            {
                if (value < SizeFieldHeightMin ||
                    SizeFieldHeightMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SizeFieldHeightValue),
                        value,
                        $"'{nameof(SizeFieldHeightValue)}' должно лежать в диапазоне от '{nameof(SizeFieldHeightMin)}' до '{nameof(SizeFieldHeightMax)}'.");
                }

                nUDHeight.Value = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию правил построения клеточных автоматов.
        /// </summary>
        [Browsable(false)]
        public IList<IRule> Rules { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="Recorder"/>.
        /// </summary>
        public Recorder()
        {
            ObservableCollection<IRule> innerRules = new ObservableCollection<IRule>();
            innerRules.CollectionChanged += InnerRules_CollectionChanged;
            Rules = innerRules;

            InitializeComponent();

            SizeFieldWidthMin = SizeFieldHeightMin = 50;
            SizeFieldWidthMax = SizeFieldHeightMax = 500;
            SizeFieldWidthValue = SizeFieldHeightValue = 100;
        }
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="ObservableCollection{T}.CollectionChanged"/>. Актуализирует состояние <see cref="cBCellularAutomatonRules"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void InnerRules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            cBCellularAutomatonRules.Items.Clear();
            cBCellularAutomatonRules.Items.AddRange((object[])Rules.Select(item => item.Name));
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Начинает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bRecord_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Сохраняет запись функционирования клеточного автомата в файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
