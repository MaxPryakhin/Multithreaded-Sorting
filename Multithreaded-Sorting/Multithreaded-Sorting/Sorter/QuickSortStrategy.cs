using System.Collections.Generic;
using System.Linq;

namespace Multithreaded_Sorting.Sorter
{
    class QuickSortStrategy : ISortStrategy
    {
        public IEnumerable<int> SortChunk(IEnumerable<int> chunk) 
        {
            var list = chunk.ToList();
            list.Sort();
            return list;
        }
    }
}
