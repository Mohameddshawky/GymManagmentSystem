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

        public async Task<TrainerDetailsViewModel> GetTrainerDetailsAsync(int id)
        {
            var trainer =await unitOfWork.GetRepository<Trainer>().GetAsync(id);
            if (trainer is null) return null!;
            var result = mapper.Map<TrainerDetailsViewModel>(trainer);
            return result;
        }

        public async Task<UpdateTrainerViewModel> GetTrainerToUbdateAsync(int id)
        {
            var trainer =await unitOfWork.GetRepository<Trainer>().GetAsync(id);

            if(trainer is null) return null!;

            var result = mapper.Map<UpdateTrainerViewModel>(trainer);
            return result;
        }

        public async Task<bool> UpdateTrainerAsync(int id, UpdateTrainerViewModel model)
        {
             var trainer =await unitOfWork.GetRepository<Trainer>().GetAsync(id);
            if(trainer is null) return false;
            try
            {
                if (await CheckIfUnique(model.Email, model.PhoneNumber)) return false;

                trainer.Name = model.Name;
                trainer.Email = model.Email;
                trainer.PhoneNumber = model.PhoneNumber;
                trainer.Address.BuildingNumber = model.BuildingNumber;
                trainer.Address.Street = model.Street;
                trainer.Address.City = model.City;
                trainer.Specialties = model.Specialization;
                unitOfWork.GetRepository<Trainer>().Update(trainer);
                return await unitOfWork.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

            
        }

        public async Task<bool> DeleteTrainerAsync(int id)
        {
            var trainer = await unitOfWork.GetRepository<Trainer>().GetAsync(id);

            var check=(await unitOfWork.GetRepository<Session>().GetAllAsync(x=>x.TrainerId==id&&x.StartDate>DateTime.Now)).Any();
            if (trainer is null||check) return false;

            try
            {
                unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
