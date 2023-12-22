using System;

namespace FunctionClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Menu();
                Console.ReadKey(true);
            }
        }

        static void Menu()
        {
            int choice = InputInt("Введите количество функций в системе: ");
            CheckBoundaries(ref choice, 1, 3);
            switch (choice)
            {
                case 1:
                    {
                        FunctionClasses func1 = GetFunction();
                        PrintTable();
                        PrintClasses(func1);
                        OneFunctionPlenitude(func1);
                        break;
                    }
                case 2:
                    {
                        FunctionClasses func1 = GetFunction();
                        FunctionClasses func2 = GetFunction();
                        PrintTable();
                        PrintClasses(func1);
                        PrintClasses(func2);
                        TwoFunctionPlenitude(func1, func2);
                        break;
                    }
                case 3:
                    {
                        FunctionClasses func1 = GetFunction();
                        FunctionClasses func2 = GetFunction();
                        FunctionClasses func3 = GetFunction();
                        PrintTable();
                        PrintClasses(func1);
                        PrintClasses(func2);
                        PrintClasses(func3);
                        ThreeFunctionPlenitude(func1, func2, func3);
                        break;
                    }
            }
        }
        static void ThreeFunctionPlenitude(FunctionClasses func1, FunctionClasses func2, FunctionClasses func3)
        {
            bool res = (!func1.isPreserving0() || !func2.isPreserving0() || !func3.isPreserving0())
                && (!func1.isPreserving1() || !func2.isPreserving1() || !func3.isPreserving1())
                && (!func1.isLinear() || !func2.isLinear() || !func3.isLinear())
                && (!func1.isSelfDual() || !func2.isSelfDual() || !func3.isSelfDual())
                && (!func1.isMonotonous() || !func2.isMonotonous() || !func3.isMonotonous())
                ;

            if (res)
                Console.WriteLine("Функциональная полнота есть");
            else
                Console.WriteLine("Функциональной полноты нет");
        }
        static void TwoFunctionPlenitude(FunctionClasses func1, FunctionClasses func2)
        {
            bool res = (!func1.isPreserving0() || !func2.isPreserving0())
                && (!func1.isPreserving1() || !func2.isPreserving1())
                && (!func1.isLinear() || !func2.isLinear())
                && (!func1.isSelfDual() || !func2.isSelfDual())
                && (!func1.isMonotonous() || !func2.isMonotonous())
                ;

            if (res)
                Console.WriteLine("Функциональная полнота есть");
            else
                Console.WriteLine("Функциональной полноты нет");
        }
        static void OneFunctionPlenitude(FunctionClasses func)
        {
            bool res = !func.isPreserving0() && !func.isPreserving1()
                && !func.isLinear() && !func.isSelfDual()
                && !func.isMonotonous();
            if (res)
                Console.WriteLine("Функциональная полнота есть");
            else
                Console.WriteLine("Функциональной полноты нет");
        }

        static void PrintTable()
        {
            Console.Write("{0, -5}|", "  T1");
            Console.Write("{0, -5}|", "  T0");
            Console.Write("{0, -5}|", "  T*");
            Console.Write("{0, -5}|", "  Tl");
            Console.Write("{0, -5}|", "  Tm");
            Console.Write("\n------------------------------\n");

        }
        static void PrintClasses(FunctionClasses func)
        {
            if (!func.isPreserving1())
                Console.Write("{0, -5}|", "  -");
            else
                Console.Write("{0, -5}|", "  +");
            if (!func.isPreserving0())
                Console.Write("{0, -5}|", "  -");
            else
                Console.Write("{0, -5}|", "  +");
            if (!func.isSelfDual())
                Console.Write("{0, -5}|", "  -");
            else
                Console.Write("{0, -5}|", "  +");
            if (!func.isLinear())
                Console.Write("{0, -5}|", "  -");
            else
                Console.Write("{0, -5}|", "  +");
            if (!func.isMonotonous())
                Console.Write("{0, -5}|", "  -");
            else
                Console.Write("{0, -5}|", "  +");
            Console.Write("\n------------------------------\n");
        }


        static FunctionClasses GetFunction()
        {
            Console.Write("Введите вектор: ");
            string input = Console.ReadLine();
            FunctionClasses func = new FunctionClasses();
            func.Function = ReadVector(input);
            return func;
        }
        static public int InputInt(string msg = "")
        {
            bool isNumber;
            string inputLine;
            int res;
            Console.Write(msg);
            do
            {
                inputLine = Console.ReadLine();
                isNumber = int.TryParse(inputLine, out res);
                if (!isNumber)
                    Console.Write("Ошибка! Введите число\n" + msg);

            } while (!isNumber);
            return res;
        }
        static int[] ReadVector(string input)
        {
            int[] vec = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                vec[i] = int.Parse(input[i].ToString());
            }
            return vec;
        }

        static public void CheckBoundaries(ref int value, int left = 0, int right = int.MaxValue)
        {
            if (left > right)
                (left, right) = (right, left);
            while ((value < left) || (value > right))
                value = InputInt($"Число не входит в границы[{left}, {right}]. Попробуйте снова: ");
        }
    }
}