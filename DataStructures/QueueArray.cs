using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1051DS
{
    public class QueueArray<T> : IEnumerable<T>
    {
        T[] queueArray;
        int headInd;
        int tailInd;
        //int count

        public QueueArray(int capacity)
        {
            if (capacity <= 1) throw new ArgumentOutOfRangeException("incorrect size...");
            queueArray = new T[capacity];
            headInd = tailInd = -1;
        }


        public bool EnQueue(T itemInInsert)
        {
            if (IsFull()) return false;

            if (IsEmpty()) headInd = 0;// both equals 0, first item inserted
            tailInd = CircleIncrementIndex(tailInd);
            queueArray[tailInd] = itemInInsert;
         
            return true;
        }

        private int CircleIncrementIndex(int index) => (index + 1) % queueArray.Length;

        public bool DeQueue(out T deletedItem)
        {
            if (IsEmpty())
            {
                deletedItem = default;
                return false;
            }


            deletedItem = queueArray[headInd];
            if (headInd == tailInd) // remove the last and the ONLY item from queue
            {               
                headInd = tailInd = -1;
                return true;
            }
            
            headInd = CircleIncrementIndex(headInd);
            return true;
        }      

        public bool Peek(out T item)
        {
            if (IsEmpty())
            {
                item = default;
                return false;
            }

            item = queueArray[headInd];          
            return true;
        }

        public bool IsEmpty() => tailInd == -1; 
       
        public bool IsFull() => CircleIncrementIndex(tailInd) == headInd;

        //ToString OR IEnumerable

        public override string ToString() // Yuri !!!!
        {
            StringBuilder sb = new StringBuilder();
            if (IsEmpty()) return "";

            int i = headInd;
            do 
            {
                sb.AppendLine(queueArray[i].ToString());
                i = CircleIncrementIndex(i);
            } while (i != CircleIncrementIndex(tailInd));        

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty()) yield break;

            int i = headInd;
            do
            {
                // sb.AppendLine(queueArray[i].ToString());
                yield return queueArray[i];
                i = CircleIncrementIndex(i);
            } while (i != CircleIncrementIndex(tailInd));           
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
