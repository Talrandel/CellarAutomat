#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 06.07.2017 0:36
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
using CellularAutomaton.Core;
using CellularAutomaton.Core.Rules;

using ControlExt;

namespace CellularAutomaton.Components.Recorder
{
    /// <summary>
    /// Представляет регистратор функционирования клеточного автомата описываемого <see cref="Core.CellularAutomaton"/>.
    /// </summary>
    public partial class CellularAutomatonRecorder : UserControl
    {
        #region Events
        /// <summary>
        /// Происходит при начале записи.
        /// </summary>
        public event EventHandler StartRecord;

        /// <summary>
        /// Происходит при остановке записи.
        /// </summary>
        public event EventHandler StopRecord;
        #endregion

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
        /// Значение по умолчанию свойства <see cref="MaxFrames"/>.
        /// </summary>
        private const int MaxFramesDefValue = 0;

        /// <summary>
        /// Верхняя граница диапазона значений свойства <see cref="MaxFrames"/>.
        /// </summary>
        private const decimal MaxFramesMaxDefValue = int.MaxValue;

        /// <summary>
        /// Нижняя граница диапазона значений свойства <see cref="MaxFrames"/>.
        /// </summary>
        private const decimal MaxFramesMinDefValue = 0;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightMax"/>.
        /// </summary>
        private const int SizeFieldHeightMaxDefValue = 500;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightMin"/>.
        /// </summary>
        private const int SizeFieldHeightMinDefValue = 50;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldHeightValue"/>.
        /// </summary>
        private const int SizeFieldHeightValueDefValue = 100;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthMax"/>.
        /// </summary>
        private const int SizeFieldWidthMaxDefValue = 500;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthMin"/>.
        /// </summary>
        private const int SizeFieldWidthMinDefValue = 50;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="SizeFieldWidthValue"/>.
        /// </summary>
        private const int SizeFieldWidthValueDefValue = 100;
        #endregion

        #region Fields
        /// <summary>
        /// Флаг включения режима ограничения количества записываемых кадров.
        /// </summary>
        private bool _isLimitCountFrame;

        /// <summary>
        /// Метод обновления информации о количестве записанных кадров.
        /// </summary>
        private Action<int> _progress;

        /// <summary>
        /// Объект реализующий интерфейс <see cref="IRecorder"/>, которым осуществляется управление.
        /// </summary>
        private IRecorder _recorder;
        #endregion

        #region Properties
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityMax) + CADescriptionAttribute.Suffix)]
        public byte DencityMax
        {
            get { return Convert.ToByte(nUDDencity.Maximum); }
            set
            {
                if (value < DencityMin ||
                    100 < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(DencityMax),
                            nameof(DencityMin),
                            100));
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityMin) + CADescriptionAttribute.Suffix)]
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
                            0,
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
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(DencityValue) + CADescriptionAttribute.Suffix)]
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileExtension) + CADescriptionAttribute.Suffix)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Возвращает или задаёт фильтр имён файлов в диалоговом окне "Сохранить как...".
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileFilter) + CADescriptionAttribute.Suffix)]
        public string FileFilter { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя файла в который осуществляется сохранение записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(FileName) + CADescriptionAttribute.Suffix)]
        public string FileName { get; set; }

        /// <summary>
        /// Возвращает доступную только для чтения записанную запись.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IReadOnlyRecord GetRecord => _recorder.GetRecord;

        /// <summary>
        /// Возвращает или задаёт максимальное число кадров в записи. Значение 0 - без ограничений.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 0.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="MaxFrames"/>' должно лежать в диапазоне от '0' до '<see cref="int.MaxValue"/>'.</exception>
        [DefaultValue(MaxFramesDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(MaxFrames) + CADescriptionAttribute.Suffix)]
        public int MaxFrames
        {
            get { return Convert.ToInt32(nUDMaxFrames.Value); }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(MaxFrames),
                            0,
                            int.MaxValue));
                }

                nUDMaxFrames.Value = value;
            }
        }

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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightMax) + CADescriptionAttribute.Suffix)]
        public int SizeFieldHeightMax
        {
            get { return Convert.ToInt32(nUDHeight.Maximum); }
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightMin) + CADescriptionAttribute.Suffix)]
        public int SizeFieldHeightMin
        {
            get { return Convert.ToInt32(nUDHeight.Minimum); }
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
                            0,
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
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldHeightValue) + CADescriptionAttribute.Suffix)]
        public int SizeFieldHeightValue
        {
            get { return Convert.ToInt32(nUDHeight.Value); }
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthMax) + CADescriptionAttribute.Suffix)]
        public int SizeFieldWidthMax
        {
            get { return Convert.ToInt32(nUDWidth.Maximum); }
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
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthMin) + CADescriptionAttribute.Suffix)]
        public int SizeFieldWidthMin
        {
            get { return Convert.ToInt32(nUDWidth.Minimum); }
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
                            0,
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
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(SizeFieldWidthValue) + CADescriptionAttribute.Suffix)]
        public int SizeFieldWidthValue
        {
            get { return Convert.ToInt32(nUDWidth.Value); }
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
        ///     <b>Значение по умолчанию - <see cref="Core.CellularAutomaton.StatesCountMax"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMax"/>' должно лежать в интервале от '<see cref="StatesCountMin"/>' до <see cref="Core.CellularAutomaton.StatesCountMax"/>.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesCountMax)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountMax) + CADescriptionAttribute.Suffix)]
        public int StatesCountMax
        {
            get { return Convert.ToInt32(nUDStatesCount.Maximum); }
            set
            {
                if (value < StatesCountMin ||
                    Core.CellularAutomaton.StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(StatesCountMax),
                            nameof(StatesCountMin),
                            Core.CellularAutomaton.StatesCountMax));
                }

                nUDStatesCount.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальное число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Core.CellularAutomaton.StatesCountMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMin"/>' должно лежать в диапазоне от <see cref="Core.CellularAutomaton.StatesCountMin"/> до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesCountMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountMin) + CADescriptionAttribute.Suffix)]
        public int StatesCountMin
        {
            get { return Convert.ToInt32(nUDStatesCount.Minimum); }
            set
            {
                if (value < Core.CellularAutomaton.StatesCountMin ||
                    StatesCountMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(StatesCountMin),
                            nameof(Core.CellularAutomaton.StatesCountMin),
                            nameof(StatesCountMax)));
                }

                nUDStatesCount.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт число состояний клетки клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Core.CellularAutomaton.StatesCountMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountValue"/>' должно лежать в диапазоне от '<see cref="StatesCountMin"/>' до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue(Core.CellularAutomaton.StatesCountMin)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonRecorder) + "__" + nameof(StatesCountValue) + CADescriptionAttribute.Suffix)]
        public int StatesCountValue
        {
            get { return Convert.ToInt32(nUDStatesCount.Value); }
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
            gBProgress.EqualizationVerticalPadding();
            gBControlButton.EqualizationVerticalPadding();

            InitializeProperties();
            InitializeActions();
        }
        #endregion

        #region Members
        /// <summary>
        /// Освобождает ресурсы занимаемые <see cref="Recorder"/> .
        /// </summary>
        /// <exception cref="InvalidOperationException">Запись не остановлена.</exception>
        public void Clear()
        {
            // TODO: Можно сделать лучше освобождение памяти после _recorder?
            if (_recorder == null)
                return;

            if (_recorder.State != StateRecorder.Stop)
                throw new InvalidOperationException(Resources.Ex__RecordingIsNotStopped);

            _recorder.Dispose();
            //int gen = GC.GetGeneration(_recorder);
            _recorder = null;
            //GC.Collect(gen);

            _progress(0);
        }

        /// <summary>
        /// Начинает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="indexRule">Индекс правила функционирования клеточного автомата заданного в <see cref="Rules"/>.</param>
        /// <exception cref="InvalidOperationException">Не заданы правила функционирования клеточного автомата в <see cref="Rules"/>.</exception>
        public void Record(int indexRule)
        {
            InitializeRecorder(indexRule);

            _isLimitCountFrame = 0 < MaxFrames;
            _recorder.Record();
        }

        /// <summary>
        /// Сохраняет запись в файл с именем <see cref="FileName"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Нет данных для сохранения.</exception>
        public void SaveRecord()
        {
            if (_recorder == null)
                throw new InvalidOperationException(Resources.Ex__RecordIsEmpty);

            _recorder.Save(FileName);
        }

        /// <summary>
        /// Останавливает запись функционирования клеточного автомата.
        /// </summary>
        public void Stop()
        {
            _recorder?.Stop();
        }

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
            Record(cBCellularAutomatonRules.SelectedIndex);
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Сохраняет запись функционирования клеточного автомата в файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bSave_Click(object sender, EventArgs e)
        {
            if (ShowSaveFileDialog())
                SaveRecord();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Обработчик события <see cref="UserControl.Load"/>. Приводит компонент в начальное состояние.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void CellularAutomatonRecorder_Load(object sender, EventArgs e)
        {
            PrepareStart();
        }

        /// <summary>
        /// Инициализирует делегаты обновляющие состояние формы.
        /// </summary>
        private void InitializeActions()
        {
            _progress = (e => lRecordedFramesValue.Text = e.ToString(CultureInfo.InvariantCulture));
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

            StatesCountMin = Core.CellularAutomaton.StatesCountMin;
            StatesCountMax = Core.CellularAutomaton.StatesCountMax;
            StatesCountValue = Core.CellularAutomaton.StatesCountMin;

            FileName = Resources.CellularAutomatonRecorder__SaveFileDialogRecordDefFileName;
            FileExtension = Resources.CellularAutomatonRecorder__SaveFileDialogRecordExt;
            FileFilter = Resources.CellularAutomatonRecorder__SaveFileDialogRecordFilter;

            nUDMaxFrames.Minimum = MaxFramesMinDefValue;
            nUDMaxFrames.Maximum = MaxFramesMaxDefValue;
            MaxFrames = MaxFramesDefValue;
        }

        /// <summary>
        /// Инициализирует <see cref="Recorder"/> заданными параметрами.
        /// </summary>
        /// <param name="indexRule">Индекс правила функционирования клеточного автомата заданного в <see cref="Rules"/>.</param>
        /// <exception cref="InvalidOperationException">Не заданы правила функционирования клеточного автомата в <see cref="Rules"/>.</exception>
        private void InitializeRecorder(int indexRule)
        {
            if (Rules.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__RulesIsEmpty,
                        nameof(Rules)));
            }

            Clear();

            Core.CellularAutomaton ca = new Core.CellularAutomaton(
                Rules[indexRule],
                SizeFieldWidthValue,
                SizeFieldHeightValue,
                StatesCountValue,
                DencityValue);

            _recorder = new Recorder(ca);

            _recorder.StartRecord += Recorder_StartRecord;
            _recorder.StopRecord += Recorder_StopRecord;
            _recorder.FrameRecorded += Recorder_FrameRecorded;
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
                PrepareStart();
        }

        /// <summary>
        /// Проверяет заданы ли все необходимые для функционирования свойства.
        /// </summary>
        private void PrepareStart()
        {
            Enabled = 0 < Rules.Count;
        }

        /// <summary>
        /// Обработчик события <see cref="IRecorder.FrameRecorded"/>. Проверяет ограничение максимального количества кадров и обновляет информацию о количестве записанных кадров.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void Recorder_FrameRecorded(object sender, EventArgs e)
        {
            int currentCountFrames = _recorder.GetRecord.Count;

            if (_isLimitCountFrame)
            {
                if (MaxFrames <= currentCountFrames)
                    Stop();
            }

            Invoke(_progress, currentCountFrames);
        }

        /// <summary>
        /// Обработчик события <see cref="IRecorder.StartRecord"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void Recorder_StartRecord(object sender, EventArgs e)
        {
            Invoke(new Action(Started));
        }

        /// <summary>
        /// Обработчик события <see cref="IRecorder.StopRecord"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void Recorder_StopRecord(object sender, EventArgs e)
        {
            Invoke(new Action(Stoped));
        }

        /// <summary>
        /// Отображает диалог выбора расположения для сохранения файла с записью работы клеточного автомата.
        /// </summary>
        /// <returns><b>True</b>, если пользователь нажал кнопку "Сохранить", иначе <b>false</b>.</returns>
        private bool ShowSaveFileDialog()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.AddExtension = true;
                saveFileDialog.DereferenceLinks = true;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Title = Resources.CellularAutomatonRecorder__SaveFileDialogRecordTitle;
                saveFileDialog.FileName = FileName;
                saveFileDialog.DefaultExt = FileExtension;
                saveFileDialog.Filter = FileFilter;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Обрабатывает действия возникающие при начале записи.
        /// </summary>
        private void Started()
        {
            bRecord.Enabled = false;
            bStop.Enabled = true;
            bSave.Enabled = false;
            gBSettings.Enabled = false;
            _progress(0);

            OnStartRecord();
        }

        /// <summary>
        /// Обрабатывает действия возникающие при остановке записи.
        /// </summary>
        private void Stoped()
        {
            bRecord.Enabled = true;
            bStop.Enabled = false;
            bSave.Enabled = true;
            gBSettings.Enabled = true;

            OnStopRecord();
        }
        #endregion
    }
}
