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
            if(await IsCategoryNotExistAsync(session.CategoryId) ||
               await IsTrainerNotExistAsync(session.TrainerId)||
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

        public async Task<UpdateSessionViewModel?> GetToUpdateSessionAsync(int id)
        {
            var session=await unitOfWork.sessionRepository.GetAsync(id);
            if (await IsAvailableToUpdate(session! )) return null!;
            var res=mapper.Map<UpdateSessionViewModel>(session);        
            return res;
        }

        public async Task<bool> UpdateSessionAsync(int id, UpdateSessionViewModel model)
        {
            if (await IsTrainerNotExistAsync(model.TrainerId)==false) return false;
            if(model.StartDate>model.EndDate) return false;
            try
            {

                var session = await unitOfWork.sessionRepository.GetAsync(id);          
                mapper.Map(model,session);
                session!.UbdatedAt=DateTime.Now; 
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> DeleteSessionAsync(int id)
        {
            var session = await unitOfWork.sessionRepository.GetAsync(id);
            if(await IsAvailableToRemove(session)==false) return false;
            try
            {
                unitOfWork.sessionRepository.Delete(session!);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex) { return false; }
        }
        private async Task<bool> IsTrainerNotExistAsync(int TrainerId)
        {
            var trainer = await unitOfWork.GetRepository<Trainer>().GetAsync(TrainerId);
            return trainer is null; 
        }
        private async Task<bool> IsCategoryNotExistAsync(int CategotyId)
        {
            var category = await unitOfWork.GetRepository<Trainer>().GetAsync(CategotyId);
            return category is null; 
        }
        private async Task<bool> IsAvailableToRemove(Session? session)
        {
            if (session is null) return false;
            
            if(session.StartDate>DateTime.Now)return false;
            if (session.StartDate<=DateTime.Now&&session.EndDate>DateTime.Now) return false;

            var flag = await unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id) > 0;
            return !flag;

        }
        private async Task<bool> IsAvailableToUpdate(Session? session)
        {
            if (session is null) return false;
            
            if(session.EndDate<DateTime.Now)return false;
            if (session.StartDate<DateTime.Now) return false;

            var flag = await unitOfWork.sessionRepository.GetCountOfBookedSlotsAsync(session.Id) > 0;
            return !flag;

        }

    }
}
