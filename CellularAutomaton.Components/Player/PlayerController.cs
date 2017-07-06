#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PlayerController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 06.07.2017 0:50
// Last Revision : 06.07.2017 10:20
// Description   : 
#endregion

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;
using CellularAutomaton.Core;

using ControlExt;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Представляет элемент управления проигрывателем.
    /// </summary>
    public partial class PlayerController : UserControl
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
        /// Объект <see cref="Player"/>, которым осуществляется управление.
        /// </summary>
        private Player _player;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает или задаёт имя файла, из которого осуществляется загрузка записи.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Data")]
        [CADescription(nameof(PlayerController) + "__" + nameof(FileName) + CADescriptionAttribute.Suffix)]
        public string FileName { get; set; }

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
        [CADescription(nameof(PlayerController) + "__" + nameof(FinderLargeChange) + CADescriptionAttribute.Suffix)]
        public short FinderLargeChange
        {
            get { return (short)tBFinder.LargeChange; }
            set { tBFinder.LargeChange = value; }
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
        [CADescription(nameof(PlayerController) + "__" + nameof(FinderSmallChange) + CADescriptionAttribute.Suffix)]
        public short FinderSmallChange
        {
            get { return (short)tBFinder.SmallChange; }
            set { tBFinder.SmallChange = value; }
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
        [CADescription(nameof(PlayerController) + "__" + nameof(FinderTickFrequency) + CADescriptionAttribute.Suffix)]
        public short FinderTickFrequency
        {
            get { return (short)tBFinder.TickFrequency; }
            set { tBFinder.TickFrequency = value; }
        }

        /// <summary>
        /// Возвращает или задаёт скорость воспроизведения записи (кадры в минуту) функционирования клеточного автомата.
        /// </summary>
        /// <remarks>
        ///     <b>Значение по умолчанию - <see cref="Player.FramesPerMinuteValueDefValue"/>.</b>
        /// </remarks>
        [DefaultValue(Player.FramesPerMinuteValueDefValue)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [CACategory("Appearance")]
        [CADescription(nameof(PlayerController) + "__" + nameof(FramesPerMinuteValue) + CADescriptionAttribute.Suffix)]
        public short FramesPerMinuteValue { get; set; }

        /// <summary>
        /// Возвращает воспроизводимую запись.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IReadOnlyRecord Record => _player?.Record;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerController"/>.
        /// </summary>
        public PlayerController()
        {
            InitializeComponent();
            groupBox.EqualizationVerticalPadding();

            SetToolTiptBFinder();
            InitializeProperties();
        }
        #endregion

        #region Members
        /// <summary>
        /// Инициализирует <see cref="Player"/> заданным полотном.
        /// </summary>
        /// <param name="e">Поверхность рисования GDI+ на которую осуществляется вывод изображения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="e"/> имеет значение <b>null</b>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e")]
        public void InitializePlayer(Graphics e)
        {
            _player = new Player(e);

            _player.ChangeFrame += PlayerChangeFrame;
            _player.StartPlay += PlayerStartPlay;
            _player.PausePlay += PlayerPausePlay;
            _player.StopPlay += PlayerStopPlay;
        }

        /// <summary>
        /// Загрузка записи функционирования клеточного автомата из файла заданного свойством <see cref="FileName"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Не инициализирован <see cref="Player"/>. Для его инициализации вызовите метод <see cref="InitializePlayer(Graphics)"/>.</exception>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void LoadRecord()
        {
            if (_player == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__NotInitializePlayer,
                        nameof(Player),
                        nameof(InitializePlayer)));
            }

            _player.Load(FileName);
            PrepareStart();
        }

        /// <summary>
        /// Приостанавливает воспроизведение записи.
        /// </summary>
        /// <exception cref="InvalidOperationException">Не инициализирован <see cref="Player"/>. Для его инициализации вызовите метод <see cref="InitializePlayer(Graphics)"/>.</exception>
        public void Pause()
        {
            if (_player == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__NotInitializePlayer,
                        nameof(Player),
                        nameof(InitializePlayer)));
            }

            _player.Pause();
        }

        /// <summary>
        /// Освобождает ресурсы занимаемые воспроизводимой записью.
        /// </summary>
        /// <exception cref="InvalidOperationException">Воспроизведение не остановлено.</exception>
        public void RecordClear()
        {
            _player?.RecordClear();
            PrepareStart();
            bPlay.Enabled = false;
        }

        /// <summary>
        /// Перерисовывает текущий кадр.
        /// </summary>
        public void RepaintCurrentFrame()
        {
            _player?.PaintCurrentFrame();
        }

        /// <summary>
        /// Начинает воспроизведение записи.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     <para>Не инициализирован <see cref="Player"/>. Для его инициализации вызовите метод <see cref="InitializePlayer(Graphics)"/>.</para>
        ///     <para>-- или --</para>
        ///     <para>Текущая запись не содержит данных для воспроизведения.</para>
        /// </exception>
        public void Start()
        {
            if (_player == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__NotInitializePlayer,
                        nameof(Player),
                        nameof(InitializePlayer)));
            }

            if (_player.Record.Count == 0)
                throw new InvalidOperationException(Resources.Ex__CurrentRecordIsEmpty);

            _player.FramesPerMinute = FramesPerMinuteValue;
            _player.Play();
        }

        /// <summary>
        /// Останавливает воспроизведение записи.
        /// </summary>
        /// <exception cref="InvalidOperationException">Не инициализирован <see cref="Player"/>. Для его инициализации вызовите метод <see cref="InitializePlayer(Graphics)"/>.</exception>
        public void Stop()
        {
            if (_player == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__NotInitializePlayer,
                        nameof(Player),
                        nameof(InitializePlayer)));
            }

            _player.Stop();
        }

        /// <summary>
        /// Вызывает событие <see cref="PausePlay"/>.
        /// </summary>
        protected virtual void OnPausePlay()
        {
            PausePlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StartPlay"/>.
        /// </summary>
        protected virtual void OnStartPlay()
        {
            StartPlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Вызывает событие <see cref="StopPlay"/>.
        /// </summary>
        protected virtual void OnStopPlay()
        {
            StopPlay?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Приостанавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Начинает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bPlay_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Устанавливает значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            FramesPerMinuteValue = Player.FramesPerMinuteValueDefValue;

            FinderLargeChange = FinderLargeChangeDefValue;
            FinderSmallChange = FinderSmallChangeDefValue;
            FinderTickFrequency = FinderTickFrequencyDefValue;

            FileName = nameof(FileName);
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.ChangeFrame"/>. Обрабатывает переход к следующему кадру.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerChangeFrame(object sender, EventArgs e)
        {
            tBFinder.Value = _player.CurrenFrame;
            SetToolTiptBFinder();
        }

        /// <summary>
        /// Обработчик события <see cref="UserControl.Load"/>. Приводит компонент в начальное состояние.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerController_Load(object sender, EventArgs e)
        {
            PrepareStart();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Paint"/>. Перерисовывает текущий кадр.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerController_Paint(object sender, PaintEventArgs e)
        {
            RepaintCurrentFrame();
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.PausePlay"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerPausePlay(object sender, EventArgs e)
        {
            bPlay.Enabled = true;
            bPause.Enabled = false;
            bStop.Enabled = true;

            OnPausePlay();
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.StartPlay"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerStartPlay(object sender, EventArgs e)
        {
            bPlay.Enabled = false;
            bPause.Enabled = true;
            bStop.Enabled = true;

            OnStartPlay();
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.StopPlay"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerStopPlay(object sender, EventArgs e)
        {
            StopedWithoutEvent();
            OnStopPlay();
        }

        /// <summary>
        /// Подготавливает компонент к воспроизведению новой записи.
        /// </summary>
        private void PrepareStart()
        {
            bool isPlayer = _player != null;
            bool isEnabled = isPlayer && (0 < _player.Record.Count);

            tBFinder.Maximum = isPlayer ? ((0 < _player.Record.Count) ? (_player.Record.Count - 1) : 0) : 0;
            SetToolTiptBFinder();

            if (Enabled == isEnabled)
                RepaintCurrentFrame();
            else
                Enabled = isEnabled;

            StopedWithoutEvent();
        }

        /// <summary>
        /// Устанавливает текст всплывающей подсказки элемента управления <see cref="tBFinder"/>.
        /// </summary>
        private void SetToolTiptBFinder()
        {
            toolTip.SetToolTip(
                tBFinder,
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.PlayerController__SetToolTip__Finder,
                    tBFinder.Value + 1,
                    _player?.Record.Count ?? 0));
        }

        /// <summary>
        /// Обрабатывает действия возникающие при остановке воспроизведения.
        /// </summary>
        private void StopedWithoutEvent()
        {
            bPlay.Enabled = true;
            bPause.Enabled = false;
            bStop.Enabled = false;

            tBFinder.Value = 0;
        }

        /// <summary>
        /// Обработчик события <see cref="Control.MouseDown"/>. Приостанавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_MouseDown(object sender, MouseEventArgs e)
        {
            Pause();
        }

        /// <summary>
        /// Обработчик события <see cref="TrackBar.Scroll"/>. Осуществляет переход к указанному спомощью <see cref="tBFinder_Scroll"/> кадру записи и отображает о нём информацию.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_Scroll(object sender, EventArgs e)
        {
            _player.Rewind(tBFinder.Value);
            SetToolTiptBFinder();
        }
        #endregion
    }
}
