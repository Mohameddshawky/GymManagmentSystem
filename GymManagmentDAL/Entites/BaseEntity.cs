using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UbdatedAt { get; set; }
    }
}
