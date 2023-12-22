using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab3
{
    internal class Boolean4
    {
        private int[] function;
        private int[,] truthTable = new int[16, 4];
        private List<string> minterms = new List<string>();
        private List<string> sdnf = new List<string>();
        public int[] Function
        {
            get => function;
            set
            {
                function = value;
                TruthTable();
                makeSDNF();
            }
        }
        private string Variable(int j)
        {
            if (j == 0)
                return "x";
            if (j == 1)
                return "y";
            if (j == 2)
                return "z";
            else
                return "w";
        }
        private void TruthTable()
        {
            for (int i = 0; i < 16; ++i)
            {
                string res = Convert.ToString(i, 2);
                for (int j = 0; j < (4 - res.Length); ++j)
                {
                    truthTable[i, j] = 0;
                }
                int counter = 0;
                for (int j = (4 - res.Length); j < 4; j++)
                {
                    truthTable[i, j] = int.Parse(res[counter].ToString());
                    ++counter;
                }
            }
        }
        public void PrintTruthTable()
        {
            Console.WriteLine("\n-> Таблица истинности");
            Console.WriteLine(" x | y | z | w | f");
            Console.WriteLine("------------------");
            for (int i = 0; i < 16; ++i)
            {
                string result = " " + truthTable[i, 0] + "   "
                    + truthTable[i, 1] + "   " + truthTable[i, 2]
                    + "   " + truthTable[i, 3] + " | "
                    + Function[i];
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }
        private void makeSDNF()
        {
            for (int i = 0; i < 16; ++i)
            {
                if (Function[i] == 1)
                {
                    string temp = "";
                    for (int j = 0; j < 4; j++)
                    {
                        string variable = Variable(j);
                        if (truthTable[i, j] == 0)
                        {
                            temp = String.Concat(temp, "-" + variable);
                        }
                        else
                        {
                            temp = String.Concat(temp, variable);
                        }
                    }
                    minterms.Add(temp);
                    sdnf.Add(temp);
                }
            }
        }
        public void SDNF()
        {
            if (sdnf.Count == 0)
            {
                Console.WriteLine("Выражение является противоречием: построение СДНФ, склейки и импликантной матрицы невозможно");
                return;
            }
            Console.WriteLine("-> СДНФ");
            for (int i = 0; i < sdnf.Count - 1; ++i)
            {
                Console.Write(sdnf[i] + " + ");
            }
            Console.Write(sdnf[sdnf.Count - 1] + "\n");
        }
        private MatchCollection GetVars(string st)
        {
            Regex regex = new Regex("-*[xywz]");
            return regex.Matches(st);
        }
        private bool Splice(string first, string second, out string minterm)
        {
            int vars = 0;
            minterm = "";
            MatchCollection firstMatches = GetVars(first);
            MatchCollection secondMatches = GetVars(second);
            if (firstMatches.Count != secondMatches.Count)
                return false;
            for (int i = 0; i < firstMatches.Count; ++i)
            {
                first = firstMatches[i].Value;
                second = secondMatches[i].Value;
                if (first.IndexOf(second[second.Length - 1]) != -1)
                {
                    if (first == second)
                    {
                        vars++;
                        minterm += firstMatches[i].Value;
                    }
                }
                else
                {
                    return false;
                }

            }
            return ((vars == firstMatches.Count - 1) && (firstMatches.Count != 1)) ? true : false;
        }
        public void Splicing()
        {
            if (sdnf.Count == 0)
                return;
            List<string> res;
            bool[] used;
            bool needContinue;
            string result = "";
            string minterm = "";
            do
            {
                result = "";
                Console.Write("\n-> Склейка");
                res = new List<string>();
                used = new bool[minterms.Count];
                needContinue = false;
                for (int i = 0; i < minterms.Count; ++i)
                {
                    for (int j = i + 1; j < minterms.Count; ++j)
                    {
                        string first = minterms[i];
                        string second = minterms[j];
                        if (Splice(first, second, out minterm))
                        {
                            used[i] = true;
                            used[j] = true;
                            needContinue = true;
                            res.Add(minterm);
                            Console.Write($"\n{i + 1}-{j + 1}: {minterm}");
                        }

                    }

                }
                for (int i = 0; i < used.Length; ++i)
                {
                    if (!used[i])
                    {
                        res.Add(minterms[i]);
                    }
                }
                minterms = res;
                string[] temp = minterms.ToArray();
                var tempRes = temp.Distinct();
                minterms = tempRes.ToList();

                if (!needContinue)
                {
                    Console.Write(" завершена");
                }

                Console.WriteLine();
                for (int i = 0; i < minterms.Count; ++i)
                {
                    result = result + minterms[i] + " + ";
                }
                result = result.Substring(0, result.Length - 2);
                Console.WriteLine($"Результат: {result}");

            } while (needContinue);
        }
        public void ImplicantMatrix()
        {
            if (minterms.Count == 0)
                return;
            bool isInMinterm;
            Console.Write("\n");
            string implicate = "-> Импликантная матрица";
            int tab = ((9 * (sdnf.Count + 1)) - implicate.Length) / 2;
            for (int i = 0; i < tab; ++i)
            {
                implicate = String.Concat(" ", implicate);
            }
            Console.WriteLine(implicate);
            Console.Write("{0, -8}|", "");
            for (int i = 0; i < sdnf.Count; ++i)
            {
                Console.Write("{0, -8}|", sdnf[i]);
            }
            Console.Write("\n");
            for (int j = 0; j < minterms.Count; ++j)
            {
                Console.Write("{0, -8}|", minterms[j]);
                for (int i = 0; i < sdnf.Count; ++i)
                {
                    isInMinterm = true;
                    MatchCollection vars = GetVars(minterms[j]);
                    foreach (Match m in vars)
                    {
                        int index = sdnf[i].IndexOf(m.Value);
                        if (index == -1)
                        {
                            isInMinterm = false;
                        }
                        else if (index != 0)
                        {
                            if (sdnf[i][index - 1] == '-')
                            {
                                isInMinterm = false;
                            }
                        }
                    }
                    if (isInMinterm)
                    {
                        Console.Write("{0, -8}|", " +");
                    }
                    else
                    {
                        Console.Write("{0, -8}|", "");
                    }
                }
                Console.Write("\n");
            }
        }

    }
}