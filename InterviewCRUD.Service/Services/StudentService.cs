using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Repositories;
using InterviewCRUD.Service.Models.DTO;
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

        public void AddNewStudent(StudentDTO studentDTO)
        {
            _studentRepository.Add(new Student() 
            {
                Number = studentDTO.Number,
                Birthday = DateTime.Parse(studentDTO.Birthday),
                Name = studentDTO.Name,
                Email = studentDTO.Email,
            });

            _studentRepository.SaveChanges();
        }

        public void DeleteStudent(string number)
        {
            var student = _studentRepository.GetById(number);
            _studentRepository.Remove(student);

            _studentRepository.SaveChanges();
        }
    }
}
