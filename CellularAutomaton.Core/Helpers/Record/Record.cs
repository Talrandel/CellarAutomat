#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 7.0
// File          : Record.cs
// Author        : Антипкин С.С.
// Created       : 13.06.2017 21:43
// Last Revision : 14.06.2017 21:38
// Description   : 
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CellularAutomaton.Core.Helpers.Record
{
    /// <summary>
    /// Представляет запись функционирования клеточного автомата.
    /// </summary>
    [Serializable]
    public class Record
    {
        #region Fields
        /// <summary>
        /// Форматтер используемый для сериализации записи.
        /// </summary>
        [NonSerialized]
        private readonly BinaryFormatter _bf;

        /// <summary>
        /// Количество состояний клетки клеточного автомата.
        /// </summary>
        private int _statesCount;
        #endregion

        #region Properties
        /// <summary>
        /// Возвращает запись.
        /// </summary>
        public ICollection<Bitmap> Rec { get; private set; }

        /// <summary>
        /// Возвращает или задаёт название правила поведения клеточного автомата.
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// Возвращает или задаёт количество состояний клетки клеточного автомата.
        /// </summary>
        public int StatesCount
        {
            get { return _statesCount; }
            set
            {
                if (value < CellularAutomaton.StatesNumberMin)
                    _statesCount = CellularAutomaton.StatesNumberMin;
                else
                {
                    _statesCount = CellularAutomaton.StatesNumberMax < value
                        ? CellularAutomaton.StatesNumberMax
                        : value;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый пустой экземпляр класса <see cref="Record"/>.
        /// </summary>
        public Record()
        {
            Rec = new LinkedList<Bitmap>();
            _bf = new BinaryFormatter();
        }
        #endregion

        #region Members
        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Save(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Имя файла не задано, пустое или состоит из одних пробелов.",
                                            nameof(fileName));
            }

            using (FileStream fs = File.Create(fileName))
                _bf.Serialize(fs, this);
        }

        /// <summary>
        /// Загружает запись из указанного файла.
        /// </summary>
        /// <param name="fileName">Имя файла для содержащего запись.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Load(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Имя файла не задано, пустое или состоит из одних пробелов.",
                                            nameof(fileName));
            }

            using (FileStream fs = File.OpenRead(fileName))
            {
                Record loadedRec = (Record)_bf.Deserialize(fs);
                Rec = loadedRec.Rec;
                Rule = loadedRec.Rule;
                StatesCount = loadedRec.StatesCount;
            }
        }
        #endregion
    }
}
