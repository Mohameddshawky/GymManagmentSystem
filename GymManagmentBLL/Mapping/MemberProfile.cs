using AutoMapper;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class MemberProfile:Profile

    {
        public MemberProfile()
        {
            CreateMap<Member, MemberViewModel>()
                .ForMember(x=>x.Gender,
                des=>des.MapFrom(src=>src.Gender.ToString()))
                .ReverseMap();

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(x => x.healthRecord,
                des => des.MapFrom(src => src.healthRecordViewModel))
                .ForMember(x=>x.Address.BuildingNumber,
                des=>des.MapFrom(src=>src.BuildingNumber))
                .ForMember(x=>x.Address.Street,
                des=>des.MapFrom(src=>src.Street))
                .ForMember(x=>x.Address.City,
                des=>des.MapFrom(src=>src.City));

            CreateMap<Member,UpdateMemberViewModel>()
                .ForMember(x => x.BuildingNumber,
                des => des.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(x => x.Street,
                des => des.MapFrom(src => src.Address.Street))
                .ForMember(x => x.City,
                des => des.MapFrom(src => src.Address.City)).ReverseMap();
        }
    }
}
