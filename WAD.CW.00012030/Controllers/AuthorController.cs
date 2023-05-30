using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAD.CW1._00012030.Dto;
using WAD.CW1._00012030.Interfaces;
using WAD.CW1._00012030.Models;
using WAD.CW1._00012030.Repositories;

namespace WAD.CW1._00012030.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
              _authorRepository = authorRepository;
              _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Author>))]
        public IActionResult GetAll()
        {
            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAll());
            if (ModelState.IsValid)
            {
                return Ok(authors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] int authorId, [FromBody] AuthorDto authorCreate)
        {
            if (authorCreate == null)
                return BadRequest(ModelState);

            var authors = _authorRepository.GetAuthorTrimToUpper(authorCreate);

            if (authors != null)
            {
                ModelState.AddModelError("", "Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authorMap = _mapper.Map<Author>(authorCreate);


            if (!_authorRepository.CreateAuthor(authorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int authorId,
            [FromBody] AuthorDto updateAuthor)
        {
            if (updateAuthor == null)
                return BadRequest(ModelState);

            if (authorId != updateAuthor.AuthorId)
                return BadRequest(ModelState);

            if (!_authorRepository.IsExist(authorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var authorMap = _mapper.Map<Author>(updateAuthor);

            if (!_authorRepository.UpdateAuthor(authorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!_authorRepository.IsExist(authorId))
            {
                return NotFound();
            }
            var authorToDelete = _authorRepository.GetById(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authorRepository.DeleteAuthor(authorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }

    }
}
