using GymManagmentBLL.ViewModels.MemberSeesionViewModels;
using GymManagmentBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberSesionService
    {

        Task<SessionScheduleViewModel> GetSessionsForBookingAndAttendance();
        Task<IEnumerable<MemberSessionViewModel>> GetMembersForSession(int sessionId);
        Task<bool> CreateBooking(int sessionId, int memberId);
        Task<bool> CancelBooking(int memberId, int sessionId);
        Task<bool> MarkAttended(int memberId, int sessionId);
    }
}
