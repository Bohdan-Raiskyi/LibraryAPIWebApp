// Models/BookCategory.cs (клас для зв'язку багато-до-багатьох)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIWebApp.Models;
using System.Text.Json.Serialization;

namespace LibraryAPIWebApp.Models
{
    public class BookCategory
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }

        [JsonIgnore]
        public virtual Book? Book { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; } = null!;
    }
}
