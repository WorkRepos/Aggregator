using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Entities;

namespace NewsAggregator.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RssUrl> RssUrls { get; set; }
        public DbSet<News> News { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RssUrl>(x =>
            {
                x.ToTable("RssUrl");
                x.HasKey(_ => _.Id);
                x.HasAlternateKey(_ => _.Url);
                x.Property(_ => _.Url).IsRequired();
                x.Property(_ => _.PublicationDate);
            });

            builder.Entity<News>(x =>
            {
                x.ToTable("News");
                x.HasKey(_ => _.Id);
                x.Property(_ => _.Title).IsRequired();
                x.Property(_ => _.Link).IsRequired();
                x.HasOne(_ => _.RssUrl).WithMany(_ => _.News).HasForeignKey(_ => _.RssUrlId);
            });
        }
    }
}
