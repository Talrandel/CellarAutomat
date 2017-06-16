#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : CellularAutomaton.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 16.06.2017 12:37
// Last Revision : 16.06.2017 12:48
// Description   : 
#endregion

using System;

using CellularAutomaton.Core.Rules;

[assembly: CLSCompliant(true)]

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Представляет клеточный автомат.
    /// </summary>
    public class CellularAutomaton
    {
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
        public readonly Field CurrentField;

        /// <summary>
        /// Поле клеточного автомата на прошлой итерации.
        /// </summary>
        private Field _pastField;

        /// <summary>
        /// Количество состояний клетки клеточного автомата.
        /// </summary>
        private int _statesCount;
        #endregion

        #region Properties
        /// <summary>
        /// Правило работы клеточного автомата.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public IRule Rule { get; }

        /// <summary>
        /// Возвращает или задаёт флаг остановки работы клеточного автомата.
        /// </summary>
        public bool Stop { get; set; }

        /// <summary>
        /// Возвращает или задаёт количество состояний клетки клеточного автомата.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public int StatesCount
        {
            get { return _statesCount; }
            set
            {
                if (value <= 0)
                    _statesCount = StatesNumberMin;
                else
                    _statesCount = StatesNumberMax < value ? StatesNumberMax : value;
            }
        }

        /// <summary>
        /// Возвращает номер поколения.
        /// </summary>
        public int Generation { get; private set; }
        #endregion

        #region Constructors
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
        public CellularAutomaton(IRule rule, Field createdField, int statesCount)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));
            if (createdField == null)
                throw new ArgumentNullException(nameof(createdField));

            Rule = rule;
            _pastField = createdField;

            CurrentField = new Field(createdField.Width, createdField.Height);
            createdField.Copy(ref CurrentField);
            StatesCount = statesCount;

            Stop = false;

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
        public CellularAutomaton(IRule rule, Field createdField, int statesCount, byte density) :
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
        public CellularAutomaton(IRule rule, int widthField, int heidhtField, int statesCount, byte density) :
            this(rule, new Field(widthField, heidhtField), statesCount, density) { }
        #endregion

        #region Members
        /// <summary>
        /// Событие смены поколения.
        /// </summary>
        public event EventHandler GenerationChanged;

        /// <summary>
        /// Вызывает событие <see cref="GenerationChanged"/>.
        /// </summary>
        protected virtual void OnGenerationChanged()
        {
            GenerationChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Осуществляет расчёт поведения клеточного автомата.
        /// </summary>
        public void Processing()
        {
            for (;;)
            {
                NextGeneration();
                OnGenerationChanged();

                if (_pastField.Equals(CurrentField))
                {
                    Stop = true;
                    break;
                }

                if (Stop)
                    break;

                CurrentField.Copy(ref _pastField);
            }
        }

        /// <summary>
        /// Устанавливает плотность распределения клеток на поле.
        /// </summary>
        /// <param name="density">Плотность распределения живых клеток на поле [0; 100].</param>
        public void SetDensityForField(byte density)
        {
            if (100 < density)
                density = 100;

            _pastField.SetStartValues(StatesCount, density);
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
                    CurrentField.SetCell(i, j, Rule.TransformCell(_pastField, i, j, StatesCount));
        }

        /// <summary>
        /// Инициализирует новый клеточный автомат.
        /// </summary>
        public void Initialize()
        {
            _pastField.SetStartValues(StatesCount, 0);
            Generation = 0;
        }
        #endregion
    }
}
