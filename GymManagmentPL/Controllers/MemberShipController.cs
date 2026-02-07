using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    }
}
