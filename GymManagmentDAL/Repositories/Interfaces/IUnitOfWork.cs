using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
       IGenericRepository<T>GetRepository<T>() where T :BaseEntity,new();

        Task<int> SaveChangesAsync();
        public ISessionREpository sessionRepository { get;  }
    }
}
