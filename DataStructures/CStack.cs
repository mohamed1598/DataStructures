using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class CStack<T>:IEnumerable<T>
    {
        List<T> list = new List<T>();

        public CStack(List<T> list, CStack<T>.IteratorBehavior iteratorBehavior = IteratorBehavior.PeekWhenIterate)
        {
            this.list = list;
            this.iteratorBehavior = iteratorBehavior;
        }

        public CStack(CStack<T>.IteratorBehavior iteratorBehavior = IteratorBehavior.PeekWhenIterate)
        {
            this.iteratorBehavior = iteratorBehavior;
        }

        public IteratorBehavior iteratorBehavior { get; set; } = IteratorBehavior.PeekWhenIterate;

        public T Pop()
        {
            T item = Peek();
            list.RemoveAt(list.Count - 1);
            return item;
        } 

        public T Peek()
        {
            return list[list.Count - 1];
        }

        public void Push(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public T[] ToArray()
        {
            return list.ToArray();
        }

        public List<T> ToList()
        {
            return list;
        }

        public T[] CopyTo(T[] arr,int index = 0)
        {
            for(int i = list.Count - 1; i >= 0 && index < arr.Length; i--)
            {
                arr[index] = list[i];
                index++;
            }
            return arr;
        }

        public void PeekWhenIterate()
        {
            iteratorBehavior = IteratorBehavior.PeekWhenIterate;
        }
        public void PopWhenIterate()
        {
            iteratorBehavior = IteratorBehavior.PopWhenIterate;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator(list, iteratorBehavior);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class StackEnumerator : IEnumerator<T>
        {
            private List<T> list = new List<T>();
            private int currentIndex =  - 1;
            public IteratorBehavior iteratorBehavior { get; set; } = IteratorBehavior.PeekWhenIterate;
            public StackEnumerator(List<T> list, IteratorBehavior iteratorBehavior = IteratorBehavior.PeekWhenIterate)
            {
                this.list = list;
                this.iteratorBehavior = iteratorBehavior;
                currentIndex = list.Count - 1;
            }

            public T Current => list[currentIndex];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if(iteratorBehavior == IteratorBehavior.PopWhenIterate && currentIndex >= 0)
                {
                    list.RemoveAt(currentIndex);
                }
                currentIndex--;
                return currentIndex >= -1;
            }

            public void Reset()
            {
                currentIndex = list.Count - 1;
            }

            public void Dispose()
            {
            }
        }

        public enum IteratorBehavior
        {
            PopWhenIterate,
            PeekWhenIterate
        }
    }
}
