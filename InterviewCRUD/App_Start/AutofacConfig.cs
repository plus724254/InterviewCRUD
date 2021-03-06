using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using InterviewCRUD.Repository.Entities;
using InterviewCRUD.Repository.Infrastructures;
using InterviewCRUD.Repository.Repositories;
using InterviewCRUD.Service.Services;
using System.Data.Entity;
using System.Reflection;
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

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseSelectionEntities>().As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentService>().As<IStudentService>()
               .InstancePerDependency();

            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}