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

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomQueue()
        {
            _queue = new Queue<T>();
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