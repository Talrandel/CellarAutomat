#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PlayerController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 23:22
// Last Revision : 22.06.2017 19:49
// Description   : 
#endregion

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;

namespace CellularAutomaton.Components.Player
{
    // TODO: Добавить задание свойств tBFinder: Min, Max, TickFrequency.
    /// <summary>
    /// Представляет элемент управления проигрывателем.
    /// </summary>
    /// <remarks>
    /// BUG: <b>При странном поведении разметки компонента в первую очередь попробовать отключить свойство <see cref="ButtonBase.AutoSize"/>.</b>
    /// </remarks>
    public partial class PlayerController : UserControl
    {
        #region Fields
        /// <summary>
        /// Представляет метод обрабатывающий событие <see cref="IPlayer.PausePlay"/>.
        /// </summary>
        private Action _paused;

        /// <summary>
        /// Представляет метод обновляющий значение свойства <see cref="TrackBar.Value"/> элемента управления <see cref="tBFinder"/>.
        /// </summary>
        private Action<short> _setValueFinder;

        /// <summary>
        /// Представляет метод обрабатывающий событие <see cref="IPlayer.StartPlay"/>.
        /// </summary>
        private Action _started;

        /// <summary>
        /// Представляет метод обрабатывающий событие <see cref="IPlayer.StopPlay"/>.
        /// </summary>
        private Action _stoped;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает или задаёт число на которое перемещается ползунок по шкале поиска при щелчке мыши по элементу управления или нажатию клавиш PAGE UP, PAGE DOWN.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("CatBehavior")]
        [SRDescription(nameof(FinderLargeChange) + SRDescriptionAttribute.Suffix)]
        public short FinderLargeChange
        {
            get { return (short)tBFinder.LargeChange; }
            set { tBFinder.LargeChange = value; }
        }

        /// <summary>
        /// Возвращает или задаёт число на которое перемещается ползунок по шкале поиска при прокрутке колёсика мыши или нажатия клавиш LEFT, RIGHT, UP, DOWN.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("CatBehavior")]
        [SRDescription(nameof(FinderSmallChange) + SRDescriptionAttribute.Suffix)]
        public short FinderSmallChange
        {
            get { return (short)tBFinder.SmallChange; }
            set { tBFinder.SmallChange = value; }
        }

        /// <summary>
        /// Возвращает или задаёт интервал между отметками на шкале поиска.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("CatAppearance")]
        [SRDescription(nameof(FinderTickFrequency) + SRDescriptionAttribute.Suffix)]
        public short FinderTickFrequency
        {
            get { return (short)tBFinder.TickFrequency; }
            set { tBFinder.TickFrequency = value; }
        }

        /// <summary>
        /// Возвращает объект реализующий интерфейс <see cref="IPlayer"/> которым осуществляется управление.
        /// </summary>
        public IPlayer Player { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerController"/>.
        /// </summary>
        /// <remarks>
        /// <b>Используется для поддержки конструктора.</b> В своих разработках используйте перегрузку <see cref="PlayerController(IPlayer)"/>.
        /// </remarks>
        public PlayerController()
        {
            InitializeComponent();
            InitializeToolTip();
            InitializeAction();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerController"/> заданным объектом реализующим интерфейс <see cref="IPlayer"/>.
        /// </summary>
        /// <param name="player">Экземпляр объекта реализующего интерфейс <see cref="IPlayer"/>.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="player"/> имеет значение <b>null</b>.</exception>
        public PlayerController(IPlayer player) : this()
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            player.ChangeFrame += PlayerChangeFrame;
            player.StartPlay += PlayerStartPlay;
            player.PausePlay += PlayerPausePlay;
            player.StopPlay += PlayerStopPlay;
            Player = player;

            tBFinder.Maximum = player.Record.Count;
        }
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Приостанавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bPause_Click(object sender, EventArgs e)
        {
            Player.Pause();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Начинает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bPlay_Click(object sender, EventArgs e)
        {
            Player.Play();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            Player.Stop();
            tBFinder.Value = Player.CurrenFrame;
        }

        /// <summary>
        /// Инициализирует делегаты обновления состояния пользовательского интерфейса.
        /// </summary>
        private void InitializeAction()
        {
            _setValueFinder = (e => tBFinder.Value = e);

            _started = (() =>
            {
                bPlay.Enabled = false;
                bPause.Enabled = true;
                bStop.Enabled = true;
            });

            _paused = (() =>
            {
                bPlay.Enabled = true;
                bPause.Enabled = false;
                bStop.Enabled = true;
            });

            _stoped = (() =>
            {
                bPlay.Enabled = true;
                bPause.Enabled = false;
                bStop.Enabled = false;
            });
        }

        /// <summary>
        /// Инициализирует тексты подсказок элементов управления.
        /// </summary>
        private void InitializeToolTip()
        {
            SetToolTiptBFinder();

            toolTip.SetToolTip(bPlay, Resources.PlayerController__SetToolTip__Play);
            toolTip.SetToolTip(bPause, Resources.PlayerController__SetToolTip__Pause);
            toolTip.SetToolTip(bStop, Resources.PlayerController__SetToolTip__Stop);
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
        /// Обработчик события <see cref="IPlayer.PausePlay"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerPausePlay(object sender, EventArgs e)
        {
            Invoke(_paused);
        }

        /// <summary>
        /// Обработчик события <see cref="IPlayer.StartPlay"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void PlayerStartPlay(object sender, EventArgs e)
        {
            Invoke(_started);
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
                    tBFinder.Value,
                    Player?.Record.Count ?? 0));
        }

        /// <summary>
        /// Обработчик события <see cref="Control.MouseDown"/>. Осуществляет переход к указанному спомощью <see cref="tBFinder_Scroll"/> кадру записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_MouseUp(object sender, MouseEventArgs e)
        {
            if (tBFinder.Value != Player.CurrenFrame)
            {
                Player.Rewind((short)tBFinder.Value);
                Player.Play();
            }
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
