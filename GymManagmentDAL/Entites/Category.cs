using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }=string.Empty;

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
