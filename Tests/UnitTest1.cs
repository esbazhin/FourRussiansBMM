using System;
using Xunit;
using FourRussians;
using System.Collections.Generic;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadMatrixTest()
        {
            string[] lines = new string[]
            {
                "1,1,0,1",
                "1,0,1,1",
                "1,0,0,1",
                "0,1,0,1"
            };

            bool[][] a = new bool[4][];
            a[0] = new bool[4] { true, true, false, true };
            a[1] = new bool[4] { true, false, true, true };
            a[2] = new bool[4] { true, false, false, true };
            a[3] = new bool[4] { false, true, false, true };

            bool[][] b = new bool[4][];
            b[0] = new bool[4] { true, true, true, false };
            b[1] = new bool[4] { true, false, false, true };
            b[2] = new bool[4] { false, true, false, false };
            b[3] = new bool[4] { true, true, true, true };

            var resA = FourRussiansMethods.ReadMatrix("", false, lines);
            var resB = FourRussiansMethods.ReadMatrix("", true, lines);
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    Assert.Equal(a[i][j], resA[i][j]);
                    Assert.Equal(b[i][j], resB[i][j]);
                }
            }          
        }

        [Fact]
        public void SplitMatrixTest()
        {
            bool[][] a = new bool[4][];
            a[0] = new bool[4] { true, true, false, true };
            a[1] = new bool[4] { true, false, true, true };
            a[2] = new bool[4] { true, false, false, true };
            a[3] = new bool[4] { false, true, false, true };

            int n = 4;
            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);

            var res = FourRussiansMethods.SplitMatrix(a, m, t);

            Assert.Equal(t, res.Length);

            var antiSplit = new List<bool[]>();
            foreach (var r in res)
            {
                Assert.Equal(m, r.Length);
                foreach(var rr in r)
                {
                    antiSplit.Add(rr);
                }
            }

            for (int k = 0; k < t; ++k)
            {
                for (int i = 0; i < m; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        if (k * m + i >= n)
                        {
                            Assert.False(res[k][i][j]);
                        }
                        else
                        {
                            Assert.Equal(a[k * m + i][j], res[k][i][j]);
                        }
                    }
                }
            }

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {                   
                    Assert.Equal(a[i][j], antiSplit[i][j]);
                }
            }
        }


        [Theory]
        [InlineData(new bool[4] { true, true, false, true }, new bool[4] { true, false, true, true }, new bool[4] { true, true, true, true })]
        [InlineData(new bool[4] { true, true, false, true }, new bool[4] { true, false, false, true }, new bool[4] { true, true, false, true })]
        [InlineData(new bool[4] { true, true, false, true }, new bool[4] { false, true, false, true }, new bool[4] { true, true, false, true })]
        [InlineData(new bool[4] { true, false, true, true }, new bool[4] { true, false, false, true }, new bool[4] { true, false, true, true })]
        [InlineData(new bool[4] { true, false, true, true }, new bool[4] { false, true, false, true }, new bool[4] { true, true, true, true })]
        [InlineData(new bool[4] { true, false, false, true }, new bool[4] { false, true, false, true }, new bool[4] { true, true, false, true })]
        public void AddRowsTest(bool[] a, bool[] b, bool[] c)
        {
            int n = a.Length;
            var res = FourRussiansMethods.AddRows(a, b);
            for(int i = 0; i < n; ++i)
            {
                Assert.Equal(c[i], res[i]);
            }
        }

        [Fact]
        public void GetRowNumTest()
        {
            bool[][] a = new bool[3][];
            a[0] = new bool[2] { true, false };
            a[1] = new bool[2] { false, true };
            a[2] = new bool[2] { true, true };
            Assert.Equal(5, FourRussiansMethods.GetRowNum(a, 0));
            Assert.Equal(6, FourRussiansMethods.GetRowNum(a, 1));
        }

        [Fact]
        public void AddMatricesTest()
        {
            bool[][] a = new bool[4][];
            a[0] = new bool[4] { true, true, false, true };
            a[1] = new bool[4] { true, false, true, true };
            a[2] = new bool[4] { true, false, false, true };
            a[3] = new bool[4] { false, true, false, true };

            bool[][] b = new bool[4][];
            b[0] = new bool[4] { true, true, true, false };
            b[1] = new bool[4] { true, false, false, true };
            b[2] = new bool[4] { false, true, false, false };
            b[3] = new bool[4] { true, true, true, true };

            bool[][] c = new bool[4][];
            c[0] = new bool[4] { true, true, true, true };
            c[1] = new bool[4] { true, false, true, true };
            c[2] = new bool[4] { true, true, false, true };
            c[3] = new bool[4] { true, true, true, true };

            int n = a.Length;
            var res = FourRussiansMethods.AddMatrices(a, b);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Assert.Equal(c[i][j], res[i][j]);
                }
            }
        }

        [Fact]
        public void FourRussiansBMMTest1()
        {
            bool[][] a = new bool[3][];
            a[0] = new bool[3] { false, true, false };
            a[1] = new bool[3] { true, false, true };
            a[2] = new bool[3] { false, true, true };

            bool[][] b = new bool[3][];
            b[0] = new bool[3] { true, true, false };
            b[1] = new bool[3] { false, false, false };
            b[2] = new bool[3] { false, true, true };

            bool[][] c = new bool[3][];
            c[0] = new bool[3] { false, false, false };
            c[1] = new bool[3] { true, true, true };
            c[2] = new bool[3] { false, true, true };

            int n = a.Length;
            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);

            var splitA = FourRussiansMethods.SplitMatrix(a, m, t);
            var splitB = FourRussiansMethods.SplitMatrix(b, m, t);

            var res = FourRussiansMethods.FourRussiansBMM(splitA, splitB, n, m, t);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Assert.Equal(c[i][j], res[i][j]);
                }
            }
        }

        [Fact]
        public void FourRussiansBMMTest2()
        {
            bool[][] a = new bool[3][];
            a[0] = new bool[3] { true, false, true };
            a[1] = new bool[3] { false, false, true };
            a[2] = new bool[3] { false, true, false };

            bool[][] b = new bool[3][];
            b[0] = new bool[3] { false, true, true };
            b[1] = new bool[3] { true, false, false };
            b[2] = new bool[3] { true, true, false };

            bool[][] c = new bool[3][];
            c[0] = new bool[3] { false, true, true };
            c[1] = new bool[3] { true, true, false };
            c[2] = new bool[3] { true, true, true };

            int n = a.Length;
            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);

            var splitA = FourRussiansMethods.SplitMatrix(a, m, t);
            var splitB = FourRussiansMethods.SplitMatrix(b, m, t);

            var res = FourRussiansMethods.FourRussiansBMM(splitA, splitB, n, m, t);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Assert.Equal(c[i][j], res[i][j]);
                }
            }
        }

        [Fact]
        public void FourRussiansBMMTest3()
        {
            bool[][] a = new bool[4][];
            a[0] = new bool[4] { true, true, false, true };
            a[1] = new bool[4] { true, false, true, true };
            a[2] = new bool[4] { true, false, false, true };
            a[3] = new bool[4] { false, true, false, true };

            bool[][] b = new bool[4][];
            b[0] = new bool[4] { true, true, true, false };
            b[1] = new bool[4] { true, false, false, true };
            b[2] = new bool[4] { false, true, false, false };
            b[3] = new bool[4] { true, true, true, true };

            bool[][] c = new bool[4][];
            c[0] = new bool[4] { true, true, true, true };
            c[1] = new bool[4] { true, true, true, true };
            c[2] = new bool[4] { true, false, false, true };
            c[3] = new bool[4] { true, true, true, true };

            int n = a.Length;
            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);

            var splitA = FourRussiansMethods.SplitMatrix(a, m, t);
            var splitB = FourRussiansMethods.SplitMatrix(b, m, t);

            var res = FourRussiansMethods.FourRussiansBMM(splitA, splitB, n, m, t);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Assert.Equal(c[i][j], res[i][j]);
                }
            }
        }

        [Fact]
        public void FourRussiansBMMTest4()
        {
            bool[][] a = new bool[4][];
            a[0] = new bool[4] { false, false, true, true };
            a[1] = new bool[4] { true, true, true, false };
            a[2] = new bool[4] { true, false, false, false };
            a[3] = new bool[4] { true, false, true, true };

            bool[][] b = new bool[4][];
            b[0] = new bool[4] { true, false, false, true };
            b[1] = new bool[4] { false, false, true, true };
            b[2] = new bool[4] { true, false, true, false };
            b[3] = new bool[4] { false, true, false, true };

            bool[][] c = new bool[4][];
            c[0] = new bool[4] { true, true, true, true };
            c[1] = new bool[4] { false, false, true, true };
            c[2] = new bool[4] { true, true, true, true };
            c[3] = new bool[4] { true, true, false, true };

            int n = a.Length;
            int m = (int)Math.Floor(Math.Log(n));
            int t = (int)Math.Ceiling((double)n / (double)m);

            var splitA = FourRussiansMethods.SplitMatrix(a, m, t);
            var splitB = FourRussiansMethods.SplitMatrix(b, m, t);

            var res = FourRussiansMethods.FourRussiansBMM(splitA, splitB, n, m, t);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Assert.Equal(c[i][j], res[i][j]);
                }
            }
        }
    }
}
