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
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        public CourseService(IGenericRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetAllCourses() => _courseRepository.GetAll().ToList();

        public void AddNewCourse(CourseDTO courseDTO)
        {
            CheckRepeatCourse(courseDTO.Number);

            _courseRepository.Add(new Course()
            {
                Number = courseDTO.Number,
                Name = courseDTO.Name,
                Credit = int.Parse(courseDTO.Credit),
                Place = courseDTO.Place,
                TeacherName = courseDTO.TeacherName,
            });

            _courseRepository.SaveChanges();
        }

        public void DeleteCourse(string number)
        {
            var course = _courseRepository.GetById(number);
            _courseRepository.Remove(course);

            _courseRepository.SaveChanges();
        }

        public void ReplaceCourse(string sourceNumber, CourseDTO courseDTO)
        {
            CheckRepeatCourse(courseDTO.Number);

            var oldCourse = _courseRepository.GetById(sourceNumber);
            _courseRepository.Add(new Course()
            {
                Number = courseDTO.Number,
                Name = courseDTO.Name,
                Credit = int.Parse(courseDTO.Credit),
                Place = courseDTO.Place,
                TeacherName = courseDTO.TeacherName,
            });
            _courseRepository.Remove(oldCourse);

            _courseRepository.SaveChanges();
        }

        public void EditCourse(CourseDTO courseDTO)
        {
            var sourceCourse = _courseRepository.GetById(courseDTO.Number);

            sourceCourse.Name = courseDTO.Name;
            sourceCourse.Credit = int.Parse(courseDTO.Credit);
            sourceCourse.Place = courseDTO.Place;
            sourceCourse.TeacherName = courseDTO.TeacherName;

            _courseRepository.SaveChanges();
        }

        private void CheckRepeatCourse(string number)
        {
            if (_courseRepository.GetById(number) != null)
            {
                throw new Exception("課號重複");
            }
        }
    }
}
