using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHeap
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

	public abstract class MyHeap<T>
	{
		private List<HeapItem<T>> heapArray;

		protected MyHeap()
		{
			this.heapArray = new List<HeapItem<T>>();
		}

		public int Count
		{
			get { return this.heapArray.Count; }
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

		protected abstract void heapifyUp(int latestIndex);
		protected abstract void heapifyDown(int latestIndex);

		protected HeapItem<T> ElementAt(int index)
		{
			return this.heapArray.ElementAtOrDefault(index);
		}

		protected HeapItem<T> removeItem(int indexToRemove)
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

		protected void swapItems(int index, int swapIndex)
		{
			HeapItem<T> tmp = this.heapArray[index];
			this.heapArray[index] = this.heapArray[swapIndex];
			this.heapArray[swapIndex] = tmp;
		}

		protected int getParentIndex(int latestIndex)
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
}
