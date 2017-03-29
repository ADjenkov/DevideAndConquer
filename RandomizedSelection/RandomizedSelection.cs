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
			//int counter = 0;
			//string line;
			//int[] array = new int[10000];

			//StreamReader file = new StreamReader(@"QuickSort.txt");
			//while ((line = file.ReadLine()) != null)
			//{
			//	array[counter] = Int32.Parse(line);
			//	//Console.WriteLine(array[counter]);
			//	counter++;
			//}

			int[] unsortedArray = new int[] { 3, 8, 1, 7, 2, 4, 9 }; //1,2,3,4,7,8,9

			int orderStatistic = randomizedSelection(unsortedArray, 0, unsortedArray.Length, 1);
			Console.WriteLine(orderStatistic);

		}

		static int[] swapItems(int itemA, int itemB, int[] array)
		{
			int temp = array[itemA];
			array[itemA] = array[itemB];
			array[itemB] = temp;

			return array;
		}

		static int randomizedSelection(int[] array, int left, int right, int orderStatistic)
		{
			orderStatistic--;
			//j - is what we've seen
			//k - is the split between the seen elements that are bigger and less than pivot
			int pivotPosition = new Random().Next(left, right - 1);
			array = swapItems(left, pivotPosition, array);

			int pivot = array[left];
			int i = left + 1;

			for (int j = left + 1; j < right; j++)
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

			array[left] = array[i - 1];
			array[i - 1] = pivot;

			int currentOrderStatisticPosition = i - 1;

			if (currentOrderStatisticPosition == orderStatistic)
			{
				return array[i - 1];
			}
			else if (currentOrderStatisticPosition > orderStatistic)
			{
				if (i - left >= 2)
				{
					return randomizedSelection(array, left, i - 1, orderStatistic);
				}
				else return array[i - 1];
			}
			else
			{
				if (right - i >= 2)
				{
					return randomizedSelection(array, i + 1, right, orderStatistic);
				}
				else return array[i + 1];
			}
		}
	}
}
