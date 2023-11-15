using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        T[] array = new T[16];
        int size = 0;
        int length = 0;
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
                if (length > Size)
                {
                    Size = length;
                }
            }
        }

        private int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value > size)
                    if (value > size + 16)
                    {
                        Array.Resize(ref array, value);
                        size = value;
                    }
                    else
                    {
                        Array.Resize(ref array, size + 16);
                        size += 16;
                    }
                        
                
            }
        }

        public DynamicArray()
        {
        }

        public DynamicArray(T[] array)
        {
            this.array = array;
            this.Length = array.Length;
        }
        public T this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                if (index >= Length)
                    Length = index + 1;
                array[index] = value;
            }
        }

        public void Add(T item)
        {
            array[Length] = item;
        }

        public void AddRange(T[] range)
        {
            foreach(var item in range)
            {
                array[Length] = item;
            }
        }

        public void Clear()
        {
            array = new T[0];
            Length = 0;
            Size = 0;
        }

        public bool Contains(T item)
        {
            return array.Contains(item);
        }

        public DynamicArray<T> GetRange(int start, int count)
        {
            return new DynamicArray<T>(array[start..(start + count)]);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Length;i++)
            {
                if (array[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index,T item)
        {
            for(int i = Length; i > index; i--)
            {
                array[i] = array[i - 1];
            }
            array[index] = item;
        }

        public void InsertRange(int index , T[] items)
        {
            for(int i = length+items.Length;i > index; i--)
            {
                array[i] = array[i - items.Length];
            }
            for(int i = 0; i < items.Length; i++)
            {
                array[index + i] = items[i];
            }
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < Length - 2; i++)
            {
                array[i] = array[i + 1];
            }
            array[length - 1] = default(T);
            Length = Length - 1;
        }

        public void Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
                RemoveAt(index);
        }

        public void TrimToSize()
        {
            Array.Resize(ref array, Length);
        }

        public T[] ToArray()
        {
            TrimToSize();
            return array;
        }

        public void Sort()
        {
            Array.Sort(array);
        }

        public void Reverse()
        {
            Array.Reverse(array);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicArrayEnumerator(this.array, this.Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        private class DynamicArrayEnumerator : IEnumerator<T>
        {
            private T[] array;
            private int length;
            private int currentIndex = -1;

            public DynamicArrayEnumerator(T[] array, int length)
            {
                this.array = array;
                this.length = length;
            }

            public T Current => array[currentIndex];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                currentIndex++;
                return currentIndex < length;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}
