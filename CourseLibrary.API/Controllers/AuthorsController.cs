using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.API.Helpers;
using AutoMapper;
using CourseLibrary.API.ResourceParameter;

namespace CourseLibrary.API.Controllers
{
    
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibaryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibaryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors( AuthorsResourcesParameter authorsResourcesParameter)
        {
            var authorsFormRepo = _courseLibaryRepository.GetAuthors(authorsResourcesParameter);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFormRepo));

        }

        [HttpGet("{authorId}", Name ="GetAuthor")]
        public IActionResult GetAuthorById(Guid authorId) {
            var authorFormRepo = _courseLibaryRepository.GetAuthor(authorId);
            if (authorFormRepo == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<AuthorDto>(authorFormRepo));
        }
        
        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorCreationDto author)
        {
            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courseLibaryRepository.AddAuthor(authorEntity);
                _courseLibaryRepository.Save();
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { authorId = authorToReturn.Id }, authorToReturn);

        }


        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
