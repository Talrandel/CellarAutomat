using System;
using System.Collections.Generic;

namespace CellarAutomat
{
    /// <summary>
    /// Набор правил для КА
    /// </summary>
    public enum CellarAutomatRules
    {
        BaseLife = 0, MooreSimple, NeimanSimple, MooreCyclic, NeimanCyclic, OneDimentionalCyclic, BelousovZhabotinskyReaction, VenusSurface
    }

    /// <summary>
    /// Класс клеточного автмата
    /// </summary>
    public class CellarAutomat
    {
        /// <summary>
        /// Максимальное количество состояний клеток
        /// </summary>
        public const int StatesNumberMax = 16;

        /// <summary>
        /// Минимальное количество состояний клеток
        /// </summary>
        public const int StatesNumberMin = 2;

        /// <summary>
        /// Поле КА на текущей итерации
        /// </summary>
        private Field CurrentField;

        /// <summary>
        /// Поле КА на прошлой итерации
        /// </summary>
        private Field PastField;

        /// <summary>
        /// Выбранное правило КА
        /// </summary>
        public CellarAutomatRules Rule { get; set; }

        /// <summary>
        /// Признак остановки работы КА
        /// </summary>
        public bool Stop { get; set; }

        private int statesCount;
        /// <summary>
        /// Количество состояний клеток для экземпляра КА
        /// </summary>
        public int StatesCount { get { return statesCount; } set { if (value <= 0) statesCount = StatesNumberMin; else if (value > StatesNumberMax) statesCount = StatesNumberMax; else statesCount = value; } }

        /// <summary>
        /// Набор пар: правило - логика изменения состояний клеток
        /// </summary>
        private Dictionary<CellarAutomatRules, ITransform> _transformation;

        /// <summary>
        /// Поколение КА
        /// </summary>
        public int Generation { get; private set; }

        /// <summary>
        /// Событие изменения поколения
        /// </summary>
        public event Action GenerationChanged;

        /// <summary>
        /// Основная логика функционирования КА
        /// </summary>
        public void CellarAutomatProcess()
        {
            for (; ;)
            {
                NextGeneration();
                GenerationChanged?.Invoke();
                if (PastField.CheckIdentity(CurrentField))
                {
                    Stop = true;
                    break;
                }
                if (Stop)
                    break;
                CurrentField.CopyFieldToAnother(ref PastField);
            }
        }

        /// <summary>
        /// Инициализация нового КА
        /// </summary>
        public void NewCellarAutomat()
        {
            PastField.SetStartValues(StatesCount);
            Generation = 0;
        }

        /// <summary>
        /// Установить плотность для поля КА
        /// </summary>
        /// <param name="density">Плотность распределения клеток на поле</param>
        public void SetDensityForField(int density)
        {
            if (density < 0)
                density = 0;
            if (density > 100)
                density = 100;
            PastField.SetStartValues(density, StatesCount);
        }

        /// <summary>
        /// Изменение поколения (+1)
        /// </summary>
        private void NextGeneration()
        {
            TransformCells();
            Generation++;
        }

        /// <summary>
        /// Изменение состояния клеток в соответствии с выбранным правилом
        /// </summary>
        private void TransformCells()
        {
            for (int i = 0; i < PastField.GetHeight(); i++)
                for (int j = 0; j < PastField.GetWidth(); j++)
                    CurrentField.SetCell(i, j, _transformation[Rule].TransformCell(PastField, i, j));
        }

        /// <summary>
        /// Инициализация словаря для изменения состояний клеток
        /// </summary>
        private void InitializeTransormationPairs()
        {
            _transformation.Add(CellarAutomatRules.BaseLife, new BaseLife(StatesCount));
            _transformation.Add(CellarAutomatRules.MooreSimple, new MooreSimple(StatesCount));
            _transformation.Add(CellarAutomatRules.NeimanSimple, new NeimanSimple(StatesCount));
            _transformation.Add(CellarAutomatRules.MooreCyclic, new MooreCyclic(StatesCount));
            _transformation.Add(CellarAutomatRules.NeimanCyclic, new NeimanCyclic(StatesCount));
            _transformation.Add(CellarAutomatRules.OneDimentionalCyclic, new OneDimentionalCyclic(StatesCount));
            _transformation.Add(CellarAutomatRules.BelousovZhabotinskyReaction, new BelousovZhabotinskyReaction(StatesCount));
            _transformation.Add(CellarAutomatRules.VenusSurface, new VenusSurface(StatesCount));
        }

        /// <summary>
        /// Обновить количество состояний клеток для выбранного правила КА
        /// </summary>
        /// <param name="rule">Выбранное правило</param>
        /// <param name="StatesCount">Количество состояний</param>
        public void RefreshRuleStatesCount(CellarAutomatRules rule, int StatesCount)
        {
            _transformation[rule].StatesCount = StatesCount;
        }

        /// <summary>
        /// Конструктор КА
        /// </summary>
        /// <param name="rule">Выбранное правило работы КА</param>
        /// <param name="createdField">Созданное поле</param>
        public CellarAutomat(CellarAutomatRules rule, Field createdField, int statesCount)
        {
            PastField = createdField;
            CurrentField = new Field(createdField.GetWidth(), createdField.GetHeight());
            createdField.CopyFieldToAnother(ref CurrentField);
            Rule = rule;
            Stop = false;
            _transformation = new Dictionary<CellarAutomatRules, ITransform>();
            InitializeTransormationPairs();
            StatesCount = statesCount;
        }
    }
}