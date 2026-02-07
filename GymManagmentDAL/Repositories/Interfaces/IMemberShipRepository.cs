using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IMemberShipRepository: IGenericRepository<MemberShip>
    {
        Task<IEnumerable<MemberShip>> GetMemberShipWithIncludeAsync();
    }
}
