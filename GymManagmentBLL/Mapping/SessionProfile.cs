using AutoMapper;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Mapping
{
    public class SessionProfile:Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(x => x.CategoryName, src => src.MapFrom
                (des => des.SessionCategory.CategoryName))
                .ForMember(x => x.TrainerName, src => src.MapFrom
                (des => des.SessionTrainer.Name))
                .ForMember(x => x.TrainerName, src => src.MapFrom
                (des => des.SessionTrainer.Name))
                .ForMember(x=>x.AvalibleSlots,op=>op.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();


        }
    }
}
