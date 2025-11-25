using GymManagmentDAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Entites
{
    public abstract class GymUser:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }

    public class Address
    {
        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        
    }


}
