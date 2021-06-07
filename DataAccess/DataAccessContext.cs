using System.Data.Entity;

using DataAccess.Models;

namespace DataAccess
{
    /// <summary>
    /// Defines database context.
    /// </summary>
    public sealed class DataAccessContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        static DataAccessContext()
        {
            Database.SetInitializer<DataAccessContext>(null);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public DataAccessContext(string connectionString)
            : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        /// <summary>
        /// Reference to <see cref="Decrees"/> entity.
        /// </summary>
        public DbSet<Decree> Decrees { get; set; }

        /// <summary>
        /// Reference to <see cref="Car"/> entity.
        /// </summary>
        public DbSet<Car> Car { get; set; }

        /// <summary>
        /// Reference to <see cref="Photo"/> entity.
        /// </summary>
        public DbSet<Photo>  Photo { get; set; }
    }
}