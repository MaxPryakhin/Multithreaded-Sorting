using System;
using System.Collections.Generic;
using System.Text;

namespace Multithreaded_Sorting.Sorter
{
    interface ISortStrategy
    {
        public IEnumerable<int> SortChunk(IEnumerable<int> chunk);
    }
}
