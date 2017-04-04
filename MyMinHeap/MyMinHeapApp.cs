using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMinHeap
{
	class MyMinHeapApp
	{
		public class HeapItem<T>
		{
			public int key;
			public T item;

			public HeapItem(int key, T item)
			{
				this.key = key;
				this.item = item;
			}
		}

		public class MyMinHeap<T>
		{
			//private Dictionary<int, T> items;
			private List<HeapItem<T>> heapArray;

			public MyMinHeap()
			{
				//this.items = new Dictionary<int, T>();
				this.heapArray = new List<HeapItem<T>>();
			}

			public void Add(int key, T item)
			{
				var newItem = new HeapItem<T>(key, item);
				this.heapArray.Add(newItem);

				this.heapify(this.heapArray.Count - 1, newItem);
			}

			private void heapify(int latestIndex, HeapItem<T> latestItem)
			{
				if (this.heapArray.Count == 1)
				{
					return;
				}

				int parent = getParentIndex(latestIndex);

				while (this.heapArray.ElementAt(parent).key > latestItem.key)
				{
					HeapItem<T> tmp = this.heapArray[parent];
					this.heapArray[parent] = this.heapArray[latestIndex];
					this.heapArray[latestIndex] = tmp;

					latestItem = heapArray.ElementAt(parent);
					latestIndex = parent;
					parent = getParentIndex(parent);
				}
			}

			private int getParentIndex(int latestIndex)
			{
				int parent = 0;
				if (latestIndex % 2 == 0)
				{
					parent = latestIndex / 2;
					parent--;
				}
				else
				{
					parent = (int)Math.Floor((double)latestIndex / 2);
				}

				return parent;
			}
		}

		static void Main(string[] args)
		{
			MyMinHeap<int> heap = new MyMinHeap<int>();
			heap.Add(4, 1);
			heap.Add(4, 1);
			heap.Add(8, 1);
			heap.Add(9, 1);
			heap.Add(4, 1);
			heap.Add(12, 1);
			heap.Add(9, 1);
			heap.Add(11, 1);
			heap.Add(13, 1);

			heap.Add(7, 1);
			heap.Add(10, 1);

			heap.Add(5, 1);
		}
	}
}
