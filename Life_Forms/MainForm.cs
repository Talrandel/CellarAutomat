using CellarAutomat;
using System;
using System.Windows.Forms;

namespace CellarAutomatForm
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Контроллер для формы
        /// </summary>
        private MainFormController controller;

        public MainForm()
        {
            InitializeComponent();
            controller = new MainFormController(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            controller.InitializeForm();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            controller.Build(int.Parse(textBoxHeight.Text), int.Parse(textBoxWidth.Text), int.Parse(textBoxDencity.Text), (CellarAutomatRules)comboBoxRules.SelectedIndex, int.Parse(textBoxStatesCount.Text));
        }

        private void textBox_KeyPress_SkipLetters(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            controller.LoadCA();
        }

        private void buttonStopBuilding_Click(object sender, EventArgs e)
        {
            controller.StopBuilding();
        }

        private void buttonStopPlaying_Click(object sender, EventArgs e)
        {
            controller.StopPlaying();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            controller.Play(int.Parse(textBoxShowDelay.Text));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.Close();
        }
    }
}