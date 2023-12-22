using System;

namespace FunctionClasses
{

    internal class FunctionClasses
    {
        private int[] function;
        private int values;
        private int vars;
        private int[,] truthTable;
        public int[] Function
        {
            get => function;
            set
            {
                function = value;
                if (function.Length == 1)
                {
                    int[] temp = new int[2];
                    temp[0] = function[0];
                    temp[1] = function[0];
                    function = temp;
                }
                values = function.Length;
                vars = (int)Math.Log(values, 2);
                truthTable = new int[values, vars];
                TruthTable();
            }
        }
        private void TruthTable()
        {
            for (int i = 0; i < values; ++i)
            {
                string res = Convert.ToString(i, 2);
                for (int j = 0; j < (vars - res.Length); ++j)
                {
                    truthTable[i, j] = 0;
                }
                int counter = 0;
                for (int j = (vars - res.Length); j < vars; j++)
                {
                    truthTable[i, j] = int.Parse(res[counter].ToString());
                    ++counter;
                }
            }
        }
        public void PrintTruthTable()
        {
            Console.WriteLine("\n-> Таблица истинности");
            if (vars == 2)
            {
                Console.WriteLine(" x | y | f");
                Console.WriteLine("----------");
            }
            else if (vars == 3)
            {
                Console.WriteLine(" x | y | z | f");
                Console.WriteLine("--------------");
            }
            else
            {
                Console.WriteLine(" x | y | z | w | f");
                Console.WriteLine("------------------");
            }
            string result = "";
            for (int i = 0; i < values; ++i)
            {
                result = " ";
                for (int j = 0; j < vars; j++)
                {
                    result += truthTable[i, j] + "   ";
                }
                result = result.Substring(0, result.Length - 2);
                result += "| " + Function[i];
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }
        public bool isPreserving0()
        {
            if (Function[0] == 0)
                return true;
            return false;
        }
        public bool isPreserving1()
        {
            if (Function[values - 1] == 1)
                return true;
            return false;
        }

        public bool isMonotonous()
        {
            int previous;
            int current;
            string ones;
            if (vars == 1)
                return Function[0] <= Function[1];
            for (int var = 2; var <= 4; var++)
            {
                previous = 0;
                ones = "";
                for (int i = 0; i < values; ++i)
                {
                    ones += Function[i];
                    if ((i + 1) % (values / var) == 0)
                    {
                        current = Convert.ToInt32(ones, 2);
                        if (previous > current)
                            return false;
                        previous = current;
                        ones = "";
                    }
                }
            }

            return true;
        }
        public bool isSelfDual()
        {
            for (int i = 0; i < values / 2; ++i)
            {
                if (Function[values - i - 1] == Function[i])
                    return false;
            }
            return true;
        }

        private int CountOnes(int i)
        {
            int count = 0;
            for (int j = 0; j < vars; j++)
            {
                if (truthTable[i, j] == 1)
                    count++;
            }
            return count;
        }
        public bool isLinear()
        {
            int A, B;
            int[,] matrix = new int[values, values];
            for (int i = 0; i < values; ++i)
            {
                matrix[0, i] = Function[i];
            }
            int count;
            for (int i = 1; i < values; ++i)
            {
                count = 0;
                for (int j = 0; count < values - 1; ++j)
                {
                    A = matrix[i - 1, count];
                    count++;
                    B = matrix[i - 1, count];
                    matrix[i, j] = XOR(A, B);
                }
            }
            for (int i = 0; i < values; ++i)
            {
                if (matrix[i, 0] == 1)
                {
                    if (CountOnes(i) > 1)
                        return false;
                }
            }
            return true;
        }
        private int XOR(int a, int b)
        {
            if (a == b)
                return 0;
            return 1;
        }
    }
}