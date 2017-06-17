#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : MainForm.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 10.06.2017 22:06
// Last Revision : 16.06.2017 12:49
// Description   : 
#endregion

using System;
using System.Windows.Forms;
using CellarAutomatForm.Presenter;

namespace CellarAutomatForm
{
    public partial class MainForm : Form, IViewMainForm
    {
        #region Fields
        /// <summary>
        /// Контроллер для формы
        /// </summary>
        private readonly MainFormController _controller;
        #endregion

        #region Constructors
        public MainForm()
        {
            InitializeComponent();


            _controller = new MainFormController(this);
        }
        #endregion

        #region Members
        private void MainForm_Load(object sender, EventArgs e)
        {
            _controller.InitializeForm();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //_controller.Build(int.Parse(textBoxHeight.Text), int.Parse(textBoxWidth.Text),
            //                  byte.Parse(textBoxDencity.Text),
            //                  (MainFormController.CellarAutomatRules)cBCellularAutomatonRules.SelectedIndex,
            //                  int.Parse(textBoxStatesCount.Text));
        }

        private void textBox_KeyPress_SkipLetters(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            _controller.LoadCA();
        }

        private void buttonStopBuilding_Click(object sender, EventArgs e)
        {
            _controller.StopBuilding();
        }

        private void buttonStopPlaying_Click(object sender, EventArgs e)
        {
            _controller.StopPlaying();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            //_controller.Play(int.Parse(textBoxShowDelay.Text));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.Close();
        }

        public void PlayRecord()
        {
            // Изменение состояний контролов
        }

        public void StopRecord()
        {
            throw new NotImplementedException();
        }

        public void RewindRecord(short frame)
        {
            throw new NotImplementedException();
        }

        public void LoadRecord(string fileName)
        {
            throw new NotImplementedException();
        }

        public void SaveRecord(string fileName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
