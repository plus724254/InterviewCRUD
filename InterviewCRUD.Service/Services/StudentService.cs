using InterviewCRUD.Common.CustomExceptions;
using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Infrastructures;
using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewCRUD.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Student> GetAllStudents() => _unitOfWork.GetRepository<IStudentRepository>().GetAll().ToList();

        public void AddNewStudent(StudentDTO studentDTO)
        {
            CheckRepeatStudent(studentDTO.Number);

            _unitOfWork.GetRepository<IStudentRepository>().Add(new Student() 
            {
                Number = studentDTO.Number,
                Birthday = DateTime.Parse(studentDTO.Birthday),
                Name = studentDTO.Name,
                Email = studentDTO.Email,
            });

            _unitOfWork.SaveChanges();
        }

        public void DeleteStudent(string number)
        {
            var courseSelectRepository = _unitOfWork.GetRepository<IGenericRepository<CourseSelect>>();
            var studentRepository = _unitOfWork.GetRepository<IStudentRepository>();

            var selectCourses = courseSelectRepository.Find(x => x.StudentNumber == number);
            var student = studentRepository.GetById(number);

            courseSelectRepository.RemoveRange(selectCourses);
            studentRepository.Remove(student);

            _unitOfWork.SaveChanges();
        }

        public void ReplaceStudent(string sourceNumber, StudentDTO studentDTO)
        {
            CheckRepeatStudent(studentDTO.Number);
            var studentRepository = _unitOfWork.GetRepository<IStudentRepository>();

            try
            {
                var oldStudent = studentRepository.GetById(sourceNumber);

                studentRepository.Remove(oldStudent);
                studentRepository.Add(new Student()
                {
                    Number = studentDTO.Number,
                    Birthday = DateTime.Parse(studentDTO.Birthday),
                    Name = studentDTO.Name,
                    Email = studentDTO.Email,
                });

                _unitOfWork.SaveChanges();
            }
            catch(Exception)
            {
                throw new DataErrorException("尚有課程無法修改學號");
            }
        }

        public void EditStudent(StudentDTO studentDTO)
        {
            var student = new Student()
            {
                Number = studentDTO.Number,
                Birthday = DateTime.Parse(studentDTO.Birthday),
                Name = studentDTO.Name,
                Email = studentDTO.Email,
            };

            _unitOfWork.GetRepository<IStudentRepository>().Update(student);
            _unitOfWork.SaveChanges();
        }

        public StudentCourseSelectionDTO GetStudentCourses(string studentNumber)
        {
            return _unitOfWork.GetRepository<IStudentRepository>().GetStudentCourses(studentNumber);
        }

        public void AddStudentCourse(StudentCourseSelectionDTO studentCourseSelectionDTO)
        {
            var courseSelectRepository = _unitOfWork.GetRepository<IGenericRepository<CourseSelect>>();

            var seletedCourse = courseSelectRepository
                .Find(x => x.StudentNumber == studentCourseSelectionDTO.StudentNumber)
                .Select(x => x.CourseNumber)
                .ToList();

            var studentCourses = studentCourseSelectionDTO.Courses
                .Where(x => !seletedCourse.Contains(x.Number))
                .Select(x => new CourseSelect()
                {
                    StudentNumber = studentCourseSelectionDTO.StudentNumber,
                    CourseNumber = x.Number,
                })
                .ToList();

            courseSelectRepository.AddRange(studentCourses);

            _unitOfWork.SaveChanges();
        }

        public List<StudentCourseSelectionDTO> GetAllStudentCourses()
        {
            return _unitOfWork.GetRepository<IStudentRepository>().GetAllStudentCourses().Where(x=>x.Courses.Any(y=>!string.IsNullOrEmpty(y.Number))).ToList();
        }

        public void DeleteStudentCourses(string studentNumber)
        {
            var courseSelectRepository = _unitOfWork.GetRepository<IGenericRepository<CourseSelect>>();
            var studentCourses = courseSelectRepository.Find(x => x.StudentNumber == studentNumber).ToList();
            courseSelectRepository.RemoveRange(studentCourses);

            _unitOfWork.SaveChanges();
        }
        public void ReplaceStudentCourses(StudentCourseSelectionDTO studentCourseSelectionDTO)
        {
            var courseSelectRepository = _unitOfWork.GetRepository<IGenericRepository<CourseSelect>>();

            var existCourses = courseSelectRepository.Find(x => x.StudentNumber == studentCourseSelectionDTO.StudentNumber).ToList();
            courseSelectRepository.RemoveRange(existCourses);

            var newCourses = studentCourseSelectionDTO.Courses
                .Where(x=>x.IsSeleted == true)
                .Select(x => new CourseSelect() 
                { 
                    StudentNumber = studentCourseSelectionDTO.StudentNumber,
                    CourseNumber = x.Number,
                })
                .ToList();

            courseSelectRepository.AddRange(newCourses);

            _unitOfWork.SaveChanges();
        }

        private void CheckRepeatStudent(string number)
        {
            if(_unitOfWork.GetRepository<IStudentRepository>().GetById(number) != null)
            {
                throw new DataErrorException("學號重複");
            }
        }
    }
}
