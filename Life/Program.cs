using System;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Field f = new Field(10, 10);
            CellarAutomat Automat = new CellarAutomat(CellarAutomatRules.BaseLife, f);
            Automat.NewCellarAutomat();
            Automat.TimeDelay = 2000;
            Automat.SetDensityForField(90);
            
            Automat.CellarAutomatProcess();
            Console.ReadLine();
        }
    }
}
// Восстановление матрицы по столбцу и строке. 
// Формула: Zrs = Zrt + Zts. t - вспомогательный индекс, r - строка, s - столбец
// Нормальный закон в равномерном шкалировании: 
// Смотрим на мнения одного эксперта. Мнения остальных экспертов (вспомогательные веса) считаются такими, чтобы они были согласованы с мнениями выбранного эксперта.
// Почему разность значений меньшая СКО удовлетворяет условию: наибольшее по абсолютной величине расхождение между расчетной частотой и взятой из экспертных оценок равняется VALUE, что меньше трех СКО (меньше трех сигм). Из этого следует, что экспертные оценки непротиворечивы.