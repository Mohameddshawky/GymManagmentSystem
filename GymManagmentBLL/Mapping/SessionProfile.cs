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
            CreateMap<Session, SessionViewModel>();

        }
    }
}
