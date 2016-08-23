using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFS;
using NUnit.Framework;

namespace Test
{
    class BFSSimpleFixture
    {
        private static int[,] GetMap(string filePath)
        {
            int width = 0;
            int height = 0;
            int[,] map = null;
            try
            {
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            var line = sr.ReadLine();
                            var headLine = line.Split(',');
                            height = int.Parse(headLine[0]);
                            width = int.Parse(headLine[1]);
                            map = new int[height, width];

                            int index = 0;
                            while (!string.IsNullOrEmpty(line))
                            {
                                line = sr.ReadLine();
                                for (int i = 0; i < line.Length; i++)
                                {
                                    map[index, i] = int.Parse(line[i].ToString());
                                }
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return map;
        }

        private static bool IsGridAvailable(int value)
        {
            return value != 1;
        }

        private static void ArraryEqual(List<IPoint> a, List<Tuple<int, int>> b)
        {
            Assert.AreEqual(a.Count, b.Count);
            for (int i = 0; i < a.Count; i++)
            {
                Assert.AreEqual(a[i].X, b[i].Item1);
                Assert.AreEqual(a[i].Y, b[i].Item2);
            }
        }

        [Test]
        public void First2EndTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "1.txt");
            var map = GetMap(filePath);
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 3;

            BFSSimple<int> BFSSimple = new BFSSimple<int>(map, IsGridAvailable);
            var result = BFSSimple.FindPath(startX, startY, endX, endY);

            Assert.AreEqual(result.Count, 10);
            ArraryEqual(result, new List<Tuple<int, int>>()
            {
               new Tuple<int, int>(0,0),
               new Tuple<int, int>(1,0),
               new Tuple<int, int>(2,0),
               new Tuple<int, int>(3,0),
               new Tuple<int, int>(3,1),
               new Tuple<int, int>(3,2),
               new Tuple<int, int>(3,3),
               new Tuple<int, int>(2,3),
               new Tuple<int, int>(1,3),
               new Tuple<int, int>(0,3)
            });
        }

        [Test]
        public void CannotReachTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "2.txt");
            var map = GetMap(filePath);
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 3;

            BFSSimple<int> BFSSimple = new BFSSimple<int>(map, IsGridAvailable);
            var result = BFSSimple.FindPath(startX, startY, endX, endY);

            Assert.AreEqual(result.Count, 0);
            ArraryEqual(result, new List<Tuple<int, int>>()
            {
            });
        }

        [Test]
        public void SingleRowTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "3.txt");
            var map = GetMap(filePath);
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 3;

            BFSSimple<int> BFSSimple = new BFSSimple<int>(map, IsGridAvailable);
            var result = BFSSimple.FindPath(startX, startY, endX, endY);

            Assert.AreEqual(result.Count, 4);
            ArraryEqual(result, new List<Tuple<int, int>>()
            {
                  new Tuple<int, int>(0,0),
                  new Tuple<int, int>(0,1),
                  new Tuple<int, int>(0,2),
                  new Tuple<int, int>(0,3)
            });
        }

        [Test]
        public void SingleColTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "4.txt");
            var map = GetMap(filePath);
            int startX = 0;
            int startY = 0;
            int endX = 3;
            int endY = 0;

            BFSSimple<int> BFSSimple = new BFSSimple<int>(map, IsGridAvailable);
            var result = BFSSimple.FindPath(startX, startY, endX, endY);

            Assert.AreEqual(result.Count, 4);
            ArraryEqual(result, new List<Tuple<int, int>>()
            {
                  new Tuple<int, int>(0,0),
                  new Tuple<int, int>(1,0),
                  new Tuple<int, int>(2,0),
                  new Tuple<int, int>(3,0)
            });
        }


        [Test]
        public void MutilTest()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestMap", "5.txt");
            var map = GetMap(filePath);
            int startX = 0;
            int startY = 2;
            int endX = 2;
            int endY = 1;

            BFSSimple<int> BFSSimple = new BFSSimple<int>(map, IsGridAvailable);
            var result = BFSSimple.FindPath(startX, startY, endX, endY);

            Assert.AreEqual(result.Count, 10);
            ArraryEqual(result, new List<Tuple<int, int>>()
            {
                  new Tuple<int, int>(0,2),
                  new Tuple<int, int>(0,3),
                  new Tuple<int, int>(0,4),
                  new Tuple<int, int>(1,4),
                  new Tuple<int, int>(2,4),
                  new Tuple<int, int>(2,3),
                  new Tuple<int, int>(3,3),
                  new Tuple<int, int>(3,2),
                  new Tuple<int, int>(3,1),
                  new Tuple<int, int>(2,1),
            });
        }
    }
}
