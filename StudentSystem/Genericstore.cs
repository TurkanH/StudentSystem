using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StudentSystem
{
    [Serializable]
    internal class Genericstore<T> : IEnumerable<T>
        where T : class
    {
        T[] array = new T[0];

        public void Add(T entity)
        {
            //3+1=4 demeli Length 4, index ise 3 olacaq
            Array.Resize(ref array, array.Length + 1 );
            array[array.Length - 1] = entity;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= array.Length)
                return;


            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];    
            }

            Array.Resize(ref array, array.Length - 1);
        }

        public void Remove(T item)
        {
            int index= Array.IndexOf(array, item);

            RemoveAt(index);

        }

        public bool Exists(Func<T, bool> predicate)
        {
            bool hasentity = array.Any(predicate);
            return hasentity;

            //return array.Any(predicate);
            //array.Where(predicate);
        }

        public T Find(Func<T, bool> predicate)
        {
            T current = array.FirstOrDefault(predicate);
            return current;
        }

        public T[] FindAll(Func<T, bool> predicate)
        {
            T[] current = array.Where(predicate).ToArray();
            return current;
        }


        public /*int*/ T this[int index]
        {
            get
            {
                if( index < 0 || index>=array.Length )
                {
                    throw new IndexOutOfRangeException();
                }
                return array[index];
            }
        }

        public int Count
        {
            get
            {
                return array.Length;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in array)
            {
                yield return item;
            }
            //throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        { 
            //return (this as IEnumerable).GetEnumerator();
            return GetEnumerator();
        }
    }
}
