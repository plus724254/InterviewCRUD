using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Repository.Models.CustomExceptions;
using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Service.Services;
using InterviewCRUD.Tools;
using InterviewCRUD.Tools.AutoMappers;
using System;
using System.Web.Http;

namespace InterviewCRUD.Controllers.Api
{
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IHttpActionResult GetCourses()
        {
            return Ok(_courseService.GetAllCourses());
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetCourse(string number)
        {
            return Ok(new CourseViewModel());
        }

        [HttpPost]
        public IHttpActionResult AddCourse(CourseViewModel course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            try
            {
                _courseService.AddNewCourse(AutoMap.Mapper.Map<CourseDTO>(course));
                return Ok();
            }
            catch(DataErrorException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{number}")]
        public IHttpActionResult DeleteCourse(string number)
        {
            _courseService.DeleteCourse(number);
            return Ok();
        }

        [HttpPut, Route("{number}")]
        public IHttpActionResult ReplaceCourse(string number, CourseViewModel editCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            try
            {
                _courseService.ReplaceCourse(number, AutoMap.Mapper.Map<CourseDTO>(editCourse));
                return Ok();
            }
            catch(DataErrorException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IHttpActionResult EditCourse(CourseViewModel editCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            _courseService.EditCourse(AutoMap.Mapper.Map<CourseDTO>(editCourse));
            return Ok();
        }
    }
}