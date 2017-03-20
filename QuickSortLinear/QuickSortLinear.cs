﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSortLinear
{
	class QuickSortLinear
	{
		static long allComparisons = 0;
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

			int[] unsortedArray = new int[] { 3, 8, 2, 5, 1, 4, 7, 6 };

			//foreach (var item in quickSort(unsortedArray, 0, unsortedArray.Length))
			//{
			//	Console.WriteLine(item);
			//}

			foreach (var item in quickSort(array, 0, array.Length))
			{
				Console.WriteLine(item);
			}
		}

		static int[] swapItems(int itemA, int itemB, int[] array)
		{
			int temp = array[itemA];
			array[itemA] = array[itemB];
			array[itemB] = temp;

			return array;
		}

		static int[] quickSort(int[] array, int left, int right)
		{
			//j - is what we've seen
			//k - is the split between the seen elements that are bigger and less than pivot
			int pivotPosition = new Random().Next(left, right);
			swapItems(left, pivotPosition, array);
			int pivot = array[left];
			int i = left + 1;

			for (int j = left + 1; j < right; j++)
			{
				if (array[j] < pivot)//DO NOT SWAP ITEMS WITH SAME VALUE
				{
					if (array[j] != array[i])
					{
						Console.WriteLine("Swap {0} and {1}", array[j], array[i]);
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

			if (i - left > 2)
			{
				array = quickSort(array, left, i - 1);
			}

			if (right - i >= 2)
			{
				array = quickSort(array, i, right);
			}

			//Console.WriteLine();
			//for (int j = left; j < right; j++)
			//{
			//	Console.Write("{0}", array[j]);
			//}
			//Console.WriteLine();

			return array;
		}
	}
}
