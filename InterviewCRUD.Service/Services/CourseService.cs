using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Infrastructures;
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
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Course> GetAllCourses() => _unitOfWork.GetRepository<IGenericRepository<Course>>().GetAll().ToList();

        public void AddNewCourse(CourseDTO courseDTO)
        {
            CheckRepeatCourse(courseDTO.Number);

            _unitOfWork.GetRepository<IGenericRepository<Course>>().Add(new Course()
            {
                Number = courseDTO.Number,
                Name = courseDTO.Name,
                Credit = int.Parse(courseDTO.Credit),
                Place = courseDTO.Place,
                TeacherName = courseDTO.TeacherName,
            });

            _unitOfWork.SaveChanges();
        }

        public void DeleteCourse(string number)
        {
            var courseSelectRepository = _unitOfWork.GetRepository<IGenericRepository<CourseSelect>>();
            var courseRepository = _unitOfWork.GetRepository<IGenericRepository<Course>>();

            var selectCource = courseSelectRepository.Find(x => x.CourseNumber == number).ToList();
            courseSelectRepository.RemoveRange(selectCource);

            var course = courseRepository.GetById(number);
            courseRepository.Remove(course);

            _unitOfWork.SaveChanges();
        }

        public void ReplaceCourse(string sourceNumber, CourseDTO courseDTO)
        {
            CheckRepeatCourse(courseDTO.Number);
            var courseRepository = _unitOfWork.GetRepository<IGenericRepository<Course>>();

            try
            {
                var oldCourse = courseRepository.GetById(sourceNumber);
                courseRepository.Add(new Course()
                {
                    Number = courseDTO.Number,
                    Name = courseDTO.Name,
                    Credit = int.Parse(courseDTO.Credit),
                    Place = courseDTO.Place,
                    TeacherName = courseDTO.TeacherName,
                });
                courseRepository.Remove(oldCourse);

                _unitOfWork.SaveChanges();
            }
            catch(Exception)
            {
                throw new DataErrorException("尚有課程無法修改課號");
            }
        }

        public void EditCourse(CourseDTO courseDTO)
        {
            var course = new Course
            {
                Number = courseDTO.Number,
                Name = courseDTO.Name,
                Credit = int.Parse(courseDTO.Credit),
                Place = courseDTO.Place,
                TeacherName = courseDTO.TeacherName,
            };

            _unitOfWork.GetRepository<IGenericRepository<Course>>().Update(course);
            _unitOfWork.SaveChanges();
        }

        private void CheckRepeatCourse(string number)
        {
            if (_unitOfWork.GetRepository<IGenericRepository<Course>>().GetById(number) != null)
            {
                throw new DataErrorException("課號重複");
            }
        }
    }
}
