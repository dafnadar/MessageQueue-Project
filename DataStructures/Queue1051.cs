using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Queue1051<T> : IEnumerable<T>
    {
        readonly LinkedList1051<T> messegesQueue = new LinkedList1051<T>();

        public void EnQueue(T item) => messegesQueue.AddLast(item);

        public bool DeQueue() => messegesQueue.RemoveFirst();

        public bool Peek(out T item) => messegesQueue.GetAt(0, out item);

        public override string ToString() => messegesQueue.ToString();

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in messegesQueue)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
