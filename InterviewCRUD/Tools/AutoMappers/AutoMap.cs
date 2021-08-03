using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewCRUD.Tools.AutoMappers
{
    public static class AutoMap
    {
        public static IMapper Mapper { get; set; }

        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(
                config =>
                {
                    config.AddProfile<CommonProfile>();
                });

            Mapper = mapperConfiguration.CreateMapper();
        }
        
    }
}