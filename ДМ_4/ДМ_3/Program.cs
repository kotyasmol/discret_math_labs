using System;

namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Boolean4 work;
            while (true)
            {
                Console.Clear();
                work = new Boolean4();
                Console.WriteLine("Введите вектор: ");
                string input = Console.ReadLine();
                int[] vec = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 16; i++)
                {
                    vec[i] = int.Parse(input[i].ToString());
                }
                work.Function = vec;
                work.PrintTruthTable();
                work.SDNF();
                work.Splicing();
                work.ImplicantMatrix();
                Console.Write("\nНажмите, чтобы повторить...");
                Console.ReadKey(true);
            }

        }

    }
}