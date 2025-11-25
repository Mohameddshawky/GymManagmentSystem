using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class MemberSession:BaseEntity
    {
        //booking date -> CreatedAt of baseEntity   

        public bool IsAttended { get; set; }

        public int MemberId { get; set; }
        public Member member { get; set; } = null!;
        public Session session { get; set; } = null!;
        public int SessionId { get; set; }

    }
}
