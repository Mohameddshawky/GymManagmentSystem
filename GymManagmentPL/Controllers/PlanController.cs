using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public async Task<IActionResult> Index()
        {
            var plans = await planService.GetAllPlanAsync();
            return View(plans);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id.";
                return RedirectToAction(nameof(Index));

            }
            var PlanDetails = await planService.GetPlanDetailsAsync(id);
            if (PlanDetails == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(PlanDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id.";
                return RedirectToAction(nameof(Index));

            }
            var plan = await planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Can Not Be Updated.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);

        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdatePlanViewModel plan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(plan);
            }
            var isUpdated = await planService.UpdatePlanAsync(id, plan);
            if (!isUpdated)
            {
                TempData["ErrorMessage"] = "Plan Can Not Be Updated.";
            }
            TempData["SuccessMessage"] = "Plan Updated Successfully.";
            return RedirectToAction(nameof(Index));


        }
        [HttpPost]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id.";
                return RedirectToAction(nameof(Index));
            }
            var isToggled = await planService.TogglePlanAsync(id);
            if (!isToggled)
            {
                TempData["ErrorMessage"] = "Plan Can Not Be Toggled.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan Toggled Successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
