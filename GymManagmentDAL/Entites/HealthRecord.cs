using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    [Owned]
    public class HealthRecord:BaseEntity
    {

        public Double Height { get; set; }
        public Double Weight { get; set; }
        public string BloodType { get; set; }=string.Empty;
        public string Note { get; set;} = string.Empty;

    }
}
