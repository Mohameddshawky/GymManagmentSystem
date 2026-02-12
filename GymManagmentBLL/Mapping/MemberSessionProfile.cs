using AutoMapper;
using GymManagmentBLL.ViewModels.MemberSeesionViewModels;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class MemberSessionProfile:Profile
    {
        public MemberSessionProfile()
        {
            CreateMap<Member, MemberSessionViewModel>()
                .ForMember(x => x.MemberName, src => src.MapFrom(x => x.Name));
            CreateMap<MemberSession, MemberSessionViewModel>()
                .ForMember(x => x.MemberName, src => src.MapFrom(x => x.member.Name))
                .ForMember(x => x.BookingDate, src => src.MapFrom(x => x.CreatedAt));
                

        }
    }
}
