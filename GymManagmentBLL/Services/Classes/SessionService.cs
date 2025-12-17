using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class SessionService(IUnitOfWork unitOfWork) : ISessionService
    {
        public async Task<IEnumerable<SessionViewModel>> GetAllSessionAsync()
        {
            var sessions = await unitOfWork.GetRepository<Session>().GetAllAsync();
            if (!sessions.Any()) return [];

        }
    }
}
