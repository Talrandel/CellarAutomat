#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 6.0
// File          : MainFormController.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 10.06.2017 22:06
// Last Revision : 16.06.2017 12:49
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CellularAutomaton.Core.Rules;

namespace CellarAutomatForm
{
    /// <summary>
    /// Контроллер MainForm
    /// </summary>
    internal class MainFormController
    {
        #region Constructors
        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="mf"></param>
        public MainFormController(MainForm mf)
        {
            MainForm = mf;

            _isBuilding = false;
            _isPlaying = false;
            _isLoaded = false;

            _ruleName = new Dictionary<CellarAutomatRules, string>
            {
                {CellarAutomatRules.BaseLife, "Жизнь"},
                {CellarAutomatRules.MooreSimple, "Автомат Мура"},
                {CellarAutomatRules.NeimanSimple, "Автомат Неймана"},
                {CellarAutomatRules.MooreCyclic, "Циклический автомат Мура"},
                {CellarAutomatRules.NeimanCyclic, "Циклический автомат Неймана"},
                {CellarAutomatRules.OneDimentionalCyclic, "Одномерный автомат"},
                {CellarAutomatRules.BelousovZhabotinskyReaction, "Реакция Белоусова-Жаботинского"},
                {CellarAutomatRules.VenusSurface, "Поверхность Венеры"}
            };

            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
        }
        #endregion

        #region Members
        /// <summary>
        /// Инициализация вспомогательных переменных для КА
        /// </summary>
        private void InitializeCAVariables(int height, int width, byte dencity, IRule rule, int statesCount)
        {
            _automat = new CellularAutomaton.Core.CellularAutomaton(rule, width, height, statesCount, dencity);
        }

        /// <summary>
        /// Инициализация контролов формы
        /// </summary>
        public void InitializeForm()
        {
            foreach (KeyValuePair<CellarAutomatRules, string> rule in _ruleName)
                MainForm.comboBoxRules.Items.Add(rule.Value);

            // TODO: вынести в константы?
            MainForm.comboBoxRules.SelectedIndex = 0;
            MainForm.textBoxHeight.Text = "200";
            MainForm.textBoxWidth.Text = "200";
            MainForm.textBoxDencity.Text = "50";
            MainForm.textBoxShowDelay.Text = "200";
            MainForm.labelMessage.Text = "Нет сообщений.";
            MainForm.textBoxHint.Text = CAMessages.Hint;
            MainForm.buttonStopBuild.Enabled = false;
            MainForm.buttonStopPlay.Enabled = false;
            MainForm.buttonPlay.Enabled = false;
            MainForm.textBoxStatesCount.Text = "2";
        }

        /// <summary>
        /// Инициализировать автомат
        /// </summary>
        public void Build(int height, int width, byte dencity, CellarAutomatRules ruleI, int statesCount)
        {
            if (_isBuilding)
                return;

            if (_isPlaying)
                return;

            // Пара объектов для отмены асинхронной операции
            _cts = new CancellationTokenSource();
            _ct = _cts.Token;

            Task.Factory.StartNew(() =>
            {
                _isBuilding = true;
                // Меняем значения контролов формы
                MainForm.Invoke(new Action(() => MainForm.pictureBoxField.Image = null));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Build));

                IRule rule;
                switch (ruleI)
                {
                    case CellarAutomatRules.BaseLife:
                        rule = new Life();
                        break;
                    case CellarAutomatRules.BelousovZhabotinskyReaction:
                        rule = new BelousovZhabotinskyReaction();
                        break;
                    case CellarAutomatRules.MooreCyclic:
                        rule = new MooreCyclic();
                        break;
                    case CellarAutomatRules.MooreSimple:
                        rule = new MooreSimple();
                        break;
                    case CellarAutomatRules.NeimanCyclic:
                        rule = new NeimanCyclic();
                        break;
                    case CellarAutomatRules.NeimanSimple:
                        rule = new NeimanSimple();
                        break;
                    case CellarAutomatRules.OneDimentionalCyclic:
                        rule = new OneDimentionalCyclic();
                        break;
                    case CellarAutomatRules.VenusSurface:
                        rule = new VenusSurface();
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(ruleI), ruleI, null);
                }

                // Инициализация переменных для КА
                InitializeCAVariables(height, width, dencity, rule, statesCount);

                // Обновляем количество состояний клеток для выбранного правила
                //automat.RefreshRuleStatesCount(rule, statesCount);

                // Запускаем основной процесс
                _automat.Processing();
            }, _ct).ContinueWith(t =>
            {
                _isBuilding = false;
                // Сохраняем построенный КА в файл
                SaveCA();
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Built));
            }, _ct);
        }

        /// <summary>
        /// Остановить построение КА
        /// </summary>
        public void StopBuilding()
        {
            if (!_isBuilding)
                return;

            _isBuilding = false;
            _automat.Stop = true;

            MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.StopBuilding));
        }

        /// <summary>
        /// Сохранить построенный КА в файл
        /// </summary>
        public void SaveCA()
        {
#warning SAVE

            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Saved));
        }

        /// <summary>
        /// Загрузка последнего построенного КА из файла
        /// </summary>
        public void LoadCA()
        {
            while (true)
                try
                {
#warning LOAD

                    _isLoaded = true;
                    MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
                    MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Loaded));
                    MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Loaded));
                    break;
                }
                catch
                {
                    MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.LoadError));
                }
        }

        /// <summary>
        /// Воспроизвести загруженный КА
        /// </summary>
        /// <param name="showDelay"></param>
        public void Play(int showDelay)
        {
            if (_isBuilding)
            {
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Building));
                return;
            }

            if (_isPlaying)
                return;

            _cts = new CancellationTokenSource();
            _ct = _cts.Token;

#warning PLAY RECORD
            Task.Factory.StartNew(() =>
            {
                _isPlaying = true;
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Play));
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = true));

                // PLAY RECORD
            }, _ct).ContinueWith(t =>
            {
                _isPlaying = false;
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.PlayingEnded));
            }, _ct);
        }

        /// <summary>
        /// Остановка воспроизведения КА
        /// </summary>
        public void StopPlaying()
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            _isPlaying = false;
            MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = false));
            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.StopPlaying));
        }

        /// <summary>
        /// Отмена выполнения заданий при закрытии формы
        /// </summary>
        public void Close()
        {
            _cts.Cancel();
        }
        #endregion

        #region Переменные и константы
        /// <summary>
        /// Имя файла для хранения последнего построенного КА
        /// </summary>
        public const string FileName = "data.bin";

        /// <summary>
        /// Ссылка на представление - MainForm
        /// </summary>
        public MainForm MainForm { get; private set; }

        /// <summary>
        /// Объект для отправки токену сообщения об отмене выполнения
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Уведомление об остановке задачи.
        /// </summary>
        private CancellationToken _ct;

        /// <summary>
        /// Клеточный автомат - КА
        /// </summary>
        private CellularAutomaton.Core.CellularAutomaton _automat;

        /// <summary>
        /// Строится ли сейчас КА
        /// </summary>
        private bool _isBuilding;

        /// <summary>
        /// Воспроизводится ли сейчас КА
        /// </summary>
        private bool _isPlaying;

        /// <summary>
        /// Загружен ли КА из файла
        /// </summary>
        private bool _isLoaded;

        /// <summary>
        /// Набор правил для КА
        /// </summary>
        public enum CellarAutomatRules
        {
            BaseLife,

            MooreSimple,

            NeimanSimple,

            MooreCyclic,

            NeimanCyclic,

            OneDimentionalCyclic,

            BelousovZhabotinskyReaction,

            VenusSurface
        }

        /// <summary>
        /// Локализация правил КА
        /// </summary>
        private readonly Dictionary<CellarAutomatRules, string> _ruleName;
        #endregion
    }
}
