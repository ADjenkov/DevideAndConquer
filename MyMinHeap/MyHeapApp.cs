using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHeap
{
	class MyHeapApp
	{
		static void Main(string[] args)
		{
			//MyMinHeap<int> heap = new MyMinHeap<int>();
			//heap.Add(4, 1);
			//heap.Add(4, 2);
			//heap.Add(8, 3);
			//heap.Add(9, 4);
			//heap.Add(4, 5);
			//heap.Add(12, 6);
			//heap.Add(9, 7);
			//heap.Add(11, 8);
			//heap.Add(13, 9);

			//heap.Add(7, 10);
			//heap.Add(10, 11);

			//heap.Add(5, 12);
			//heap.Add(3, 13);
			//heap.Add(20, 14);

			//var s = heap.getMin();

			MyMaxHeap<int> heap = new MyMaxHeap<int>();
			heap.Add(4, 1);
			heap.Add(4, 2);
			heap.Add(8, 3);
			heap.Add(9, 4);
			heap.Add(4, 5);
			heap.Add(12, 6);
			heap.Add(9, 7);
			heap.Add(11, 8);
			heap.Add(13, 9);

			heap.Add(7, 10);
			heap.Add(10, 11);

			heap.Add(5, 12);
			heap.Add(3, 13);
			heap.Add(20, 14);



			var s = heap.getMax();

			heap.Remove(14);

			var s2 = heap.getMax();

		}
	}
}
