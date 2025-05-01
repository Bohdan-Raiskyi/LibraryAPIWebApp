// Models/BookAuthor.cs (клас для зв'язку багато-до-багатьох)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIWebApp.Models;
using System.Text.Json.Serialization;

namespace LibraryAPIWebApp.Models
{
    public class BookAuthor
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }

        [JsonIgnore]
        public virtual Book? Book { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int AuthorId { get; set; }

        [JsonIgnore]
        public virtual Author? Author { get; set; } = null!;
    }
}
