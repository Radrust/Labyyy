using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    class Magazine : Edition, IRateAndCopy, IEnumerable //класс Magazine определен как производный от класса Edition. Реализован интерфейс IRateAndCopy
    {
        private Frequency freq; //периодичность выхода журнала
        private List<Article> articles; //список статей
        protected List<Person> editors; //список редакторов

        public Magazine(string name, Frequency freq, DateTime release, int amount) : base(name, release, amount) //инициализация полей класса
        {
            this.freq = freq;
            this.articles = new List<Article>();
            editors = new List<Person>();
        }

        public Magazine() : base() //инициализация полей класса значениями по умолч
        {
            this.freq = Frequency.Weekly;
            this.articles = new List<Article>();
            editors = new List<Person>();
        }

        public Edition Edition
        {
            get { return new Edition(name, release, amount); }
            set
            {
                this.name = value.Name;
                this.release = value.Release;
                this.amount = value.Amount;
            }
        }

        public double MeanRating //вычисление среднего рейтинга
        {
            get
            {
                if (this.Articles.Count == 0) //выпусков нет - значит, и рейтинга нет
                    return 0;
                return this.Articles.Sum(a => a.rate) / this.Articles.Count; //суммируем рейтинг и делим на кол-во выпусков
            }
        }

        public bool CheckFreq(Frequency value)
        {
            return value == this.freq;
        }

        public void AddArticles(Article[] new_articles) //добавление выпусков
        {
            this.Articles.AddRange(new_articles);
        }

        public void AddEditors(Person[] new_redactors) //добавление редакторов
        {
            this.editors.AddRange(new_redactors);
        }

        public new Magazine DeepCopy() //создание полной копии объекта, не зависящего от исходника
        {
            Magazine magazine = new Magazine(this.name, this.freq, this.release, this.amount);
            magazine.AddArticles(this.articles.ToArray());
            magazine.AddEditors(this.editors.ToArray());
            return magazine;
        }
        object IRateAndCopy.DeepCopy()
        {
            Magazine magazine = new Magazine(this.name, this.freq, this.release, this.amount);
            magazine.AddArticles(this.articles.ToArray());
            magazine.AddEditors(this.editors.ToArray());
            return magazine;
        }
        double IRateAndCopy.Rating => this.MeanRating;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MagazineEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(this.editors, this.articles);
        }

        public IEnumerable<Article> GetArticlesWithRating(double rating) //вывод статей с рейтингом больше заданного знач
        {
            foreach (Article article in this.articles)
            {
                if (article.rate > rating)
                    yield return article;
            }
        }

        public IEnumerable<Article> GetArticlesWithStr(string str) //вывод статей, содержащих заданную строку в названии
        {
            foreach (Article article in this.articles)
            {
                if (article.title.Contains(str))
                    yield return article;
            }
        }

        public IEnumerable<Article> GetArticlesWithAuthorIsEditor() //статьи, где авторы явл редакторами журнала
        {
            foreach (Article article in this.articles)
                if (this.editors.Contains(article.author))
                    yield return article;
        }

        public IEnumerable<Person> GetEditorIsNotAuthors() //редакторы без статей в журнале
        {
            foreach (Person person in this.editors)
                if (this.articles.Find(a => a.author == person) == null)
                    yield return person;
        }

        public virtual string ToShortString() //поля класса без статей и редакторов, но со сред рейтингом
        {
            return ("Название: " + this.name + ", частота выхода: " + this.freq.ToString() + ", дата выхода: " + this.release.ToString() +
                    ", тираж: " + this.amount.ToString() + ", средний рейтинг: " + this.MeanRating.ToString());
        }

        public override string ToString() //строка со всеми полями класса, вклю список стетей и редакторов
        {
            string res = ("Название: " + this.name + ", частота выхода: " + this.freq.ToString() + ", дата выхода: " + this.release.ToString() +
                ", тираж: " + this.amount.ToString() + "\nВыпуски: ");
            if (this.articles.Count == 0)
                res += "- ";
            else
                foreach (Article article in this.articles)
                {
                    res += article.ToString() + '\n';
                }
            res += "\nРедакторы: ";
            if (this.editors.Count == 0)
                res += "- ";
            else
                foreach (Person redactor in this.editors)
                {
                    res += redactor.ToString() + '\n';
                }
            return res;
        }

        public Frequency Freq //доступ к полям класса
        {
            get { return this.freq; }
            set { this.freq = value; }
        }

        public List<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public List<Person> Editors
        {
            get { return this.editors; }
        }
    }

    class MagazineEnumerator : IEnumerator //определение вспомог класса MagazineEnumerator
    {
        public List<Article> people;
        private int position = -1;

        public MagazineEnumerator(List<Person> editors, List<Article> articles)
        {
            this.people = new List<Article>();
            foreach (Article article in articles) //список статей, авторы которых не редакторы журнала
            {
                if (!editors.Contains(article.author))
                    this.people.Add(article);
            }
        }

        public object Current
        {
            get
            {
                try
                {
                    return people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < people.Count);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}