using System.Data.Entity;

using DataAccess.Models;

namespace DataAccess
{
    public sealed class DataAccessContext : DbContext
    {
        static DataAccessContext()
        {
            Database.SetInitializer<DataAccessContext>(null);
        }

        public DataAccessContext(string connectionString)
            : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Decree> Decrees { get; set; }

        public DbSet<Car> Car { get; set; }

        public DbSet<Photo>  Photo { get; set; }
    }
}