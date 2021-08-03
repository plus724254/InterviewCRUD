using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        public StudentService(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> GetAllStudents() => _studentRepository.GetAll().ToList();
    }
}
