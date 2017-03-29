using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosestPair
{
    class ClosestPair
    {
        class Pair
        {
            public Point p1
            {
                get;
                set;
            }

            public Point p2
            {
                get;
                set;
            }

            public double delta
            {
                get;
                set;
            }

            public Pair(Point p1, Point p2, double delta)
            {
                this.p1 = p1;
                this.p2 = p2;
                this.delta = delta;
            }
        }

        class Point
        {
            public int x
            {
                get;
                set;
            }

            public int y
            {
                get;
                set;
            }

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static void Main(string[] args)
        {
            Point[] points = new Point[] { 
                                        new Point(10,3),
                                        new Point(9,14),
                                        new Point(7,6),
                                        new Point(6,1),
                                        new Point(5,4),
                                        new Point(1,5)

            };

            Point[] Px = mergeSort(points, true);
            Point[] Py = mergeSort(points, false);

            printPointArray(Px);
            printPointArray(Py);

            Pair closestPoints = closestPair(Px, Py);

            Pair closestSplitPoints = closestSplitPair(Px, Py, closestPoints.delta);


            Console.WriteLine("Closest points ({0},{1})-({2},{3}) = {4}", closestPoints.p1.x, closestPoints.p1.y,
                closestPoints.p2.x, closestPoints.p2.y, closestPoints.delta);

            Console.WriteLine("Closest split points ({0},{1})-({2},{3}) = {4}", closestSplitPoints.p1.x, closestSplitPoints.p1.y,
            closestSplitPoints.p2.x, closestSplitPoints.p2.y, closestSplitPoints.delta);
        }

        static Pair closestSplitPair(Point[] Px, Point[] Py, double closestDelta)
        {
            double xBar = Px[Px.Length / 2 - 1].x;
            Point[] Sy = Py.Where(item => (item.x >= xBar - closestDelta && item.x <= xBar + closestDelta)).ToArray();

            printPointArray(Sy);

            int SyLen = Sy.Length %2 == 0 ? Sy.Length / 2: Sy.Length/2 + 1;
            for (int i = 0; i < SyLen; i++)
            {
                for (int j = 1; j < Sy.Length-1; j++)
                {
                    Point p = Sy[i];
                    Point q = Sy[j+i];

                    Console.WriteLine("Points ({0},{1})-({2},{3})", p.x, p.y,q.x,q.y);

                    if (getDistance(p, q) < closestDelta)
                    {
                        return new Pair(p, q, getDistance(p, q));
                    }
                }
            }
            return new Pair(Px[0], Px[0], 23);
        }

        static Pair closestPair(Point[] Px, Point[] Py)
        {
            Point[] Qx = Px.Take(Px.Length / 2).ToArray();
            Point[] Rx = Px.Skip(Px.Length / 2).ToArray();
            Point[] Qy = Py.Take(Py.Length / 2).ToArray();
            Point[] Ry = Py.Skip(Py.Length / 2).ToArray();

            Pair deltaQ = new Pair(Qx[0], Qy[0], getDistance(Qx[0], Qy[0]));
            Pair deltaR = new Pair(Rx[0], Ry[0], getDistance(Rx[0], Ry[0]));

            if (Qx.Length > 1)
            {
                deltaQ = closestPair(Qx, Qy);
            }

            if (Rx.Length > 1)
            {
                deltaR = closestPair(Rx, Ry);
            }

            if (deltaQ.delta > deltaR.delta)
            {
                return deltaR.delta != 0 ? deltaR : deltaQ;
            }
            else
            {
                return deltaQ.delta != 0 ? deltaQ : deltaR;
            }
        }

        static Point[] mergeSort(Point[] arr, bool mergeByX)
        {
            Point[] a = arr.Take(arr.Length / 2).ToArray();
            Point[] b = arr.Skip(arr.Length / 2).ToArray();

            if (a.Length > 1)
            {
                a = mergeSort(a, mergeByX);
            }

            if (b.Length > 1)
            {
                b = mergeSort(b, mergeByX);
            }

            return merge(a, b, mergeByX);
        }

        static double getDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }

        static Point[] merge(Point[] arr1, Point[] arr2, bool mergeByX)
        {
            Point[] temp = new Point[arr1.Length + arr2.Length];
            int i = 0;
            int j = 0;

            for (int k = 0; k < temp.Length; k++)
            {
                if (i == arr1.Length)
                {
                    //Alternative: Can fill up temp[k] with the rest of arr2
                    temp[k] = arr2[j];
                    j++;
                    continue;
                }

                if (j == arr2.Length)
                {
                    //Alternative: Can fill up temp[k] with the rest of arr1
                    temp[k] = arr1[i];
                    i++;
                    continue;
                }

                bool isArr2BiggerArr1 = mergeByX ? arr1[i].x < arr2[j].x : arr1[i].y < arr2[j].y;
                bool isArr1BiggerArr2 = mergeByX ? arr2[j].x < arr1[i].x : arr2[j].y < arr1[i].y;

                if (isArr2BiggerArr1 || (!isArr2BiggerArr1 && !isArr1BiggerArr2)) //OR EQUAL
                {
                    temp[k] = arr1[i];
                    i++;
                }
                else if (isArr1BiggerArr2)
                {
                    temp[k] = arr2[j];
                    j++;
                }
            }

            return temp;
        }

        static void printPointArray(Point[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write("({0},{1})", item.x, item.y);
            }

            Console.WriteLine();
        }
    }
}
