using System.ComponentModel.DataAnnotations;

namespace BiblioTech.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? GoogleBooks_Id { get; set; }

        [Required( ErrorMessage = "Title is required." )]
        [StringLength( 255, ErrorMessage = "Title should not be longer than 255 characters." )]
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public List<AuthorDTO> Authors { get; set; }

        [StringLength( 10, MinimumLength = 10, ErrorMessage = "ISBN10 should be exactly 10 characters." )]
        public string? ISBN10 { get; set; }

        [StringLength( 13, MinimumLength = 13, ErrorMessage = "ISBN13 should be exactly 13 characters." )]
        public string? ISBN13 { get; set; }
        public string Publisher { get; set; }
        public int? PublishDate { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }

        [Url( ErrorMessage = "Invalid URL format for Cover Image Path." )]
        public string? CoverImagePath { get; set; }

        [Url( ErrorMessage = "Invalid URL format for Thumbnail." )]
        public string? Thumbnail { get; set; }
    }
}
