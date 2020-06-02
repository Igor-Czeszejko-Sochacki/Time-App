using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.DbModels;

namespace TimeApp.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T> GetSingleEntity(Expression<Func<T, bool>> Func);
        Task Delete(T entity);
        Task Patch(T entity);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
    }
}
