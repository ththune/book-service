using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Book>()
                .ToTable("Book", "dbo")
                .HasKey(book => book.BookId);

            modelBuilder.Entity<Author>()
                .ToTable("Author", "dbo")
                .HasKey(author => author.AuthorId);

            modelBuilder.Entity<BookAuthor>()
                .ToTable("BookAuthor", "dbo")
                .HasKey(bookAuthor => new { bookAuthor.BookAuthorBookId, bookAuthor.BookAuthorAuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Book)
                .WithMany(book => book.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.BookAuthorBookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(bookAuthor => bookAuthor.Author)
                .WithMany(author => author.BookAuthors)
                .HasForeignKey(bookAuthor => bookAuthor.BookAuthorAuthorId);

            modelBuilder.Entity<User>()
                .ToTable("User", "dbo")
                .HasKey(user => user.UserId);
        }
    }
}
