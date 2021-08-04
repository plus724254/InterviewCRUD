using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewCRUD.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGenericRepository<CourseSelect> _courseSelectRepository;
        public StudentService(IStudentRepository studentRepository, IGenericRepository<CourseSelect> courseSelectRepository)
        {
            _studentRepository = studentRepository;
            _courseSelectRepository = courseSelectRepository;
        }

        public List<Student> GetAllStudents() => _studentRepository.GetAll().ToList();

        public void AddNewStudent(StudentDTO studentDTO)
        {
            CheckRepeatStudent(studentDTO.Number);

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
            var selectCourses = _courseSelectRepository.Find(x => x.StudentNumber == number);
            var student = _studentRepository.GetById(number);

            _courseSelectRepository.RemoveRange(selectCourses);
            _studentRepository.Remove(student);

            _courseSelectRepository.SaveChanges();
            _studentRepository.SaveChanges();
        }

        public void ReplaceStudent(string sourceNumber, StudentDTO studentDTO)
        {
            CheckRepeatStudent(studentDTO.Number);

            try
            {
                var oldStudent = _studentRepository.GetById(sourceNumber);

                _studentRepository.Remove(oldStudent);
                _studentRepository.Add(new Student()
                {
                    Number = studentDTO.Number,
                    Birthday = DateTime.Parse(studentDTO.Birthday),
                    Name = studentDTO.Name,
                    Email = studentDTO.Email,
                });

                _studentRepository.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("尚有課程無法修改學號");
            }
        }

        public void EditStudent(StudentDTO studentDTO)
        {
            var sourceStudent = _studentRepository.GetById(studentDTO.Number);

            sourceStudent.Birthday = DateTime.Parse(studentDTO.Birthday);
            sourceStudent.Name = studentDTO.Name;
            sourceStudent.Email = studentDTO.Email;

            _studentRepository.SaveChanges();
        }
        public StudentCourseSelectionDTO GetStudentCourses(string studentNumber)
        {
            return _studentRepository.GetStudentCourses(studentNumber);
        }

        public void AddStudentCourse(StudentCourseSelectionDTO studentCourseSelectionDTO)
        {
            var studentCourses = studentCourseSelectionDTO.Courses
                .Select(x => new CourseSelect()
                {
                    StudentNumber = studentCourseSelectionDTO.StudentNumber,
                    CourseNumber = x.Number,
                })
                .ToList();

            _courseSelectRepository.AddRange(studentCourses);

            _courseSelectRepository.SaveChanges();
        }

        public List<StudentCourseSelectionDTO> GetAllStudentCourses()
        {
            return _studentRepository.GetAllStudentCourses().Where(x=>x.Courses.Any(y=>!string.IsNullOrEmpty(y.Number))).ToList();
        }

        public void DeleteStudentCourses(string studentNumber)
        {
            var studentCourses = _courseSelectRepository.Find(x => x.StudentNumber == studentNumber).ToList();
            _courseSelectRepository.RemoveRange(studentCourses);

            _courseSelectRepository.SaveChanges();
        }
        public void ReplaceStudentCourses(StudentCourseSelectionDTO studentCourseSelectionDTO)
        {
            var existCourses = _courseSelectRepository.Find(x => x.StudentNumber == studentCourseSelectionDTO.StudentNumber).ToList();
            _courseSelectRepository.RemoveRange(existCourses);

            var newCourses = studentCourseSelectionDTO.Courses
                .Where(x=>x.IsSeleted == true)
                .Select(x => new CourseSelect() 
                { 
                    StudentNumber = studentCourseSelectionDTO.StudentNumber,
                    CourseNumber = x.Number,
                })
                .ToList();

            _courseSelectRepository.AddRange(newCourses);

            _courseSelectRepository.SaveChanges();
        }

        private void CheckRepeatStudent(string number)
        {
            if(_studentRepository.GetById(number) != null)
            {
                throw new Exception("學號重複");
            }
        }
    }
}
