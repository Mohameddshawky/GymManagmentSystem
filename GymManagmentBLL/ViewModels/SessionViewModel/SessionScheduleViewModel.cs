using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.SessionViewModel
{
    public class SessionScheduleViewModel
    {
        public IEnumerable<SessionViewModel> UpcomingSessions { get; set; } = new List<SessionViewModel>();
        public IEnumerable<SessionViewModel> OngoingSessions { get; set; } = new List<SessionViewModel>();
    }
}
