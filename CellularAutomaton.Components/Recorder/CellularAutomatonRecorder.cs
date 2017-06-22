#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 22.06.2017 14:17
// Last Revision : 22.06.2017 20:26
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
        #region Events
        public event EventHandler SaveRecord;

        public event EventHandler StartRecord;

        public event EventHandler StopRecord;
        #endregion

        #region Static Fields and Constants
        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityMax"/>.
        /// </summary>
        private const short DencityMaxDefValue = 100;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityMin"/>.
        /// </summary>
        private const short DencityMinDefValue = 0;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="DencityValue"/>.
        /// </summary>
        private const short DencityValueDefValue = 50;

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
        private IRecorder _recorder;
        #endregion

        #region Properties
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
        public IList<IRule> Rules { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="CellularAutomatonRecorder"/>.
        /// </summary>
        /// <remarks>
        /// <b>Используется для поддержки конструктора.</b> В своих разработках используйте перегрузку <see cref="CellularAutomatonRecorder(IRecorder)"/>.
        /// </remarks>
        public CellularAutomatonRecorder()
        {
            InitializeComponent();
            InitializeProperties();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomatonRecorder"/> с заданной реализацией <see cref="IRecorder"/>.
        /// </summary>
        /// <param name="recorder">Реализация регистратора.</param>
        public CellularAutomatonRecorder(IRecorder recorder) : this()
        {
            _recorder = recorder;
        }
        #endregion

        #region Members
        /// <summary>
        /// Вызывает событие <see cref="StartRecord"/>.
        /// </summary>
        protected virtual void OnStartRecord()
        {
            StartRecord?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StopRecord"/>.
        /// </summary>
        protected virtual void OnStopRecord()
        {
            StopRecord?.Invoke(this, EventArgs.Empty);
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
        /// Обработчик события <see cref="Control.Click"/>. Сохраняет запись функционирования клеточного автомата в файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bSave_Click(object sender, EventArgs e)
        {
            SaveRecordDlg();
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
        /// Устанавливает значения свойств по умолчанию.
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
        /// Обработчик события <see cref="ObservableCollection{T}.CollectionChanged"/>. Актуализирует состояние <see cref="cBCellularAutomatonRules"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void InnerRules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            cBCellularAutomatonRules.Items.Clear();
            cBCellularAutomatonRules.Items.AddRange(Rules.Select(item => item.Name).ToArray<object>());
        }

        /// <summary>
        /// Отображает диалог выбора расположения для охранения файла с записью работы клеточного автомата.
        /// </summary>
        /// <returns>Имя файла, в который необходимо сохранить запись.</returns>
        private string SaveRecordDlg()
        {
            using (SaveFileDialog svfDlg = new SaveFileDialog())
            {
                svfDlg.CheckFileExists = false;
                svfDlg.CheckPathExists = true;
                svfDlg.ValidateNames = true;

                svfDlg.AddExtension = true;
                svfDlg.DereferenceLinks = true;
                svfDlg.RestoreDirectory = true;
                svfDlg.OverwritePrompt = true;

                svfDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                svfDlg.Title = Resources.CellularAutomatonRecorder__SaveFileDialogRecordTitle;
                svfDlg.FileName = FileName;
                svfDlg.DefaultExt = FileExtension;
                svfDlg.Filter = FileFilter;

                return FileName = svfDlg.ShowDialog() == DialogResult.OK ? svfDlg.FileName : FileName;
            }
        }
        #endregion

        #region SizeField
        #region SizeFieldWidth
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldWidthMin), nameof(SizeFieldWidthMax)));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___не_должно_быть_меньше___1___, nameof(SizeFieldWidthMax), nameof(SizeFieldWidthMin)));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldWidthValue), nameof(SizeFieldWidthMin), nameof(SizeFieldWidthMax)));
                }

                nUDWidth.Value = value;
            }
        }
        #endregion

        #region SizeFieldHeight
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldHeightMin), nameof(SizeFieldHeightMax)));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___не_должно_быть_меньше___1___, nameof(SizeFieldHeightMax), nameof(SizeFieldHeightMin)));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(SizeFieldHeightValue), nameof(SizeFieldHeightMin), nameof(SizeFieldHeightMax)));
                }

                nUDHeight.Value = value;
            }
        }
        #endregion
        #endregion

        #region Dencity
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
        public short DencityMin
        {
            get { return Convert.ToInt16(nUDDencity.Minimum); }
            set
            {
                if (value < 0 ||
                    DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityMin), nameof(DencityMax)));
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
        [DefaultValue(DencityMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityMax) + SRDescriptionAttribute.Suffix)]
        public short DencityMax
        {
            get { return Convert.ToInt16(nUDDencity.Maximum); }
            set
            {
                if (value < DencityMin ||
                    value < 100)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityMax), nameof(DencityMin), 0));
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
        [DefaultValue(DencityValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Data")]
        [SRDescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityValue) + SRDescriptionAttribute.Suffix)]
        public short DencityValue
        {
            get { return Convert.ToInt16(nUDDencity.Value); }
            set
            {
                if (value < DencityMin ||
                    DencityMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(DencityValue), nameof(DencityMin), nameof(DencityMax)));
                }

                nUDDencity.Value = value;
            }
        }
        #endregion

        #region StatesCount
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountMin), nameof(Core.CellularAutomaton.StatesNumberMin), nameof(StatesCountMax)));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountMax), nameof(StatesCountMin), Core.CellularAutomaton.StatesNumberMax));
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
                        string.Format(CultureInfo.CurrentCulture, Resources.Ex__Значение___0___должно_лежать_в_диапазоне_от___1___до___2___, nameof(StatesCountValue), nameof(StatesCountMin), nameof(StatesCountMax)));
                }

                nUDStatesCount.Value = value;
            }
        }
        #endregion
    }
}
