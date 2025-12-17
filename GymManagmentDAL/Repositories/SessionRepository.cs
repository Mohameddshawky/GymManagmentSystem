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
    public class SessionRepository : GenericRepository<Session>, ISessionREpository
    {
        private readonly GymDbcontext dbcontext;

        public SessionRepository(GymDbcontext dbcontext):base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<int> GetCountOfBookedSlots(int id)
        {
            return await dbcontext.memberSessions.CountAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Session>> GetSessionsWithTrainerAndCategoryAsync()
        {
            return await dbcontext.sessions.Include(x => x.SessionTrainer)
                .Include(x => x.SessionCategory).ToListAsync();
        }
    }
}
