using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionController
    {
        private readonly ICourseLibraryRepository _courseLibaryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibaryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
      //  [HttpGet("({ids})", Name = "GetAuthorCollection")]
      //  public IActionResult GetAuthorCollection(
      //[FromRoute]
      //  [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
      //  {
      //      if (ids == null)
      //      {
      //          return BadRequest();
      //      }

      //      var authorEntities = _courseLibaryRepository.GetAuthors(ids);

      //      if (ids.Count() != authorEntities.Count())
      //      {
      //          return NotFound();
      //      }

      //      var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

      //      return Ok(authorsToReturn);
      //  }


    //    [HttpPost]
    //    public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorCreationDto> authorCollection)
    //    {
    //        var authorEntites = _mapper.Map<IEnumerable<Entities.Author>>(authorCollection);
    //        foreach(var author in authorEntites)
    //        {
    //            _courseLibaryRepository.AddAuthor(author);
    //        }
    //        _courseLibaryRepository.Save();
    //        return Ok();
    //    }

    }
}
