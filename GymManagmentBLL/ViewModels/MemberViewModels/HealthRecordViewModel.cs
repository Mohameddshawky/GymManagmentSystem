using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is Required")]
        [Range(0.1,300,ErrorMessage ="Hight must be Greater than 0 and less than 300")]
        public Double Height { get; set; }

        [Range(0.1, 500, ErrorMessage = "Weight must be Greater than 0 and less than 500")]
        [Required(ErrorMessage = "Weight is Required")]
        public Double Weight { get; set; }

        [Required(ErrorMessage = "BloodType is Required")]
        [StringLength(3,ErrorMessage = "BloodType Must Be 3 char or less")]
        public string BloodType { get; set; } = string.Empty;
        [Required(ErrorMessage = "Note is Required")]

        public string Note { get; set; } = string.Empty;
    }
}
