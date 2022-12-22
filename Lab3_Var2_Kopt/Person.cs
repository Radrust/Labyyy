using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    internal class Person
    {
        private string name; //закрытое поле, содержащее имя
        private string surname; //фамилию
        private System.DateTime bday; //дату рождения

        public Person(string name, string surname, DateTime bday) //конструктор для инициализации всех полей класса
        {
            this.name = name;
            this.surname = surname;
            this.bday = bday;
        }

        public Person() //конструктор, инициализирующий все поля класса значениями по умолчанию
        {
            this.name = "имя";
            this.surname = "фамилия";
            this.bday = new System.DateTime(0);
        }

        public Person DeepCopy() //создание полной копии объекта, не зависящего от исходника
        {
            return new Person(this.name, this.surname, this.bday);
        }

        public override int GetHashCode() //переопределение виртуал метода int GetHashCode(). Определяет равенство объектов как равенство ссылок на них
        {
            return (Shifter.ShiftAndWrap(this.bday.GetHashCode(), 4) ^ Shifter.ShiftAndWrap(this.surname.GetHashCode(), 2) ^ this.name.GetHashCode());
        }

        public override bool Equals(object obj) //переопределение виртуал метода bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Person))
                return false;
            Person person = (Person)obj;
            return (this.name.Equals(person.Name) && this.surname.Equals(person.Surname) && this.bday.Equals(person.Bday));
        }

        public static bool operator ==(Person p1, Person p2) //определение операции равенства ==
        {
            return p1.Name == p2.Name && p1.Surname == p2.Surname && p1.Bday == p2.Bday;
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2); //неравенство обратно равенству
        }

        public string Name //свойство с методами get и set для доступа к полю с именем
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Surname //с фамилией
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        public DateTime Bday //с датой рождения
        {
            get { return this.bday; }
            set { this.bday = value; }
        }

        public int SetBday //свойство с get для получения информации и set для изменения поля с датой рождения
        {
            get { return (int)this.bday.Ticks; }
            set { this.bday = new DateTime(value); }
        }

        public override string ToString() //перегруженная версия виртуал метода string ToString. Выводит строку со всеми полями класса
        {
            return (this.name + " " + this.surname + ", дата рожд " + this.bday.ToString());
        }

        public virtual string ToShortString() //виртуал метод string ToShortString. Выводит строку с именем и фамилией
        {
            return (this.name + " " + this.surname);
        }
    }
}
