using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories
{
    internal class GenaricRepository<T>(GymDbcontext context) : IGenaricRepository<T> where T : BaseEntity
    {
        public async Task AddAsync(T data)
        {
           await context.Set<T>().AddAsync(data);
        }

        public  void Delete(T data)
        {
            context.Set<T>().Remove(data);  

        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await context.Set<T>().ToListAsync();
        

        public async Task<T?> GetAsync(int id)
            => await context.Set<T>().FindAsync(id);
        

        public async Task<int> SaveChangesAsync()
            =>await context.SaveChangesAsync();

        public Task<IEnumerable<T>> SearchAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(T data)=>
            context.Set<T>().Update(data);
    }
}
