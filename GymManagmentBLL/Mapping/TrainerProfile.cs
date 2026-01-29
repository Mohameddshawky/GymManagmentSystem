using AutoMapper;
using GymManagmentBLL.ViewModels.TrainerViewModels;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class TrainerProfile:Profile
    {
        public TrainerProfile()
        {
            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(x => x.Specialization
                , des => des.MapFrom(src => src.Specialties.ToString()))
                .ForMember(x=>x.Phone, des=>des.MapFrom(src=>src.PhoneNumber))
                .ReverseMap();
            CreateMap<CreateTrainerViewModel, Trainer>()
                       .ForMember(x => x.Specialties,
                           opt => opt.MapFrom(src => src.Specialization))

                       .ForPath(dest => dest.Address.BuildingNumber,
         opt => opt.MapFrom(src => src.BuildingNumber))
     .ForPath(dest => dest.Address.Street,
         opt => opt.MapFrom(src => src.Street))
     .ForPath(dest => dest.Address.City,
         opt => opt.MapFrom(src => src.City));

            CreateMap<UpdateTrainerViewModel, Trainer>()
                .ForPath(dest => dest.Address.BuildingNumber,
         opt => opt.MapFrom(src => src.BuildingNumber))
     .ForPath(dest => dest.Address.Street,
         opt => opt.MapFrom(src => src.Street))
     .ForPath(dest => dest.Address.City,
         opt => opt.MapFrom(src => src.City));



            CreateMap<UpdateTrainerViewModel, Trainer>();


            CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(x => x.DateOfBirth
                , des => des.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
                .ForMember(x=>x.Phone, des=>des.MapFrom(src=>src.PhoneNumber))
                .ForMember(x => x.Address,
                des => des.MapFrom(src => $"{src.Address.City} {src.Address.Street} {src.Address.BuildingNumber}"))
                .ReverseMap();
        }

    }
}
