using GymManagmentDAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.MemberViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Photo { get; set; }=string.Empty; 
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string Phone { get; set; }=string.Empty;
        public string  Gender { get; set; }=string.Empty;                   
    }
}
