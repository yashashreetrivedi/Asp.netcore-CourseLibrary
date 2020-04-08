using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibaryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibaryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        [HttpGet()]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesFromAuthors(Guid authorId)
        {
           if(!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorFromRepo = _courseLibaryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courseForAuthorFromRepo));
        }

        [HttpGet("{courseId}", Name ="GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseLibaryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseCreationDto course)
        {
            if(!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Entities.Course>(course);
            _courseLibaryRepository.AddCourse(authorId, courseEntity);
            _courseLibaryRepository.Save();
            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor", new { authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);


        }

        [HttpPut("{courseId}")]
        public ActionResult UpdateCourseForAuthor(Guid authorId , 
            Guid courseId, 
            CourseForUpdateDto courseForUpdateDto)
        {
            if(!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorFromRepo = _courseLibaryRepository.GetCourse(authorId, courseId);
            if(courseForAuthorFromRepo == null)
            {
                var courseToAdd = _mapper.Map<Entities.Course>(courseForUpdateDto);
                courseToAdd.Id = courseId;
                _courseLibaryRepository.AddCourse(authorId, courseToAdd);
                _courseLibaryRepository.Save();
            }

            _mapper.Map(courseForUpdateDto, courseForAuthorFromRepo);
            _courseLibaryRepository.UpdateCourse(courseForAuthorFromRepo);
            _courseLibaryRepository.Save();
            return NoContent();

        }

        [HttpPatch("{couseId}")]
        public ActionResult PartiallyUpdateCourseForAuthore(Guid authorId , Guid courseId, 
            JsonPatchDocument<CourseForUpdateDto> patchdocument)
        {
            if (!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorFromRepo = _courseLibaryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }
            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseForAuthorFromRepo);
            patchdocument.ApplyTo(courseToPatch);
            _mapper.Map(courseToPatch, courseForAuthorFromRepo);
            _courseLibaryRepository.UpdateCourse(courseForAuthorFromRepo);
            _courseLibaryRepository.Save();
            return NoContent();

        }
        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourseForAuthor(Guid authorId , Guid courseId)
        {
            if (!_courseLibaryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorFromRepo = _courseLibaryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }
            _courseLibaryRepository.DeleteCourse(courseForAuthorFromRepo);
            _courseLibaryRepository.Save();
            return NoContent();
        }
    }
}
