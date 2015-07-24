using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        ICollection<T> FindBy(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        void Delete(T entity);
        T Edit(T entity);

        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> InsertAsync(T entity);
        Task<T> EditAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
