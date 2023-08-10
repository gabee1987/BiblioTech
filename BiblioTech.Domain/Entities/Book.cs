namespace BiblioTech.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PublishDate { get; set; }
        public Genre Genre { get; set; }
        public string Language { get; set; }
        public string CoverImagePath { get; set; }
    }
}
