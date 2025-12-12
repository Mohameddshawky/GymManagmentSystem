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
                .ReverseMap();

            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(x => x.Address.BuildingNumber,
                des => des.MapFrom(src => src.BuildingNumber))
                .ForMember(x => x.Address.Street,
                des => des.MapFrom(src => src.Street))
                .ForMember(x => x.Address.City,
                des => des.MapFrom(src => src.City));

            CreateMap<UpdateTrainerViewModel, Trainer>()
                .ForMember(x => x.Address.BuildingNumber,
                des => des.MapFrom(src => src.BuildingNumber))
                .ForMember(x => x.Address.Street,
                des => des.MapFrom(src => src.Street))
                .ForMember(x => x.Address.City,
                des => des.MapFrom(src => src.City));


            CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(x => x.DateOfBirth
                , des => des.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
                .ForMember(x => x.Address,
                des => des.MapFrom(src => $"{src.Address.City} {src.Address.Street} {src.Address.BuildingNumber}"))
                .ReverseMap();
        }

    }
}
