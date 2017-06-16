﻿#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 7.0
// File          : MainForm.cs
// Author        : Антипкин С.С.
// Created       : 10.06.2017 22:06
// Last Revision : 14.06.2017 21:39
// Description   : 
#endregion

using System;
using System.Windows.Forms;

namespace CellarAutomatForm
{
    public partial class MainForm : Form
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
            _controller.Build(int.Parse(textBoxHeight.Text), int.Parse(textBoxWidth.Text),
                              byte.Parse(textBoxDencity.Text),
                              (MainFormController.CellarAutomatRules)comboBoxRules.SelectedIndex,
                              int.Parse(textBoxStatesCount.Text));
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
            _controller.Play(int.Parse(textBoxShowDelay.Text));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.Close();
        }
        #endregion
    }
}