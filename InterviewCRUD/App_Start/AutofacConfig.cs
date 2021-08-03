using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using InterviewCRUD.Repository.Repositories;
using InterviewCRUD.Service.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InterviewCRUD.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterFilterProvider();
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<StudentService>().As<IStudentService>()
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}