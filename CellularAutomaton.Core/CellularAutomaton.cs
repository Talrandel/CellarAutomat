#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomaton.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 17.06.2017 17:27
// Last Revision : 24.06.2017 22:32
// Description   : 
#endregion

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using CellularAutomaton.Core.Properties;
using CellularAutomaton.Core.Rules;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Представляет клеточный автомат.
    /// </summary>
    public class CellularAutomaton
    {
        #region Events
        /// <summary>
        /// Событие смены поколения.
        /// </summary>
        public event EventHandler GenerationChanged;
        #endregion

        #region Static Fields and Constants
        /// <summary>
        /// Максимальное количество состояний клетоки.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public const int StatesNumberMax = 16;

        /// <summary>
        /// Минимальное количество состояний клетки.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public const int StatesNumberMin = 2;
        #endregion

        #region Fields
        /// <summary>
        /// Поле клеточного автомата на текущей итерации.
        /// </summary>
        private readonly IField _currentField;

        /// <summary>
        /// Представляет, вызываемый при отправке сообщения в контекст синхронизации <see cref="_synchronizationContext"/>.
        /// </summary>
        private readonly SendOrPostCallback _invokeHandlers;

        /// <summary>
        /// Объект синхронизации.
        /// </summary>
        private readonly object _lockObj = new object();

        /// <summary>
        /// Контекст синхронизации.
        /// </summary>
        private readonly SynchronizationContext _synchronizationContext;

        /// <summary>
        /// Токен отмены.
        /// </summary>
        private CancellationToken _ct;

        /// <summary>
        /// Поле клеточного автомата на прошлой итерации.
        /// </summary>
        private IField _pastField;

        /// <summary>
        /// Количество состояний клетки клеточного автомата.
        /// </summary>
        private int _statesCount;

        /// <summary>
        /// TODO: выкинуть _stop
        /// </summary>
        private bool _stop;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает текущее поле.
        /// </summary>
        public IReadOnlyField CurrentField => (IReadOnlyField)_currentField;

        /// <summary>
        /// Возвращает номер поколения.
        /// </summary>
        public int Generation { get; private set; }

        /// <summary>
        /// Возвращает правило работы клеточного автомата.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public IRule Rule { get; }

        /// <summary>
        /// Возвращает или задаёт количество состояний клетки клеточного автомата.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Количество состояний клетки клеточного автомата должно лежать в интервале [<see cref="StatesNumberMin"/>; <see cref="StatesNumberMax"/>].</exception>
        // ReSharper disable once MemberCanBePrivate.Global
        public int StatesCount
        {
            get { return _statesCount; }
            set
            {
                if ((value < StatesNumberMin) ||
                    (StatesNumberMax < value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__StatesCountOutOfRange,
                            nameof(StatesNumberMin),
                            nameof(StatesNumberMax)));
                }

                _statesCount = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт флаг остановки работы клеточного автомата.
        /// </summary>
        public bool Stop
        {
            get { return _stop; }
            set
            {
                lock (_lockObj)
                    _stop = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomaton"/> заданными: правилом работы, полем и минимальным количеством состояний клетки <see cref="StatesNumberMin"/>.
        /// </summary>
        /// <param name="rule">Правило работы клеточного автомата.</param>
        /// <param name="createdField">Поле клеточного автомата.</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>Параметр <paramref name="rule"/> имеет значение <b>null</b>.</para>
        ///     <para>-- или --</para>
        ///     <para>Параметр <paramref name="createdField"/> имеет значение <b>null</b>.</para>
        /// </exception>
        public CellularAutomaton(IRule rule, IField createdField) :
            this(rule, createdField, StatesNumberMin) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomaton"/> заданными: правилом работы, полем и количеством состояний клетки.
        /// </summary>
        /// <param name="rule">Правило работы клеточного автомата.</param>
        /// <param name="createdField">Поле клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>Параметр <paramref name="rule"/> имеет значение <b>null</b>.</para>
        ///     <para>-- или --</para>
        ///     <para>Параметр <paramref name="createdField"/> имеет значение <b>null</b>.</para>
        /// </exception>
        public CellularAutomaton(IRule rule, IField createdField, int statesCount)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));
            if (createdField == null)
                throw new ArgumentNullException(nameof(createdField));

            Rule = rule;
            _pastField = createdField;

            _currentField = new Field(createdField.Width, createdField.Height);
            createdField.Copy(ref _currentField);
            StatesCount = statesCount;

            Stop = false;

            _synchronizationContext = new SynchronizationContext();
            _invokeHandlers = state => OnGenerationChanged();

            Initialize();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomaton"/> заданными: правилом работы, размерами поля и количеством состояний клетки.
        /// </summary>
        /// <param name="rule">Правило работы клеточного автомата.</param>
        /// <param name="widthField">Ширина поля клеточного автомата.</param>
        /// <param name="heidhtField">Высота поля клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="rule"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <para>Ширина поля <paramref name="widthField"/> меньше нуля.</para>
        ///     <para>-- или --</para>
        ///     <para>Высота поля <paramref name="heidhtField"/> меньше нуля.</para>
        /// </exception>
        public CellularAutomaton(IRule rule, int widthField, int heidhtField, int statesCount) :
            this(rule, new Field(widthField, heidhtField), statesCount) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomaton"/> заданными: правилом работы, полем, количеством состояний клетки и плотностью распределения живых клеток на поле.
        /// </summary>
        /// <param name="rule">Правило работы клеточного автомата.</param>
        /// <param name="createdField">Поле клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        /// <exception cref="ArgumentNullException">
        ///     <para>Параметр <paramref name="rule"/> имеет значение <b>null</b>.</para>
        ///     <para>-- или --</para>
        ///     <para>Параметр <paramref name="createdField"/> имеет значение <b>null</b>.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">Величина плотности распределения живых клеток на поле должна лежать в интервале [0; 100].</exception>
        public CellularAutomaton(IRule rule, IField createdField, int statesCount, byte density) :
            this(rule, createdField, statesCount)
        {
            SetDensityForField(density);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CellularAutomaton"/> заданными: правилом работы, полем, количеством состояний клетки и плотностью распределения живых клеток на поле.
        /// </summary>
        /// <param name="rule">Правило работы клеточного автомата.</param>
        /// <param name="widthField">Ширина поля клеточного автомата.</param>
        /// <param name="heidhtField">Высота поля клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки.</param>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="rule"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <para>Ширина поля <paramref name="widthField"/> меньше нуля.</para>
        ///     <para>-- или --</para>
        ///     <para>Высота поля <paramref name="heidhtField"/> меньше нуля.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">Величина плотности распределения живых клеток на поле должна лежать в интервале [0; 100].</exception>
        public CellularAutomaton(IRule rule, int widthField, int heidhtField, int statesCount, byte density) :
            this(rule, new Field(widthField, heidhtField), statesCount, density) { }
        #endregion

        #region Members
        /// <summary>
        /// Инициализирует новый клеточный автомат.
        /// </summary>
        public void Initialize()
        {
            _pastField.SetStartValues(StatesCount, 0);
            Generation = 0;
        }

        /// <summary>
        /// Осуществляет расчёт поведения клеточного автомата.
        /// </summary>
        public void Processing()
        {
            Stop = false;

            while (true)
            {
                // TODO: Заменить на InnerProcessingAsync(), убрать Stop заменив на CancellationToken.
                NextGeneration();
                OnGenerationChanged();

                if (Stop || _pastField.Equals(CurrentField))
                {
                    Stop = true;
                    break;
                }

                CurrentField.Copy(ref _pastField);
            }
        }

        /// <summary>
        /// Осуществляет асинхронный расчёт поведения клеточного автомата.
        /// </summary>
        /// <param name="ct">Токен отмены, который должен использоваться для отмены работы.</param>
        /// <exception cref="OperationCanceledException">Операция вычисления отменена.</exception>
        public Task ProcessingAsync(CancellationToken ct)
        {
            _ct = ct;
            return Task.Run(new Action(InnerProcessingAsync), ct);
        }

        /// <summary>
        /// Устанавливает плотность распределения клеток на поле.
        /// </summary>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        /// <exception cref="ArgumentOutOfRangeException">Величина плотности распределения живых клеток на поле должна лежать в интервале [0; 100].</exception>
        public void SetDensityForField(byte density)
        {
            if (100 < density)
                throw new ArgumentOutOfRangeException(nameof(density), density, Resources.Ex__DensityOutOfRange);

            _pastField.SetStartValues(StatesCount, density);
        }

        /// <summary>
        /// Вызывает событие <see cref="GenerationChanged"/>.
        /// </summary>
        protected virtual void OnGenerationChanged()
        {
            GenerationChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// <b>Внутренний метод.</b> Осуществляет вычисления при асинхронном расчёте поведения клеточного автомата.
        /// </summary>
        /// <exception cref="OperationCanceledException">Операция вычисления отменена.</exception>
        private void InnerProcessingAsync()
        {
            Stop = false;

            while (true)
            {
                NextGeneration();

                // TODO: Проверить работоспособность Async варианта и перейти на него.
                //OnGenerationChanged();
                _synchronizationContext.Send(_invokeHandlers, null);

                if (_ct.IsCancellationRequested || _pastField.Equals(CurrentField))
                {
                    Stop = true;
                    _ct.ThrowIfCancellationRequested();
                    break;
                }

                CurrentField.Copy(ref _pastField);
            }
        }

        /// <summary>
        /// Осуществляет переход к следующему поколению.
        /// </summary>
        private void NextGeneration()
        {
            TransformCells();
            Generation++;
        }

        /// <summary>
        /// Изменяет состояния клеток в соответствии с выбранным правилом.
        /// </summary>
        private void TransformCells()
        {
            for (int i = 0; i < _pastField.Height; i++)
                for (int j = 0; j < _pastField.Width; j++)
                    _currentField[i, j] = Rule.TransformCell(_pastField, i, j, StatesCount);
        }
        #endregion
    }
}
