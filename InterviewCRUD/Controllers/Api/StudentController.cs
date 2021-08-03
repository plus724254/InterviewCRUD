using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Service.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace InterviewCRUD.Controllers.Api
{
    //[RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        //[Route("")]
        public IHttpActionResult GetStudents()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetStudent()
        {
            return Ok(new StudentViewModel());
        }

        [HttpPost]
        public IHttpActionResult AddStudent()
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent()
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditStudent()
        {
            return Ok();
        }
    }
}
