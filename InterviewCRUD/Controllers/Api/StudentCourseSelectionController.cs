using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace InterviewCRUD.Controllers.Api
{
    [RoutePrefix("api/StudentCourseSelection")]
    public class StudentCourseSelectionController : ApiController
    {
        private readonly IStudentService _studentService;
        public StudentCourseSelectionController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IHttpActionResult GetAllStudentCourseSelection()
        {
            return Ok(_studentService.GetAllStudentCourses());
        }
        [HttpGet]
        [Route("{studentNumber}")]
        public IHttpActionResult GetStudentCourseSelection(string studentNumber)
        {
            return Ok(_studentService.GetStudentCourses(studentNumber));
        }

        [HttpPost]
        public IHttpActionResult AddStudentCourseSelection(StudentCourseSelectionDTO studentCourseSelectionDTO)
        {
            _studentService.AddStudentCourse(studentCourseSelectionDTO);
            return Ok();
        }

        [HttpDelete]
        [Route("{studentNumber}")]
        public IHttpActionResult DeleteStudentCourses(string studentNumber)
        {

            _studentService.DeleteStudentCourses(studentNumber);
            return Ok();
        }
    }
}