using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
	public class Leaf<T>
	{
		public int key;
		public Leaf<T> parent;
		public Leaf<T> leftChild;
		public Leaf<T> rightChild;
		public T data;

		public int childrenCount
		{
			get
			{
				if ((this.rightChild != null && this.leftChild == null) ||
					(this.rightChild == null && this.leftChild != null))
				{
					return 1;
				}
				else if (this.rightChild == null && this.leftChild == null)
				{
					return 0;
				}
				else
				{
					return 2;
				}
			}
		}

		public Leaf(int key, T data, Leaf<T> parent = null, Leaf<T> leftChild = null, Leaf<T> rightChild = null)
		{
			this.key = key;
			this.data = data;
			this.parent = parent;
			this.leftChild = leftChild;
			this.rightChild = rightChild;
		}
	}

	public class BinaryTree<T>
	{
		private Leaf<T> root;
		private int count;


		public Leaf<T> Root
		{
			get { return root; }
		}


		public BinaryTree()
		{
			this.root = null;
			this.count = 0;
		}

		public Leaf<T> Predecessor(int key)
		{
			Leaf<T> findKeyLeaf = this.Search(this.root, key);

			//Info: If left subtree available
			if (findKeyLeaf != null && findKeyLeaf.leftChild != null)
			{
				return this.Max(findKeyLeaf.leftChild);
			}
			else if (findKeyLeaf != null && findKeyLeaf.parent != null)
			{
				Leaf<T> parent = findKeyLeaf.parent;

				while (parent.key > findKeyLeaf.key)
				{
					parent = parent.parent;
				}

				return parent;
			}

			return null;
		}

		public void Delete(int key)
		{
			Leaf<T> itemToDelete = this.Search(this.root, key);

			if (itemToDelete != null)
			{
				//Info: if no children
				if (itemToDelete.childrenCount == 0)
				{
					if (itemToDelete.parent.leftChild.key == itemToDelete.key)
					{
						itemToDelete.parent.leftChild = null;
					}
					else if (itemToDelete.parent.rightChild.key == itemToDelete.key)
					{
						itemToDelete.parent.rightChild = null;
					}
				}
				else if (itemToDelete.childrenCount == 1)
				{
					this.deleteNodeWithSingleChild(itemToDelete);
				}
				else
				{
					//TODO: Implement 
				}
			}
		}

		public void PrintInOrder(Leaf<T> root)
		{
			if (root != null)
			{
				if (root.leftChild != null)
				{
					PrintInOrder(root.leftChild);
				}
				Console.WriteLine(root.key);
			}

			if (root != null)
			{
				if (root.rightChild != null)
				{
					PrintInOrder(root.rightChild);
				}
			}
		}

		public Leaf<T> Max(Leaf<T> node)
		{
			if (node.rightChild == null)
			{
				return node;
			}
			else
			{
				return Min(node.rightChild);
			}
		}

		public Leaf<T> Min(Leaf<T> node)
		{
			if (node.leftChild == null)
			{
				return node;
			}
			else
			{
				return Min(node.leftChild);
			}
		}

		public void Add(int key, T data)
		{
			if (this.root == null)
			{
				this.root = new Leaf<T>(key, data);
				this.count = 1;
			}
			else
			{
				Leaf<T> currentLeaf = this.root;
				Leaf<T> newLeaf = new Leaf<T>(key, data);

				bool positionFound = false;

				while (!positionFound)
				{
					if (key <= currentLeaf.key)
					{
						if (currentLeaf.leftChild != null)
						{
							currentLeaf = currentLeaf.leftChild;
						}
						else
						{
							positionFound = true;
							currentLeaf.leftChild = newLeaf;
							newLeaf.parent = currentLeaf;
							count++;
						}
					}
					else
					{
						if (currentLeaf.rightChild != null)
						{
							currentLeaf = currentLeaf.rightChild;
						}
						else
						{
							positionFound = true;
							currentLeaf.rightChild = newLeaf;
							newLeaf.parent = currentLeaf;
							count++;
						}
					}
				}
			}
		}

		public Leaf<T> Search(Leaf<T> leafToSearch, int key)
		{
			if (leafToSearch == null)
			{
				return null;
			}

			if (leafToSearch.key == key)
			{
				return leafToSearch;
			}
			else if (leafToSearch.key > key)
			{
				return Search(leafToSearch.leftChild, key);
			}
			else if (leafToSearch.key <= key)
			{
				return Search(leafToSearch.rightChild, key);
			}

			return null;
		}

		private void deleteNodeWithSingleChild(Leaf<T> node)
		{
			Leaf<T> leafToRedirect = node.leftChild;

			if (node.leftChild == null)
			{
				leafToRedirect = node.rightChild;
			}

			leafToRedirect.parent = node.parent;

			if (node.parent.leftChild.key == node.key)
			{
				node.parent.leftChild = leafToRedirect;
			}
			else if (node.parent.rightChild.key == node.key)
			{
				node.parent.rightChild = leafToRedirect;
			}
		}
	}

	class BinaryTree
	{
		static void Main(string[] args)
		{
			BinaryTree<int> btree = new BinaryTree<int>();

			btree.Add(17, 1);
			btree.Add(23, 1);
			btree.Add(21, 1);
			btree.Add(15, 1);
			btree.Add(28, 1);
			btree.Add(16, 2);
			btree.Add(14, 1);

			var s = btree.Search(btree.Root, 16);
			var min = btree.Min(btree.Root);
			var max = btree.Max(btree.Root);

			btree.PrintInOrder(btree.Root);
			var pred = btree.Predecessor(23);
		}
	}
}
