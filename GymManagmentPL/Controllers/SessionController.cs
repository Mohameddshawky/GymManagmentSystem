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
           await Helper1();
            await Helper2();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                await Helper1();
                await Helper2();
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
                await Helper1();
                await Helper2();
                TempData["ErrorMessage"] = "Failed to create session.";
                return View(createSession);

            }
        }
        public async Task<IActionResult> Edit(int id)
        {

            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id.";
                return RedirectToAction(nameof(Index));
            }
            var session=await sessionService.GetToUpdateSessionAsync(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Can Not Be Updated.";
                return RedirectToAction(nameof(Index));
            }
            await Helper2();
            return View(session);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                await Helper2();
                return View(updateSession);
            }
            var isUpdated = await sessionService.UpdateSessionAsync(id, updateSession);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await Helper2();
                TempData["ErrorMessage"] = "Failed to update session.";
                return View(updateSession);
            }
        }
        private async Task Helper1()
        {
            var Categories = await sessionService.GetCategoryFroDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
          
        }
        private async Task Helper2()
        {
            var Trainers = await sessionService.GetTrainersFroDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }
    }
}
