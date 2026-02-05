using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AccountViewModel;
using GymManagmentDAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class AccountService(UserManager<ApplicationUser> manager) : IAccountService
    {
        public async Task<ApplicationUser?> ValidateUserAsync(LoginViewModel model)
        {
            var user = await manager.FindByEmailAsync(model.Email);
            if (user == null) return null;    
            
            var isPassValid =await manager.CheckPasswordAsync(user, model.Password);
            return isPassValid ? user : null;   
        }
    }
}
