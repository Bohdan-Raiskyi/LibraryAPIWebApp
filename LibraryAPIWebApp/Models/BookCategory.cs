// Models/BookCategory.cs (клас для зв'язку багато-до-багатьох)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIWebApp.Models;

namespace LibraryAPIWebApp.Models
{
    public class BookCategory
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }
        public Book? Book { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
    }
}
