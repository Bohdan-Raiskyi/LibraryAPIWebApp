// Models/Book.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPIWebApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int PublishedYear { get; set; }

        public int PageCount { get; set; }

        // Зв'язки багато-до-багатьох
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
    }
}
