using AutoMapper;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class MemberShipProfile:Profile
    {
        public MemberShipProfile()
        {
            CreateMap<MemberShip, MemberShipViewModel>()
                .ForMember(x => x.MemberName, src => src.MapFrom(x => x.member.Name))
                .ForMember(x => x.PlanName, src => src.MapFrom(x => x.Plan.Name))
                .ForMember(x => x.StartDate, src => src.MapFrom(x => x.CreatedAt));
            CreateMap<Member, MemberToDropDownViewModel>();
            CreateMap<Plan, PlanToDropDownViewModel>();

           CreateMap<CreateMemberShipViewModel, MemberShip>()
                .ForMember(x => x.MemberId, src => src.MapFrom(x => x.MemberId))
                .ForMember(x => x.PlanId, src => src.MapFrom(x => x.PlanId))
                .ForMember(x => x.CreatedAt, src => src.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, src => src.MapFrom(x => x.EndDate));
        }
    }
}
