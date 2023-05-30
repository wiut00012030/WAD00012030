using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Models;

namespace WAD.CW1._00012030.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetAll();
        Book GetById(int id);
        Book GetBySubject(string Subject);
        Book GetBookTrimToUpper(BookDto bookCreate);
        bool IsExist(int id);
        bool CreateBook(int authorId, Book book);
        bool UpdateBook(int authorId, Book book);
        bool DeleteBook(Book book);
        bool Save();
    }
}
