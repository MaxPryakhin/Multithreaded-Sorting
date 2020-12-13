using Multithreaded_Sorting.Sorter;
using System;
using System.Linq;

namespace Multithreaded_Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var size = random.Next(100, 10001);
            var array1 = new int[size];
            var array2 = new int[size];
            var array3 = new int[size];
            var array4 = new int[size];
            for (int i = 0; i < size; i++)
            {
                var number = random.Next(10001);
                array1[i] = number;
                array2[i] = number;
                array3[i] = number;
                array4[i] = number;
            }

            var list1 = array1.ToList();
            list1.Sort();
            Console.WriteLine("single core");
            Console.WriteLine(list1[0]);
            Console.WriteLine(list1[size - 1]);

            var bubbleStrategy = new BubbleSortStrategy();
            var bubbleSorter = new Sorter.Sorter(bubbleStrategy);
            var sortedArray2 = bubbleSorter.Sort(array2).ToArray();
            Console.WriteLine("bubble sort");
            Console.WriteLine(sortedArray2[0]);
            Console.WriteLine(sortedArray2[size - 1]);

            var quickStrategy = new BubbleSortStrategy();
            var quickSorter = new Sorter.Sorter(quickStrategy);
            var sortedArray3 = quickSorter.Sort(array3).ToArray();
            Console.WriteLine("quick sort");
            Console.WriteLine(sortedArray3[0]);
            Console.WriteLine(sortedArray3[size - 1]);

            var mergeStrategy = new BubbleSortStrategy();
            var mergeSorter = new Sorter.Sorter(mergeStrategy);
            var sortedArray4 = mergeSorter.Sort(array4).ToArray();
            Console.WriteLine("merge sort");
            Console.WriteLine(sortedArray4[0]);
            Console.WriteLine(sortedArray4[size - 1]);


        }
    }
}
