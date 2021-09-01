using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext context) : base(context)
        {

        }

        public StudentCourseSelectionDTO GetStudentCourses(string studentNumber)
        {
            return (from s in _context.Set<Student>()
                     join cs in _context.Set<CourseSelect>()
                     on s.Number equals cs.StudentNumber into tb
                     from cs in tb.DefaultIfEmpty()
                     join c in _context.Set<Course>()
                     on cs.CourseNumber equals c.Number into tb2
                     from c in tb2.DefaultIfEmpty()
                     select new { s.Number, s.Name, CourseNumber = c.Number, CourseName = c.Name })
            .Where(x => x.Number == studentNumber)
            .AsEnumerable()
            .GroupBy(x => new { x.Number, x.Name })
            .Select(x => new StudentCourseSelectionDTO()
            {
                StudentNumber = x.Key.Number,
                StudentName = x.Key.Name,
                Courses = x.Select(y => new CourseDTO()
                {
                    Number = y.CourseNumber,
                    Name = y.CourseName,
                }).ToList()
            })
            .FirstOrDefault();
        }

        public List<StudentCourseSelectionDTO> GetAllStudentCourses()
        {
            return (from s in _context.Set<Student>()
                    join cs in _context.Set<CourseSelect>()
                    on s.Number equals cs.StudentNumber into tb
                    from cs in tb.DefaultIfEmpty()
                    join c in _context.Set<Course>()
                    on cs.CourseNumber equals c.Number into tb2
                    from c in tb2.DefaultIfEmpty()
                    select new { s.Number, s.Name, CourseNumber = c.Number, CourseName = c.Name })
            .AsEnumerable()
            .GroupBy(x => new { x.Number, x.Name })
            .Select(x => new StudentCourseSelectionDTO()
            {
                StudentNumber = x.Key.Number,
                StudentName = x.Key.Name,
                Courses = x.Select(y => new CourseDTO()
                {
                    Number = y.CourseNumber,
                    Name = y.CourseName,
                }).ToList()
            })
            .ToList();
        }
    }
}
