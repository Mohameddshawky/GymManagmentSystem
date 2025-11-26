using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IGenaricRepository<T> where T : BaseEntity, new()          
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchAsync(string name);
        Task<T?> GetAsync(int id);
        Task AddAsync(T data);
        void Update(T data);
        void Delete(T data);
        Task<int> SaveChangesAsync();    
    }
}
