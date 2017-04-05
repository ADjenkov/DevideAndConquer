using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianMaintenance
{
	class MedianMaintenance
	{
		static void Main(string[] args)
		{
			MyHeap.MyMaxHeap<int> lowHeap = new MyHeap.MyMaxHeap<int>();
			MyHeap.MyMinHeap<int> highHeap = new MyHeap.MyMinHeap<int>();

			List<int> ints = new List<int>() { 5, 15, 1, 3 };
			lowHeap.Add(ints.First(), ints.First());

			double sumMedians = ints.First();


			for (int i = 1; i < ints.Count; i++)
			{
				int k = i + 1;

				int integer = ints[i];

				int lowHeapMax = lowHeap.getMax();

				if (highHeap.Count == 0 && lowHeapMax < integer)
				{
					highHeap.Add(integer, integer);
				}

				int highHeapMin = highHeap.getMin();

				if (integer < lowHeapMax)
				{
					lowHeap.Add(integer, integer);
				}
				else if (integer > highHeapMin)
				{
					highHeap.Add(integer, integer);
				}

				if (lowHeap.Count + 1 < highHeap.Count)
				{
					lowHeap.Add(highHeapMin, highHeapMin);
					highHeap.Remove(highHeapMin);
				}
				else if (lowHeap.Count > highHeap.Count + 1)
				{
					highHeap.Add(lowHeapMax, lowHeapMax);
					lowHeap.Remove(lowHeapMax);
				}

				lowHeapMax = lowHeap.getMax();
				highHeapMin = highHeap.getMin();

				if (k % 2 == 0)
				{
					sumMedians += (lowHeapMax + highHeapMin) / 2;
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

			double s = sumMedians;
		}
	}
}
