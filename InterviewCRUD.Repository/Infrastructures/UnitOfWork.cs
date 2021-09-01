using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private Dictionary<Type, object> repositories;
        public IGenericRepository<Course> CourseRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IGenericRepository<CourseSelect> CourseSelectRepository { get; private set; }
        public UnitOfWork(DbContext context,
            IGenericRepository<Course> courseRepository,
            IStudentRepository studentRepository,
            IGenericRepository<CourseSelect> courseSelectRepository)
        {
            _context = context;
            CourseRepository = courseRepository;
            StudentRepository = studentRepository;
            CourseSelectRepository = courseSelectRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
