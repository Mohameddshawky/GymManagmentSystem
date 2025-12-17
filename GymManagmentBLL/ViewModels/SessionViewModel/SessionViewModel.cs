using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.ViewModels.SessionViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }=string.Empty;
        public string Description { get; set; } =string.Empty;
        public string TrainerName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capcity { get; set; }
        public int AvalibleSlots{ get; set; }


        public string DisplayDate => $"{StartDate:MMM,dd,yyyy}";
        public string DisplayTime => $"{StartDate:hh:mm tt}-{EndDate:hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;

        public string Status 
        {
            get
            {
                if (DateTime.Now < StartDate) return "Upcoming";
                else if (DateTime.Now > EndDate) return "Completed";
                else return "Ongiong";
            }
        }
    }
}
