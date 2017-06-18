#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:21
// Last Revision : 18.06.2017 12:34
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core.Rules;

namespace CellularAutomaton.Components.Recorder
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
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldWidthMin), nameof(SizeFieldWidthMax)));
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
                        string.Format(Resources.Ex__Значение___0___не_должно_быть_меньше___1___, nameof(SizeFieldWidthMax), nameof(SizeFieldWidthMin)));
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
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldWidthValue), nameof(SizeFieldWidthMin), nameof(SizeFieldWidthMax)));
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
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldHeightMin), nameof(SizeFieldHeightMax)));
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
                        string.Format(Resources.Ex__Значение___0___не_должно_быть_меньше___1___, nameof(SizeFieldHeightMax), nameof(SizeFieldHeightMin)));
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
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightValue"/>' должно лежать в диапазоне от '<see cref="SizeFieldHeightMin"/>' до '<see cref="SizeFieldHeightMax"/>'.</exception>
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
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldHeightValue), nameof(SizeFieldHeightMin), nameof(SizeFieldHeightMax)));
                }

                nUDHeight.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную плотность распределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 0.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityMin"/>' должно лежать в диапазоне от 0 до '<see cref="DencityMax"/>'.</exception>
        [DefaultValue(0)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Dencity")]
        [SRDescription(nameof(DencityMin) + SRDescriptionAttribute.Suffix)]
        public short DencityMin
        {
            get { return Convert.ToInt16(nUDDencity.Minimum); }
            set
            {
                if (value < 0 ||
                    DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(DencityMin),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityMin), nameof(DencityMax)));
                }

                nUDDencity.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную плотность распределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityMax"/>' должно лежать в интервале от '<see cref="DencityMin"/>' до 100.</exception>
        [DefaultValue(100)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Dencity")]
        [SRDescription(nameof(DencityMax) + SRDescriptionAttribute.Suffix)]
        public short DencityMax
        {
            get { return Convert.ToInt16(nUDDencity.Maximum); }
            set
            {
                if (value < DencityMin ||
                    value < 100)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(DencityMax),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityMax), nameof(DencityMin), 0));
                }

                nUDDencity.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт плотность рапределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityValue"/>' должно лежать в диапазоне от '<see cref="DencityMin"/>' до '<see cref="DencityMax"/>'.</exception>
        [DefaultValue(50)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Dencity")]
        [SRDescription(nameof(DencityValue) + SRDescriptionAttribute.Suffix)]
        public short DencityValue
        {
            get { return Convert.ToInt16(nUDDencity.Value); }
            set
            {
                if (value < DencityMin ||
                    DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(DencityValue),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityValue), nameof(DencityMin), nameof(DencityMax)));
                }

                nUDDencity.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальное число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMin"/>' должно лежать в диапазоне от <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/> до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesNumberMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("StatesCount")]
        [SRDescription(nameof(StatesCountMin) + SRDescriptionAttribute.Suffix)]
        public short StatesCountMin
        {
            get { return Convert.ToInt16(nUDStatesCount.Minimum); }
            set
            {
                if (value < Core.CellularAutomaton.StatesNumberMin ||
                    StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(StatesCountMin),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountMin), nameof(Core.CellularAutomaton.StatesNumberMin), nameof(StatesCountMax)));
                }

                nUDStatesCount.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальное число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMax"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMax"/>' должно лежать в интервале от '<see cref="StatesCountMin"/>' до <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMax"/>.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesNumberMax)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("StatesCount")]
        [SRDescription(nameof(StatesCountMax) + SRDescriptionAttribute.Suffix)]
        public short StatesCountMax
        {
            get { return Convert.ToInt16(nUDStatesCount.Maximum); }
            set
            {
                if (value < StatesCountMin ||
                    value < Core.CellularAutomaton.StatesNumberMax)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(StatesCountMax),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountMax), nameof(StatesCountMin), Core.CellularAutomaton.StatesNumberMax));
                }

                nUDStatesCount.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountValue"/>' должно лежать в диапазоне от '<see cref="StatesCountMin"/>' до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesNumberMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("StatesCount")]
        [SRDescription(nameof(StatesCountValue) + SRDescriptionAttribute.Suffix)]
        public short StatesCountValue
        {
            get { return Convert.ToInt16(nUDStatesCount.Value); }
            set
            {
                if (value < StatesCountMin ||
                    StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(StatesCountValue),
                        value,
                        string.Format(Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountValue), nameof(StatesCountMin), nameof(StatesCountMax)));
                }

                nUDStatesCount.Value = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию правил построения клеточных автоматов.
        /// </summary>
        [Browsable(false)]
        public IList<IRule> Rules { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="Recorder"/>.
        /// </summary>
        public Recorder()
        {
            InitializeComponent();
            InitializeProperties();
        }
        #endregion

        #region Members
        /// <summary>
        /// Устанавливает значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            ObservableCollection<IRule> innerRules = new ObservableCollection<IRule>();
            innerRules.CollectionChanged += InnerRules_CollectionChanged;
            Rules = innerRules;

            SizeFieldWidthMin = SizeFieldHeightMin = 50;
            SizeFieldWidthMax = SizeFieldHeightMax = 500;
            SizeFieldWidthValue = SizeFieldHeightValue = 100;

            DencityMin = 0;
            DencityMax = 100;
            DencityValue = 50;

            StatesCountMin = Core.CellularAutomaton.StatesNumberMin;
            StatesCountMax = Core.CellularAutomaton.StatesNumberMax;
            StatesCountValue = Core.CellularAutomaton.StatesNumberMin;
        }

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
