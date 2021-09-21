using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.DTO;
using System.Collections.Generic;

namespace InterviewCRUD.Repository.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        StudentCourseSelectionDTO GetStudentCourses(string studentNumber);
        List<StudentCourseSelectionDTO> GetAllStudentCourses();
    }
}
