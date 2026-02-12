using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberSeesionViewModels;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberSessionService(IUnitOfWork unitOfWork,IMapper mapper) : IMemberSesionService
    {
        public async Task<bool> CancelBooking(int memberId, int sessionId)
        {
            var booking =(await unitOfWork.MemberSessionRepository
              .GetAllAsync(x => x.MemberId == memberId && x.SessionId == sessionId))
              .FirstOrDefault();

            if (booking == null) return false;
             unitOfWork.MemberSessionRepository.Delete(booking);
            return await unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateBooking(int sessionId, int memberId)
        {
            var session =await unitOfWork.sessionRepository.GetAsync(sessionId);
            if (session == null || session.StartDate < DateTime.Now)
                return false;

            var member =await unitOfWork.GetRepository<Member>().GetAsync(memberId);
            if (member == null)
                return false;

            var exists =(await unitOfWork.MemberSessionRepository
                .GetAllAsync(x => x.MemberId == memberId && x.SessionId == sessionId))
                .Any();

            if (exists)
                return false;

            var bookedCount =(await unitOfWork.MemberSessionRepository
                .GetAllAsync(x => x.SessionId == sessionId)
                ).Count();

            if (bookedCount >= session.Capacity)
                return false;

            var entity = new MemberSession
            {
                MemberId = memberId,
                SessionId = sessionId,
                CreatedAt = DateTime.Now
            };

            await unitOfWork.MemberSessionRepository.AddAsync(entity);
            return await unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MemberSessionViewModel>> GetMembersForSession(int sessionId)
        {
            var members =await unitOfWork.MemberSessionRepository.GetMemberSessionsWithIncludeAsync(sessionId);
            var result = mapper.Map<IEnumerable<MemberSessionViewModel>>(members);         
            return result;

        }

        

        public async Task<SessionScheduleViewModel> GetSessionsForBookingAndAttendance()
        {
            var sessions =await unitOfWork.sessionRepository.GetSessionsWithTrainerAndCategoryAsync();
            var result1 = mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            var upcomingSessions = result1.Where(x=>x.Status== "Upcoming");
            var ongoingSessions = result1.Where(x => x.Status == "Ongiong");
            foreach (var s in upcomingSessions.Concat(ongoingSessions))
            {
                var booked =(await unitOfWork.MemberSessionRepository.GetAllAsync(x => x.SessionId == s.Id)).Count();
                s.AvalibleSlots = Math.Max(s.Capcity - booked, 0);
            }
            var result = new SessionScheduleViewModel();
            result.UpcomingSessions = upcomingSessions;
            result.OngoingSessions = ongoingSessions;
            return result;


        }

        public async Task<bool> MarkAttended(int memberId, int sessionId)
        {
            var booking = (await unitOfWork.MemberSessionRepository
             .GetAllAsync(x => x.MemberId == memberId && x.SessionId == sessionId))
             .FirstOrDefault();

            if (booking == null) return false;
            booking.IsAttended = true;
             unitOfWork.MemberSessionRepository.Update(booking);
            return await unitOfWork.SaveChangesAsync() > 0;

        }


       
    }
}
