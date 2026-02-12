using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberSeesionViewModels
{
    public class CreateBookingViewModel
    {
        [Required]
        public int SessionId { get; set; }

        [Required]
        public int MemberId { get; set; }
    }
}
