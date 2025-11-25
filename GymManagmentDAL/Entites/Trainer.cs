using GymManagmentDAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class Trainer:GymUser
    {
        public Specialties Specialties { get; set; }
        public ICollection<Session> TrainerSessions { get; set; } = null!;
    }
}
