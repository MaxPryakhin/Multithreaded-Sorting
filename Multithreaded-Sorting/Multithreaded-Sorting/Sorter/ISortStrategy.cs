using System;
using System.Collections.Generic;
using System.Text;

namespace Multithreaded_Sorting.Sorter
{
    public interface ISortStrategy
    {
        public IEnumerable<int> SortChunk(IEnumerable<int> chunk);
    }
}
