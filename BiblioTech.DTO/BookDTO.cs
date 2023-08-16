namespace BiblioTech.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PublishDate { get; set; }
        public GenreDTO Genre { get; set; }
        public string Language { get; set; }
        public string CoverImagePath { get; set; }
    }
}
