using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.DTO;
using System.Collections.Generic;

namespace InterviewCRUD.Service.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        void AddNewStudent(StudentDTO student);
        void DeleteStudent(string number);
        void ReplaceStudent(string sourceNumber, StudentDTO student);
        void EditStudent(StudentDTO student);
        StudentCourseSelectionDTO GetStudentCourses(string studentNumber);
        List<StudentCourseSelectionDTO> GetAllStudentCourses();
        void AddStudentCourse(StudentCourseSelectionDTO studentCourseSelectionDTO);
        void DeleteStudentCourses(string studentNumber);
        void ReplaceStudentCourses(StudentCourseSelectionDTO studentCourseSelectionDTO);
    }
}