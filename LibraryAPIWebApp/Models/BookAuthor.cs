// Models/BookAuthor.cs (клас для зв'язку багато-до-багатьох)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIWebApp.Models;

namespace LibraryAPIWebApp.Models
{
    public class BookAuthor
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Key]
        [Column(Order = 1)]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
