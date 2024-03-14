using Journalism.Entites.Categories;
using Journalism.Entites.News;
using Journalism.Entites.NewsPapers;
using Journalism.Entites.PublishedNewsPaper;
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
        public DbSet<NewsPaper> NewsPapers{ get; set; }
        public DbSet<Entites.News.News> News{ get; set; }
        public DbSet<PublishedNewsPaper> PublishedNewsPapers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(EFDataContext).Assembly);
        }
    }