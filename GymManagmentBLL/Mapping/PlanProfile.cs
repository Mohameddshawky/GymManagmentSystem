using AutoMapper;
using GymManagmentBLL.ViewModels.PlanViewModels;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class PlanProfile:Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan,PlanViewModel>().ReverseMap();

        }
    }
}
