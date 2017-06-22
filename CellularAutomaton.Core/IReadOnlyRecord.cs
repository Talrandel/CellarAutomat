using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Определяет методы используемые для управления полями клеточных автоматов доступными только для чтения.
    /// </summary>
    interface IReadOnlyRecord
    {
        string Rule { get; }
        int StatesCount { get; }
        int Count { get; }
        bool Contains(Bitmap item);
        void CopyTo(Bitmap[] array, int arrayIndex);
        void Save(string fileName);
        void Load(string fileName);
    }
}
