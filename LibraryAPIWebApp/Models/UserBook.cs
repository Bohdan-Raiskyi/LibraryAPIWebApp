// Models/UserBook.cs (клас для зв'язку багато-до-багатьох)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAPIWebApp.Models;
using System.Text.Json.Serialization;

namespace LibraryAPIWebApp.Models
{
    public class UserBook
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; } = null!;

        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }

        [JsonIgnore]
        public virtual Book? Book { get; set; } = null!;
    }
}
