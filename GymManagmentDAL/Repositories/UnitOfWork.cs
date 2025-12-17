using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext gymDbcontext;
        private ConcurrentDictionary<string, object> repositories;

        public UnitOfWork(GymDbcontext gymDbcontext, ISessionREpository sessionRepository)
        {
            repositories = new ConcurrentDictionary<string, object>();
            this.gymDbcontext = gymDbcontext;
            this.sessionRepository = sessionRepository;
        }

        public ISessionREpository sessionRepository {  get; }

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity,new()
        {
            var key = typeof(T).Name;
            return (IGenericRepository<T>) repositories.GetOrAdd(key, (_) => new GenericRepository<T>(gymDbcontext));
        }

       
        public async Task<int> SaveChangesAsync()
        {
            return await gymDbcontext.SaveChangesAsync();
        }
    }
}
