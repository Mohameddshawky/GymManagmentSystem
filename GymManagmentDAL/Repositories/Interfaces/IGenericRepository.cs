using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity, new()          
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>?check=null);
        Task<IEnumerable<T>> SearchAsync(string name);
        Task<T?> GetAsync(int id);
        Task AddAsync(T data);
        void Update(T data);
        void Delete(T data);
      
    }
}
