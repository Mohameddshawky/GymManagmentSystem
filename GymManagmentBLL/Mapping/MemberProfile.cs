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
                .ForMember(x=>x.Phone,des=>des.MapFrom(src=>src.PhoneNumber))
                .ReverseMap();

            CreateMap<CreateMemberViewModel, Member>()
     .ForMember(dest => dest.healthRecord,
         opt => opt.MapFrom(src => src.healthRecordViewModel))
     .ForPath(dest => dest.Address.BuildingNumber,
         opt => opt.MapFrom(src => src.BuildingNumber))
     .ForPath(dest => dest.Address.Street,
         opt => opt.MapFrom(src => src.Street))
     .ForPath(dest => dest.Address.City,
         opt => opt.MapFrom(src => src.City));
  



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
