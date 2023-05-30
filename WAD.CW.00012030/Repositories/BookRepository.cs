using AutoMapper;
using WAD.CW1._00012030.Data;
using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Interfaces;
using WAD.CW1._00012030.Models;

namespace WAD.CW1._00012030.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public BookRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public ICollection<Book> GetAll()
        {
            return _dataContext.Books.OrderBy(p => p.BookId).ToList();
        }

        public Book GetById(int id)
        {
            return _dataContext.Books.Where(b => b.BookId == id).FirstOrDefault();
        }

        public Book GetBySubject(string Subject)
        {
            return _dataContext.Books.Where(b => b.Subject == Subject).FirstOrDefault();
        }

        public bool IsExist(int id)
        {
            return _dataContext.Books.Where(b => b.BookId == id).Any();
        }

        public bool CreateBook(int authorId, Book book)
        {
           // var bookAuthorEntity = _dataContext.Books.Where(a => a.BookId == authorId).FirstOrDefault();
            _dataContext.Add(book);
            return Save();
        }
        public Book GetBookTrimToUpper(BookDto pokemonCreate)
        {
            return GetAll().Where(c => c.Subject.Trim().ToUpper() == pokemonCreate.Subject.TrimEnd().ToUpper())
                .FirstOrDefault();
        }
        public bool UpdateBook(int authorId, Book book)
        {
            _dataContext.Update(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _dataContext.Remove(book);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
