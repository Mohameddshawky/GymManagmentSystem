using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberService(
        IGenaricRepository<Member> repository,
        IMapper mapper,
        IGenaricRepository<MemberShip> membershipRepository,
        IGenaricRepository<Plan> PlanRepository,
        IGenaricRepository<HealthRecord> healthRepo
        ) : IMemberService
    {
        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model)
        {
            try
            {
                var check =
                      (await repository.GetAllAsync(x => x.Email == model.Email || x.PhoneNumber == model.PhoneNumber)).Any();
                if (check) return false;
                var member = mapper.Map<Member>(model);
                await repository.AddAsync(member);
                var result = await repository.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync()
        {
            var members = await repository.GetAllAsync();
            var result = mapper.Map<IEnumerable<MemberViewModel>>(members);
            return result;

        }
        public async Task<MemberViewModel?> GetMemberDetailsAsync(int id)
        {
            var member = await repository.GetAsync(id);
            if (member == null) return null;
            var result = new MemberViewModel
            {
                Id = member.Id,
                Photo = member.Photo!,
                Name = member.Name,
                Email = member.Email,
                Phone = member.PhoneNumber,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                Address = $" {member.Address.BuildingNumber} , {member.Address.Street} , {member.Address.City}",
            };
            var Activemembership = (await membershipRepository.GetAllAsync(x => x.MemberId == member.Id&&x.Statue=="Active")).FirstOrDefault();
            if(Activemembership != null)
            {               
                result.MemberShipStartDate = Activemembership.CreatedAt.ToString("yyyy-MM-dd");
                result.MemberShipEndDate = Activemembership.EndDate.ToString("yyyy-MM-dd");
                var plan = await PlanRepository.GetAsync(Activemembership!.PlanId);
                result.PlanName = plan!.Name;
            }

            return result;
        }

        public async Task<HealthRecordViewModel> GetMemberHealthDetailsAsync(int id)
        {
           var healthRecord=  await healthRepo.GetAsync(id);
              if(healthRecord == null) return null!;
              var result = mapper.Map<HealthRecordViewModel>(healthRecord);
                return result;
        }
    }
}
