using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
	class QuickSortWithArray
	{
		static void Main(string[] args)
		{
			int[] unsortedArray = new int[] { 3, 8, 2, 5, 1, 4, 7, 6 };

			unsortedArray = quickSort(unsortedArray);

			foreach (var item in quickSort(unsortedArray))
			{
				Console.WriteLine(item);
			}
		}

		static int[] quickSort(int[] array)
		{
			int[] tempArray = new int[array.Length];
			int pivot = array[0];
			int k = array.Length - 1;
			int j = 0;

			for (int i = 1; i < array.Length; i++)
			{
				if (array[i] > pivot)
				{
					tempArray[k] = array[i];
					k--;
				}
				else
				{
					tempArray[j] = array[i];
					j++;
				}
			}

			tempArray[j] = pivot;

			int[] left = tempArray.Take(j).ToArray();
			int[] right = tempArray.Skip(j + 1).ToArray();

			if (left.Length > 1)
			{
				left = quickSort(left);
			}

			if (right.Length > 1)
			{
				right = quickSort(right);
			}

			Array.Copy(left, 0, tempArray, 0, j);
			Array.Copy(right, 0, tempArray, j + 1, right.Length);

			return tempArray;
		}
	}
}
