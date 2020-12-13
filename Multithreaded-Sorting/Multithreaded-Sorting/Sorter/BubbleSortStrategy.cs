using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multithreaded_Sorting.Sorter
{
    class BubbleSortStrategy : ISortStrategy
    {
        public IEnumerable<int> SortChunk(IEnumerable<int> chunk)
        {
            var array = chunk.ToArray();
            int temp;

            for (var i = 0; i < array.Length - 1; i++)
            {
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }
    }
}
