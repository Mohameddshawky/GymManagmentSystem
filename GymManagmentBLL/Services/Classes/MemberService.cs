using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberService(
        
        IMapper mapper,
        IUnitOfWork unitOfWork

        ) : IMemberService
    {
        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model)
        {
            try
            {
                if (CheckIfUnique(model.Email, model.PhoneNumber)) return false;
                var member = mapper.Map<Member>(model);
                await unitOfWork.GetRepository<Member>().AddAsync(member);
                var result = await unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member =await unitOfWork.GetRepository<Member>().GetAsync(id);
            if(member == null) return false;
            var Sessions = (await unitOfWork.GetRepository<MemberSession>().GetAllAsync(x => x.MemberId == id)).Select(x => x.SessionId);

            var HasActiveSessions= (await unitOfWork.GetRepository<Session>().GetAllAsync(x => Sessions.Contains(x.Id) && x.StartDate > DateTime.Now)).Any();
            if (HasActiveSessions) return false;

            var memberships = await unitOfWork.GetRepository<MemberShip>().GetAllAsync(x => x.MemberId == id);
            try
            {
                if (memberships.Any())
                {
                    foreach (var item in memberships)
                    {
                        unitOfWork.GetRepository<MemberShip>().Delete(item);
                    }
                   
                }
                unitOfWork.GetRepository<Member>().Delete(member); 
                return await unitOfWork.SaveChangesAsync()>0;

            }
            catch (Exception ex)
            {
                return false;
            }
             
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync()
        {
            var members = await unitOfWork.GetRepository<Member>().GetAllAsync();
            var result = mapper.Map<IEnumerable<MemberViewModel>>(members);
            return result;

        }
        public async Task<MemberViewModel?> GetMemberDetailsAsync(int id)
        {
            var member = await unitOfWork.GetRepository<Member>().GetAsync(id);
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
            var Activemembership = (await unitOfWork.GetRepository<MemberShip>().GetAllAsync(x => x.MemberId == member.Id)).FirstOrDefault(x=>x.Statue == "Active");
            if(Activemembership != null)
            {               
                result.MemberShipStartDate = Activemembership.CreatedAt.ToString("yyyy-MM-dd");
                result.MemberShipEndDate = Activemembership.EndDate.ToString("yyyy-MM-dd");
                var plan = await unitOfWork.GetRepository<Plan>().GetAsync(Activemembership!.PlanId);
                result.PlanName = plan!.Name;
            }

            return result;
        }

        public async Task<HealthRecordViewModel> GetMemberHealthDetailsAsync(int id)
        {
           var healthRecord=  await unitOfWork.GetRepository<HealthRecord>().GetAsync(id);
              if(healthRecord == null) return null!;
              var result = mapper.Map<HealthRecordViewModel>(healthRecord);
                return result;
        }

        public async Task<UpdateMemberViewModel> GetMemberToUbdateAsync(int id)
        {
            var member =await unitOfWork.GetRepository<Member>().GetAsync(id);
            if (member == null) return null!;
            var result = mapper.Map<UpdateMemberViewModel>(member);
            return result;
        }

        public async Task<bool> UpdateMemberAsync(int id, UpdateMemberViewModel model)
        {
            try
            {
               
                var emailExist=await unitOfWork.GetRepository<Member>().GetAllAsync(x=>x.Email==model.Email && x.Id != id);
                var phoneExsit=await unitOfWork.GetRepository<Member>().GetAllAsync(x=>x.PhoneNumber==model.PhoneNumber && x.Id != id);
                if(emailExist.Any() || phoneExsit.Any()) return false;

                var member = await unitOfWork.GetRepository<Member>().GetAsync(id);
                member.Email = model.Email;
                member.PhoneNumber = model.PhoneNumber;
                member.Address.BuildingNumber = model.BuildingNumber;
                member.Address.Street = model.Street;
                member.Address.City = model.City;
                member.UbdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Member>().Update(member);
                return (unitOfWork.SaveChangesAsync().Result > 0);

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool CheckIfUnique(string email, string phoneNumber)
        {
            var check =
                (unitOfWork.GetRepository<Member>().GetAllAsync(x => x.Email == email || x.PhoneNumber == phoneNumber).Result).Any();
            return check;
        }
    }
}
