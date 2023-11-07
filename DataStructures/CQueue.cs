using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class CQueue<T> : IEnumerable<T>
    {
        protected List<T> list = new List<T>();
        public QueueIteratorBehavior queueIteratorBehavior { get; set; } = QueueIteratorBehavior.PeekWhenIterate;

        public CQueue(List<T> list, QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
        {
            this.list = list;
            this.queueIteratorBehavior = queueIteratorBehavior;
        }

        public CQueue(QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
        {
            this.queueIteratorBehavior = queueIteratorBehavior;
        }


        public T Dequeue()
        {
            T item = Peek();
            list.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            return list[0];
        }

        public void Enqueue(T item)
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

        public T[] CopyTo(T[] arr, int index = 0)
        {
            for (int i = 0; i <= list.Count - 1 && index < arr.Length; i++)
            {
                arr[index] = list[i];
                index++;
            }
            return arr;
        }

        public void PeekWhenIterate()
        {
            queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate;
        }
        public void DequeueWhenIterate()
        {
            queueIteratorBehavior = QueueIteratorBehavior.DequeueWhenIterate;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator(list, queueIteratorBehavior);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class QueueEnumerator : IEnumerator<T>
        {
            private List<T> list = new List<T>();
            private int currentIndex = -1;
            public QueueIteratorBehavior queueIteratorBehavior { get; set; } = QueueIteratorBehavior.PeekWhenIterate;
            public QueueEnumerator(List<T> list, QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
            {
                this.list = list;
                this.queueIteratorBehavior = queueIteratorBehavior;
            }

            public T Current => list[currentIndex];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (queueIteratorBehavior == QueueIteratorBehavior.DequeueWhenIterate && currentIndex >= -1)
                {
                    list.RemoveAt(0);
                    return currentIndex >= -1;
                }
                currentIndex++;
                return currentIndex >= -1;
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
    public enum QueueIteratorBehavior
    {
        DequeueWhenIterate,
        PeekWhenIterate
    }

}
