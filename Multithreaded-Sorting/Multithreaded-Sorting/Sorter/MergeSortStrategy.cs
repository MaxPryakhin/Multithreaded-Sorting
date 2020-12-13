using System;
using System.Collections.Generic;
using System.Linq;

namespace Multithreaded_Sorting.Sorter
{
	public class MergeSortStrategy : ISortStrategy
    {
		int[] MergeSort(int[] array)
		{
			if (array.Length == 1)
			{
				return array;
			}

			int middle = array.Length / 2;
			return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
		}

		int[] Merge(int[] arr1, int[] arr2)
		{
			int ptr1 = 0, ptr2 = 0;
			int[] merged = new int[arr1.Length + arr2.Length];

			for (int i = 0; i < merged.Length; ++i)
			{
				if (ptr1 < arr1.Length && ptr2 < arr2.Length)
				{
					merged[i] = arr1[ptr1] > arr2[ptr2] ? arr2[ptr2++] : arr1[ptr1++];
				}
				else
				{
					merged[i] = ptr2 < arr2.Length ? arr2[ptr2++] : arr1[ptr1++];
				}
			}

			return merged;
		}
		public IEnumerable<int> SortChunk(IEnumerable<int> chunk)
        {
            return MergeSort(chunk.ToArray());
        }
    }
}
