namespace BiblioTech.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? GoogleBooks_Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public List<AuthorDTO> Authors { get; set; }
        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string Publisher { get; set; }
        public int? PublishDate { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public string? Thumbnail { get; set; }
    }
}
