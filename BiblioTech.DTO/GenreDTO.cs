using System.ComponentModel.DataAnnotations;

namespace BiblioTech.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }

        [Required( ErrorMessage = "Genre name is required." )]
        [StringLength( 255, ErrorMessage = "Genre name should not be longer than 255 characters." )]
        public string Name { get; set; }
    }
}
