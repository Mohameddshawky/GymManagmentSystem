using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace GymManagmentBLL.Services.Classes
{
    public class SessionService(IUnitOfWork unitOfWork,IMapper mapper) : ISessionService
    {

        public async Task<IEnumerable<SessionViewModel>>  GetAllSessionAsync()
        {
            var repo =  unitOfWork.sessionRepository;
            var sessions =await repo.GetSessionsWithTrainerAndCategoryAsync();
            if (!sessions.Any()) return [];

            var result=mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            foreach (var item in result)
            {
                var cnt =await repo.GetCountOfBookedSlotsAsync(item.Id);
                item.AvalibleSlots =item.Capcity- cnt;
            }
            return result;


        }

        public async Task<SessionViewModel?> GetSessionDetailsAsync(int id)
        {
           var repo=unitOfWork.sessionRepository;
            var session=await repo.GetByIdWithIncludeAsync(id);
            if (session is null) return null;

            var result=mapper.Map<SessionViewModel>(session);
            var cnt = await repo.GetCountOfBookedSlotsAsync(result.Id);
            result.AvalibleSlots = result.Capcity - cnt;
            return result;

        }
        public async Task<bool> CreateSessionAsync(CreateSessionViewModel session)
        {
            if(await IsCategoryExistAsync(session.CategoryId) ||
               await IsTrainerExistAsync(session.TrainerId)||
               session.StartDate>session.EndDate||
               session.Capacity>25) return false;

            try
            {
                var Created = mapper.Map<Session>(session);
                await unitOfWork.GetRepository<Session>().AddAsync(Created);
                return await unitOfWork.SaveChangesAsync()>0;
            }
            catch (Exception ex)
            {
                return false;   
            }

        }
        private async Task<bool> IsTrainerExistAsync(int TrainerId)
        {
            var trainer = await unitOfWork.GetRepository<Trainer>().GetAsync(TrainerId);
            return trainer is null; 
        }
        private async Task<bool> IsCategoryExistAsync(int CategotyId)
        {
            var category = await unitOfWork.GetRepository<Trainer>().GetAsync(CategotyId);
            return category is null; 
        }
    }
}
