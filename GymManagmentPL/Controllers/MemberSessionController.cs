using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberSeesionViewModels;
using GymManagmentBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly IMemberSesionService service;
        private readonly IMemberService memberService;

        public MemberSessionController(IMemberSesionService service,IMemberService memberService)
        {
            this.service = service;
            this.memberService = memberService;
        }
        public async Task<IActionResult> Index()
        {
            var sessions =await service.GetSessionsForBookingAndAttendance();
            return View(sessions);
        }

     

        public async Task<IActionResult> GetMembersForUpcomingSession(int sessionId)
        {
            var members =await service.GetMembersForSession(sessionId);
            ViewBag.SessionId = sessionId;
            return View(members);
        }

        public async Task<IActionResult> GetMembersForOngoingSessions(int sessionId)
        {
            var members =await service.GetMembersForSession(sessionId);
            ViewBag.SessionId = sessionId;
            return View(members);
        }

       

  

        public async Task<IActionResult> Create(int sessionId)
        {
            await LoadMembersDropDown();
            ViewBag.SessionId = sessionId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await  LoadMembersDropDown();
                ViewBag.SessionId = model.SessionId;
                ModelState.AddModelError("InvalidData", "Please select a member.");
                return View(model);
            }

            var ok =await service.CreateBooking(model.SessionId, model.MemberId);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = ok
                ? "Booking created successfully."
                : "Failed to create booking. Member might already be booked or session is full.";

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { sessionId = model.SessionId });
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int memberId, int sessionId)
        {
            var ok = await service.CancelBooking(memberId, sessionId);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = ok
                ? "Booking canceled successfully."
                : "Failed to cancel booking.";
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { sessionId });
        }




        [HttpPost]
        public async Task<IActionResult> MarkAttended(int memberId, int sessionId)
        {
            var ok = await service.MarkAttended(memberId, sessionId);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = ok
                ? "Member marked as attended."
                : "Failed to update attendance.";
            return RedirectToAction(nameof(GetMembersForOngoingSessions), new { sessionId });
        }


        private async Task LoadMembersDropDown()
        {
            var members =await memberService.GetAllMemberAsync() ?? new List<MemberViewModel>();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }


    }
}
