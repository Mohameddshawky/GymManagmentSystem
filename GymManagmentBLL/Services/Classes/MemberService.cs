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
    public class MemberService(IGenaricRepository<Member> repository,IMapper mapper) : IMemberService
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
    }
}
