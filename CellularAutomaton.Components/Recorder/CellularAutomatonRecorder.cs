﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonRecorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 01.07.2017 22:59
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
        /// Возвращает число кадров в записи.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FramesCount => _recorder.RecordCount;

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
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesCountMax"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMax"/>' должно лежать в интервале от '<see cref="StatesCountMin"/>' до <see cref="CellularAutomaton.Core.CellularAutomaton.StatesCountMax"/>.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesCountMax)]
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
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesCountMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountMin"/>' должно лежать в диапазоне от <see cref="CellularAutomaton.Core.CellularAutomaton.StatesCountMin"/> до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesCountMin)]
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
        ///     <b>Значение по умолчанию - <see cref="CellularAutomaton.Core.CellularAutomaton.StatesCountMin"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="StatesCountValue"/>' должно лежать в диапазоне от '<see cref="StatesCountMin"/>' до '<see cref="StatesCountMax"/>'.</exception>
        [DefaultValue((short)Core.CellularAutomaton.StatesCountMin)]
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
            InitializeProperties();
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
        }

        /// <summary>
        /// Начинает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="indexRule">Индекс правила функционирования клеточного автомата заданного в <see cref="Rules"/>.</param>
        /// <exception cref="InvalidOperationException">Не заданы правила функционирования клеточного автомата в <see cref="Rules"/>.</exception>
        public void Record(int indexRule)
        {
            InitializeRecorder(indexRule);
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
            Stoped();
            PrepareStart();
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
        [SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области")]
        private bool ShowSaveFileDialog()
        {
            try
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
            catch
            {
                _saveFileDialog?.Dispose();
                throw;
            }
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

            OnStartRecord();
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

            OnStopRecord();
        }
        #endregion
    }
}
