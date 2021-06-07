using System;
using System.Collections.Generic;
using HttpClient.Events;

namespace HttpClient.Helpers
{
    /// <summary>
    /// Defines custom queue that notifies when data was changed inside.
    /// </summary>
    /// <typeparam name="T">Entity.</typeparam>
    public class CustomQueue<T>
    {
        /// <summary>
        /// Event that notifies that data was changed.
        /// </summary>
        public event EventHandler<CollectionModifiedEventArgs<T>> CollectionModified;

        /// <summary>
        /// Synchronization object for safely access to queue.
        /// </summary>
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Local <see cref="Queue{T}"/> instance.
        /// </summary>
        private readonly Queue<T> _queue;

        private readonly int? _maxEntities;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomQueue(int? maxEntities = null)
        {
            _queue = new Queue<T>();
            _maxEntities = maxEntities;
        }

        public bool IsEmpty
        {
            get
            {
                lock (_syncRoot)
                {
                    return _queue.Count <= 0;
                }
            }
        }

        /// <summary>
        /// Adds entity to queue.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public void Enqueue(T entity)
        {
            lock (_syncRoot)
            {
                _queue.Enqueue(entity);

                if (_maxEntities.HasValue)
                {
                    if (_maxEntities.Value == _queue.Count)
                    {
                        CollectionModified?.Invoke(this, new CollectionModifiedEventArgs<T>(entity));
                        return;
                    }
                }

                CollectionModified?.Invoke(this, new CollectionModifiedEventArgs<T>(entity));
            }
        }

        /// <summary>
        /// Removes entity from queue.
        /// </summary>
        /// <returns>Entity object.</returns>
        public T Dequeue()
        {
            lock (_syncRoot)
            {
                return _queue.Dequeue();
            }
        }
    }
}