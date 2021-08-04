using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        StudentCourseSelectionDTO GetStudentCourses(string studentNumber);
        List<StudentCourseSelectionDTO> GetAllStudentCourses();
    }
}
