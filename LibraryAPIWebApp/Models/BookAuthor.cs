// Models/BookAuthor.cs (клас для зв'язку багато-до-багатьох)
using LibraryAPIWebApp.Models;

namespace LibraryAPIWebApp.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
