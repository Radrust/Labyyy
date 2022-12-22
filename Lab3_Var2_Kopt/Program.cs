using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    class Program
    {
        static void Main()
        {
            MagazineCollection mc = new MagazineCollection();
            mc.AddMagazines(
                new Magazine[3] //создание коллекции с заданным числом эл-тов (у нас их 3)
                {
                    new Magazine("Exit", Frequency.Monthly, new System.DateTime(1), 200),
                    new Magazine("People", Frequency.Weekly, new System.DateTime(2), 510),
                    new Magazine("Frieze", Frequency.Yearly, new System.DateTime(3), 300),
                }
            );

            mc.SortByName();
            Console.WriteLine("Сортировка по названию:\n" + mc.ToString());

            mc.SortByRelease();
            Console.WriteLine("Сортировка по дате выхода:\n" + mc.ToString());

            mc.SortByAmount();
            Console.WriteLine("Сортировка по тиражу:\n" + mc.ToString());

            Console.WriteLine("\nМаксимальный сред рейтинг:", mc.MaxRating, "\n");

            foreach (Magazine magazine in mc.MonthlyMagazines)
                Console.WriteLine(magazine.ToString());

            foreach (Magazine magazine in mc.RatingGroup(3))
                Console.WriteLine(magazine.ToString());
            TestCollections test = new TestCollections(1000);
            int[] a = test.Test(0);

            Console.WriteLine("\nпервый эл-т:");
            for (int i = 0; i < 4; ++i)
            {
                Console.Write(a[i].ToString() + ' ');
            }
            Console.WriteLine();
            a = test.Test(500);

            Console.WriteLine("\nсредний эл-т:");
            for (int i = 0; i < 4; ++i)
            {
                Console.Write(a[i].ToString() + ' ');
            }
            Console.WriteLine();
            a = test.Test(999);

            Console.WriteLine("\nпоследний эл-т:");
            for (int i = 0; i < 4; ++i)
            {
                Console.Write(a[i].ToString() + ' ');
            }
            Console.WriteLine();
            a = test.Test(-1);

            Console.WriteLine("\nне входит в коллекции:");
            for (int i = 0; i < 4; ++i)
            {
                Console.Write(a[i].ToString() + ' ');
            }
            Console.ReadKey();
        }
    }
}