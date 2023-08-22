using System.ComponentModel.DataAnnotations;

namespace BiblioTech.Models
{
    public class BookSearchModel
    {
        [Required]
        [StringLength( 100, MinimumLength = 3, ErrorMessage = "Query should be between 3 and 100 characters." )]
        public string Query { get; set; }
    }
}
