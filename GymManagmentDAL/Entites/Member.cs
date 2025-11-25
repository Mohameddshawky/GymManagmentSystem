using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class Member:GymUser
    {
        public string? Photo { get; set; }=string.Empty;

        public HealthRecord healthRecord { get; set; }=null!;

        public ICollection<MemberShip> MemberShips { get; set; }=null!;
        public ICollection<MemberSession> memberSessions { get; set; }=null!;

    }
}
