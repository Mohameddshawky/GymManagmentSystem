using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface ISessionREpository:IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetSessionsWithTrainerAndCategoryAsync();

        int GetCountOfBookedSlots(int id);
    }
}
