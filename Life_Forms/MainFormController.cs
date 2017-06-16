using FastBitmap;
using CellarAutomat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace CellarAutomatForm
{
    /// <summary>
    /// Контроллер MainForm
    /// </summary>
    internal class MainFormController
    {
        #region Переменные и константы

        /// <summary>
        /// Максимальное количество итераций
        /// </summary>
        public const int MaxIterations = 50000;

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
        private CellarAutomat.CellarAutomat automat;

        /// <summary>
        /// Поле для КА
        /// </summary>
        private Field _field;

        /// <summary>
        /// Объект для отображения КА в форме
        /// </summary>
        private Bitmap _map;

        /// <summary>
        /// Обёртка для быстрой работы с Bitmap посредством использования unsafe кода
        /// </summary>
        private LockImage _lI;

        /// <summary>
        /// Строится ли сейчас КА
        /// </summary>
        private bool isBuilding;

        /// <summary>
        /// Воспроизводится ли сейчас КА
        /// </summary>
        private bool isPlaying;

        /// <summary>
        /// Загружен ли КА из файла
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Обёртка для сохранения построенного КА в файл
        /// </summary>
        private SerializableBitmap _sBitmap;

        /// <summary>
        /// Номер поколения КА
        /// </summary>
        private int _generation = 0;

        /// <summary>
        /// Объект для сериализации/сохранения КА
        /// </summary>
        private BinaryFormatter _bf;

        /// <summary>
        /// Локализация правил КА
        /// </summary>
        private Dictionary<CellarAutomatRules, string> RuleName;

        #endregion

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="mf"></param>
        public MainFormController(MainForm mf)
        {
            this.MainForm = mf;
            _bf = new BinaryFormatter();
            _sBitmap = new SerializableBitmap();

            isBuilding = false;
            isPlaying = false;
            isLoaded = false;
            _sBitmap.BitmapArray = new Bitmap[MaxIterations];

            RuleName = new Dictionary<CellarAutomatRules, string>();
            RuleName.Add(CellarAutomatRules.BaseLife, "Жизнь");
            RuleName.Add(CellarAutomatRules.MooreSimple, "Автомат Мура");
            RuleName.Add(CellarAutomatRules.NeimanSimple, "Автомат Неймана");
            RuleName.Add(CellarAutomatRules.MooreCyclic, "Циклический автомат Мура");
            RuleName.Add(CellarAutomatRules.NeimanCyclic, "Циклический автомат Неймана");
            RuleName.Add(CellarAutomatRules.OneDimentionalCyclic, "Одномерный автомат");
            RuleName.Add(CellarAutomatRules.BelousovZhabotinskyReaction, "Реакция Белоусова-Жаботинского");
            RuleName.Add(CellarAutomatRules.VenusSurface, "Поверхность Венеры");

            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
        }

        /// <summary>
        /// Инициализация вспомогательных переменных для КА
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="dencity"></param>
        /// <param name="rule"></param>
        private void InitializeCAVariables(int height, int width, int dencity, CellarAutomatRules rule, int statesCount)
        {
            _map = new Bitmap(width, height);
            _field = new Field(width, height);
            //_lI = new LockImage(ref _map);
            InitializeCA(dencity, rule, statesCount);
        }

        /// <summary>
        /// Инициализация контролов формы
        /// </summary>
        public void InitializeForm()
        {
            foreach (var rule in RuleName)
            {
                MainForm.comboBoxRules.Items.Add(rule.Value);
            }
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
        /// <param name="height">A</param>
        /// <param name="width">B</param>
        /// <param name="dencity">C</param>
        /// <param name="rule">D</param>
        public void Build(int height, int width, int dencity, CellarAutomatRules rule, int statesCount)
        {
            if (isBuilding)
                return;
            if (isPlaying)
                return;
            // Пара объектов для отмены асинхронной операции
            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
            Task.Factory.StartNew(() =>
            {
                isBuilding = true;
                // Меняем значения контролов формы
                MainForm.Invoke(new Action(() => MainForm.pictureBoxField.Image = null));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Build));
                // Инициализируем массив изображений - поколений КА
                _sBitmap.BitmapArray = new Bitmap[MaxIterations];
                // Инициализация переменных для КА
                InitializeCAVariables(height, width, dencity, rule, statesCount);
                // Обновляем количество состояний клеток для выбранного правила
                automat.RefreshRuleStatesCount(rule, statesCount);
                // Запускаем основной процесс
                automat.CellarAutomatProcess();
            }, _ct).ContinueWith((t) =>
            {
                isBuilding = false;
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
            if (!isBuilding)
                return;
            isBuilding = false;
            automat.Stop = true;
            MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.StopBuilding));
        }

        /// <summary>
        /// Сохранить текущее поколение КА
        /// </summary>
        public void SaveCAState()
        {
            _sBitmap.BitmapArray[_generation++] = _map.Clone() as Bitmap;
        }

        /// <summary>
        /// Сохранить построенный КА в файл
        /// </summary>
        public void SaveCA()
        {
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var bitmapArray = new Bitmap[_generation];
                Array.Copy(_sBitmap.BitmapArray, bitmapArray, _generation);
                _bf.Serialize(fs, _sBitmap);
            }
            _sBitmap.BitmapArray = new Bitmap[MaxIterations];
            _generation = 0;
            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Saved));
        }

        /// <summary>
        /// Загрузка последнего построенного КА из файла
        /// </summary>
        public void LoadCA()
        {
            while (true)
            {
                try
                {
                    using (FileStream fs = new FileStream(FileName, FileMode.Open))
                    {
                        _sBitmap = _bf.Deserialize(fs) as SerializableBitmap;
                    }
                    isLoaded = true;
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
        }

        /// <summary>
        /// Обновление поля КА и сохранение построенного состояния
        /// </summary>
        private void RefreshField()
        {
            //_lI.SetImage(ref _map);
            for (int i = 0; i < _map.Width; i++)
            {
                for (int j = 0; j < _map.Height; j++)
                {
                    var color = GetColorByPixelValue(_field.GetCell(i, j));
                    //_lI.SetLockPixel(i, j, color);
                    _map.SetPixel(i, j, color);
                }
            }
            //_lI.UnlockImage();
            SaveCAState();
        }

        /// <summary>
        /// Получить цвет клетки по номеру ее состояния
        /// </summary>
        /// <param name="value">Состояние клетки от 0 до 15</param>
        /// <returns>Цвет</returns>
        private Color GetColorByPixelValue(int value)
        {
            switch (value)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Pink;
                case 5:
                    return Color.DarkBlue;
                case 6:
                    return Color.White;
                case 7:
                    return Color.Orange;
                case 8:
                    return Color.GreenYellow;
                case 9:
                    return Color.MediumVioletRed;
                case 10:
                    return Color.BlueViolet;
                case 11:
                    return Color.PaleVioletRed;
                case 12:
                    return Color.LightGreen;
                case 13:
                    return Color.Purple;
                case 14:
                    return Color.PapayaWhip;
                case 15:
                    return Color.SaddleBrown;
                default:
                    return Color.Black;
            }
        }

        /// <summary>
        /// Воспроизвести загруженный КА
        /// </summary>
        /// <param name="showDelay"></param>
        public void Play(int showDelay)
        {
            if (isBuilding)
            {
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Building));
                return;
            }
            if (isPlaying)
                return;
            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
            Task.Factory.StartNew(() =>
            {
                isPlaying = true;
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.Play));
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = true));
                for (int j = 0; j < _sBitmap.BitmapArray.Length; j++)
                {
                    // Массив хранит 50000 поколений, но не все заполнены. Если встречается пустой bitmap, это признак последней итерации
                    if (_sBitmap.BitmapArray[j] == null)
                        break;
                    Tick(j);
                    Thread.Sleep(showDelay);
                }
            }, _ct)
            .ContinueWith((t) =>
            {
                isPlaying = false;
                MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
                MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = false));
                MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.PlayingEnded)); 
            }, _ct);
        }

        /// <summary>
        /// Отображение поколения КА с заданным номером
        /// </summary>
        /// <param name="number">Номер поколения КА </param>
        public void Tick(int number)
        {
            MainForm.pictureBoxField.Image = _sBitmap.BitmapArray[number];
        }

        /// <summary>
        /// Остановка воспроизведения КА
        /// </summary>
        public void StopPlaying()
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            isPlaying = false;
            MainForm.Invoke(new Action(() => MainForm.buttonStopBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonBuild.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonPlay.Enabled = true));
            MainForm.Invoke(new Action(() => MainForm.buttonStopPlay.Enabled = false));
            MainForm.Invoke(new Action(() => MainForm.labelMessage.Text = CAMessages.StopPlaying));
        }

        /// <summary>
        /// Инициализация автомата с заданными параметрами
        /// </summary>
        /// <param name="dencity"></param>
        /// <param name="rule"></param>
        private void InitializeCA(int dencity, CellarAutomatRules rule, int statesCount)
        {
            automat = new CellarAutomat.CellarAutomat(rule, _field, statesCount);
            automat.GenerationChanged += RefreshField;
            automat.NewCellarAutomat();
            automat.SetDensityForField(dencity);
        }
        
        /// <summary>
        /// Отмена выполнения заданий при закрытии формы
        /// </summary>
        public void Close()
        {
            _cts.Cancel();
        }
    }
}