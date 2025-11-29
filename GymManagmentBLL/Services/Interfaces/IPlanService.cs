using GymManagmentBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanViewModel>> GetAllPlanAsync();
        Task<PlanViewModel?> GetPlanDetailsAsync(int id);

        Task<UpdatePlanViewModel?> GetPlanToUpdate(int id);
        Task<bool> UpdatePlanAsync( int id, UpdatePlanViewModel model);
        Task<bool> TogglePlanAsync(int id);                             
    }
}
