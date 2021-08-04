using AutoMapper;
using InterviewCRUD.Models.ViewModels;
using InterviewCRUD.Service.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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