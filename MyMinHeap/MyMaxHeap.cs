using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHeap
{
	public class MyMaxHeap<T> : MyHeap<T>
	{
		public T GetMax()
		{
			return this.removeItem(0).item;
		}

		public T Max()
		{
			if (this.Count != 0)
			{
				return this.ElementAt(0).item;
			}

			return default(T);
		}

		protected override void heapifyDown(int latestIndex)
		{
			if (this.Count == 1)
			{
				return;
			}

			int biggestChildIndex = getBiggestChildIndex(latestIndex);

			while (biggestChildIndex != -1 && this.ElementAt(latestIndex).key < this.ElementAt(biggestChildIndex).key)
			{
				this.swapItems(latestIndex, biggestChildIndex);
				latestIndex = biggestChildIndex;
				biggestChildIndex = getBiggestChildIndex(latestIndex);
			}
		}

		protected override void heapifyUp(int latestIndex)
		{
			if (this.Count == 1)
			{
				return;
			}

			int parent = getParentIndex(latestIndex);

			while (this.ElementAt(parent).key < this.ElementAt(latestIndex).key)
			{
				this.swapItems(parent, latestIndex);

				if (parent != 0)
				{
					latestIndex = parent;
					parent = getParentIndex(parent);
				}
			}
		}

		public int getBiggestChildIndex(int parentIndex)
		{
			int leftChildIndex = -1;
			int rightChildIndex = -1;

			if (this.ElementAt(2 * parentIndex + 1) != null)
			{
				leftChildIndex = 2 * parentIndex + 1;
			}

			if (this.ElementAt(2 * parentIndex + 2) != null)
			{
				rightChildIndex = 2 * parentIndex + 2;
			}

			if (leftChildIndex != -1 && rightChildIndex != -1)
			{
				return this.ElementAt(rightChildIndex).key >= this.ElementAt(leftChildIndex).key ? rightChildIndex : leftChildIndex;
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
	}
}
