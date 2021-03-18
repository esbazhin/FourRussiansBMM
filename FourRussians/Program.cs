using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FourRussians
{
    class Program
    {
        static void Main(string[] args)
        {
            string file1 = "m1.txt";
            string file2 = "m2.txt";

            var a = FourRussiansMethods.ReadMatrix(file1, true);
            var b = FourRussiansMethods.ReadMatrix(file2, false);

            int n = a.Length;

            if (n != b.Length)
            {
                throw new InvalidDataException("Matrices are not of the same size!");
            }

            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);
            Console.WriteLine(n);
            Console.WriteLine(m);
            Console.WriteLine(t);

            var splitA = FourRussiansMethods.SplitMatrix(a, m, t);
            var splitB = FourRussiansMethods.SplitMatrix(b, m, t);

            var c = FourRussiansMethods.FourRussiansBMM(splitA, splitB, n, m, t);

            foreach (var l in c)
            {
                Console.WriteLine(string.Join(",", l.Select(b => b ? "1" : "0")));
            }
        }
    }
    public static class FourRussiansMethods
    {
        public static bool[][] ReadMatrix(string file, bool byLine, string[] lines = null)
        {
            if (lines is null)
            {
                lines = File.ReadAllLines(file);
            }

            int n = lines[0].Split(",").Length;

            if (n != lines.Length)
            {
                throw new InvalidDataException("Matrix in file " + file + " is not square!");
            }

            bool[][] matr = new bool[n][];
            for (int i = 0; i < n; ++i)
            {
                matr[i] = new bool[n];
            }

            for (int i = 0; i < n; ++i)
            {
                var curLine = lines[i];
                var curData = curLine.Split(",");
                if (n != curData.Length)
                {
                    throw new InvalidDataException("Matrix in file " + file + " is not square!");
                }

                for (int j = 0; j < n; ++j)
                {
                    var curChar = curData[j];
                    if (curChar != "0" && curChar != "1")
                    {
                        throw new InvalidDataException("Invalid char " + curChar + " in matrix in file " + file);
                    }

                    var curRes = curChar == "1";

                    if (byLine)
                    {
                        matr[j][i] = curRes;
                    }
                    else
                    {
                        matr[i][j] = curRes;
                    }
                }
            }

            return matr;
        }

        public static bool[][][] SplitMatrix(bool[][] matr, int m, int t)
        {
            int n = matr.Length;
            var res = new bool[t][][];

            for (int k = 0; k < t; ++k)
            {
                res[k] = new bool[m][];

                for (int i = 0; i < m; ++i)
                {
                    if (k * m + i >= n)
                    {
                        res[k][i] = new bool[n];
                    }
                    else
                    {
                        res[k][i] = matr[k * m + i];
                    }
                }
            }
            return res;
        }

        public static bool[][] FourRussiansBMM(bool[][][] a, bool[][][] b, int n, int m, int t)
        {
            //initializing result matrix
            bool[][] c = new bool[n][];
            for (int i = 0; i < n; ++i)
            {
                c[i] = new bool[n];
            }

            int pow2m = (int)Math.Pow(2, m);

            for (int i = 0; i < t; ++i)
            {
                //initialazing row sums
                bool[][] rs = new bool[pow2m][];
                rs[0] = new bool[n];

                int numbersBetweenPowersCount = 1;
                int k = 0;

                //precomputing all possible row sums
                for (int j = 1; j < pow2m; ++j)
                {
                    int pow2k = (int)Math.Pow(2, k);
                    rs[j] = AddRows(rs[j - pow2k], b[i][k]);

                    //shifting k for j to be between current powers
                    if (numbersBetweenPowersCount == 1)
                    {
                        numbersBetweenPowersCount = j + 1;
                        k++;
                    }
                    else
                    {
                        numbersBetweenPowersCount--;
                    }
                }

                //computing ci as row sums of needed rows from ai
                var ci = new bool[n][];
                for (int j = 0; j < n; ++j)
                {
                    var num = GetRowNum(a[i], j);
                    ci[j] = rs[num];
                }

                //adding ci to result
                c = AddMatrices(c, ci);
            }

            return c;
        }

        public static bool[] AddRows(bool[] row1, bool[] row2)
        {
            int n = row1.Length;

            if (n != row2.Length)
            {
                throw new InvalidDataException("Rows are not of the same size!");
            }

            var res = new bool[n];

            for (int i = 0; i < n; ++i)
            {
                res[i] = row1[i] || row2[i];
            }

            return res;
        }

        public static int GetRowNum(bool[][] matr, int row)
        {
            int n = matr.Length;
            int res = 0;

            for (int i = 0; i < n; ++i)
            {
                if (matr[i][row])
                {
                    int curData = (int)Math.Pow(2, i);

                    res += curData;
                }
            }
            return res;
        }

        public static bool[][] AddMatrices(bool[][] matr1, bool[][] matr2)
        {
            int n = matr1.Length;

            if (n != matr2.Length)
            {
                throw new InvalidDataException("Matrices are not of the same size!");
            }

            bool[][] res = new bool[n][];
            for (int i = 0; i < n; ++i)
            {
                res[i] = AddRows(matr1[i], matr2[i]);
            }

            return res;
        }      
    }
}
