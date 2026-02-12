using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberSeesionViewModels;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberSessionService(IUnitOfWork unitOfWork,IMapper mapper) : IMemberSesionService
    {
        public Task<bool> CancelBooking(int memberId, int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateBooking(int sessionId, int memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MemberSessionViewModel>> GetMembersForOnGoingSession(int sessionId)
        {
            var members =await unitOfWork.MemberSessionRepository.GetMemberSessionsWithIncludeAsync(sessionId);
            var result = mapper.Map<IEnumerable<MemberSessionViewModel>>(members);         
            return result;

        }

        public async Task<IEnumerable<MemberSessionViewModel>> GetMembersForUpComingSession(int sessionId)
        {
            var members =(await unitOfWork.MemberShipRepository.GetMemberShipWithIncludeAsync()).Select(x=>x.member);
            var result = mapper.Map<IEnumerable<MemberSessionViewModel>>(members); return result;


        }

        public async Task<SessionScheduleViewModel> GetSessionsForBookingAndAttendance()
        {
            var sessions =await unitOfWork.sessionRepository.GetSessionsWithTrainerAndCategoryAsync();
            var result1 = mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            var upcomingSessions = result1.Where(x=>x.Status== "Upcoming");
            var ongoingSessions = result1.Where(x => x.Status == "Ongiong");
            var result = new SessionScheduleViewModel();
            result.UpcomingSessions = upcomingSessions;
            result.OngoingSessions = ongoingSessions;
            return result;


        }

        public Task<bool> MarkAttended(int memberId, int sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
