using GymManagmentBLL.Services.Interfaces;
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
            var plans =await planService.GetAllPlanAsync();
            return View(plans);
        }
    }
}
