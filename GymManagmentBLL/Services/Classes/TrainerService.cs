using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.TrainerViewModels;
using GymManagmentDAL.Entites;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class TrainerService
        (
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
        : ITrainerService
    {

        public async Task<IEnumerable<TrainerViewModel>> GetAllTrainerAsync()
        {
            var trainers =await unitOfWork.GetRepository<Trainer>().GetAllAsync();
            if (trainers is null || !trainers.Any()) return [];
            var result = mapper.Map<IEnumerable<TrainerViewModel>>(trainers);
            return result;

        }

        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel model)
        {
            //trainer.CreatedAt=DateTime.Now;
            
            try
            {
                if (await CheckIfUnique(model.Email, model.PhoneNumber)) return false;
                var trainer = mapper.Map<Trainer>(model);
                await unitOfWork.GetRepository<Trainer>().AddAsync(trainer);
                return await unitOfWork.SaveChangesAsync() > 0; 
            }
            catch (Exception)
            {           
                return false;
            }


        }



        private async Task<bool> CheckIfUnique(string email, string phoneNumber)
        {
            var check =
                (await unitOfWork.GetRepository<Trainer>().GetAllAsync(x => x.Email == email || x.PhoneNumber == phoneNumber)).Any();
            return check;
        }
    }
}
