using GymManagmentBLL.ViewModels.MemberShipViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberShipService
    {
        Task<IEnumerable<MemberShipViewModel>> GetAllMemberShipAsync();
        Task<bool> CreateMemberShipAsync(CreateMemberShipViewModel model);
        Task<bool> DeleteMemberShipAsync(int memberId, int planId);
        Task<IEnumerable<MemberToDropDownViewModel>> GetMemberToDropDownsAsync();
        Task<IEnumerable<PlanToDropDownViewModel>> GetPlanToDropDownsAsync();   

    }
}
