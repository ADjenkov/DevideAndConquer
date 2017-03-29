using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountInversion
{
    class CountInversions
    {
        static long allInversions = 0;
        static void Main(string[] args)
        {
            int counter = 0;
            string line;
            int[] array = new int[100000];

            StreamReader file = new StreamReader(@"IntegerArray.txt");
            while ((line = file.ReadLine()) != null)
            {
                array[counter] = Int32.Parse(line);
                //Console.WriteLine(bigArray[counter]);
                counter++;
            }

            file.Close();

            array = mergeSort(array);


            Console.WriteLine("INVERSIONS {0}", allInversions);
        }

        static int[] mergeSort(int[] arr)
        {
            int[] a = arr.Take(arr.Length / 2).ToArray();
            int[] b = arr.Skip(arr.Length / 2).ToArray();

            //printArray(a);
            //printArray(b);

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
                    allInversions += arr1.Length - i;
                    temp[k] = arr2[j];
                    j++;
                }
            }

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("\n Inve {0}",inversions);
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
