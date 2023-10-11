using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Resturant.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using Microsoft.EntityFrameworkCore;


namespace Resturant.Identity
{
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(
          DbConnection existingConnection,
          string defaultSchema)
        : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
        }
    }

    public class MySqlConfiguration : DbConfiguration
    {
        public MySqlConfiguration()
        {
            SetHistoryContext("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }

    public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();
            }
            else
            {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'IdentityMySQLDatabase' AND table_name = '__MigrationHistory'");

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
        }
    }

    public class MySqlDbContext : DbContext
    {
        //public Microsoft.EntityFrameworkCore.DbSet<string> DisplayName { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<string> Phone { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseMySql("SERVER=sql.anders-jensen.dk; uid=G9WT; password=7niJwCe#XsyH!y2M4Fgw; database=resturant;", new MySqlServerVersion(new Version(8, 0, 34)));
    }

}