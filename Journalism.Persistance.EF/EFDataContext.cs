using Journalism.Entites.Categories;
using Journalism.Entites.Tags;
using Microsoft.EntityFrameworkCore;

namespace Journalism.Persistence.EF;


    public class EFDataContext : DbContext
    {
        public EFDataContext(string connectionString) :
            this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        { }

     
        public EFDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Tag> Tags{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(EFDataContext).Assembly);
        }
    }