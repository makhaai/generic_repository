using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository
{
    public abstract class GenericRepository<T, C> : IGenericRepository<T>
        where T : class
        where C : DbContext, new()
    {

        private C _entities = new C();

        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }


        public ICollection<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            ICollection<T> query = _entities.Set<T>().Where(predicate).ToList();
            return query;
        }

        public T Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
            _entities.SaveChanges();
            return entity;
        }

        public T Edit(T entity)
        {
            int result = -1;
            _entities.Entry(entity).State = EntityState.Modified;
            result = _entities.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            _entities.SaveChanges();
        }

        public ICollection<T> GetAll()
        {
            ICollection<T> query = _entities.Set<T>().ToList();
            return query;
        }

        public string Message
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        #region IDisposible
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Async

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _entities.Set<T>().ToListAsync();
        }
        public async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> InsertAsync(T entity)
        {
            _entities.Set<T>().Add(entity);
            await _entities.SaveChangesAsync();
            return entity;
        }
        public async Task<T> EditAsync(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            await _entities.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _entities.Set<T>().Remove(entity);
            await _entities.SaveChangesAsync();
        }

        #endregion
    }
}
