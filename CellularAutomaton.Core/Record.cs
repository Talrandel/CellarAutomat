#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Core
// Project type  : 
// Language      : C# 6.0
// File          : Record.cs
// Author        : Антипкин С.С., Макаров Е.А.
// Created       : 29.06.2017 15:26
// Last Revision : 01.07.2017 22:20
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
    // TODO: Оптимизация памяти. В целях оптимизации использования память необходимо реализовать запись и чтение файла записи, а не хранить её всю в памяти.
    // TODO: Формат записи. Добавить: 1. уникальный ИД записи (GUID), что позволит не загружать уже загруженную запись; 2. Версия формата записи.
    /// <summary>
    /// Представляет запись функционирования клеточного автомата.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class Record : IRecord, IReadOnlyRecord
    {
        #region Fields
        /// <summary>
        /// Внутренняя структура содержащая данные записи.
        /// </summary>
        private LinkedList<Bitmap> _rec;
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализирует новый пустой экземпляр класса <see cref="Record"/>.
        /// </summary>
        public Record()
        {
            _rec = new LinkedList<Bitmap>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Record"/> заданным именем правила поведения и количеством состояний клетки.
        /// </summary>
        /// <param name="nameRule">Название правила поведения клеточного автомата.</param>
        /// <param name="statesCount">Количество состояний клетки клеточного автомата.</param>
        /// <param name="fieldSize">Размеры поля клеточного автомата.</param>
        /// <param name="generation">Число поколений клеточного автомата.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <para>Количество состояний клетки клеточного автомата должно лежать в интервале [<see cref="CellularAutomaton.StatesCountMin"/>; <see cref="CellularAutomaton.StatesCountMax"/>].</para>
        ///     <para>-- или --</para>
        ///     <para>Ширина поля <paramref name="fieldSize"/> меньше нуля.</para>
        ///     <para>-- или --</para>
        ///     <para>Высота поля <paramref name="fieldSize"/> меньше нуля.</para>
        ///     <para>-- или --</para>
        ///     <para>Значение параметра <paramref name="generation"/> меньше 0.</para>
        /// </exception>
        public Record(string nameRule, int statesCount, Size fieldSize, int generation) : this()
        {
            if ((statesCount < CellularAutomaton.StatesCountMin) ||
                (CellularAutomaton.StatesCountMax < statesCount))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(statesCount),
                    statesCount,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__StatesCountOutOfRange,
                        nameof(CellularAutomaton.StatesCountMin),
                        nameof(CellularAutomaton.StatesCountMax)));
            }

            if (fieldSize.Width < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(fieldSize),
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__Ширина_поля__0__меньше_нуля_,
                        nameof(fieldSize)));
            }

            if (fieldSize.Height < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(fieldSize),
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__Высота_поля__0__меньше_нуля_,
                        nameof(fieldSize)));
            }

            if (generation < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(generation),
                    generation,
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Ex__Значение_параметра__0__меньше__1__,
                        nameof(generation),
                        0));
            }

            Rule = nameRule;
            StatesCount = statesCount;
            FieldSize = fieldSize;
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
            FieldSize = new Size(ca.CurrentField.Width, ca.CurrentField.Height);
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
            FieldSize = record.FieldSize;

            foreach (Bitmap bitmap in record)
                _rec.AddLast(bitmap);
        }
        #endregion

        #region IRecord Members
        /// <summary>
        /// Возвращает название правила поведения клеточного автомата.
        /// </summary>
        public string Rule { get; private set; }

        /// <summary>
        /// Возвращает количество состояний клетки клеточного автомата.
        /// </summary>
        public int StatesCount { get; private set; }

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
        /// Возвращает размеры поля клеточного автомата.
        /// </summary>
        public Size FieldSize { get; private set; }

        /// <summary>
        /// Возвращает число поколений.
        /// </summary>
        public int Generation => Count;

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
            if (0 < _rec.Count)
            {
                _rec.Clear();
                GC.Collect();
            }
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
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this);
            }
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

            Clear();

            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Record loadedRec = bf.Deserialize(fs) as Record;
                if (loadedRec == null)
                    throw new ArgumentException("Файл с записью повреждён.", nameof(fileName));

                _rec = loadedRec._rec;
                Rule = loadedRec.Rule;
                StatesCount = loadedRec.StatesCount;
                FieldSize = loadedRec.FieldSize;
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
