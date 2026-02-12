using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberShipService:IMemberShipService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberShipService(
            IUnitOfWork unitOfWork
            ,IMapper mapper
            )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> CreateMemberShipAsync(CreateMemberShipViewModel model)
        {
            try
            {
            var check=(await unitOfWork.MemberShipRepository.GetAllAsync(x=>x.MemberId==model.MemberId)).Any();
            if (check)
                return false;

            var memberShip = mapper.Map<MemberShip>(model);
            var plan=(await unitOfWork.GetRepository<Plan>().GetAsync(model.PlanId));
            if(plan==null) return false;

            var days = plan.DurationDays;
            memberShip.EndDate =DateTime.Now.AddDays(days);
            
                await unitOfWork.MemberShipRepository.AddAsync(memberShip);
                return await unitOfWork.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                return false;
            }
             
        }

        public async Task<bool> DeleteMemberShipAsync(int  memberId, int planId)
        {
            try
            {
                var memberShip =( await unitOfWork.MemberShipRepository.GetAllAsync(x => x.MemberId == memberId && x.PlanId == planId)).FirstOrDefault();
                if (memberShip == null) return false;
                //if (membership.EndDate.Date <= DateTime.Now.Date) return false;
                unitOfWork.MemberShipRepository.Delete(memberShip);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<MemberShipViewModel>> GetAllMemberShipAsync()
        {
            var memberShips =await unitOfWork.MemberShipRepository.GetMemberShipWithIncludeAsync();
            var res=mapper.Map<IEnumerable<MemberShipViewModel>>(memberShips);
            return res; 

        }

        public async Task<IEnumerable<MemberToDropDownViewModel>> GetMemberToDropDownsAsync()
        {
            var member = await unitOfWork.GetRepository<Member>().GetAllAsync();
            var res = mapper.Map<IEnumerable<MemberToDropDownViewModel>>(member);
            return res;
        }

        public async Task<IEnumerable<PlanToDropDownViewModel>> GetPlanToDropDownsAsync()
        {
            var plan =await  unitOfWork.GetRepository<Plan>().GetAllAsync(x=>x.IsActive==true);
            var res = mapper.Map<IEnumerable<PlanToDropDownViewModel>>(plan);
            return res;
        }
    }
}
