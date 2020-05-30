using System;
using System.Collections.Generic;
using System.Text;

namespace lab11
{
    class TestCollections
    {
        public Stack<Engine> stackEng = new Stack<Engine>();
        public Stack<string> stackStr = new Stack<string>();
        public Dictionary<Engine, Diesel> dictEng = new Dictionary<Engine, Diesel>();
        public Dictionary<string, Diesel> dictStr = new Dictionary<string, Diesel>();

        public int Size { get; set; }

        public TestCollections(int size)
        {
            Size = size;
            for (int i = 0; i < size; i++)
            {
                Diesel dies = new Diesel();
                dies = dies.MakeRandom();
                stackEng.Push(dies.BaseEngine());              
                stackStr.Push(dies.ToString());
                dictEng.Add(dies.BaseEngine(), dies);
                dictStr.Add(dies.ToString(), dies);
            }
        }

        public void WriteStack()
        {
            Console.WriteLine("stack");
            foreach (Engine elem in stackEng)
            {
                Console.WriteLine(elem.ToString());
            }
        }

        public void WriteDict()
        {
            Console.WriteLine("dictionary");
            foreach (KeyValuePair<Engine, Diesel> keyValue in dictEng)
            {
                Diesel eng = keyValue.Value as Diesel;
                if (eng != null) Console.WriteLine(eng.ToString());
                
            }
        }
        static int InputNum(int maxNum)
        {
            int num;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out num);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (num < 0 || num > maxNum) Console.WriteLine($"Некорректный ввод. Введите число не больше {maxNum}");
            } while (!ok || num < 0 || num > maxNum);

            return num;

        }
        public void Add()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);
            Console.WriteLine("Введите количество тактов: 4 или 2");
            int stroke = InputNum(4);

            Diesel dies =new Diesel(name, num, stroke);
            stackEng.Push(dies.BaseEngine());
            stackStr.Push(dies.ToString());
            dictEng.Add(dies.BaseEngine(), dies);
            dictStr.Add(dies.ToString(), dies);
            Console.WriteLine("Добавлено");
            Size++;
        }    
        public void Delete()
        {
            Engine eng = stackEng.Pop();
            stackStr.Pop();
            if (dictEng.ContainsKey(eng))
            {
                dictEng.Remove(eng);
            }

            Console.WriteLine("Элемент удален");
            Size--;
        }
     
    }
}
