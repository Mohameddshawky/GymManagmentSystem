using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService memberShipService;

        public MemberShipController(IMemberShipService memberShipService)
        {
            this.memberShipService = memberShipService;
        }
        public async Task<IActionResult> Index()
        {
            var memberShips =await memberShipService.GetAllMemberShipAsync();
            return View(memberShips);
        }
        public async Task<IActionResult> Create() 
        {
            await Helper();
            return View();      
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await Helper();
                return View(model);
            }
            var isCreated = await memberShipService.CreateMemberShipAsync(model);
            if (isCreated)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await Helper();
                TempData["ErrorMessage"] = "Failed to create session.";
                return View(model);

            }
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int memberId, int planId)
        {
            var res = await memberShipService.DeleteMemberShipAsync(memberId, planId);
            if (res)
                TempData["SuccessMessage"] = "Member deleted successfully.";
            else
                TempData["ErrorMessage"] = "Failed to delete Member.";
            return RedirectToAction(nameof(Index));

        }
        private async Task Helper()
        {
            var Members = await memberShipService.GetMemberToDropDownsAsync();
            ViewBag.Members = new SelectList(Members, "Id", "Name");

            var Plans = await memberShipService.GetPlanToDropDownsAsync();
            ViewBag.Plans = new SelectList(Plans, "Id", "Name");
        }
    }
}
