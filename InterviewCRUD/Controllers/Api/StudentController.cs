using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Repository.Models.CustomExceptions;
using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Service.Services;
using InterviewCRUD.Tools;
using System;
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
        public IHttpActionResult GetStudents()
        {
            return Ok(_studentService.GetAllStudents());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetStudent(string number)
        {
            return Ok(new StudentViewModel());
        }

        [HttpPost]
        public IHttpActionResult AddStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            try
            {
                _studentService.AddNewStudent(AutoMap.Mapper.Map<StudentDTO>(student));
                return Ok();
            }
            catch(DataErrorException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{number}")]
        public IHttpActionResult DeleteStudent(string number)
        {

            _studentService.DeleteStudent(number);
            return Ok();
        }

        [HttpPut]
        [Route("{number}")]
        public IHttpActionResult ReplaceStudent(string number, StudentViewModel editStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            try
            {
                _studentService.ReplaceStudent(number, AutoMap.Mapper.Map<StudentDTO>(editStudent));
                return Ok();
            }
            catch (DataErrorException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IHttpActionResult EditStudent(StudentViewModel editStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorAnalyze.GetModelStateError(ModelState));
            }

            _studentService.EditStudent(AutoMap.Mapper.Map<StudentDTO>(editStudent));
            return Ok();
        }
    }
}
