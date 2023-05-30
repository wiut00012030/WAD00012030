namespace WAD.CW1._00012030.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Subject { get; set; }
        public string Explanation { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
