using System;
using System.Collections.Generic;

namespace Life
{
    public enum CellarAutomatRules
    {
        BaseLife = 0, MooreSimple, NeimanSimple, MooreCyclic, NeimanCyclic, OneDimentionalCyclic, BelousovZhabotinskyReaction, VenusSurface
    }
    class CellarAutomat
    {
        public Field CurrentField;
        public Field PastField;
        public CellarAutomatRules Rule;
        public bool Stop;
        private int statesNumber;
        public int StatesNumber { get { return statesNumber; } set { if (value <= 0) statesNumber = 2; else if (value > 10) statesNumber = 10; else statesNumber = value; } }

        private Dictionary<CellarAutomatRules, ITransform> Transformation;
        private int timeDelay;
        public int TimeDelay { get { return timeDelay; } set { if (value <= 0 || value >= 5000) timeDelay = 250; else timeDelay = value; } }
        public int Generation { get; private set; }

        public void CellarAutomatProcess()
        {
            for (; ; )
            {
                NextGeneration();
                if (PastField.CheckIdentity(CurrentField) == true)
                {
                    Console.WriteLine("The game process was stopped because of identity of two cellar automat iterations.");
                    break;
                }
                if (Stop)
                {
                    Console.WriteLine("The game process was stopped by user.");
                    break;
                }
                PrintLife();
                CurrentField.CopyFieldToAnother(ref PastField);
                System.Threading.Thread.Sleep(timeDelay);
            }
        }
        public void NewCellarAutomat()
        {
            PastField.SetStartValues(0);
            Generation = 0;
        }
        public void SetDensityForField(int density)
        {
            PastField.SetStartValues(density);
        }
        public void PrintLife()
        {
            Console.WriteLine("Cellar automat rules: " + Enum.GetName(typeof(CellarAutomatRules), Rule));
            CurrentField.PrintField();
            Console.WriteLine("Generation number: " + Generation);
            Console.WriteLine();
        }
        private void NextGeneration()
        {
            TransformCells();
            Generation++;
        }
        private void TransformCells()
        {
            for (int i = 0; i < PastField.GetHeight(); i++)
                for (int j = 0; j < PastField.GetWidth(); j++)
                    CurrentField.SetCell(i, j, Transformation[Rule].TransformCell(PastField, i, j));
        }
        private void InitializeTransormationPairs()
        {
            Transformation.Add(CellarAutomatRules.BaseLife, new BaseLife());
            Transformation.Add(CellarAutomatRules.MooreSimple, new MooreSimple());
            Transformation.Add(CellarAutomatRules.NeimanSimple, new NeimanSimple());
            Transformation.Add(CellarAutomatRules.MooreCyclic, new MooreCyclic(StatesNumber));
            Transformation.Add(CellarAutomatRules.NeimanCyclic, new NeimanCyclic(StatesNumber));
            Transformation.Add(CellarAutomatRules.OneDimentionalCyclic, new OneDimentionalCyclic(StatesNumber));
            Transformation.Add(CellarAutomatRules.BelousovZhabotinskyReaction, new BelousovZhabotinskyReaction());
            Transformation.Add(CellarAutomatRules.VenusSurface, new VenusSurface());
        }

        public CellarAutomat(CellarAutomatRules rule, Field createdField)
        {
            PastField = createdField;
            CurrentField = new Field(createdField.GetWidth(), createdField.GetHeight());
            createdField.CopyFieldToAnother(ref CurrentField);
            this.Rule = rule;
            timeDelay = 250;
            Transformation = new Dictionary<CellarAutomatRules, ITransform>();
            StatesNumber = 4;
            InitializeTransormationPairs();
        }
    }
}