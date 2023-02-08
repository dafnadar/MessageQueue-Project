using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class HashTable1051<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        LinkedList<Data>[] hashArray;
        public int ItemsCount { get; set; } = 0;


        public HashTable1051(int capacity = 100) => hashArray = new LinkedList<Data>[capacity];

        public void Add(TKey key, TValue value)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] == null) hashArray[ind] = new LinkedList<Data>();
            else
            {
                if (ContainsKey(key))
                    throw new ArgumentException($"An item with the same key: {key} has already been added.");
            }

            hashArray[ind].AddLast(new Data(key, value));
            ItemsCount++;

            if (ItemsCount > hashArray.Length)
            {
                ReHash();
            }
        }

        private void ReHash()
        {
            var tmp = hashArray;
            hashArray = new LinkedList<Data>[hashArray.Length * 2];
            ItemsCount = 0;

            foreach (var list in tmp)
            {
                if (list != null)
                {
                    foreach (Data keyValueItem in list)
                    {
                        Add(keyValueItem.key, keyValueItem.value);
                    }
                }
            }
        }

        public double CalcAverLoad() => hashArray.Where(lst => lst != null).Average(lst => lst.Count);

        public TValue GetValue(TKey key)
        {
            int ind = KeyToIndex(key);
            Data keyValue;
            if (hashArray[ind] != null)
            {
                keyValue = hashArray[ind].FirstOrDefault(item => item.key.Equals(key));
                if (keyValue != null) return keyValue.value;
            }            
            return default;
        }

        public bool ContainsKey(TKey key)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] == null) return false;
            return hashArray[ind].Any(item => item.key.Equals(key));
        }

        private int KeyToIndex(TKey key)
        {
            int calcRes = Math.Abs(key.GetHashCode());
            return calcRes % hashArray.Length;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in hashArray)
            {
                if (list != null)
                    foreach (Data data in list)
                        yield return new KeyValuePair<TKey, TValue>(data.key, data.value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TValue this[TKey key] => GetValue(key);


        class Data
        {
            internal TKey key;
            internal TValue value;

            public Data(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}
