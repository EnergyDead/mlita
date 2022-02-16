using System.Collections.Generic;

namespace _14._10
{
    public class PriorityQueue
    {
        private int _totalSize;
        private readonly SortedDictionary<int, Queue<int>> _storage;

        public PriorityQueue()
        {
            _storage = new SortedDictionary<int, Queue<int>>();
            _totalSize = 0;
        }

        public bool IsEmpty()
        {
            return _totalSize == 0;
        }

        public int? Dequeue()
        {
            if ( IsEmpty() )
                return null;
            else
                foreach ( Queue<int> queue in _storage.Values )
                {
                    if ( queue.Count > 0 )
                    {
                        _totalSize--;
                        return queue.Dequeue();
                    }
                }

            return null;
        }

        public object Dequeue( int item )
        {
            _totalSize--;

            return _storage[ item ].Dequeue();
        }

        public void Enqueue( int item )
        {
            if ( !_storage.ContainsKey( item ) )
            {
                _storage.Add( item, new Queue<int>() );
            }
            _storage[ item ].Enqueue( item );
            _totalSize++;
        }
    }
}