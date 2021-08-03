using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Service.Models.DTO;
using System.Collections.Generic;

namespace InterviewCRUD.Service.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        void AddNewStudent(StudentDTO student);
        void DeleteStudent(string number);
    }
}