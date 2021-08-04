using InterviewCRUD.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Models.DTO
{
    public class StudentCourseSelectionDTO
    {
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public List<CourseDTO> Courses { get; set; }
    }
}
