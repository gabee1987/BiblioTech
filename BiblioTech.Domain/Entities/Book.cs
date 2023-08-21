namespace BiblioTech.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? GoogleBooks_Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string? Publisher { get; set; }
        public int? PublishDate { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public string? Language { get; set; }
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public string? Thumbnail { get; set; }
    }
}
