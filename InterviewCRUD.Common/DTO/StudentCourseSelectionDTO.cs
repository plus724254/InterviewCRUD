using InterviewCRUD.Common.DTO;
using System.Collections.Generic;

namespace InterviewCRUD.Repository.Models.DTO
{
    public class StudentCourseSelectionDTO
    {
        public string StudentNumber { get; set; }
        public string StudentName { get; set; }
        public List<CourseDTO> Courses { get; set; }
    }
}
