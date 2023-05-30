using AutoMapper;
using WAD.CW1._00012030.Data;
using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Interfaces;
using WAD.CW1._00012030.Models;


namespace WAD.CW1._00012030.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public AuthorRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public ICollection<Author> GetAll()
        {
            return _dataContext.Author.ToList();
        }

        public Author GetById(int id)
        {
            return _dataContext.Author.Where(b => b.AuthorId == id).FirstOrDefault();
        }

        public Author GetByName(string name)
        {
            return _dataContext.Author.Where(b => b.WriterName == name).FirstOrDefault();
        }

        public bool IsExist(int id)
        {
            return _dataContext.Author.Where(b => b.AuthorId == id).Any();
        }

        public bool CreateAuthor(Author author)
        {
            //var authorsBookEntity = _dataContext.Author.Where(a => a.AuthorId == bookId).FirstOrDefault();
            _dataContext.Add(author);
            return Save();
        }
        public Author GetAuthorTrimToUpper(AuthorDto authorCreate)
        {
            return GetAll().Where(c => c.WriterName.Trim().ToUpper() == authorCreate.WriterName.TrimEnd().ToUpper())
                .FirstOrDefault();
        }
        public bool UpdateAuthor(Author author)
        {
            _dataContext.Update(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _dataContext.Remove(author);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
