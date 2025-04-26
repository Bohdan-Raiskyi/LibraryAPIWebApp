using Microsoft.EntityFrameworkCore;

namespace LibraryAPIWebApp.Models
    {
        public class LibraryAPIContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<BookAuthor> BookAuthors { get; set; }
            public DbSet<BookCategory> BookCategories { get; set; }
            public DbSet<UserBook> UserBooks { get; set; }

            public LibraryAPIContext(DbContextOptions<LibraryAPIContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Налаштування складеного ключа для BookAuthor
                modelBuilder.Entity<BookAuthor>()
                    .HasKey(ba => new { ba.BookId, ba.AuthorId });

                // Налаштування складеного ключа для BookCategory
                modelBuilder.Entity<BookCategory>()
                    .HasKey(bc => new { bc.BookId, bc.CategoryId });

                // Налаштування складеного ключа для UserBook
                modelBuilder.Entity<UserBook>()
                    .HasKey(ub => new { ub.BookId, ub.UserId });

            base.OnModelCreating(modelBuilder);
            }
        }
    }