﻿using System;

namespace Lab2
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MaxHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            if (initialArray == null)
            {
                return;
            }

            foreach (var item in initialArray)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Returns the min item but does NOT remove it.
        /// Time complexity: O(1).
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            return array[0];
        }

        // TODO
        /// <summary>
        /// Adds given item to the heap.
        /// Time complexity: O( log(N) ).
        /// </summary>
        public void Add(T item)
        {
            int nextEmptyIndex = Count;

            array[nextEmptyIndex] = item;

            TrickleUp(nextEmptyIndex);

            Count++;

            // resize if full
            if (Count == Capacity)
            {
                DoubleArrayCapacity();
            }

        }

        public T Extract()
        {
            return ExtractMax();
        }

        // TODO
        /// <summary>
        /// Removes and returns the max item in the min-heap.
        /// Time complexity: O( N ).
        /// </summary>
        public T ExtractMax()
        {
            
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T max = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return max;


        }

        // TODO
        /// <summary>
        /// Removes and returns the min item in the min-heap.
        /// Time ctexity: O( log(n) ).
        /// </summary>
        public T ExtractMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T min = array[Count - 1];

            Count--;

            return min;

        }

        // TODO
        /// <summary>
        /// Updates the first element with the given value from the heap.
        /// Time complexity: O( ? )
        /// </summary>
        public void Update(T oldValue, T newValue)
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }
            else
            {
                if (!Contains(oldValue))
                {
                    throw new Exception();
                }
                else
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (array[i].CompareTo(oldValue) == 0)
                        {
                            array[i] = newValue;

                            if (newValue.CompareTo(oldValue) == -1)
                            {
                                TrickleUp(i);
                            }

                            else
                            {
                                TrickleDown(i);
                            }
                        }
                    }
                }
            }


        }

        // TODO
        /// <summary>
        /// Removes the first element with the given value from the heap.
        /// Time complexity: O( log(N) )
        /// </summary>
        public void Remove(T value)
        {

            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            else
            {
                for (int i = 0; i < Count; i++)
                {

                    if (array[i].CompareTo(value) == 0)
                    {
                        array[i] = array[Count - 1];
                        Count--;
                    }

                }

            }

        }

        // TODO
        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N ).
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            for (int i = 0; i < Count; i++)
            {
                if (array[i].CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {

            while (index > 0)
            {
                int parent = Parent(index);
                if (array[index].CompareTo(array[parent]) <= 0)
                {
                    return;
                }
                else
                {
                    Swap(index, parent);
                    index = parent;
                }
            }
        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            int child = LeftChild(index);
            T value = array[index];

            while (child < Count)
            {

                T maxValue = value;
                int maxIndex = -1;
                int i = 0;

                for (i = 0; i < 2 && i + child < Count; i++)
                {
                    if (array[i + child].CompareTo(maxValue) == 1)
                    {
                        maxValue = array[i + child];
                        maxIndex = i + child;
                    }
                }

                if (maxValue.CompareTo(value) == 0)
                {
                    return;
                }

                else
                {
                    Swap(index, maxIndex);
                    index = maxIndex;
                    child = 2 * index + 1;
                }
            }

        }

        // TODO
        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            return (position - 1) / 2;

        }

        // TODO
        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            return 2 * position + 1;

        }

        // TODO
        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            return 2 * position + 2;

        }

        private void Swap(int index1, int index2)
        {
            var temp = array[index1];

            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void DoubleArrayCapacity()
        {
            Array.Resize(ref array, array.Length * 2);
        }


    }
}

