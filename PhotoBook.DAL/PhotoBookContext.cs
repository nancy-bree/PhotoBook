using System.Data.Entity;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines EF DbContext for Application
    /// </summary>
    public class PhotoBookContext : DbContext
    {
        public DbSet<Photo> Photo { get; set; }

        public DbSet<Rating> Rating { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<User> User { get; set; }

        public PhotoBookContext() : base(nameOrConnectionString: "PhotoBookContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasMany(x => x.Votes)
                .WithRequired(x => x.Photo);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Votes)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photo>()
                .Property(x => x.Effect)
                .HasDatabaseGeneratedOption(0);

            modelBuilder.Entity<Photo>()
                .HasMany(x => x.Tags)
                .WithMany(y => y.Photos)
                .Map(x =>
                {
                    x.MapLeftKey("PhotoID");
                    x.MapRightKey("TagID");
                    x.ToTable("PhotoTag");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
