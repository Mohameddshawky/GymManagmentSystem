using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModels;
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

        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model)
        {

            if (ModelState.IsValid)
            {
               bool res= await memberService.CreateMemberAsync(model);
                if(res)
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
    }
}
