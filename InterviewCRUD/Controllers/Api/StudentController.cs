using InterviewCRUD.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InterviewCRUD.Controllers.Api
{
    //[RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        [HttpGet]
        //[Route("")]
        public IHttpActionResult GetStudents()
        {
            return Ok(new List<StudentViewModel>());
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
