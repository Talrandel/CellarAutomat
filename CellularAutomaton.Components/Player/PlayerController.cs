#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PlayerController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 27.06.2017 13:41
// Last Revision : 29.06.2017 18:30
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
    /// <summary>
    /// Представляет элемент управления проигрывателем.
    /// </summary>
    /// <remarks>
    /// BUG: <b>При странном поведении разметки компонента в первую очередь попробовать отключить свойство <see cref="ButtonBase.AutoSize"/>.</b>
    /// </remarks>
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

        /// <summary>
        /// Представляет метод обновляющий значение свойства <see cref="TrackBar.Value"/> элемента управления <see cref="tBFinder"/>.
        /// </summary>
        private Action<int> _setValueFinder;

        /// <summary>
        /// Представляет метод, обработчик события <see cref="IPlayer.StopPlay"/>.
        /// </summary>
        private Action _stoped;
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
            SetToolTiptBFinder();
            InitializeAction();
            InitializeProperties();
        }
        #endregion

        #region Members
        /// <summary>
        /// Инициализирует <see cref="Player"/> заданным полотном и размером области для вывода изображения.
        /// </summary>
        /// <param name="e">Поверхность рисования GDI+ на которую осуществляется вывод изображения.</param>
        /// <param name="rec">Размеры области на которую осуществяется вывод изображения.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="e"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentException">Значение высоты или ширины размера меньше или равно нулю.</exception>
        public void InitializePlayer(Graphics e, Rectangle rec)
        {
            _player = new Player(e, rec);
            _player.Load(new Record()); // Загрузка пустой записи.

            _player.ChangeFrame += PlayerChangeFrame;
            _player.StartPlay += PlayerStartPlay;
            _player.PausePlay += PlayerPausePlay;
            _player.StopPlay += PlayerStopPlay;
        }

        /// <summary>
        /// Загрузка записи функционирования клеточного автомата из файла заданного свойством <see cref="FileName"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void LoadRecord()
        {
            _player.Load(FileName);
            CheckIsStart();
            tBFinder.Maximum = _player.Record.Count;
            SetToolTiptBFinder();
        }

        /// <summary>
        /// Перерисовывает текущий кадр.
        /// </summary>
        public void RepaintCurrentFrame()
        {
            _player?.Paint();
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
            _player.FramesPerMinute = FramesPerMinuteValue;
            _player.Play();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            _player.Stop();
        }

        /// <summary>
        /// Проверяет заданы ли все необходимые для функционирования свойства.
        /// </summary>
        private void CheckIsStart()
        {
            Enabled = (_player?.Record != null) && (0 < _player.Record.Count);
            Invoke(_stoped);
        }

        /// <summary>
        /// Инициализирует делегаты обновления состояния пользовательского интерфейса.
        /// </summary>
        private void InitializeAction()
        {
            _setValueFinder = (e =>
            {
                tBFinder.Value = e;
                SetToolTiptBFinder();
            });

            _stoped = (() =>
            {
                bPlay.Enabled = true;
                bPause.Enabled = false;
                bStop.Enabled = false;

                tBFinder.Value = 0;

                OnStopPlay();
            });
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
        /// Приостанавливает воспроизведение записи.
        /// </summary>
        private void Pause()
        {
            _player.Pause();
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.ChangeFrame"/>. Обрабатывает переход к следующему кадру.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerChangeFrame(object sender, ChangeFrameEventArgs e)
        {
            Invoke(_setValueFinder, e.ReproducedFrame);
        }

        /// <summary>
        /// Обработчик события <see cref="UserControl.Load"/>. Приводит компонент в начальное состояние.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerController_Load(object sender, EventArgs e)
        {
            CheckIsStart();
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
            Invoke(_stoped);
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
                    _player?.Record?.Count + 1 ?? 0));
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
        /// Обработчик события <see cref="Control.MouseDown"/>. Осуществляет переход к указанному спомощью <see cref="tBFinder_Scroll"/> кадру записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_MouseUp(object sender, MouseEventArgs e)
        {
            _player.Rewind(tBFinder.Value);
        }

        /// <summary>
        /// Обработчик события <see cref="TrackBar.Scroll"/>. Отображает информацию об указанном кадре записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_Scroll(object sender, EventArgs e)
        {
            SetToolTiptBFinder();
        }
        #endregion
    }
}
