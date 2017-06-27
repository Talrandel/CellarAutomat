#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 15:54
// Last Revision : 23.06.2017 12:04
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
        // TODO: добавить NUD - скорость воспроизведения
        // TODO: добавить кнопку "Загрузить запись"
        // TODO: NUD - добавить свойства minValue, maxValue, Value по аналогии с рекордером
        // TODO: загрузить запись - обработчик? По аналогии с рекордером, но openFileDialog
        // TODO: открыть и открыть КАК

        #region Fields
        /// <summary>
        /// Объект реализующий интерфейс <see cref="IPlayer"/>, который осуществляет воспроизведение записи.
        /// </summary>
        private readonly Player _player;
        #endregion

        #region Properties
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
        public CellularAutomatonPlayer()
        {
            InitializeComponent();
            InitializeProperties();

            _player = new Player(
                pBMain.CreateGraphics(),
                new Rectangle(0, 0, pBMain.Size.Width, pBMain.Size.Height));
            _player.Load(new Core.Record()); // Загрузка пустой записи.

            playerController.Player = _player;
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
