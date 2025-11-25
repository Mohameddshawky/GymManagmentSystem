using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class MemberShip:BaseEntity
    {
        //startDate -> CreatedAt of baseEntity  
        public DateTime EndDate { get; set; }

        public string Statue
        {
            get
            {
                return EndDate > DateTime.Now ? "Active" : "Expired";
            }
        }

        public int MemberId { get; set; }
        public Member member { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
        public int PlanId { get; set; }
    }
}
