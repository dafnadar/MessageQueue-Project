using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class LinkedList1051<T> : IEnumerable<T>
    {
        Node start;
        Node end;
        public int Count { get; private set; }

        public void AddFirst(T value)
        {
            Node n = new Node(value);
            n.next = start;
            start = n;
            if (end == null) end = n;
            Count++;
        }

        public void AddLast(T value)
        {
            if (start == null)
            {
                AddFirst(value);
                return;
            }

            Node n = new Node(value);
            end.next = n;
            end = n;
            Count++;
        }

        public bool RemoveFirst()
        {
            if (start == null) return false;
            else
            {
                start = start.next;
                if (start == null) end = start;
                Count--;
                return true;
            }
        }

        public bool GetAt(int index, out T item)
        {
            item = default;
            if (index < 0 || index >= Count) return false;

            Node tmp = start;
            for (int i = 0; i < index; i++)
            {
                tmp = tmp.next;
            }
            item = tmp.value;
            return true;
        }

        public override string ToString()
        {
            StringBuilder allValues = new StringBuilder();

            Node tmp = start;
            while (tmp != null)
            {
                allValues.Append(tmp.value + " ");
                tmp = tmp.next;
            }
            return allValues.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = start;
            //if (start == null) yield break;

            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        class Node
        {
            internal T value;
            internal Node next;

            public Node(T value)
            {
                this.value = value;
                next = null;
            }

            public Node(T value, Node nextVal)
            {
                this.value = value;
                next = nextVal;
            }
        }
    }
}
