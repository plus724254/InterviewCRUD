using Autofac;
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
        private Dictionary<Type, object> _repositories;
        private ILifetimeScope _lifetimeScope;

        public UnitOfWork(DbContext context,
            ILifetimeScope lifetimeScope)
        {
            _context = context;
            _lifetimeScope = lifetimeScope;
        }

        public TRepository GetRepository<TRepository>()
        {
            if(_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if(!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                repository = _lifetimeScope.Resolve<TRepository>();
                _repositories.Add(typeof(TRepository), repository);
            }

            return (TRepository)repository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
