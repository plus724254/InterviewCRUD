using InterviewCRUD.Repository.Entities;
using System.Collections.Generic;

namespace InterviewCRUD.Service.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
    }
}