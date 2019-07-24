using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        public DbSet<UrlSet> UrlSets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShortenedUrl>().HasIndex(r => r.Short).IsUnique();
            builder.Entity<UrlSet>().HasIndex(r => r.Key).IsUnique();
            builder.Entity<UrlSet>().HasKey(r => r.Id);
            builder.Entity<ShortenedUrl>().HasKey(r => r.Id);
            base.OnModelCreating(builder);
        }
    }

    public class ShortenedUrl
    {
        public Guid Id { get; set; }
        public string Raw { get; set; }
        public string Short { get; set; }
    }
    public class UrlSet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ShortenedUrl Short { get; set; }
        public string Key { get; set; }
        public virtual ICollection<ShortenedUrl> Urls { get; set; }
    }
}
