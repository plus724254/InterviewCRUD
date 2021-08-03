using InterviewCRUD.Repository.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(InterviewCRUD.Startup))]

namespace InterviewCRUD
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            ConfigureServices(services);
            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.ServiceCollection services)
        {
            services.AddTransient(typeof(CourseSelectionEntities));
        }
    }

    public class DefaultDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        protected IServiceProvider serviceProvider;
        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }
}
