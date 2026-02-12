using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories
{
    public class MemberSessionRepository : GenericRepository<MemberSession>, IMemberSessionRepository
    {
        private readonly GymDbcontext dbcontext;

        public MemberSessionRepository(GymDbcontext dbcontext): base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IEnumerable<MemberSession>> GetMemberSessionsWithIncludeAsync(int sessionId)
        {
            var res=await dbcontext.memberSessions
                   .Include(x => x.member)
                   .Where(x => x.SessionId == sessionId)
                   .ToListAsync();
            return res;
        }
    
    }
}
