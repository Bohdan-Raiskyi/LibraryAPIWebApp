// Models/Book.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPIWebApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int PublishedYear { get; set; }

        public int PageCount { get; set; }

        // Зв'язки багато-до-багатьох
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    }
}
