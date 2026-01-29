using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AnalyticsViewModel;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class AnalyticsService(IUnitOfWork unitOfWork) : IAnalyticsService
    {
        public async Task<AnalyticsViewModel> GetAnalyticsDataAsync()
        {
            //var Activemembers =( await unitOfWork.GetRepository<MemberShip>().GetAllAsync(x=>x.Statue=="Active")).Count();
            var Members = (await unitOfWork.GetRepository<MemberShip>().GetAllAsync());
            var TotalMembers = Members.Count();
            var Activemembers = Members.Count(x => x.Statue == "Active");

            var TotalTrainers = (await unitOfWork.GetRepository<Trainer>().GetAllAsync()).Count();
            var sessions = await unitOfWork.GetRepository<Session>().GetAllAsync();
            int upcomingSessions = sessions.Count(x => x.StartDate > DateTime.Now);
            int OngoingSessions = sessions.Count(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now);
            int CompletedSessions = sessions.Count(x => x.EndDate < DateTime.Now);
            AnalyticsViewModel analytics = new AnalyticsViewModel()
            {
                ActiveMembers = Activemembers,
                TotalMembers = TotalMembers,
                TotalTrainers = TotalTrainers,
                upcomingSessions = upcomingSessions,
                OngoingSessions = OngoingSessions,
                CompletedSessions = CompletedSessions
            };
            return analytics;



        }
    }
}
