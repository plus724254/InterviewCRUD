using AutoMapper;
using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Service.Models.DTO;
using InterviewCRUD.Service.Services;
using System.Collections.Generic;
using System.Web.Http;
using AutoMap = InterviewCRUD.Tools.AutoMappers.AutoMap;

namespace InterviewCRUD.Controllers.Api
{
    [RoutePrefix("api/Student")]
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
        public IHttpActionResult AddStudent(StudentViewModel student)
        {
            _studentService.AddNewStudent(AutoMap.Mapper.Map<StudentDTO>(student));
            return Ok();
        }

        [HttpDelete]
        [Route("{number}")]
        public IHttpActionResult DeleteStudent(string number)
        {
            _studentService.DeleteStudent(number);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditStudent()
        {
            return Ok();
        }
    }
}
