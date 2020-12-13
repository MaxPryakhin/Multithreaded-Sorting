using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreaded_Sorting.Sorter
{
    class Sorter
    {
        private int _processesCount = Environment.ProcessorCount;
        private ISortStrategy _sortStrategy;

        public Sorter(ISortStrategy sortStrategy)
        {
            _sortStrategy = sortStrategy ?? throw new ArgumentNullException(nameof(sortStrategy));
        }

        public IEnumerable<int> Sort(IEnumerable<int> sortable)
        {
            var list = sortable.ToList();
            var initChunksSize = list.Count / _processesCount;

            if(list.Count % _processesCount > 0)
            {
                initChunksSize += 1;
            }

            var curChunkIndex = 0;
            var chunks = new List<List<int>>();
            chunks.Add(new List<int>());

            for (int i = 0; i < list.Count; i++)
            {
                if(i == initChunksSize)
                {
                    curChunkIndex++;
                    chunks.Add(new List<int>());
                }

                var curChunk = chunks[curChunkIndex];
                curChunk.Add(list[curChunkIndex]);
            }


        }
    }
}
