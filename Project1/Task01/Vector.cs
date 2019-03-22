using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_Algorithms.Project1
{

	[Serializable]
	public class Vector<T> : ISorter
	{
		T[] data;
		const int DEFAULT_CAPACITY = 10;
		int count = 0;

		public Vector()
		{
			data = new T[DEFAULT_CAPACITY];
		}


		public Vector(int CAPACITY)
		{
			data = new T[CAPACITY];
		}


		private void ExtendData(int extensionCapacity)
		{
			T[] newData = new T[data.Length + extensionCapacity];
			Array.Copy(data, 0, newData, 0, count);
			data = newData;
		}

		public void Add(T element)
		{
			if (count >= data.Length)
				ExtendData(DEFAULT_CAPACITY);
			data[count++] = element;

		}
		public bool Contains(T element)
		{
			if (IndexOf(element) > -1)
				return true;
			return false;
		}
		/*
		 * Running time T(n) = 5n + 3 
		 * Best case = 5
		 * Worst case = 5n + 3
		 */
		public int IndexOf(T element)
		{
			return Array.IndexOf(data, element);

		}
		/*
		 * 
		 * 
		 * 
		 */
		public void Insert(T element, int index)
		{
			if (index > count) throw new IndexOutOfRangeException("index out of range");
			if (count == data.Length) ExtendData(DEFAULT_CAPACITY);

			// you could use Array.Copy or simply move elements manually as follows
			for (int i = count; i > index; i--)
				data[i] = data[i - 1];
			data[index] = element;
			count++;

		}
		public bool Remove(T element)
		{
			var index = IndexOf(element);
			if (index > -1)
				return RemoveAt(index);
			return false;

		}
		public bool RemoveAt(int index)
		{
			if (index > count) throw new IndexOutOfRangeException("index out of range");
			//shift all the elements to the left
			for (int i = index; i < count; i++)
				data[i] = data[i + 1];
			//decrement count field
			count--;

			return true;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			((ICollection<T>)data).CopyTo(array, arrayIndex);
		}



		public void Clear()
		{
			((ICollection<T>)data).Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((ICollection<T>)data).GetEnumerator();
		}



		public int Capacity
		{
			get
			{
				return data.Length;
			}
			set
			{
				if (value < count)
					throw new ArgumentOutOfRangeException();
				ExtendData(value);
			}
		}
		public int Count
		{
			get { return count; }
		}

		public void SortBubble()
		{
			int n = count;
			IComparer<T> comparer = Comparer<T>.Default;
			for (int i = 0; i < n - 1; i++)
			{
				for (int j = 0; j < n - i - 1; j++)
				{
					if ( comparer.Compare(data[j] , data[j + 1]) > 0)
					{
						swap(data[j], data[j + 1]);
					}
				}
			}

		}

		void swap(T p1, T p2)
		{
			T temp = p1;
			p1 = p2;
			p2 = temp;
			
		}

		public void Sort()
		{
			Array.Sort(data, 0, count);
		}
		public void Sort(IComparer<T> comparer)
		{
			Array.Sort(data, 0, count, comparer);
		}

		private int BinarySearch(T value, int left, int right, IComparer<T> comparer)
		{
			if (comparer == null) comparer = Comparer<T>.Default;
			if (left <= right)
			{
				int middle = (left + right) / 2;
				int result = comparer.Compare(value, data[middle]);
				if (result == 0)
					return middle;
				if (result < 0)
					return BinarySearch(value, left, middle - 1, comparer);
				if (result > 0)
					return BinarySearch(value, middle + 1, right, comparer);
			}
			return -1;

		}
		public int BinarySearch(T element)
		{
			return BinarySearch(element, 0, count, null);
		}
		public int BinarySearch(T element, IComparer<T> comparer)
		{
			return BinarySearch(element, 0, count, comparer);
		}
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<T>)data).IsReadOnly;
			}
		}

		public T this[int index]
		{
			get
			{
				return data[index];
			}
			set
			{
				if (index >= count)
					throw new IndexOutOfRangeException();
				data[index] = value;

			}
		}
		public override string ToString()
		{
			string listValues = "";
			int i = 0;
			for (i = 0; i < count - 1; i++)
				listValues += string.Format("{0},", data[i]);
			listValues += string.Format("{0}", data[i]);
			return listValues;
		}

		public void Sort(SortingAlgorithm sortAlgorithm)
		{
			switch(sortAlgorithm)
			{
				case SortingAlgorithm.INSERTION: 
					InsertionSort();  break;
				case SortingAlgorithm.SELECTION: 
					SelectionSort(); break;
				case SortingAlgorithm.MERGE: 
					MergeSort(); break;
				case SortingAlgorithm.QUICK: 
					QuickSort(); break; 
				case SortingAlgorithm.MICROSOFT: 
					MicrosoftSort(); break;

			}
}

		void MicrosoftSort()
		{
			Array.Sort(data, 0, count);
		}



		void quickSort(int left, int right)
		{
			
		}

		public void QuickSort()
		{
			quickSort(0, Count - 1);
		}

		void MergeSort()
		{
		}



		void SelectionSort()
		{
			IComparer<T> comparer = Comparer<T>.Default;
			long index_of_min = 0;
			for (int iterator = 0; iterator < Count - 1; iterator++)
			{
				index_of_min = iterator;
				for (int index = iterator + 1; index < Count; index++)
				{
					if (comparer.Compare(data[index], data[index_of_min] ) < 0)
						index_of_min = index;
				}
				Swap(ref data[iterator], ref data[index_of_min]);
			}
		}

		void Swap(ref T t1, ref T t2)
		{
			T tmp = t2;
			t2 = t1;
			t1 = tmp;
		}

		void InsertionSort()
		{
			IComparer <T> comparer  = Comparer<T>.Default;
			int j = 0;
			T temp;
			for (int index = 1; index < Count; index++)
			{
				j = index;
				temp = data[index];
				while ((j > 0) && comparer.Compare(data[j - 1], temp) > 0)
				{
					data[j] = data[j - 1];
					j = j - 1;
				}
				data[j] = temp;
			}
		}
}
    }
