using InterviewCRUD.Common.DTO;
using InterviewCRUD.Repository.Entities;
using System.Collections.Generic;

namespace InterviewCRUD.Service.Services
{
    public interface ICourseService
    {
        void AddNewCourse(CourseDTO courseDTO);
        void DeleteCourse(string number);
        void EditCourse(CourseDTO courseDTO);
        List<Course> GetAllCourses();
        void ReplaceCourse(string sourceNumber, CourseDTO courseDTO);
    }
}