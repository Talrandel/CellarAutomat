#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : PlayerController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 20.06.2017 23:22
// Last Revision : 22.06.2017 14:07
// Description   : 
#endregion

using System;
using System.Globalization;
using System.Windows.Forms;

using CellularAutomaton.Components.Properties;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Представляет элемент управления проигрывателем.
    /// </summary>
    /// <remarks>
    ///     <b>При странном поведении разметки компонента в первую очередь попробовать отключить свойство <see cref="ButtonBase.AutoSize"/>.</b>
    /// </remarks>
    public partial class PlayerController : UserControl
    {
        #region Fields
        /// <summary>
        /// Номер кадра к которому необходимо перейти при завершении перемещения поискового ползунка.
        /// </summary>
        private short _gotoFrame;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает объект реализующий интерфейс <see cref="IPlayer"/> которым осуществляется управление.
        /// </summary>
        public IPlayer Player { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerController"/>.
        /// </summary>
        protected PlayerController()
        {
            InitializeComponent();
            SetToolTiptBFinder();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PlayerController"/> заданным объектом реализующим интерфейс <see cref="IPlayer"/>.
        /// </summary>
        /// <param name="player">Экземпляр объекта реализующего класс <see cref="IPlayer"/>.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="player"/> имеет значение <b>null</b>.</exception>
        public PlayerController(IPlayer player) : this()
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Player = player;
        }
        #endregion

        #region Members
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
        /// Обработчик события <see cref="Control.Click"/>. Приостанавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bPause_Click(object sender, EventArgs e)
        {
            Player.Pause();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает воспроизведение записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            Player.Stop();
        }

        /// <summary>
        /// Обработчик события <see cref="TrackBar.Scroll"/>. Показывает информацию об указанном кадре записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_Scroll(object sender, EventArgs e)
        {
            _gotoFrame = (short)tBFinder.Value;
            SetToolTiptBFinder();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.MouseDown"/>. Осуществляет переход к указанному спомощью <see cref="tBFinder_Scroll"/> кадру записи.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void tBFinder_MouseUp(object sender, MouseEventArgs e)
        {
            if (_gotoFrame != Player.CurrenFrame)
            {
                Player.Rewind(_gotoFrame);
                Player.Play();
            }
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
                    Resources.PlayerController__SetToolTipFinder,
                    _gotoFrame,
                    Player.Record.Count));
        }
        #endregion
    }
}
