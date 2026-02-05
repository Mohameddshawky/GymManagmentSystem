using GymManagmentBLL.ViewModels.AccountViewModel;
using GymManagmentDAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> ValidateUserAsync(LoginViewModel model);
    }
}
