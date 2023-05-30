namespace WAD.CW1._00012030.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string WriterName { get; set; }
        public DateTime BirthDate { get; set; }
        public int BooksCount { get; set; }
        public ICollection<Book> Books { get; set;}
    }
}
