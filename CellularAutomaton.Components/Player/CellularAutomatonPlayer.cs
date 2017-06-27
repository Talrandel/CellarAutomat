﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 27.06.2017 13:42
// Description   : 
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;

namespace CellularAutomaton.Components.Player
{
    // TODO: Настроить ToolTip.
    /// <summary>
    /// Представляет элемент управления - проигрыватель.
    /// </summary>
    public partial class CellularAutomatonPlayer : UserControl
    {
        #region Static Fields and Constants
        /// <summary>
        /// Значение по умолчанию свойства <see cref="FramesPerMinuteMin"/>.
        /// </summary>
        private const short FramesPerMinuteMinDefValue = 1;
        #endregion

        #region Fields
        /// <summary>
        /// Экземпляр класса <see cref="OpenFileDialog"/> - окно загрузки файла с записью.
        /// </summary>
        private OpenFileDialog _openFileDialog;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает или задаёт расширение файла, из которого осуществляется загрузка записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FileExtension) + CADescriptionAttribute.Suffix)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Возвращает или задаёт фильтр имён файлов в диалоговом окне "Открыть...".
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FileFilter) + CADescriptionAttribute.Suffix)]
        public string FileFilter { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя файла, из которого осуществляется загрузка записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FileName) + CADescriptionAttribute.Suffix)]
        public string FileName { get; set; }

        /// <summary>
        /// Возвращает или задаёт максимальное количество кадров в минуту для скорости воспроизведения записи функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Player.FramesPerMinuteMaxDefValue"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteMax"/>' должно лежать в интервале от '<see cref="FramesPerMinuteMin"/>' до 3600.</exception>
        [DefaultValue(Player.FramesPerMinuteMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteMax) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteMax
        {
            get { return Convert.ToByte(nUDFramesPerMinute.Maximum); }
            set
            {
                if (value < FramesPerMinuteMin ||
                    value < 100)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinuteMax),
                            nameof(FramesPerMinuteMin), 0));
                }

                nUDFramesPerMinute.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальное количество кадров в минуту для скорости воспроизведения записи функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 1.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteMin"/>' должно лежать в диапазоне от 1 до '<see cref="FramesPerMinuteMax"/>'.</exception>
        [DefaultValue(FramesPerMinuteMinDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteMin) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteMin
        {
            get { return Convert.ToByte(nUDFramesPerMinute.Minimum); }
            set
            {
                if (FramesPerMinuteMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinuteMin),
                            nameof(FramesPerMinuteMax)));
                }

                nUDFramesPerMinute.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт количество кадров в минуту для скорости воспроизведения записи функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Player.FramesPerMinuteMaxDefValue"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteValue"/>' должно лежать в диапазоне от '<see cref="FramesPerMinuteMin"/>' до '<see cref="FramesPerMinuteMax"/>'.</exception>
        [DefaultValue(Player.FramesPerMinuteMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteValue) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteValue
        {
            get { return Convert.ToByte(nUDFramesPerMinute.Value); }
            set
            {
                if (value < FramesPerMinuteMin ||
                    FramesPerMinuteMax < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinuteValue),
                            nameof(FramesPerMinuteMin),
                            nameof(FramesPerMinuteMax)));
                }

                nUDFramesPerMinute.Value = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт режим размещения изображения.
        /// </summary>
        [DefaultValue(PictureSizeMode.CenterImage)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [CACategory("Behavior")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(SizeMode) + CADescriptionAttribute.Suffix)]
        public PictureSizeMode SizeMode
        {
            get { return (PictureSizeMode)pBMain.SizeMode; }
            set { pBMain.SizeMode = (PictureBoxSizeMode)value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomatonPlayer"/>.
        /// </summary>
        public CellularAutomatonPlayer()
        {
            InitializeComponent();
            InitializeProperties();

            playerController.InitializePlayer(
                pBMain.CreateGraphics(),
                new Rectangle(0, 0, pBMain.Size.Width, pBMain.Size.Height));
        }
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Загружает запись функционирования клеточного автомата из файла.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bLoadRecord_Click(object sender, EventArgs e)
        {
            if (ShowOpenFileDialog())
                playerController.LoadRecord(FileName);
        }

        /// <summary>
        /// Устанавливает значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            SizeMode = PictureSizeMode.CenterImage;

            FramesPerMinuteMin = FramesPerMinuteMinDefValue;
            FramesPerMinuteMax = Player.FramesPerMinuteMaxDefValue;
            FramesPerMinuteValue = Player.FramesPerMinuteValueDefValue;

            FileName = Resources.CellularAutomatonPlayer__OpenFileDialogRecordDefFileName;
            FileExtension = Resources.CellularAutomatonPlayer__OpenFileDialogRecordExt;
            FileFilter = Resources.CellularAutomatonPlayer__OpenFileDialogRecordFilter;
        }

        /// <summary>
        /// Отображает диалог выбора расположения для загрузки файла с записью работы клеточного автомата.
        /// </summary>
        /// <returns><b>True</b>, если пользователь нажал кнопку "Открыть", иначе <b>false</b>.</returns>
        private bool ShowOpenFileDialog()
        {
            if (_openFileDialog == null)
            {
                _openFileDialog = new OpenFileDialog
                {
                    CheckFileExists = false,
                    CheckPathExists = true,
                    ValidateNames = true,
                    AddExtension = true,
                    DereferenceLinks = true,
                    RestoreDirectory = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                    Title = Resources.CellularAutomatonPlayer__OpenFileDialogRecordTitle,
                    FileName = FileName,
                    DefaultExt = FileExtension,
                    Filter = FileFilter
                };
            }

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = _openFileDialog.FileName;
                return true;
            }

            return false;
        }
        #endregion
    }
}
