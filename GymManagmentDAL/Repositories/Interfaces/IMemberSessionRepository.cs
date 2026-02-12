using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepository: IGenericRepository<MemberSession>
    {
        Task<IEnumerable<MemberSession>> GetMemberSessionsWithIncludeAsync(int sessionId);
    }
}
