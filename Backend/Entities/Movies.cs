using System.ComponentModel.DataAnnotations;

namespace Backend.Entities {
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal Price { get; set; }

        // Navigation Property
        public Genre? Genre { get; set; }

        // Foreign Key
        public int GenreId { get; set; }

        public DateOnly ReleaseDate { get; set; }
    }
}
