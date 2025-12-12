using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.TrainerViewModels
{
    public class TrainerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string DateOfBirth { get; set; }=string.Empty;   
        public string Address { get; set; } = string.Empty;
    }
}
