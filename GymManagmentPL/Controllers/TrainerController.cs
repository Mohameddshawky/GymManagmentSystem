using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModels;
using GymManagmentBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        public async Task<IActionResult> Index()
        {
            var trainers =await trainerService.GetAllTrainerAsync();
            
            return View(trainers);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));

            }
            var trainerDetails = await trainerService.GetTrainerDetailsAsync(id);
            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainerDetails);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> CreateTrainer(CreateTrainerViewModel model)
        {

            if (ModelState.IsValid)
            {
                bool res = await trainerService.CreateTrainerAsync(model);
                if (res)
                    TempData["SuccessMessage"] = "Member created successfully.";
                else
                    TempData["ErrorMessage"] = "Failed to create Member.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(nameof(Create), model);
            }
        }


        public async Task<IActionResult> EditTrainer(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));

            }
            var TrainerDetails = await trainerService.GetTrainerToUbdateAsync(id);
            if (TrainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(TrainerDetails);

        }
        [HttpPost]
        public async Task<IActionResult> EditTrainer([FromRoute] int id, UpdateTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool res = await trainerService.UpdateTrainerAsync(id, model);
                if (res)
                    TempData["SuccessMessage"] = "Trainer updated successfully.";
                else
                    TempData["ErrorMessage"] = "Failed to update Trainer.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(nameof(EditTrainer), model);
            }
        }
    }
}
