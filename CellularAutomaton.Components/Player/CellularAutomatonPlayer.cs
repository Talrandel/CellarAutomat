#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 15:54
// Last Revision : 22.06.2017 23:14
// Description   : 
#endregion

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// Представляет элемент управления - проигрыватель.
    /// </summary>
    public partial class CellularAutomatonPlayer : UserControl
    {
        #region Properties
        /// <summary>
        /// Возвращает объект реализующий интерфейс <see cref="IPlayer"/>, который осуществляет воспроизведение записи.
        /// </summary>
        public IPlayer Player { get; }

        /// <summary>
        /// Возвращает или задаёт режим размещения изображения.
        /// </summary>
        [DefaultValue(PictureBoxSizeMode.CenterImage)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [SRCategory("Behavior")]
        [SRDescription(nameof(CellularAutomatonPlayer) + "__" + nameof(SizeMode) + SRDescriptionAttribute.Suffix)]
        public PictureBoxSizeMode SizeMode
        {
            // TODO: Хорошо бы заменить на своё перечисление.
            get { return pBMain.SizeMode; }
            set { pBMain.SizeMode = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomatonPlayer"/>.
        /// </summary>
        /// <remarks>
        /// <b>Используется для поддержки конструктора.</b> В своих разработках используйте перегрузку <see cref="PlayerController(IPlayer)"/>.
        /// </remarks>
        public CellularAutomatonPlayer()
        {
            InitializeComponent();
            InitializeProperties();

            Player = new Player(pBMain.CreateGraphics(), new Rectangle(0, 0, pBMain.Size.Width, pBMain.Size.Height));
            playerController.Player = Player;
        }
        #endregion

        #region Members
        /// <summary>
        /// Устанавливает значения свойств по умолчанию.
        /// </summary>
        private void InitializeProperties()
        {
            // TODO: Вынести в ресурсы.

            SizeMode = PictureBoxSizeMode.CenterImage;
        }
        #endregion
    }
}
