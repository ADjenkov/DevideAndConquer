using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMinHeap
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

	public class MyHeap<T>
	{
		private List<HeapItem<T>> heapArray;

		public MyHeap()
		{
			this.heapArray = new List<HeapItem<T>>();
		}

		public void Add(int key, T item)
		{
			var newItem = new HeapItem<T>(key, item);
			this.heapArray.Add(newItem);

			this.heapifyUp(this.heapArray.Count - 1);
		}

		public void Remove(T itemToRemove)
		{
			HeapItem<T> itemToFind = this.heapArray.Where(x => x.item.Equals(itemToRemove)).FirstOrDefault();

			if (itemToFind != null)
			{
				int index = this.heapArray.IndexOf(itemToFind);

				this.removeItem(index);
			}
		}

		public T getMin()
		{
			return this.removeItem(0).item;
		}

		public int Count
		{
			get { return this.heapArray.Count; }
		}

		private HeapItem<T> removeItem(int indexToRemove)
		{
			HeapItem<T> heapItem = null;

			if (this.heapArray.Count == 1)
			{
				heapItem = this.heapArray.ElementAt(indexToRemove);
				this.heapArray.RemoveAt(indexToRemove);
			}
			else if (this.heapArray.Count > 1)
			{
				heapItem = this.heapArray.ElementAt(indexToRemove);

				this.swapItems(indexToRemove, this.heapArray.Count - 1);
				this.heapArray.RemoveAt(this.heapArray.Count - 1);

				this.heapifyDown(indexToRemove);
			}

			return heapItem;
		}

		private void heapifyDown(int latestIndex)
		{
			if (this.heapArray.Count == 1)
			{
				return;
			}

			int smallestChildIndex = getSmallestChildIndex(latestIndex);

			while (smallestChildIndex != -1 && this.heapArray.ElementAt(latestIndex).key > this.heapArray.ElementAt(smallestChildIndex).key)
			{
				this.swapItems(latestIndex, smallestChildIndex);
				latestIndex = smallestChildIndex;
				smallestChildIndex = getSmallestChildIndex(latestIndex);
			}
		}

		private void heapifyUp(int latestIndex)
		{
			if (this.heapArray.Count == 1)
			{
				return;
			}

			int parent = getParentIndex(latestIndex);

			while (this.heapArray.ElementAt(parent).key > this.heapArray.ElementAt(latestIndex).key)
			{
				this.swapItems(parent, latestIndex);

				if (parent != 0)
				{
					latestIndex = parent;
					parent = getParentIndex(parent);
				}
			}
		}

		private void swapItems(int index, int swapIndex)
		{
			HeapItem<T> tmp = this.heapArray[index];
			this.heapArray[index] = this.heapArray[swapIndex];
			this.heapArray[swapIndex] = tmp;
		}

		public int getSmallestChildIndex(int parentIndex)
		{
			int leftChildIndex = -1;
			int rightChildIndex = -1;

			if (this.heapArray.ElementAtOrDefault(2 * parentIndex + 1) != null)
			{
				leftChildIndex = 2 * parentIndex + 1;
			}

			if (this.heapArray.ElementAtOrDefault(2 * parentIndex + 2) != null)
			{
				rightChildIndex = 2 * parentIndex + 2;
			}

			if (leftChildIndex != -1 && rightChildIndex != -1)
			{
				return this.heapArray.ElementAt(rightChildIndex).key <= this.heapArray.ElementAt(leftChildIndex).key ? rightChildIndex : leftChildIndex;
			}
			else if (leftChildIndex != -1)
			{
				return leftChildIndex;
			}
			else
			{
				return rightChildIndex;
			}
		}

		private int getParentIndex(int latestIndex)
		{
			int parent = 0;

			if (latestIndex != 0)
			{
				if (latestIndex % 2 == 0)
				{
					parent = latestIndex / 2;
					parent--;
				}
				else
				{
					parent = (int)Math.Floor((double)latestIndex / 2);
				}
			}
			return parent;
		}
	}

	class MyMinHeapApp
	{
		static void Main(string[] args)
		{
			MyHeap<int> heap = new MyHeap<int>();
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

			//var s = heap.getMin();
			heap.Remove(4);
		}
	}
}
