using System.ComponentModel.DataAnnotations;

namespace Backend.Entities {
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public required string Name { get; set; }

        // Navigation Property
        // public Movie[]? Movies { get; set; }
    }
}
