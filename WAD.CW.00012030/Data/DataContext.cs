using Microsoft.EntityFrameworkCore;
using WAD.CW1._00012030.Models;

namespace WAD.CW1._00012030.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne<Author>(t => t.Author)
                .WithMany(s => s.Books) 
                .HasForeignKey(t => t.AuthorId);
        }
    }
}
