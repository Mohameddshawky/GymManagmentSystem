using GymManagmentBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetAllMemberAsync();
        Task<bool> CreateMemberAsync(CreateMemberViewModel model);

        Task<MemberViewModel?> GetMemberDetailsAsync(int id);
        Task<HealthRecordViewModel> GetMemberHealthDetailsAsync(int id);
        
        Task<UpdateMemberViewModel> GetMemberToUbdateAsync(int id);

        Task<bool> UpdateMemberAsync(int id,UpdateMemberViewModel model);
        Task<bool> DeleteMemberAsync(int id);
    }
}
