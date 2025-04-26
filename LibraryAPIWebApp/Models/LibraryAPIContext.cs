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
        }
    }