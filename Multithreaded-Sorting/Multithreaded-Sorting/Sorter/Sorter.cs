using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreaded_Sorting.Sorter
{
    public class Sorter
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

            var k = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if(initChunksSize - k == 0)
                {
                    k = 0;
                    curChunkIndex++;
                    chunks.Add(new List<int>());
                }
                k++;
                var curChunk = chunks[curChunkIndex];
                curChunk.Add(list[curChunkIndex]);
            }

            var chunkSize = initChunksSize;
            var workingProcessesCount = _processesCount;
            var isFullSorted = false;
            do
            {
                isFullSorted = chunkSize >= list.Count;

                var tasks = new Task<IEnumerable<int>>[workingProcessesCount];
                for (int i = 0; i < workingProcessesCount; i++)
                {
                    var chunk = chunks[i];
                    var task = new Task<IEnumerable<int>>(() => _sortStrategy.SortChunk(chunk));
                    task.Start();
                    tasks[i] = task;
                }
                Task.WaitAll(tasks);

                workingProcessesCount /= 2;
                var newChunks = new List<List<int>>();


                if(workingProcessesCount == 0)
                {
                    var newChunk = new List<int>();
                    newChunk.AddRange(tasks[0].Result);
                    newChunks.Add(newChunk);
                }
                else
                {
                    for (int i = 0; i < workingProcessesCount; i++)
                    {
                        var newChunk = new List<int>();
                        newChunk.AddRange(tasks[i * 2].Result);
                        newChunk.AddRange(tasks[i * 2 + 1].Result);
                        newChunks.Add(newChunk);
                    }
                }
                

                chunks = newChunks;
                chunkSize = chunks[0].Count;

            } while (!isFullSorted);

            return chunks[0];
        }
    }
}
