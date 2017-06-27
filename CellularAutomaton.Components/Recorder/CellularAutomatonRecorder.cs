#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 22.06.2017 23:25
// Last Revision : 26.06.2017 20:43
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core.Rules;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Представляет регистратор функционирования клеточного автомата описываемого <see cref="Core.CellularAutomaton"/>.
    /// </summary>
    public partial class CellularAutomatonRecorder : UserControl
    {
        #region Static Fields and Constants
        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityMax"/>.
        /// </summary>
        private const byte DencityMaxDefValue = 100;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityMin"/>.
        /// </summary>
        private const byte DencityMinDefValue = 0;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityValue"/>.
        /// </summary>
        private const byte DencityValueDefValue = 50;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightMax"/>.
        /// </summary>
        private const short SizeFieldHeightMaxDefValue = 500;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightMin"/>.
        /// </summary>
        private const short SizeFieldHeightMinDefValue = 50;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightValue"/>.
        /// </summary>
        private const short SizeFieldHeightValueDefValue = 100;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthMax"/>.
        /// </summary>
        private const short SizeFieldWidthMaxDefValue = 500;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthMin"/>.
        /// </summary>
        private const short SizeFieldWidthMinDefValue = 50;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthValue"/>.
        /// </summary>
        private const short SizeFieldWidthValueDefValue = 100;
        #endregion

        #region Fields
        /// <summary>
        /// Объект реализующий интерфейс <see cref="IRecorder"/>, которым осуществляется управление.
        /// </summary>
        private IRecorder _recorder;

        /// <summary>
        /// Экземпляр класса <see cref="SaveFileDialog"/> - окно сохранения файла с записью.
        /// </summary>
        private SaveFileDialog _saveFileDialog;

        /// <summary>
        /// Предсталяет метод обрабатывающий действия возникающие при начале записи <see cref="Started"/>.
        /// </summary>
        private Action _started;
        #endregion

        #region Properties

        /// <summary>
        /// Возвращает количество кадров в записи.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int FramesCount => _recorder.RecordCount; 

        /// <summary>
        /// Возвращает или задаёт максимальную плотность распределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityMax"/>' должно лежать в интервале от '<see cref="DencityMin"/>' до 100.</exception>
        [DefaultValue(DencityMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityMax) + SRDescriptionAttribute.Suffix)]
        public byte DencityMax
        {
            get { return Convert.ToByte(nUDDencity.Maximum); }
            set
            {
                if (value < DencityMin ||
                    value < 100)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(DencityMax),
                            nameof(DencityMin), 0));
                }

                nUDDencity.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную плотность распределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 0.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityMin"/>' должно лежать в диапазоне от 0 до '<see cref="DencityMax"/>'.</exception>
        [DefaultValue(DencityMinDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityMin) + SRDescriptionAttribute.Suffix)]
        public byte DencityMin
        {
            get { return Convert.ToByte(nUDDencity.Minimum); }
            set
            {
                if (DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(DencityMin),
                            nameof(DencityMax)));
                }

                nUDDencity.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт плотность рапределения клеток на поле клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="DencityValue"/>' должно лежать в диапазоне от '<see cref="DencityMin"/>' до '<see cref="DencityMax"/>'.</exception>
        [DefaultValue(DencityValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityValue) + SRDescriptionAttribute.Suffix)]
        public byte DencityValue
        {
            get { return Convert.ToByte(nUDDencity.Value); }
            set
            {
                if (value < DencityMin ||
                    DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(DencityValue),
                            nameof(DencityMin),
                            nameof(DencityMax)));
                }

                nUDDencity.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт расширение файла в который осуществляется сохранение записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileExtension) + SRDescriptionAttribute.Suffix)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Возвращает или задаёт фильтр имён файлов в диалоговом окне "Сохранить как...".
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileFilter) + SRDescriptionAttribute.Suffix)]
        public string FileFilter { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя файла в который осуществляется сохранение записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileName) + SRDescriptionAttribute.Suffix)]
        public string FileName { get; set; }

        /// <summary>
        /// Возвращает коллекцию правил построения клеточных автоматов.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<IRule> Rules { get; private set; }

        /// <summary>
        /// Возвращает или задаёт максимальную высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 500.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightMax"/>' не должно быть меньше '<see cref="SizeFieldHeightMin"/>'.</exception>
        [DefaultValue(SizeFieldHeightMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightMax) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightMax
        {
            get { return Convert.ToInt16(nUDHeight.Maximum); }
            set
            {
                if (value < SizeFieldHeightMin)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___GEQ___1___,
                            nameof(SizeFieldHeightMax),
                            nameof(SizeFieldHeightMin)));
                }

                nUDHeight.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightMin"/>' должно лежать в диапазоне от 0 до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(SizeFieldHeightMinDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightMin) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightMin
        {
            get { return Convert.ToInt16(nUDHeight.Minimum); }
            set
            {
                if (value < 0 ||
                    SizeFieldHeightMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(SizeFieldHeightMin),
                            nameof(SizeFieldHeightMax)));
                }

                nUDHeight.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт высоту поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightValue"/>' должно лежать в диапазоне от '<see cref="SizeFieldHeightMin"/>' до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(SizeFieldHeightValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightValue) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldHeightValue
        {
            get { return Convert.ToInt16(nUDHeight.Value); }
            set
            {
                if (value < SizeFieldHeightMin ||
                    SizeFieldHeightMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(SizeFieldHeightValue),
                            nameof(SizeFieldHeightMin),
                            nameof(SizeFieldHeightMax)));
                }

                nUDHeight.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 500.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldWidthMax"/>' не должно быть меньше '<see cref="SizeFieldWidthMin"/>'.</exception>
        [DefaultValue(SizeFieldWidthMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthMax) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthMax
        {
            get { return Convert.ToInt16(nUDWidth.Maximum); }
            set
            {
                if (value < SizeFieldWidthMin)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___GEQ___1___,
                            nameof(SizeFieldWidthMax),
                            nameof(SizeFieldWidthMin)));
                }

                nUDWidth.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 50.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldWidthMin"/>' должно лежать в диапазоне от 0 до '<see cref="SizeFieldWidthMax"/>'.</exception>
        [DefaultValue(SizeFieldWidthMinDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthMin) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthMin
        {
            get { return Convert.ToInt16(nUDWidth.Minimum); }
            set
            {
                if (value < 0 ||
                    SizeFieldWidthMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(SizeFieldWidthMin),
                            nameof(SizeFieldWidthMax)));
                }

                nUDWidth.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт ширину поля клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 100.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="SizeFieldHeightValue"/>' должно лежать в диапазоне от '<see cref="SizeFieldHeightMin"/>' до '<see cref="SizeFieldHeightMax"/>'.</exception>
        [DefaultValue(SizeFieldWidthValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthValue) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldWidthValue
        {
            get { return Convert.ToInt16(nUDWidth.Value); }
            set
            {
                if (value < SizeFieldWidthMin ||
                    SizeFieldWidthMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(SizeFieldWidthValue),
                            nameof(SizeFieldWidthMin),
                            nameof(SizeFieldWidthMax)));
                }

                nUDWidth.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальное число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMax"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMax"/>' должно лежать в интервале от '<see cref="StatesCountMin"/>' до <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMax"/>.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesNumberMax)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountMax) + SRDescriptionAttribute.Suffix)]
        public short StatesCountMax
        {
            get { return Convert.ToInt16(nUDStatesCount.Maximum); }
            set
            {
                if (value < StatesCountMin ||
                    value < Core.CellularAutomaton.StatesNumberMax)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(StatesCountMax),
                            nameof(StatesCountMin),
                            Core.CellularAutomaton.StatesNumberMax));
                }

                nUDStatesCount.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальное число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMin"/>' должно лежать в диапазоне от <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/> до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesNumberMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountMin) + SRDescriptionAttribute.Suffix)]
        public short StatesCountMin
        {
            get { return Convert.ToInt16(nUDStatesCount.Minimum); }
            set
            {
                if (value < Core.CellularAutomaton.StatesNumberMin ||
                    StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(StatesCountMin),
                            nameof(Core.CellularAutomaton.StatesNumberMin),
                            nameof(StatesCountMax)));
                }

                nUDStatesCount.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesNumberMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountValue"/>' должно лежать в диапазоне от '<see cref="StatesCountMin"/>' до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesNumberMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountValue) + SRDescriptionAttribute.Suffix)]
        public short StatesCountValue
        {
            get { return Convert.ToInt16(nUDStatesCount.Value); }
            set
            {
                if (value < StatesCountMin ||
                    StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(StatesCountValue),
                            nameof(StatesCountMin),
                            nameof(StatesCountMax)));
                }

                nUDStatesCount.Value = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="CellularAutomatonRecorder"/>.
        /// </summary>
        public CellularAutomatonRecorder()
        {
            InitializeComponent();
            InitializeProperties();
        }
        #endregion

        #region Members
        /// <summary>
        /// Сохраняет запись в файл с именем <see cref="FileName"/>.
        /// </summary>
        public void Save()
        {
            _recorder.Save(FileName);
        }

        /// <summary>
        /// Сохраняет запись в файл с указанным именем.
        /// </summary>
        /// <param name="fileName">Имя файла в который будет сохранена запись.</param>
        public void Save(string fileName)
        {
            _recorder.Save(fileName);
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Начинает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bRecord_Click(object sender, EventArgs e)
        {
            InitializeRecorder();
            _recorder.Record();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Сохраняет запись функционирования клеточного автомата в файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bSave_Click(object sender, EventArgs e)
        {
            if (ShowSaveFileDialog())
                Save();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            _recorder.Stop();
        }

        /// <summary>
        /// Обработчик события <see cref="UserControl.Load"/>. Приводит компонент в начальное состояние.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void CellularAutomatonRecorder_Load(object sender, EventArgs e)
        {
            Stoped();
            CheckIsStart();
        }

        /// <summary>
        /// Проверяет заданы ли все необходимые для функционирования свойства.
        /// </summary>
        private void CheckIsStart()
        {
            Enabled = 0 < Rules.Count;
        }

        /// <summary>
        /// Инициализирует значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            ObservableCollection<IRule> innerRules = new ObservableCollection<IRule>();
            innerRules.CollectionChanged += InnerRules_CollectionChanged;
            Rules = innerRules;

            SizeFieldWidthMin = SizeFieldWidthMinDefValue;
            SizeFieldHeightMin = SizeFieldHeightMinDefValue;
            SizeFieldWidthMax = SizeFieldWidthMaxDefValue;
            SizeFieldHeightMax = SizeFieldHeightMaxDefValue;
            SizeFieldWidthValue = SizeFieldWidthValueDefValue;
            SizeFieldHeightValue = SizeFieldHeightValueDefValue;

            DencityMin = DencityMinDefValue;
            DencityMax = DencityMaxDefValue;
            DencityValue = DencityValueDefValue;

            StatesCountMin = Core.CellularAutomaton.StatesNumberMin;
            StatesCountMax = Core.CellularAutomaton.StatesNumberMax;
            StatesCountValue = Core.CellularAutomaton.StatesNumberMin;

            FileName = Resources.CellularAutomatonRecorder__SaveFileDialogRecordDefFileName;
            FileExtension = Resources.CellularAutomatonRecorder__SaveFileDialogRecordExt;
            FileFilter = Resources.CellularAutomatonRecorder__SaveFileDialogRecordFilter;
        }

        /// <summary>
        /// Инициализирует <see cref="Recorder"/> заданными параметрами.
        /// </summary>
        private void InitializeRecorder()
        {
            // TODO: Можно сделать лучше освобождение памяти после _recorder?
            if (_recorder != null)
            {
                _recorder = null;
                GC.Collect();
            }

            Core.CellularAutomaton ca = new Core.CellularAutomaton(
                Rules[cBCellularAutomatonRules.SelectedIndex],
                SizeFieldWidthValue,
                SizeFieldHeightValue,
                StatesCountValue,
                DencityValue);

            _recorder = new Recorder(ca);

            _recorder.StartRecord += Recorder_StartRecord;
            _recorder.StopRecord += Recorder_StopRecord;
        }

        /// <summary>
        /// Обработчик события <see cref="ObservableCollection{T}.CollectionChanged"/>. Актуализирует состояние <see cref="cBCellularAutomatonRules"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void InnerRules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            cBCellularAutomatonRules.Items.Clear();

            if (0 < Rules.Count)
            {
                cBCellularAutomatonRules.Items.AddRange(Rules.Select(item => item.Name).ToArray<object>());
                cBCellularAutomatonRules.SelectedIndex = 0;
            }
            else
                CheckIsStart();
        }

        /// <summary>
        /// Обработчик события <see cref="IRecorder.StartRecord"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void Recorder_StartRecord(object sender, EventArgs e)
        {
            Invoke(_started = _started ?? Started);
        }

        /// <summary>
        /// Обработчик события <see cref="IRecorder.StopRecord"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void Recorder_StopRecord(object sender, EventArgs e)
        {
            Stoped();
        }

        /// <summary>
        /// Отображает диалог выбора расположения для сохранения файла с записью работы клеточного автомата.
        /// </summary>
        /// <returns><b>True</b>, если пользователь нажал кнопку "Сохранить", иначе <b>false</b>.</returns>
        private bool ShowSaveFileDialog()
        {
            if (_saveFileDialog == null)
            {
                _saveFileDialog = new SaveFileDialog
                {
                    CheckFileExists = false,
                    CheckPathExists = true,
                    ValidateNames = true,
                    AddExtension = true,
                    DereferenceLinks = true,
                    RestoreDirectory = true,
                    OverwritePrompt = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = Resources.CellularAutomatonRecorder__SaveFileDialogRecordTitle,
                    FileName = FileName,
                    DefaultExt = FileExtension,
                    Filter = FileFilter
                };
            }

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = _saveFileDialog.FileName;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Обрабатывает действия возникающие при остановке записи.
        /// </summary>
        private void Started()
        {
            bRecord.Enabled = false;
            bStop.Enabled = true;
            bSave.Enabled = false;
            gBSettings.Enabled = false;
        }

        /// <summary>
        /// Обрабатывает действия возникающие при начале записи.
        /// </summary>
        private void Stoped()
        {
            bRecord.Enabled = true;
            bStop.Enabled = false;
            bSave.Enabled = _recorder != null;
            gBSettings.Enabled = true;
        }
        #endregion
    }
}
