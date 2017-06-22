#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Components
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomatonPlayer.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 15:54
// Last Revision : 20.06.2017 23:14
// Description   : 
#endregion

using System.ComponentModel;
using System.Windows.Forms;

namespace CellularAutomaton.Components.Player
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CellularAutomatonPlayer : UserControl
    {
        #region Properties
        /// <summary>
        /// Возвращает или задаёт режим размещения изображения.
        /// </summary>
        [DefaultValue(PictureBoxSizeMode.CenterImage)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [SRCategory("CatBehavior")]
        [SRDescription(nameof(SizeMode) + SRDescriptionAttribute.Suffix)]
        public PictureBoxSizeMode SizeMode
        {
            // TODO: Хорошо бы заменить а своё перечисление.
            get { return pBMain.SizeMode; }
            set { pBMain.SizeMode = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="CellularAutomatonPlayer"/>.
        /// </summary>
        public CellularAutomatonPlayer()
        {
            InitializeComponent();
            InitializeProperties();
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
