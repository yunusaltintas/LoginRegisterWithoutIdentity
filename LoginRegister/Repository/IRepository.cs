using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoginRegister.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateUserAsync(T entity);

        Task<T> FetchSingleAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetbyId(int id);

    }
}
