// Models/Author.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPIWebApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Зв'язок багато-до-багатьох з Books через BookAuthors
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
