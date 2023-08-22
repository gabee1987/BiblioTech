using System.ComponentModel.DataAnnotations;

namespace BiblioTech.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required( ErrorMessage = "Author name is required." )]
        [StringLength( 255, ErrorMessage = "Author name should not be longer than 255 characters." )]
        public string Name { get; set; }
    }
}
