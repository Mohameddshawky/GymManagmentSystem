using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberSeesionViewModels
{
    public class MemberSessionViewModel
    {
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public string MemberName { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public bool IsAttended { get; set; } = false;
    }
}
