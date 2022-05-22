using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int randomNumber = random.Next(10, 100);

            int arraySize = 7;
            int[] array = GetRandomInitializedArray(arraySize);

            Console.WriteLine("Unsorted =   " + GetArrayAsString(array));
            BubbleSort(array);
            Console.WriteLine("BubbleSort = " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =           " + GetArrayAsString(array));
            BubbleSortFlagImpl(array);
            Console.WriteLine("BubbleSortFlagImpl = " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =      " + GetArrayAsString(array));
            SelectionSort(array);
            Console.WriteLine("SelectionSort = " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =      " + GetArrayAsString(array));
            InserionSort(array);
            Console.WriteLine("InserionSort =  " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =   " + GetArrayAsString(array));
            ShellSort(array);
            Console.WriteLine("ShellSort =  " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =   " + GetArrayAsString(array));
            MergeSort(array, 0, array.Length);
            Console.WriteLine("MergeSort =  " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
            Console.WriteLine("*****************************************");

            randomNumber = random.Next(10, 100);
            array = GetRandomInitializedArray(arraySize);
            Console.WriteLine("Unsorted =   " + GetArrayAsString(array));
            QuickSort(array, 0, array.Length);
            Console.WriteLine("QuickSort =  " + GetArrayAsString(array));
            Console.WriteLine("Has " + randomNumber + " at index " + BinarySearch(array, randomNumber));
        }


        private static void BubbleSort(int[] array)
        {
            for (int last = array.Length - 1; last > 0; last--)
            {
                for (int current = 0; current < last; current++)
                {
                    int next = current + 1;
                    if (array[current] > array[next])
                    {
                        Swap(array, current, next);
                    }
                }
            }
        }

        //optimizied to exit the loop if array happen to be sorted before all iterations
        private static void BubbleSortFlagImpl(int[] array)
        {
            int first = 0;
            int last = array.Length - 1;

            while (first < last)
            {
                bool isSorted = true;
                for (int current = 0; current < (last - first); current++)
                {
                    int next = current + 1;
                    if (array[current] > array[next])
                    {
                        Swap(array, current, next);
                        isSorted = false;
                    }
                }
                if (isSorted)
                {
                    break;
                }
                first++;
            }
        }

        private static void SelectionSort(int[] array)
        {
            for (int last = array.Length - 1; last > 0; last--)
            {
                int max = last;
                for (int i = 0; i < last; i++)
                {
                    if (array[i] > array[max])
                    {
                        max = i;
                    }
                }
                Swap(array, last, max);
            }
        }

        private static void InserionSort(int[] array)
        {
            for (int rightGoing = 1; rightGoing < array.Length; rightGoing++)
            {
                int candidate = array[rightGoing];
                int leftGoing;
                for (leftGoing = rightGoing; leftGoing > 0 && array[leftGoing - 1] > candidate; leftGoing--)
                {
                    array[leftGoing] = array[leftGoing - 1];
                }
                array[leftGoing] = candidate;
            }
        }

        private static void ShellSort(int[] array)
        {
            for (int step = array.Length / 2; step > 0; step /= 2)
            {
                for (int rightGoing = step; rightGoing < array.Length; rightGoing++)
                {
                    int candidate = array[rightGoing];
                    int leftGoing;
                    for (leftGoing = rightGoing; leftGoing >= step && array[leftGoing - step] > candidate; leftGoing -= step)
                    {
                        array[leftGoing] = array[leftGoing - step];
                    }
                    array[leftGoing] = candidate;
                }
            }
        }

        //Merge Sort 
        private static void MergeSort(int[] array, int start, int end)
        {
            if (end - start < 2)
            {
                return;
            }
            int mid = (start + end) / 2;
            MergeSort(array, start, mid);
            MergeSort(array, mid, end);
            Merge(array, start, mid, end);
        }

        private static void Merge(int[] array, int start, int mid, int end)
        {
            if (array[mid - 1] <= array[mid])
            {
                return;
            }
            int i = start;
            int j = mid;
            int tempIndex = 0;
            int[] temp = new int[end - start];
            while (i < mid && j < end)
            {
                temp[tempIndex++] = (array[i] <= array[j]) ? array[i++] : array[j++];
            }
            //handling for exmaple [1, 5] && [2, 3] array merging, optimization if we have left elements to copy from the left sub array
            //instead of copying them in the temp array and then back to the original array we move them directly within the original array,
            //and the next step will override the values in the original array any way
            Array.Copy(array, start, array, start + tempIndex, mid - i);
            Array.Copy(temp, 0, array, start, tempIndex);
        }
        //End Merge Sort

        //Quick Sort
        private static void QuickSort(int[] array, int start, int end)
        {
            if (end - start < 2)
            {
                return;
            }
            int pivotIndex = Partition(array, start, end);
            QuickSort(array, start, pivotIndex);
            QuickSort(array, pivotIndex + 1, end);
        }

        private static int Partition(int[] array, int start, int end)
        {
            int pivot = array[start];
            int i = start;
            int j = end;
            while (i < j)
            {
                while (i < j && array[--j] > pivot) ; //note the empty loop body
                if (i < j)
                {
                    array[i] = array[j];
                }
                while (i < j && array[++i] < pivot) ;  //note the empty loop body
                if (i < j)
                {
                    array[j] = array[i];
                }
            }
            array[j] = pivot;
            return j;
        }
        //End Quick Sort

        private static int BinarySearch(int[] arrayToSearchIn, int numberToSearchFor)
        {
            int first = 0;
            int last = arrayToSearchIn.Length - 1;
            while (first <= last)
            {
                int mid = (first + last) / 2;
                if (arrayToSearchIn[mid] == numberToSearchFor)
                {
                    return mid;
                }

                if (arrayToSearchIn[mid] < numberToSearchFor)
                {
                    first = mid + 1;
                }
                else
                {
                    last = mid - 1;
                }
            }
            return -1;
        }

        private static void Swap(int[] array, int leftIndex, int rightIndex)
        {
            ValidateIndexRangeIsInArrayBounds(array, leftIndex);
            ValidateIndexRangeIsInArrayBounds(array, rightIndex);

            if (leftIndex == rightIndex)
            {
                return;
            }

            if (array[leftIndex] == array[rightIndex])
            {
                return;
            }

            int temp = array[leftIndex];
            array[leftIndex] = array[rightIndex];
            array[rightIndex] = temp;
        }

        private static void ValidateIndexRangeIsInArrayBounds(int[] array, int index)
        {

            if (array == null)
            {
                throw new NullReferenceException("Array is null");
            }

            if (array.Length < 1)
            {
                throw new Exception("Array has no elements");
            }

            if (index < 0 || index >= array.Length)
            {
                throw new IndexOutOfRangeException("Index " + index + " is out of the array range");
            }
        }

        private static string GetArrayAsString(int[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += (i < array.Length - 1) ? array[i] + ", " : array[i] + "";
            }
            return "[" + result + "]";
        }

        private static int[] GetRandomInitializedArray(int arrayLength)
        {
            Random random = new Random();
            int[] array = new int[arrayLength];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(10, 100);
            }
            return array;
        }
    }
}
