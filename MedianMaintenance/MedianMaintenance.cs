using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianMaintenance
{
	class MedianMaintenance
	{
		//The goal of this problem is to implement the "Median Maintenance" algorithm (covered in the Week 3 lecture on heap applications). 
		//The text file contains a list of the integers from 1 to 10000 in unsorted order; you should treat this as a stream of numbers,arriving
		//one by one. Letting xi denote the ith number of the file, the kth median mk is defined as the median of the numbers x1,…,xk. 
		//(So, if k is odd, then mk is ((k+1)/2)th smallest number among x1,…,xk; if k is even, then mk is the (k/2)th smallest number among x1,…,xk.)
		//In the box below you should type the sum of these 10000 medians, modulo 10000 (i.e., only the last 4 digits). 
		//That is, you should compute (m1+m2+m3+⋯+m10000)mod10000.
		static MyHeap.MyMaxHeap<int> lowHeap;
		static MyHeap.MyMinHeap<int> highHeap;
		static double sumMedians = 0;
		static List<int> ints;

		static void Main(string[] args)
		{
			lowHeap = new MyHeap.MyMaxHeap<int>();
			highHeap = new MyHeap.MyMinHeap<int>();

			ints = getIntsFromFile();
			lowHeap.Add(ints.First(), ints.First());
			sumMedians = ints.First();

			//Start from the second number
			for (int i = 1; i < ints.Count; i++)
			{
				int integersCount = i + 1;
				int integer = ints[i];

				int lowHeapMax = lowHeap.Max();

				//If the second number is for the highHeap->put it in highHeap and continue with the 3rd number
				if (highHeap.Count == 0 && lowHeapMax < integer)
				{
					highHeap.Add(integer, integer);
					sumMedians += lowHeapMax;
					continue;
				}

				int highHeapMin = highHeap.Min();

				if (integer < lowHeapMax)
				{
					lowHeap.Add(integer, integer);
				}
				else if (integer > highHeapMin)
				{
					highHeap.Add(integer, integer);
				}
				else
				{
					//IF integer in between put in one of the two heaps. Doesn't matter which one
					if (lowHeap.Count <= highHeap.Count)
					{
						lowHeap.Add(integer, integer);
					}
					else if (lowHeap.Count > highHeap.Count)
					{
						highHeap.Add(integer, integer);
					}
				}

				RebalanceHeaps();
				CalcMedian(integersCount);
			}

			//Corrrect answer for Median.txt is 1213
			//Corrrect answer for MedianTest1.txt is 9335
			//Corrrect answer for MedianTest2.txt is 142
			Console.WriteLine(sumMedians % 10000);
		}

		static List<int> getIntsFromFile()
		{
			ints = new List<int>();

			string line;
			int counter = 0;

			StreamReader file = new StreamReader(@"Median.txt");
			while ((line = file.ReadLine()) != null)
			{
				ints.Add(Int32.Parse(line));

				counter++;

			}

			return ints;
		}

		static void CalcMedian(int intsCount)
		{
			int lowHeapMax = lowHeap.Max();
			int highHeapMin = highHeap.Min();

			if (intsCount % 2 == 0)
			{
				sumMedians += lowHeapMax;
			}
			else
			{
				if (lowHeap.Count > highHeap.Count)
				{
					sumMedians += lowHeapMax;
				}
				else
				{
					sumMedians += highHeapMin;
				}
			}
		}

		static void RebalanceHeaps()
		{
			if (lowHeap.Count + 1 < highHeap.Count)
			{
				var extractHiHeapMin = highHeap.GetMin();

				lowHeap.Add(extractHiHeapMin, extractHiHeapMin);
			}
			else if (lowHeap.Count > highHeap.Count + 1)
			{
				var extractLowHeapMax = lowHeap.GetMax();

				highHeap.Add(extractLowHeapMax, extractLowHeapMax);
			}
		}
	}
}
