using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2Var2_Kopt
{
    class Edition
    {
        protected string name; //название издания
        protected System.DateTime release; //дата выхода
        protected int amount; //тираж

        public Edition(string name, System.DateTime release, int amount) //конструктор с инициализацией полей класса
        {
            this.name = name;
            this.release = release;
            this.amount = amount;
        }

        public Edition() //конструктор с инициализацией значениями по умолч
        {
            this.name = "издание";
            this.release = new System.DateTime(0);
            this.amount = 0;
        }

        public virtual Edition DeepCopy() //создание полной копии объекта, не зависящего от исходника
        {
            return new Edition(this.name, this.release, this.amount);
        }

        public override string ToString()
        {
            return ("Название, тираж, дата выхода: " + this.name + ", " + this.amount.ToString() + ", " + this.release.ToString());
        }

        public override int GetHashCode() //виртуал метод int GetHashCode(). Определяет равенство объектов как равенство ссылок на них
        {
            return (Shifter.ShiftAndWrap(this.release.GetHashCode(), 4) ^ Shifter.ShiftAndWrap(this.amount.GetHashCode(), 2) ^ this.name.GetHashCode());
        }

        public override bool Equals(object obj) //переопределение виртуал метода bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Edition))
                return false;
            Edition edition = (Edition)obj;
            return (this.name.Equals(edition.name) && this.amount.Equals(edition.amount) && this.release.Equals(edition.release));
        }

        public static bool operator == (Edition edition1, Edition edition2) //Операция равенства. Объекты равны, когда все поля соответственно равны. && - лог. И
        {
            return (edition1.Release == edition2.Release && edition1.Amount == edition2.Amount && edition1.Name == edition2.Name);
        }

        public static bool operator !=(Edition edition1, Edition edition2) //Операция неравенства
        {
            return !(edition1 == edition2);
        }

        public string Name //свойства для доступа к полям типа
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public System.DateTime Release
        {
            get { return this.release; }
            set { this.release = value; }
        }

        public int Amount //доступ к тиражу
        {
            get { return this.amount; }
            set //бросаем исключение при отриц значении
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("тираж не меньше 0"); //сообщение о допустимых значениях
                this.amount = value;
            }
        }
    }
}