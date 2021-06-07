using System;

namespace HttpClient.Events
{
    /// <summary>
    /// Defines event arguments for CustomQueue.
    /// </summary>
    /// <typeparam name="T">Entity instance which was modified.</typeparam>
    public class CollectionModifiedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">Entity instance which was modified.</param>
        public CollectionModifiedEventArgs(T entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets entity object.
        /// </summary>
        public T Entity { get; }
    }
}