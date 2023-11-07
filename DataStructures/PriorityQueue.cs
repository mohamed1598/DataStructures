using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DataStructures;

namespace DataStructures
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        protected List<PQItem<T>> list = new List<PQItem<T>>();
        public QueueIteratorBehavior queueIteratorBehavior { get; set; } = QueueIteratorBehavior.PeekWhenIterate;
        public PriorityQueue(List<T> list, QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
        {
            this.list = list.Select( i => new PQItem<T>() { Priority = 0 , Item  = i}).ToList();
            this.queueIteratorBehavior = queueIteratorBehavior;
        }

        public PriorityQueue(List<PQItem<T>> list, QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
        {
            this.list = list;
            this.queueIteratorBehavior = queueIteratorBehavior;
        }

        public PriorityQueue(QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
        {
            this.queueIteratorBehavior = queueIteratorBehavior;
        }
        public (T item , int priority) Dequeue()
        {
            int peekIndex = getPeekIndex();
            PQItem<T> item = list[peekIndex];
            list.RemoveAt(peekIndex);
            return (item.Item,item.Priority);
        }
        public (T item, int priority) Peek()
        {
            int peekIndex = getPeekIndex();
            return (list[peekIndex].Item,list[peekIndex].Priority); 
        }
        private int getPeekIndex()
        {
            int priorityIndex = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Priority > list[priorityIndex].Priority)
                {
                    priorityIndex = i;
                }
            }
            return priorityIndex;
        }

        public void Enqueue(T item)
        {
            list.Add(new PQItem<T>() { Priority = 0 , Item = item });
        }

        public void Enqueue(PQItem<T> item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(PQItem<T> item)
        {
            return list.Contains(item);
        }

        public T[] ToArray()
        {
            return list.OrderBy(e => e.Priority).Select( i => i.Item).ToArray();
        }

        public List<T> ToList()
        {
            return list.OrderByDescending(e => e.Priority).Select(i => i.Item).ToList();
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
            return new PriorityQueueEnumerator(list, queueIteratorBehavior);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class PriorityQueueEnumerator : IEnumerator<T>
        {
            private List<T> list = new List<T>();
            private List<PQItem<T>> originalList = new List<PQItem<T>> ();
            private int currentIndex = -1;
            public QueueIteratorBehavior queueIteratorBehavior { get; set; } = QueueIteratorBehavior.PeekWhenIterate;
            public PriorityQueueEnumerator(List<PQItem<T>> list, QueueIteratorBehavior queueIteratorBehavior = QueueIteratorBehavior.PeekWhenIterate)
            {
                originalList = list.OrderByDescending(e => e.Priority).ToList();
                this.list = list.Select(i => i.Item).ToList();
                this.queueIteratorBehavior = queueIteratorBehavior;
            }

            public T Current => list[currentIndex];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (queueIteratorBehavior == QueueIteratorBehavior.DequeueWhenIterate && currentIndex >= -1)
                {
                    list.RemoveAt(0);
                    originalList.RemoveAt(0);
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
    public struct PQItem<T>
    {
        public int Priority { get; set; }
        public T Item { get; set; }
    }

}
