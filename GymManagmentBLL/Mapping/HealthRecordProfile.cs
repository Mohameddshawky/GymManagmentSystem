using AutoMapper;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class HealthRecordProfile:Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<HealthRecordViewModel, HealthRecord>()
                .ReverseMap();
        }
    }


}
