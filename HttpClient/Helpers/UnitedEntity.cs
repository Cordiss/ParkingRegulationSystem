namespace HttpClient.Helpers
{
    /// <summary>
    /// Defines united entity for two entities.
    /// </summary>
    /// <typeparam name="T">Instance of first entity.</typeparam>
    /// <typeparam name="E">Instance of second entity.</typeparam>
    public class UnitedEntity<T, E>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entityOne">Instance of first entity.</param>
        /// <param name="entityTwo">Instance of second entity.</param>
        public UnitedEntity(T entityOne, E[] entityTwo)
        {
            EntityOne = entityOne;
            EntityTwo = entityTwo;
        }

        /// <summary>
        /// Gets first entity instance.
        /// </summary>
        public T EntityOne { get; }

        /// <summary>
        /// Gets array of second entities.
        /// </summary>
        public E[] EntityTwo { get; }
    }
}