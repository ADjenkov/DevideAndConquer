using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class MergeSort
    {
        static void Main(string[] args)
        {
            int[] A = new int[] {666, 14, 31, 25, 4, 74, 32, 66, 33, 65, 666, 1 };

            A = mergeSort(A);

            printArray(A);
        }

        static int[] mergeSort(int[] arr)
        {
            int[] a = arr.Take(arr.Length / 2).ToArray();
            int[] b = arr.Skip(arr.Length / 2).ToArray();

            printArray(a);
            printArray(b);

            if (a.Length > 1)
            {
                a = mergeSort(a);
            }

            if (b.Length > 1)
            {
                b = mergeSort(b);
            }

            return merge(a, b);
        }

        static int[] merge(int[] arr1, int[] arr2)
        {
            int[] temp = new int[arr1.Length + arr2.Length];
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

                if (arr1[i] < arr2[j] || (arr1[i] == arr2[j])) //OR EQUAL
                {
                    temp[k] = arr1[i];
                    i++;
                }
                else if (arr2[j] < arr1[i])
                {
                    temp[k] = arr2[j];
                    j++;
                }
            }

            Console.WriteLine();
            //printArray(temp);
            Console.WriteLine();

            return temp;
        }

        static void printArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine();
        }
    }
}
