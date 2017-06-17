#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : Recorder.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 20:30
// Last Revision : 17.06.2017 22:59
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using CellularAutomaton.Core.Rules;

namespace CellarAutomatForm.Components
{
    /// <summary>
    /// Представляет регистратор функционирования <see cref="CellularAutomaton.Core.CellularAutomaton"/>.
    /// </summary>
    public partial class Recorder : UserControl
    {
        #region Properties
        /// <summary>
        /// Возвращает или задаёт минимальную ширину поля клеточного автомата.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("SizeFieldWidth")]
        [SRDescription(nameof(SizeFieldMinWidth) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldMinWidth
        {
            get { return Convert.ToInt16(nUDWidth.Minimum); }
            set
            {
                if (value < 0)
                    return;

                if (SizeFieldMaxWidth < value)
                    return;

                nUDWidth.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную ширину поля клеточного автомата.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("SizeFieldWidth")]
        [SRDescription(nameof(SizeFieldMaxWidth) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldMaxWidth
        {
            get { return Convert.ToInt16(nUDWidth.Maximum); }
            set
            {
                if (value < 0)
                    return;

                if (value < SizeFieldMinWidth)
                    return;

                nUDWidth.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт минимальную высоту поля клеточного автомата.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("SizeFieldHeight")]
        [SRDescription(nameof(SizeFieldMinHeight) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldMinHeight
        {
            get { return Convert.ToInt16(nUDHeight.Minimum); }
            set
            {
                if (value < 0)
                    return;

                if (SizeFieldMaxHeight < value)
                    return;

                nUDHeight.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт максимальную высоту поля клеточного автомата.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [SRCategory("SizeFieldHeight")]
        [SRDescription(nameof(SizeFieldMaxHeight) + SRDescriptionAttribute.Suffix)]
        public short SizeFieldMaxHeight
        {
            get { return Convert.ToInt16(nUDHeight.Maximum); }
            set
            {
                if (value < 0)
                    return;

                if (value < SizeFieldMinHeight)
                    return;

                nUDHeight.Maximum = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию правил построения клеточных автоматов.
        /// </summary>
        [Browsable(false)]
        public IList<IRule> Rules { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземплр класса <see cref="Recorder"/>.
        /// </summary>
        public Recorder()
        {
            ObservableCollection<IRule> innerRules = new ObservableCollection<IRule>();
            innerRules.CollectionChanged += InnerRules_CollectionChanged;
            Rules = innerRules;

            InitializeComponent();
        }
        #endregion

        #region Members
        /// <summary>
        /// Обработчик события <see cref="ObservableCollection{T}.CollectionChanged"/>. Актуализирует состояние <see cref="cBCellularAutomatonRules"/>.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void InnerRules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            cBCellularAutomatonRules.Items.Clear();
            cBCellularAutomatonRules.Items.AddRange((object[])Rules.Select(item => item.Name));
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Начинает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bRecord_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Останавливает запись функционирования клеточного автомата.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bStop_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обработчик события <see cref="Control.Click"/>. Сохраняет запись функционирования клеточного автомата в файл.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Сведения о событии.</param>
        private void bSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
