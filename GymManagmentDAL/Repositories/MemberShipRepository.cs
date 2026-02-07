using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories
{
    public class MemberShipRepository : GenericRepository<MemberShip>, IMemberShipRepository
    {
        private readonly GymDbcontext dbcontext;

        public MemberShipRepository(GymDbcontext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IEnumerable<MemberShip>> GetMemberShipWithIncludeAsync()
        {
            var memberShip =await dbcontext.memberShips.Include(x => x.member)
                .Include(x => x.Plan).ToListAsync();
            return memberShip;
        }
    }
}
