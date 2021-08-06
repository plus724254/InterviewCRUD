using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.CustomExceptions;
using InterviewCRUD.Repository.Models.DTO;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewCRUD.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<CourseSelect> _courseSelectRepository;

        public CourseService(IGenericRepository<Course> courseRepository, IGenericRepository<CourseSelect> courseSelectRepository)
        {
            _courseRepository = courseRepository;
            _courseSelectRepository = courseSelectRepository;
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
            var selectCource = _courseSelectRepository.Find(x => x.CourseNumber == number).ToList();
            _courseSelectRepository.RemoveRange(selectCource);
            _courseSelectRepository.SaveChanges();

            var course = _courseRepository.GetById(number);
            _courseRepository.Remove(course);

            _courseRepository.SaveChanges();
        }

        public void ReplaceCourse(string sourceNumber, CourseDTO courseDTO)
        {
            CheckRepeatCourse(courseDTO.Number);

            try
            {
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
            catch(DataErrorException)
            {
                throw new DataErrorException("尚有課程無法修改課號");
            }
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
                throw new DataErrorException("課號重複");
            }
        }
    }
}
