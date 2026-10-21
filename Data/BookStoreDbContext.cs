using BookStoreApi.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookStoreApi.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.BookId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
                .HasOne<Book>()
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BookId)
                .HasConstraintName("FK__Orders__BookId__4F7CD00D")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Book>()
                .Property(b => b.RowVersion)
                .IsRowVersion();
        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
