using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    class MagazineCollection
    {
        private List<Magazine> magazines = new List<Magazine>();

        public void AddDefault(int count) //метод для добавления в список List<Magazine> некоторого числа эл-тов типа Magazine для инициализации коллекции по умолч
        {
            this.magazines.AddRange(new List<Magazine>(count));
        }

        public void AddMagazines(Magazine[] magazines) //для добавления эл-тов в список List<Magazine>
        {
            this.magazines.AddRange(magazines);
        }

        public override string ToString() //строка с информацией, в т.ч. все поля, список статей и редакторов для каждого эл-та Magazine
        {
            string res = "";
            for (int i = 0; i < magazines.Count; i++)
            {
                res += "№" + (i+1) + ":\n" + magazines[i].ToString() + "\n\n";
            }
            return res;
        }

        public virtual string ToShortString() //строка с информацией обо всех эл-тах списка, в т.ч. все поля, число редакторов и статей без их списков
        {
            string res = "";
            for (int i = 0; i < magazines.Count; i++)
            {
                res += ("Журнал " + i.ToString() + ":\n" + magazines[i].ToShortString() + "\nКол-во статей: " +
                    magazines[i].Articles.Count + "\nКол-во редакторов: " + magazines[i].Editors.Count + "\n\n");
            }
            return res;
        }

        public void SortByName() //сортировка по названию
        {
            this.magazines.Sort();
        }

        public void SortByRelease() //сортировка по дате выхода
        {
            IComparer<Edition> comparer = new Edition();
            this.magazines.Sort(
                delegate (Magazine m1, Magazine m2)
                {
                    return comparer.Compare(m1.Edition, m2.Edition);
                }
            );
        }

        public void SortByAmount() //сортировка по тиражу
        {
            this.magazines.Sort(new EditionComparer());
        }

        public double MaxRating //макс. сред.рейтинг
        {
            get
            {
                return this.magazines.Max(m => m.MeanRating);
            }
        }
        public List<Magazine> MonthlyMagazines //элементы с частотой выхода помесячно
        {
            get
            {
                return this.magazines.Where(m => m.Freq == Frequency.Monthly).ToList();
            }
        }
        public List<Magazine> RatingGroup(double value) //элементы со сред рейтингом >= опр знач-я
        {
            return this.magazines.Where(m => m.MeanRating > value).ToList();
        }
    }
}
