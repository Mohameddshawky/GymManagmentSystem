using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface ISessionREpository:IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetSessionsWithTrainerAndCategoryAsync();

        Task<int> GetCountOfBookedSlotsAsync(int id);
        Task<Session?> GetByIdWithIncludeAsync(int id);
    }
}
