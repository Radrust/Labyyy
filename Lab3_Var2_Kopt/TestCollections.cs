using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lab3_Var2_Kopt
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
    enum Frequency { Weekly, Monthly, Yearly };

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

    class TestCollections
    {
        private List<Edition> editions;
        private List<string> names;
        private Dictionary<Edition, Magazine> dict1;
        private Dictionary<string, Magazine> dict2;
        Random random;
        IEnumerable<Magazine> GetMagazines(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Magazine((i + this.random.Next(-100000, 100000)).ToString(), Frequency.Weekly, new System.DateTime(random.Next(0, 100000)), 10);
            }
        }

        public TestCollections(int count)
        {
            random = new Random(Environment.TickCount);
            editions = GetMagazines(count).Select(m => m.Edition).ToList();
            names = GetMagazines(count).Select(m => m.Name).ToList();
            while (true)
                try
                {
                    dict1 = GetMagazines(count).ToDictionary(key => key.Edition);
                    break;
                }
                catch { }
            while (true)
                try
                {
                    dict2 = GetMagazines(count).ToDictionary(key => key.Name);
                    break;
                }
                catch { }
        }

        public int[] Test(int index)
        {
            int[] list = new int[4];
            Edition tmp;
            string str;
            if (index < 0 || index >= editions.Count)
            {
                tmp = new Edition("$$$$$", new System.DateTime(0), 123123);
                str = "$$$$$";
            }
            else
            {
                str = names[index];
                tmp = editions[index];
            }

            int start = Environment.TickCount;
            editions.Find(e => e == tmp);
            list[0] = Environment.TickCount - start;

            start = Environment.TickCount;
            names.Find(s => s == str);
            list[1] = Environment.TickCount - start;

            if (!(index < 0 || index >= editions.Count))
                tmp = dict1.Keys.ToList()[index];

            start = Environment.TickCount;
            dict1.ContainsKey(tmp);
            list[2] = Environment.TickCount - start;

            if (!(index < 0 || index >= editions.Count))
                str = dict2.Keys.ToList()[index];

            start = Environment.TickCount;
            dict2.ContainsKey(str);
            list[3] = Environment.TickCount - start;

            return list;
        }
    }
}