using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class DoubleLinkedList1051<T> : IEnumerable<T>
    {
        Node start;         
        public Node End { get; private set; } 
        public int Count { get; private set; }

        public void AddFirst(T value)
        {
            Node n = new Node(value);
            n.next = start;
            if (End != null) start.prev = n;
            start = n;
            if (End == null) End = n;
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
            End.next = n;
            n.prev = End;
            End = n;
            Count++;
        }

        public bool RemoveFirst()
        {
            if (start == null) return false;

            start = start.next;
            if (start == null) End = null;
            else start.prev = null;
            Count--;
            return true;
        }

        public bool RemoveLast()
        {
            if (End == null) return false;

            End = End.prev;
            if (End == null) start = null;
            else End.next = null;
            Count--;
            return true;
        }

        public bool RemoveNode(Node node)
        {
            if (node.next == null) RemoveLast();
            else if(node.prev == null) RemoveFirst();
            else
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
                Count--;
            }
            return true;
        }

        public bool GetAt(int position, out T value)
        {
            value = default(T);
            if (position < 0 || position >= Count) return false;

            Node tmp = start;
            for (int i = 0; i < position; i++)
            {
                tmp = tmp.next;
            }
            value = tmp.Value;
            return true;
        }

        public bool AddAt(int position, T value)
        {
            if (position < 0 || position > Count) return false;

            if (position == Count)
            {
                AddLast(value);
                return true;
            }
            if (position == 0)
            {
                AddFirst(value);
                return true;
            }

            Node n = new Node(value);
            Node tmp = start;
            for (int i = 0; i < position - 1; i++)
            {
                tmp = tmp.next;
            }
            n.prev = tmp;
            n.next = tmp.next;
            tmp.next.prev = n;
            tmp.next = n;
            return true;
        }

        public bool GetLastNode(out Node node)
        {
            node = End ?? default;
            return (End != null);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            Node tmp = start;
            while (tmp != null)
            {
                s.Append(tmp.Value + " ");
                tmp = tmp.next;
            }
            return s.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = start;            
            while (current != null)
            {
                yield return current.Value;
                current = current.next;
            }
        }

        public IEnumerator<T> GetEnumeratorReverse()
        {
            Node current = End;
            while (current != null)
            {
                yield return current.Value;
                current = current.prev;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Node
        {
            public T Value { get; private set; }
            internal Node next;
            internal Node prev;

            public Node(T value) => this.Value = value;
        }
    }                
}

    