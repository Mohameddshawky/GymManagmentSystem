using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class Session:BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }

        public Category SessionCategory { get; set; } = null!;
        public int CategoryId { get; set; }
        public Trainer SessionTrainer { get; set; } = null!;
        public int TrainerId { get; set; }

        public ICollection<MemberSession> SessionMembers { get; set; }
    }
}
