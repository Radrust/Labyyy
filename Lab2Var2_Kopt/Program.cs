using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Lab2Var2_Kopt
{
    enum Frequency { Weekly, Monthly, Yearly }; //тип с перечислением значений, в данном варианте это частота выхода выпусков

    interface IRateAndCopy  //определяем интерфейс IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }

    public static class Shifter
    {
        public static int ShiftAndWrap(int value, int positions)
        {
            positions &= 0x1F;
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }
    }

    static class Program
    {
        static void Main()
        {
            Edition edition = new Edition();
            Edition edition1 = new Edition();
            Console.WriteLine("Равенство записей: " + (edition == edition1).ToString() + ", равенство ссылок: " + ReferenceEquals(edition, edition1).ToString() +
                ", хэш-код 1: " + edition.GetHashCode().ToString() + ", хэш-код 2: " + edition1.GetHashCode().ToString());

            Console.WriteLine("\nПрисваиваем отриц значение тиражу");
            try
            {
                edition.Amount = -10;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nСоздаем журнал");
            Magazine m = new Magazine();
            m.AddArticles(
                new Article[2]
                {
                    new Article (new Person("Дарья", "Копт", new System.DateTime(2003, 12, 1)), "Лаба вторая", 4.5),
                    new Article (new Person("Иван", "Иванов", new System.DateTime(2004, 5, 17)), "Статья такая-то", 2.1)
                }
            );
            m.AddEditors(
                new Person[2]
                {
                    new Person("Евгений", "Евгеньев", new System.DateTime(2003, 1, 8)),
                    new Person("Иван", "Иванов", new System.DateTime(2004, 5, 17))
                }
            );
            Console.WriteLine(m.ToString());

            Console.WriteLine("\nИздание по умолч");
            Console.WriteLine(m.Edition.ToString());

            Console.WriteLine("\n");
            Magazine m1 = m.DeepCopy();
            ((Article)m1.Articles[0]).title = "Лабаааааааааа";
            m1.AddEditors(
                new Person[1]
                {
                    new Person("Сергей", "Сергеев", new System.DateTime(2002, 5, 5))
                }
            );
            m1.Amount = 10;
            Console.WriteLine(m.ToString());
            Console.WriteLine(m1.ToString());

            Console.WriteLine("\nСтатьи рейтинга больше 3");
            foreach (Article article in m1.GetArticlesWithRaiting(3))
                Console.WriteLine(article.ToString());

            Console.WriteLine("\nСтатьи со строкой Лаба в названии");
            foreach (Article article in m1.GetArticlesWithStr("Лаба"))
                Console.WriteLine(article.ToString());

            Console.WriteLine("\n");
            foreach (Article article in m1)
                Console.WriteLine(article.ToString());

            Console.WriteLine("\nСтатьи у которых автор редактор журнала");
            foreach (Article article in m1.GetArticlesWithAuthorIsEditor())
                Console.WriteLine(article.ToString());

            Console.WriteLine("\nРедакторы без статей в журнале");
            foreach (Person per in m1.GetEditorIsNotAuthors())
                Console.WriteLine(per.ToString());
            Console.ReadKey();
        }
    }
}