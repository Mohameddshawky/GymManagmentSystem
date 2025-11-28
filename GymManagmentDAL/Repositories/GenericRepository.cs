using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories
{
    public class GenericRepository<T>(GymDbcontext context) : IGenericRepository<T> where T : BaseEntity,new()
    {
        public async Task AddAsync(T data)
        {
           await context.Set<T>().AddAsync(data);
        }

        public  void Delete(T data)
        {
            context.Set<T>().Remove(data);  

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? check = null)
        {
            if (check == null)
                return await context.Set<T>().ToListAsync();
          
              return await  context.Set<T>().Where(check).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
            => await context.Set<T>().FindAsync(id);
        

        

        public Task<IEnumerable<T>> SearchAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(T data)=>
            context.Set<T>().Update(data);
    }
}
