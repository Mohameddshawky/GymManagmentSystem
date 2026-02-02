using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        
        public async Task<IActionResult> Create()
        {
           await Helper();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                await Helper();
                return View(createSession);
            }
            var isCreated =await sessionService.CreateSessionAsync(createSession);
            if (isCreated)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await Helper();
                TempData["ErrorMessage"] = "Failed to create session.";
                return View(createSession);

            }
        }
        private async Task Helper()
        {
            var Categories = await sessionService.GetCategoryFroDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
            var Trainers = await sessionService.GetTrainersFroDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }
    }
}
