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
    }
}
