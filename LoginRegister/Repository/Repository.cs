using LoginRegister.Models;
using LoginRegister.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoginRegister.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LoginDbContext _Context;
        private readonly DbSet<T> _db;
        public Repository(LoginDbContext Context)
        {
            _Context = Context;
            _db = _Context.Set<T>();
        }
        public async Task<T> CreateUserAsync(T entity)
        {

            await _db.AddAsync(entity);
            await _Context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> FetchSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetbyId(int id)
        {
            var CryptPass = await _db.FindAsync(id);
            return CryptPass;
        }

        public T Update(T entity)
        {
            _db.Update(entity);
            _Context.SaveChanges();

            return entity;


            
        }
    }
}
