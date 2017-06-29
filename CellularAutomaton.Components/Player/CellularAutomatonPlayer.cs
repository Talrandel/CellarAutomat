#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 29.06.2017 12:31
// Description   : 
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core;

namespace CellularAutomaton.Components.Player
{
    // BUG: Найти и устранить причину "чёрного квадрата" вместо воспроизводимой записи. Шаги воспроизведения: Загрузить запись и начать воспроизведение. PS.: В первую очередь необходимо проверить корректность записи, для этого генерируемые кадры заменить статическим изображением. Затем проверить CellularAutomaton.Components.Player.Player также на выводе статического изображения.
    /// <summary>
    /// Представляет элемент управления - проигрыватель.
    /// </summary>
    public partial class CellularAutomatonPlayer : UserControl
    {
        #region Events
        /// <summary>
        /// Происходит при приостановке воспроизведения.
        /// </summary>
        public event EventHandler PausePlay;

        /// <summary>
        /// Происходит при начале воспроизведения.
        /// </summary>
        public event EventHandler StartPlay;

        /// <summary>
        /// Происходит при окончании воспроизведения.
        /// </summary>
        public event EventHandler StopPlay;
        #endregion

        #region Static Fields and Constants
        /// <summary>
        /// Значение по умолчанию свойства <see cref="FinderLargeChange"/>.
        /// </summary>
        private const short FinderLargeChangeDefValue = 5;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="FinderSmallChange"/>.
        /// </summary>
        private const short FinderSmallChangeDefValue = 1;

        /// <summary>
        /// Значение по умолчанию свойства <see cref="FinderTickFrequency"/>.
        /// </summary>
        private const short FinderTickFrequencyDefValue = 1;
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
        public string FileName
        {
            get { return playerController.FileName; }
            set { playerController.FileName = value; }
        }

        /// <summary>
        /// Возвращает или задаёт число на которое перемещается ползунок по шкале поиска при щелчке мыши по элементу управления или нажатию клавиш PAGE UP, PAGE DOWN.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 5.</b>
        /// </remarks>
        [DefaultValue(FinderLargeChangeDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Behavior")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FinderLargeChange) + CADescriptionAttribute.Suffix)]
        public short FinderLargeChange
        {
            get { return playerController.FinderLargeChange; }
            set { playerController.FinderLargeChange = value; }
        }

        /// <summary>
        /// Возвращает или задаёт число на которое перемещается ползунок по шкале поиска при прокрутке колёсика мыши или нажатия клавиш LEFT, RIGHT, UP, DOWN.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 1.</b>
        /// </remarks>
        [DefaultValue(FinderSmallChangeDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Behavior")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FinderSmallChange) + CADescriptionAttribute.Suffix)]
        public short FinderSmallChange
        {
            get { return playerController.FinderSmallChange; }
            set { playerController.FinderSmallChange = value; }
        }

        /// <summary>
        /// Возвращает или задаёт интервал между отметками на шкале поиска.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 1.</b>
        /// </remarks>
        [DefaultValue(FinderTickFrequencyDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FinderTickFrequency) + CADescriptionAttribute.Suffix)]
        public short FinderTickFrequency
        {
            get { return playerController.FinderTickFrequency; }
            set { playerController.FinderTickFrequency = value; }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную скорость воспроизведения записи (кадры в минуту) функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Player.FramesPerMinuteMaxDefValue"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteMax"/>' должно лежать в интервале от '<see cref="FramesPerMinuteMin"/>' до '<see cref="Player.FramesPerMinuteMaxDefValue"/>'.</exception>
        [DefaultValue(Player.FramesPerMinuteMaxDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteMax) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteMax
        {
            get { return Convert.ToInt16(nUDFramesPerMinute.Maximum); }
            set
            {
                if (value < FramesPerMinuteMin ||
                    Player.FramesPerMinuteMaxDefValue < value)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinuteMax),
                            nameof(FramesPerMinuteMin),
                            Player.FramesPerMinuteMaxDefValue));
                }

                nUDFramesPerMinute.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную скорость воспроизведения записи (кадры в минуту) функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - 1.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteMin"/>' должно лежать в диапазоне от <see cref="Player.FramesPerMinuteMinDefValue"/> до '<see cref="FramesPerMinuteMax"/>'.</exception>
        [DefaultValue(Player.FramesPerMinuteMinDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Data")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteMin) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteMin
        {
            get { return Convert.ToInt16(nUDFramesPerMinute.Minimum); }
            set
            {
                if ((value < Player.FramesPerMinuteMinDefValue) ||
                    (FramesPerMinuteMax < value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__Value___0___must_be_between___1___and__2___,
                            nameof(FramesPerMinuteMin),
                            Player.FramesPerMinuteMinDefValue,
                            nameof(FramesPerMinuteMax)));
                }

                nUDFramesPerMinute.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт скорость воспроизведения записи (кадры в минуту) функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Player.FramesPerMinuteValueDefValue"/>.</b>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Значение '<see cref="FramesPerMinuteValue"/>' должно лежать в диапазоне от '<see cref="FramesPerMinuteMin"/>' до '<see cref="FramesPerMinuteMax"/>'.</exception>
        [DefaultValue(Player.FramesPerMinuteValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [CACategory("Appearance")]
        [CADescription(nameof(CellularAutomatonPlayer) + "__" + nameof(FramesPerMinuteValue) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteValue
        {
            get { return Convert.ToInt16(nUDFramesPerMinute.Value); }
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

                nUDFramesPerMinute.Value = playerController.FramesPerMinuteValue = value;
            }
        }

        /// <summary>
        /// Возвращает воспроизводимую запись.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IReadOnlyRecord Record => playerController?.Record;

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

            playerController.InitializePlayer(
                pBMain.CreateGraphics(),
                new Rectangle(0, 0, pBMain.Size.Width, pBMain.Size.Height));

            playerController.StartPlay += StartPlay;
            playerController.StartPlay += ((sender, e) =>
            {
                nUDFramesPerMinute.Enabled = false;
                bLoadRecord.Enabled = false;
            });

            playerController.PausePlay += PausePlay;
            playerController.StopPlay += StopPlay;
            playerController.StopPlay += ((sender, e) =>
            {
                nUDFramesPerMinute.Enabled = true;
                bLoadRecord.Enabled = true;
            });

            InitializeProperties();
        }
        #endregion

        #region Members
        /// <summary>
        /// Загружает запись из файла с именем заданным в <see cref="FileName"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void LoadRecord()
        {
            playerController.LoadRecord();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Загружает запись функционирования клеточного автомата из файла.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bLoadRecord_Click(object sender, EventArgs e)
        {
            if (ShowOpenFileDialog())
                LoadRecord();
        }

        /// <summary>
        /// Устанавливает значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            SizeMode = PictureSizeMode.AutoSize;

            FramesPerMinuteMin = Player.FramesPerMinuteMinDefValue;
            FramesPerMinuteMax = Player.FramesPerMinuteMaxDefValue;
            FramesPerMinuteValue = Player.FramesPerMinuteValueDefValue;

            FileName = Resources.CellularAutomatonPlayer__OpenFileDialogRecordDefFileName;
            FileExtension = Resources.CellularAutomatonPlayer__OpenFileDialogRecordExt;
            FileFilter = Resources.CellularAutomatonPlayer__OpenFileDialogRecordFilter;
        }

        /// <summary>
        /// Обработчик события <see cref="NumericUpDown.ValueChanged"/>. Обновляет значение свойства <see cref="PlayerController.FramesPerMinuteValue"/> при задании нового значения посредством элемента управления <see cref="nUDFramesPerMinute"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void nUDFramesPerMinute_ValueChanged(object sender, EventArgs e)
        {
            playerController.FramesPerMinuteValue = FramesPerMinuteValue;
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Paint"/>. Перерисовывает текущий кадр.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void pBMain_Paint(object sender, PaintEventArgs e)
        {
            playerController.RepaintCurrentFrame();
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
                    CheckFileExists = true,
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
