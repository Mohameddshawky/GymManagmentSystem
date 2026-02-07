using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberShipViewModel
{
    public class CreateMemberShipViewModel
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
