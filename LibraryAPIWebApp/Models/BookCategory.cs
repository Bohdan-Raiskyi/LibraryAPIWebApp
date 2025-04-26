// Models/BookCategory.cs (клас для зв'язку багато-до-багатьох)
using LibraryAPIWebApp.Models;

namespace LibraryAPIWebApp.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
