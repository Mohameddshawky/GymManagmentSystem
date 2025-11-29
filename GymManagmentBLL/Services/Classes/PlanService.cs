using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModels;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class PlanService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        ) : IPlanService
    {
        public async Task<IEnumerable<PlanViewModel>> GetAllPlanAsync()
        {
            var Plans = await unitOfWork.GetRepository<Plan>().GetAllAsync();
            if (Plans is null || !Plans.Any()) return []; 
            var result = mapper.Map<IEnumerable<PlanViewModel>>(Plans);
            return result;
        }

        public async Task<PlanViewModel?> GetPlanDetailsAsync(int id)
        {
            var plan =await unitOfWork.GetRepository<Plan>().GetAsync(id);
            if (plan is null) return null;
            var result = mapper.Map<PlanViewModel>(plan);
            return result;
        }

        public async Task<UpdatePlanViewModel?> GetPlanToUpdate(int id)
        {
            var plan =await  unitOfWork.GetRepository<Plan>().GetAsync(id);
            if (plan is null||plan.IsActive==false|| await HasMembershipAsync( id)) return null;
            var result = mapper.Map<UpdatePlanViewModel>(plan);
            return result;

        }

        public async Task<bool> TogglePlanAsync(int id)
        {
            var repository = unitOfWork.GetRepository<Plan>();
            var plan = await repository.GetAsync(id);

            if (await HasMembershipAsync(id)) return false;
            plan!.IsActive = (plan.IsActive == true ? false : true);
            plan.UbdatedAt = DateTime.Now;
            try
            {
                repository.Update(plan);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;

            }
        }

        public async Task<bool> UpdatePlanAsync(int id, UpdatePlanViewModel model)
        {
            var repository = unitOfWork.GetRepository<Plan>();
            var plan =await repository.GetAsync(id);

            if (await HasMembershipAsync(id)) return false;

            plan!.Description=model.Description;
            plan.Price=model.Price;
            plan.DurationDays=model.DurationDays;  
            plan.UbdatedAt=DateTime.Now;
            try
            {
                repository.Update(plan);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;

            }
        }
        private async Task<bool> HasMembershipAsync(int id)
        {
            var membership=await unitOfWork.GetRepository<MemberShip>()
                .GetAllAsync(p => p.Id == id&&p.Statue=="Active");
            return membership.Any();
        }

    }
}
