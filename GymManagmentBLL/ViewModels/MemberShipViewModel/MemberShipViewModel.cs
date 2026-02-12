using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberShipViewModel
{
    public class MemberShipViewModel
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
