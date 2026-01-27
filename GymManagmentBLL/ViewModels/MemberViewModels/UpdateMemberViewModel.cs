using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberViewModels
{
    public class UpdateMemberViewModel
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string  Photo { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]//ui hint 
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]//validation
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must be Between 2 and 50 charachters")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]//ui hint 
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]//validation
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PhoneNumber Must be 11 number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be valid Egypyain phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "BuildingNumber is Required")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Building number must be a positive number starting from 1")]
        public string BuildingNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must be Between 2 and 30 charachters")]

        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must be Between 2 and 30 charachters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must be only letters and spaces")]
        public string City { get; set; } = string.Empty;
    }
}
