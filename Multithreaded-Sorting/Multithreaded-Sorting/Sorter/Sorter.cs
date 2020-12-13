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

            if(list.Count < 3)
            {
                return _sortStrategy.SortChunk(list);
            }

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

            var chunkSize = initChunksSize;
            var workingProcessesCount = _processesCount;
            do
            {
                var tasks = new Task<IEnumerable<int>>[workingProcessesCount];
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = new Task<IEnumerable<int>>(() => _sortStrategy.SortChunk(chunks[i]));
                }
                Task.WaitAll(tasks);

                workingProcessesCount /= 2;
                var newChunks = new List<List<int>>();

                for (int i = 0; i < workingProcessesCount; i += 2)
                {
                    var newChunk = new List<int>();
                    newChunk.AddRange(tasks[i].Result);
                    newChunk.AddRange(tasks[i + 1].Result);
                    newChunks.Add(newChunk);
                }

                chunks = newChunks;
            } while (chunkSize < list.Count);

            return chunks[0];
        }
    }
}
