using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Interfaces;
using WAD.CW1._00012030.Models;

namespace WAD.CW1._00012030.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository,IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Book>))]
        public IActionResult GetAll()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetAll());
            if(ModelState.IsValid)
            {
                return Ok(books);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] int authorId, [FromBody] BookDto bookCreate)
        {
            if (bookCreate == null)
                return BadRequest(ModelState);

            var books = _bookRepository.GetBookTrimToUpper(bookCreate);

            if (books != null)
            {
                ModelState.AddModelError("", "Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookMap = _mapper.Map<Book>(bookCreate);
            bookMap.Author = _authorRepository.GetById(authorId);
            var bookMap2 = _mapper.Map<BookDto>(bookMap);

            if (!_bookRepository.CreateBook(authorId, bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int bookId, [FromQuery] int authorId,
            [FromBody] BookDto updateBook)
        {
            if (updateBook == null)
                return BadRequest(ModelState);

            if (bookId != updateBook.BookId)
                return BadRequest(ModelState);

            if (!_bookRepository.IsExist(bookId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookMap = _mapper.Map<Book>(updateBook);

            if (!_bookRepository.UpdateBook(authorId, bookMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int bookId)
        {
            if (!_bookRepository.IsExist(bookId))
            {
                return NotFound();
            }
            var bookToDelete = _bookRepository.GetById(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_bookRepository.DeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }
            return NoContent();
        }
    }
}
