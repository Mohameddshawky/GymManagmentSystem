using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetAllTrainerAsync();
        Task<bool> CreateTrainerAsync(CreateTrainerViewModel model);
        Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int id);

        Task<UpdateTrainerViewModel> GetTrainerToUbdateAsync(int id);

        Task<bool> UpdateTrainerAsync(int id, UpdateTrainerViewModel model);

        Task<bool> DeleteTrainerAsync(int id);
    }
}
