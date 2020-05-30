using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace lab11
{
    class Program
    {
        static Engine eng1 = new Engine();
        static InternalCEngine iEng1 = new InternalCEngine();
        static Diesel dies1 = new Diesel();
        static TurbojetEngine turb1 = new TurbojetEngine();
        static int InputMenu(int maxNum)
        {
            int menu;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out menu);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (menu < 0 || menu > maxNum) Console.WriteLine($"Некорректный ввод.Выберите вариант от 0 до {maxNum} из меню");
            } while (!ok || menu < 0 || menu > maxNum);

            return menu;
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
        static Engine CreateEng()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);

            return new Engine(name, num);
        }
        static InternalCEngine CreateIEng()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);

            return new InternalCEngine(name, num);
        }
        static Diesel CreateDies()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);
            Console.WriteLine("Введите количество тактов 4 или 2");
            int stroke = InputNum(4);

            return new Diesel(name, num, stroke);
        }
        static TurbojetEngine CreateTurb()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);
            Console.WriteLine("Введите 1 если есть форсажная камера и 0 если её нет");
            bool ok = InputNum(1) == 1;

            return new TurbojetEngine(name, num, ok);
        }
        static Queue SortCollect(Queue collection)
        {
            Queue someCollect = new Queue(); 
            object[] mas = new object[collection.Count];
            int i = 0;
            foreach (object elem in collection)
            {
                mas[i] = elem;
                i++;
            }
            Array.Sort(mas, new SortByPower());
            for (int j = 0; j < mas.Length; j++)
                someCollect.Enqueue(mas[j]);
            return someCollect;
        }
        static void WriteCollect(Queue collection)
        {
            foreach (object elem in collection)
            {
                Engine someEng = elem as Engine;
                if (someEng != null) someEng.Show();
            }
        }
        static void WriteCollect(Dictionary<int, Engine> collection)
        {        
            foreach(KeyValuePair<int,Engine> elem in collection)
            {
                Engine someEng = elem.Value as Engine;
                if (someEng != null) someEng.Show();
            }
        }

        static void AddElement(Queue collection)
        {
            int menu = 10;
            bool random;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0. Добавить Engine");
            Console.WriteLine("1. Добавить InternalCEngine");
            Console.WriteLine("2. Добавить Diesel");
            Console.WriteLine("3. Добавить TurbojetEngine");
            Console.ResetColor();
            menu = InputMenu(3);
            Console.WriteLine("Введите 1, если надо применить рандомный элемент, и 0, если нет");
            random = InputNum(1) == 1;
            switch (menu)
            {
                case 0:
                    if (random) collection.Enqueue(eng1.MakeRandom());
                    else collection.Enqueue(CreateEng());
                    break;
                case 1:
                    if (random) collection.Enqueue(iEng1.MakeRandom());
                    else collection.Enqueue(CreateIEng());
                    break;
                case 2:
                    if (random) collection.Enqueue(dies1.MakeRandom());
                    else collection.Enqueue(CreateDies());
                    break;
                case 3:
                    if (random) collection.Enqueue(turb1.MakeRandom());
                    else collection.Enqueue(CreateTurb());
                    break;
            }
        }
        static void AddElement(Dictionary<int, Engine> collection)
        {
            int menu = 10;
            bool random;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0. Добавить Engine");
            Console.WriteLine("1. Добавить InternalCEngine");
            Console.WriteLine("2. Добавить Diesel");
            Console.WriteLine("3. Добавить TurbojetEngine");
            Console.ResetColor();
            menu = InputMenu(3);
            Console.WriteLine("Введите 1, если надо применить рандомный элемент, и 0, если нет");
            random = InputNum(1) == 1;
            switch (menu)
            {
                case 0:
                    if (random) collection.Add(collection.Keys.Max() + 1,eng1.MakeRandom());
                    else collection.Add(collection.Keys.Max() + 1, CreateEng());
                    break;
                case 1:
                    if (random) collection.Add(collection.Keys.Max() + 1, iEng1.MakeRandom());
                    else collection.Add(collection.Keys.Max() + 1, CreateIEng());
                    break;
                case 2:
                    if (random) collection.Add(collection.Keys.Max() + 1, dies1.MakeRandom());
                    else collection.Add(collection.Keys.Max() + 1, CreateDies());
                    break;
                case 3:
                    if (random) collection.Add(collection.Keys.Max() + 1, turb1.MakeRandom());
                    else collection.Add(collection.Keys.Max() + 1, CreateTurb());
                    break;
            }
        }
        static void DeleteElement(Dictionary<int, Engine> collection)
        {
            Console.WriteLine("Введите ключ");
            int key = InputNum(collection.Keys.Max());
            collection.Remove(key);
        }
        static int AmountOfDies(Queue collection)
        {
            int count=0;
            foreach (object elem in collection)
            {
                Diesel someEng = elem as Diesel;
                if (someEng != null) count++;
            }
          
            return count;
        }
        static int AmountOfDies(Dictionary<int, Engine> collection)
        {
            int count = 0;
           
            foreach (KeyValuePair<int, Engine> elem in collection)
            {
                Diesel someEng = elem.Value as Diesel;
                if (someEng != null) count++;
            }

            return count;
        }
        static void WriteTurb(Queue collection)
        {
            foreach (object elem in collection)
            {
                TurbojetEngine someEng = elem as TurbojetEngine;
                if (someEng != null) someEng.Show();
            }
        }
        static void WriteTurb(Dictionary<int, Engine> collection)
        {
            foreach (KeyValuePair<int, Engine> elem in collection)
            {
                TurbojetEngine someEng = elem.Value as TurbojetEngine;
                if (someEng != null) someEng.Show();
            }
        }
        static int AmountOf2Stroke(Queue collection)
        {
            int count = 0;
          
            foreach (object elem in collection)
            {
                Diesel someEng = elem as Diesel;
                if (someEng != null&& someEng.NumOfStroke==2) count++;
            }
            return count;
        }
        static int AmountOf2Stroke(Dictionary<int, Engine> collection)
        {
            int count = 0;
            foreach (KeyValuePair<int, Engine> elem in collection)
            {
                Diesel someEng = elem.Value as Diesel;
                if (someEng != null && someEng.NumOfStroke == 2) count++;
            }
            
            return count;
        }
    
        static void FindElement(Queue collection)
        {
            Console.WriteLine("Введите мощность");
            int num = InputNum(9999);
            int place = 0;
            foreach (object elem in collection)
            {
                place++;
                Engine someEng = elem as Engine;
                if (someEng != null && someEng.Power==num)
                {
                    Console.WriteLine($"Элемент найден на {place} месте");
                    return;
                }
            }
            Console.WriteLine($"Элемент не найден");


        }
        static void FindMatch(Dictionary<int, Engine> collection, Engine eng)
        {
            if(collection.Values.Contains(eng)) Console.WriteLine($"Элемент найден");        
            else Console.WriteLine($"Элемент не найден");
        }
        static void FindMatch(Dictionary<int, Engine> collection, InternalCEngine eng)
        {
            if (collection.Values.Contains(eng)) Console.WriteLine($"Элемент найден");
            else Console.WriteLine($"Элемент не найден");
        }
        static void FindMatch(Dictionary<int, Engine> collection, Diesel eng)
        {
            if (collection.Values.Contains(eng)) Console.WriteLine($"Элемент найден");
            else Console.WriteLine($"Элемент не найден");
        }
        static void FindMatch(Dictionary<int, Engine> collection, TurbojetEngine eng)
        {
            if (collection.Values.Contains(eng)) Console.WriteLine($"Элемент найден");
            else Console.WriteLine($"Элемент не найден");
        }
        static void FindElement(Dictionary<int, Engine> collection)
        {
            int menu = 10;
            bool random;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0. Найти Engine");
            Console.WriteLine("1. Найти InternalCEngine");
            Console.WriteLine("2. Найти Diesel");
            Console.WriteLine("3. Найти TurbojetEngine");
            Console.ResetColor();
            menu = InputMenu(3);
            switch (menu)
            {
                case 0:

                    FindMatch(collection, CreateEng());
                    break;
                case 1:

                    FindMatch(collection, CreateIEng());
                    break;
                case 2:

                    FindMatch(collection, CreateDies());
                    break;
                case 3:

                    FindMatch(collection, CreateTurb());
                    break;
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int menu = 10;


            while (menu != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Часть 1: Oueue");
                Console.WriteLine("2. Часть 2: Dictionary<K,T>");
                Console.WriteLine("3. Часть 3: Stack<T>;Dictionary<K,T>");
                Console.WriteLine("0.Выход");
                Console.ResetColor();
                menu = InputMenu(3);

                Queue collection1 = new Queue();
                collection1.Enqueue(eng1.MakeRandom());
                collection1.Enqueue(iEng1.MakeRandom());
                collection1.Enqueue(dies1.MakeRandom());
                collection1.Enqueue(turb1.MakeRandom());

                Dictionary<int, Engine> collection2 = new Dictionary<int, Engine>();
                collection2.Add(1, eng1.MakeRandom());
                collection2.Add(2, iEng1.MakeRandom());
                collection2.Add(3, dies1.MakeRandom());
                collection2.Add(4, turb1.MakeRandom());

                if (menu == 0) break;
                else
                {
                    int menuNext = 10;
                
                    switch (menu)
                    {
                        #region task1
                        case 1:

                            while (menuNext != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("1.Показать коллекцию");
                                Console.WriteLine("2.Добавить элемент");
                                Console.WriteLine("3.Удалить элемент");
                                Console.WriteLine("4.Количество элементов типа Diesel");
                                Console.WriteLine("5.Показать все элементы типа TurbojetEngine");
                                Console.WriteLine("6.Количество двухтактовых двигателей(Diesel)");
                                Console.WriteLine("7.Найти элемент");
                                Console.WriteLine("8.Выполнить клонирование");
                                Console.WriteLine("0.Назад");
                                Console.ResetColor();
                                menuNext = InputMenu(8);
                                if (menuNext == 0) break;
                                else
                                {
                                    switch (menuNext)
                                    {
                                        case 1:
                                            WriteCollect(collection1);
                                            break;
                                        case 2:
                                            AddElement(collection1);
                                            Console.WriteLine("Элемент добавлен");
                                            break;
                                        case 3:
                                            collection1.Dequeue();
                                            Console.WriteLine("Элемент удален");
                                            break;
                                        case 4:
                                            Console.WriteLine($"Количество:{ AmountOfDies(collection1)}");
                                            break;
                                        case 5:
                                            WriteTurb(collection1);
                                            break;
                                        case 6:
                                            Console.WriteLine($"Количество:{AmountOf2Stroke(collection1)}");
                                            break;
                                        case 7:
                                            collection1 =SortCollect(collection1);
                                            WriteCollect(collection1);
                                            FindElement(collection1);
                                            break;
                                        case 8:
                                            Queue someQueue = new Queue();
                                            foreach (Engine elem in collection1)
                                            {
                                                someQueue.Enqueue(elem);
                                            }
                                           
                                            Console.WriteLine("Клон коллекции:");
                                            WriteCollect(someQueue);

                                            break;

                                    }
                                }

                            }
                            break;
                        #endregion task1

                        #region task2
                        case 2:
                            while (menuNext != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("1.Показать коллекцию");
                                Console.WriteLine("2.Добавить элемент");
                                Console.WriteLine("3.Удалить элемент");
                                Console.WriteLine("4.Количество элементов типа Diesel");
                                Console.WriteLine("5.Показать все элементы типа TurbojetEngine");
                                Console.WriteLine("6.Количество двухтактовых двигателей(Diesel)");
                                Console.WriteLine("7.Найти элемент");
                                Console.WriteLine("8.Выполнить клонирование");
                                Console.WriteLine("0.Назад");
                          
                                Console.ResetColor();
                                menuNext = InputMenu(8);
                                if (menuNext == 0) break;
                                else
                                {
                                    switch (menuNext)
                                    {
                                        case 1:
                                            WriteCollect(collection2);
                                            break;
                                        case 2:
                                            AddElement(collection2);
                                            Console.WriteLine("Элемент добавлен");
                                            break;
                                        case 3:
                                            DeleteElement(collection2);
                                            Console.WriteLine("Элемент удален");
                                            break;
                                        case 4:
                                            Console.WriteLine($"Количество:{ AmountOfDies(collection2)}");
                                            break;
                                        case 5:
                                            WriteTurb(collection2);
                                            break;
                                        case 6:
                                            Console.WriteLine($"Количество:{AmountOf2Stroke(collection2)}");
                                            break;
                                        case 7:
                                            
                                            FindElement(collection2);
                                            break;
                                        case 8:
                                            Dictionary<int, Engine> someDict = new Dictionary<int, Engine>();
                                            foreach (int keyValue in collection2.Keys)
                                            {
                                                someDict.Add(keyValue, collection2[keyValue]);
                                            }
                                         
                                            Console.WriteLine("Клон коллекции:");
                                            WriteCollect(someDict);
                                            break;
                                    }
                                }
                            }
                            break;
                        #endregion task2

                        #region task3
                        case 3:
                            int size = 7;
                            TestCollections test = new TestCollections(size);

                            while (menuNext != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("1.Показать коллекции");
                                Console.WriteLine("2.Добавить элемент");
                                Console.WriteLine("3.Удалить элемент");
                                Console.WriteLine("4.Тесты");                            
                                Console.WriteLine("0.Назад");
                                Console.ResetColor();
                               
                                menuNext = InputMenu(4);                            

                                if (menuNext == 0) break;
                                else
                                {
                                    switch (menuNext)
                                    {
                                        case 1:
                                            Console.WriteLine("коллекция:");
                                            test.WriteDict();                                             
                                            break;
                                        case 2:
                                            test.Add();
                                            break;
                                        case 3:
                                            test.Delete();
                                            break;
                                        case 4:
                                            Test(test);
                                            break;                                   
                                    }
                                }
                            }                                                  
                            break;
                            #endregion task3

                    }
                }
            }
        }

        static void Test(TestCollections test)
        {                    

            Diesel first = null;
            Diesel middle = null;
            Diesel last = null;
            int num = 0;

            foreach (KeyValuePair<Engine, Diesel> elem in test.dictEng)
            {
                if (num == 0) first = (Diesel)elem.Value.Clone();
                if (num == (int)test.Size / 2) middle = (Diesel)elem.Value.Clone();
                if (num == test.Size - 1) last = (Diesel)elem.Value.Clone();
                num++;
            }
           

            Console.WriteLine("Поиск первого:");
            Search(test, first);
            Console.WriteLine();

            Console.WriteLine("Поиск среднего:");
            Search(test, middle);
            Console.WriteLine();

            Console.WriteLine("Поиск последнего:");
            Search(test, last);
            Console.WriteLine();


        }
        static void Search(TestCollections test,Diesel element)
        {
            Stopwatch SW = new Stopwatch();
        

            SW.Restart();
            test.stackEng.Contains(element.BaseEngine());
            SW.Stop();
            long timeStackEng = SW.Elapsed.Ticks;

            SW.Restart();
            test.stackStr.Contains(element.ToString());
            SW.Stop();
            long timeStackStr = SW.Elapsed.Ticks;

            SW.Restart();
            test.dictEng.ContainsKey(element.BaseEngine());
            SW.Stop();
            long timeDictEng = SW.Elapsed.Ticks;

            SW.Restart();
            test.dictStr.ContainsKey(element.ToString());
            SW.Stop();
            long timeDictStr = SW.Elapsed.Ticks;

            Console.WriteLine($"Stack:{timeStackEng} (в тиках)");
            Console.WriteLine($"Stack<str>:{timeStackStr} (в тиках)");
            Console.WriteLine($"Dictionary:{timeDictEng} (в тиках)");
            Console.WriteLine($"Dictionary<str>:{timeDictStr} (в тиках)");
        }
    }
}
