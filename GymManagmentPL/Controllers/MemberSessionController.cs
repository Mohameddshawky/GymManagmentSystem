using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly IMemberSesionService service;

        public MemberSessionController(IMemberSesionService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var sessions =await service.GetSessionsForBookingAndAttendance();
            return View(sessions);
        }

        public async Task<IActionResult> GetMembersForUpcomingSession([FromRoute]int Id)
        {
            var members =await service.GetMembersForUpComingSession(Id);
            ViewBag.SessionId = Id;
            return (View(members));
        }
        public async Task<IActionResult> GetMembersForOngoingSessions([FromRoute]int Id)
        {
            var members =await service.GetMembersForOnGoingSession(Id);
            ViewBag.SessionId = Id;
            return (View(members));
        }


    }
}
