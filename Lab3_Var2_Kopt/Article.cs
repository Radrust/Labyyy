using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    class Article : IRateAndCopy //Реализован интерфейс IRateAndCopy
    {
        public Person author;
        public string title;
        public double rate;

        public Article()
        {
            this.author = new Person();
            this.rate = 0;
            this.title = "название";
        }

        public Article(Person author, string title, double rate)
        {
            this.author = author;
            this.title = title;
            this.rate = rate;
        }

        public override string ToString()
        {
            return (this.title + ", рейтинг " + this.rate.ToString() + ", автор " + this.author.ToString());
        }

        double IRateAndCopy.Rating => this.rate;

        object IRateAndCopy.DeepCopy() //определение виртуал метода DeepCopy()
        {
            return new Article(this.author, this.title, this.rate);
        }
    }
}