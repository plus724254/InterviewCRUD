using InterviewCRUD.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _set.Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.AsEnumerable();
        }

        public TEntity GetById(string id)
        {
            return _set.Find(id);
        }
    }
}
