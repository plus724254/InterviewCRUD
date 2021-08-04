using AutoMapper;
using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Repository.Models.DTO;

namespace InterviewCRUD.Tools.AutoMappers
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<StudentViewModel, StudentDTO>();
            CreateMap<CourseViewModel, CourseDTO>();
        }
    }
}