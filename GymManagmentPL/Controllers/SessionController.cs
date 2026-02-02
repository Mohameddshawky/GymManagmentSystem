using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        public async Task<IActionResult> Index()
        {
            var sessions =await sessionService.GetAllSessionAsync();
            return View(sessions);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id.";
                return RedirectToAction(nameof(Index));
            }
            var sessionDetails = await sessionService.GetSessionDetailsAsync(id);
            if (sessionDetails == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(sessionDetails);
        }
    }
}
