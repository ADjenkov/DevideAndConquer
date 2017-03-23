using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizedSelection
{
	class RandomizedSelection
	{
		static void Main(string[] args)
		{
			int counter = 0;
			string line;
			int[] array = new int[10000];

			StreamReader file = new StreamReader(@"QuickSort.txt");
			while ((line = file.ReadLine()) != null)
			{
				array[counter] = Int32.Parse(line);
				//Console.WriteLine(array[counter]);
				counter++;
			}

			int[] unsortedArray = new int[] { 3, 8, 1, 7, 2, 4, 9 };

			//unsortedArray = quickSort(unsortedArray, 0, unsortedArray.Length);
			unsortedArray = quickSort(array, 0, array.Length);

			Console.WriteLine("\nSorted array:");
			foreach (var item in unsortedArray)
			{
				Console.WriteLine(item);
			}

			//foreach (var item in quickSort(array, 0, array.Length))
			//{
			//	Console.WriteLine(item);
			//}
		}

		static int[] swapItems(int itemA, int itemB, int[] array)
		{
			int temp = array[itemA];
			array[itemA] = array[itemB];
			array[itemB] = temp;

			return array;
		}

		static int[] quickSort(int[] array, int arrayLength, int orderStatistic)
		{
			//j - is what we've seen
			//k - is the split between the seen elements that are bigger and less than pivot
			int pivotPosition = new Random().Next(0, arrayLength - 1);
			array = swapItems(0, pivotPosition, array);

			int pivot = array[0];
			int i = 1;

			for (int j = 1; j < arrayLength; j++)
			{
				if (array[j] < pivot)	//DO NOT SWAP ITEMS WITH SAME VALUE
				{
					if (array[j] != array[i])
					{
						swapItems(j, i, array);
						i++;
					}
					else
					{
						i++;
					}
				}
			}

			array[0] = array[i - 1];
			array[i - 1] = pivot;

			int currentOrderStatisticPosition = i - 1;

			//if (i - left > 2)
			//{
			//	array = quickSort(array, left, i - 1);
			//}

			//if (right - i >= 2)
			//{
			//	array = quickSort(array, i, right);
			//}

			return array;
		}
	}
}
