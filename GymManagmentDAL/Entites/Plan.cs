using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class Plan:BaseEntity
    {
        public string Name { get; set; }=string .Empty;
        public string Description { get; set; }=string.Empty;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public bool IsActive { get; set; }

        public ICollection<MemberShip> PlanMemberShips { get; set; } = null!;

    }
}
