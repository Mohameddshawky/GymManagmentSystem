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
    }
}
