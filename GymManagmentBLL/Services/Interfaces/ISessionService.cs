using GymManagmentBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionViewModel>> GetAllSessionAsync();
        Task<SessionViewModel?> GetSessionDetailsAsync(int id);

        Task<bool> CreateSessionAsync(CreateSessionViewModel session);
    }
}
