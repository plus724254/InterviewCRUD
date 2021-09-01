using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Infrastructures
{
    public interface IUnitOfWork
    {
        IGenericRepository<Course> CourseRepository { get; }
        IStudentRepository StudentRepository { get; }
        IGenericRepository<CourseSelect> CourseSelectRepository { get; }
        int SaveChanges();
    }
}
