using System.Collections.Generic;

namespace _14._10
{
    public class PriorityQueue
    {
        private int total_size;
        readonly SortedDictionary<int, Queue<int>> storage;

        public PriorityQueue()
        {
            storage = new SortedDictionary<int, Queue<int>>();
            total_size = 0;
        }

        public bool IsEmpty()
        {
            return total_size == 0;
        }

        public int? Dequeue()
        {
            if ( IsEmpty() )
                return null;
            else
                foreach ( Queue<int> queue in storage.Values )
                {
                    if ( queue.Count > 0 )
                    {
                        total_size--;
                        return queue.Dequeue();
                    }
                }

            return null;
        }

        public object Dequeue( int item )
        {
            total_size--;

            return storage[ item ].Dequeue();
        }

        public void Enqueue( int item )
        {
            if ( !storage.ContainsKey( item ) )
            {
                storage.Add( item, new Queue<int>() );
            }
            storage[ item ].Enqueue( item );
            total_size++;
        }
    }
}