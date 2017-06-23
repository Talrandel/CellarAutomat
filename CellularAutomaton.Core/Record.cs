#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Record.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 18.06.2017 12:18
// Last Revision : 23.06.2017 15:18
// Description   : 
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using CellularAutomaton.Core.Properties;

namespace CellularAutomaton.Core
{
    /// <summary>
    /// Представляет запись функционирования клеточного автомата.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class Record : IRecord, IReadOnlyRecord
    {
        #region Fields
        /// <summary>
        /// Форматтер используемый для сериализации записи.
        /// </summary>
        [NonSerialized]
        private readonly BinaryFormatter _bf;

        /// <summary>
        /// Внутренняя структура содержащая данные записи.
        /// </summary>
        private LinkedList<Bitmap> _rec;

        /// <summary>
        /// Количество состояний клетки клеточного автомата.
        /// </summary>
        private int _statesCount;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый пустой экземпляр класса <see cref="Record"/>.
        /// </summary>
        public Record()
        {
            _rec = new LinkedList<Bitmap>();
            _bf = new BinaryFormatter();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Record"/> заданным именем правила поведения и количеством состояний клетки.
        /// </summary>
        /// <param name="nameRule">Название правила поведения клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки клеточного автомата.</param>
        public Record(string nameRule, int statesCount) : this()
        {
            Rule = nameRule;
            StatesCount = statesCount;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Record"/> данными экземпляра класса <see cref="CellularAutomaton"/>.
        /// </summary>
        /// <param name="ca">Экземпляр класса <see cref="CellularAutomaton"/> данными которого инициализируется этот экземпляр.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="ca"/> имеет значение <b>null</b>.</exception>
        public Record(CellularAutomaton ca) : this()
        {
            if (ca == null)
                throw new ArgumentNullException(nameof(ca));

            Rule = ca.Rule.Name;
            StatesCount = ca.StatesCount;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Record"/> данными объекта реализующего интерфейс <see cref="IRecord"/>.
        /// </summary>
        /// <param name="record">Экземпляр объекта реализующего интерфейс <see cref="IRecord"/> данными которого инициализируется этот экземпляр.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="record"/> имеет значение <b>null</b>.</exception>
        public Record(IRecord record) : this()
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            Rule = record.Rule;
            StatesCount = record.StatesCount;
            foreach (Bitmap bitmap in record)
                _rec.AddLast(bitmap);
        }
        #endregion

        #region IRecord Members
        /// <summary>
        /// Возвращает или задаёт название правила поведения клеточного автомата.
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// Возвращает или задаёт количество состояний клетки клеточного автомата.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Количество состояний клетки клеточного автомата должно лежать в интервале [<see cref="CellularAutomaton.StatesNumberMin"/>; <see cref="CellularAutomaton.StatesNumberMax"/>].</exception>
        public int StatesCount
        {
            get { return _statesCount; }
            set
            {
                if ((value < CellularAutomaton.StatesNumberMin) ||
                    (CellularAutomaton.StatesNumberMax < value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        value,
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.Ex__StatesCountOutOfRange,
                            nameof(CellularAutomaton.StatesNumberMin),
                            nameof(CellularAutomaton.StatesNumberMax)));
                }

                _statesCount = value;
            }
        }

        /// <summary>
        /// Возвращает число кадров в записи.
        /// </summary>
        public int Count => _rec.Count;

        /// <summary>
        /// Возвращает значение, указывающее, доступна ли коллекци <see cref="Record"/> только для чтения.
        /// </summary>
        /// <return></return>
        public bool IsReadOnly => ((ICollection<Bitmap>)_rec).IsReadOnly;

        /// <summary>
        /// Добавляет новый кадр в конец записи.
        /// </summary>
        /// <param name="item">Добавляемый кадр.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="item"/> имеет значение <b>null</b>.</exception>
        public void Add(Bitmap item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _rec.AddLast(item);
        }

        /// <summary>
        /// Удаляет все кадры из записи.
        /// </summary>
        public void Clear()
        {
            _rec.Clear();
        }

        /// <summary>
        /// Определяет содержит ли запись указанный кадр.
        /// </summary>
        /// <param name="item">Кадр, который требуется найти в записи.</param>
        /// <returns>Значение <b>true</b>, если кадр найден в записи, иначе - <b>false</b>.</returns>
        public bool Contains(Bitmap item)
        {
            return _rec.Contains(item);
        }

        /// <summary>
        /// Копирует элементы <see cref="Record"/> в массив <see cref="Bitmap"/>, начиная с указанного индекса.
        /// </summary>
        /// <param name="array">
        /// Одномерный массив <see cref="Bitmap"/>, в который копируются элементы из записи. Массив должен иметь индексацию, начинающуюся с нуля.
        /// </param>
        /// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, указывающий начало копирования.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="array"/> имеет значение <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception>
        /// <exception cref="ArgumentException">Количество элементов в записи превышает доступное место, начиная с индекса <paramref name="arrayIndex"/> до конца массива назначения <paramref name="array"/>.</exception>
        public void CopyTo(Bitmap[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex),
                    arrayIndex,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__Значение_параметра__0__меньше__1__,
                        nameof(arrayIndex),
                        0));
            }

            if ((array.Length - arrayIndex) < Count)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__Record_CopyTo_ArgumentException,
                        nameof(arrayIndex),
                        nameof(array)),
                    nameof(arrayIndex));
            }

            _rec.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Удаляет первое вхождение указанного кадра из записи <see cref="Record"/>.
        /// </summary>
        /// <param name="item">Удаляемый кадр.</param>
        /// <returns>Значение <b>true</b>, если кадр <paramref name="item"/> успешно удален из записи, в противном случае — значение <b>false</b>. Этот метод также возвращает значение <b>false</b>, если параметр <paramref name="item"/> не найден в записи.</returns>
        public bool Remove(Bitmap item)
        {
            return _rec.Remove(item);
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий перебор элементов записи.
        /// </summary>
        /// <returns>Интерфейс <see cref="IEnumerator{T}"/>, который может использоваться для перебора элементов записи.</returns>
        public IEnumerator<Bitmap> GetEnumerator()
        {
            return _rec.GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий перебор элементов записи.
        /// </summary>
        /// <returns>Интерфейс <see cref="IEnumerator"/>, который может использоваться для перебора элементов записи.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rec.GetEnumerator();
        }

        /// <summary>
        /// Сохраняет запись в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения записи.</param>
        /// <exception cref="ArgumentException">Имя файла не задано, пустое или состоит из одних пробелов.</exception>
        public void Save(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(
                    Resources.Ex__Имя_файла_не_задано__пустое_или_состоит_из_одних_пробелов_,
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
                throw new ArgumentException(
                    Resources.Ex__Имя_файла_не_задано__пустое_или_состоит_из_одних_пробелов_,
                    nameof(fileName));
            }

            using (FileStream fs = File.OpenRead(fileName))
            {
                Record loadedRec = (Record)_bf.Deserialize(fs);
                _rec = loadedRec._rec;
                Rule = loadedRec.Rule;
                StatesCount = loadedRec.StatesCount;
            }
        }

        /// <summary>
        /// Создает новый объект, являющийся копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
        public object Clone()
        {
            return new Record(this);
        }
        #endregion
    }
}
