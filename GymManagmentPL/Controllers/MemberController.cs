using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            var members =await memberService.GetAllMemberAsync();
            return View(members);
        }
        public async Task<IActionResult>Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
                
            }
            var MemberDetails = await memberService.GetMemberDetailsAsync(id);
            if (MemberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            
            return View(MemberDetails);
        }
        public async  Task<IActionResult>HealthRecordDetails(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var MemberHealth = await memberService.GetMemberHealthDetailsAsync(id);
            if (MemberHealth == null)
            {
                TempData["ErrorMessage"] = "Member Health Record not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(MemberHealth);
        }
    }
}
