using GymManagmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagmentPL.Controllers
{
    [Authorize]
    public class HomeController (IAnalyticsService analyticsService): Controller
    {
        public async Task<IActionResult> Index()
        {
            var data =await analyticsService.GetAnalyticsDataAsync();
            return View(data);
        }
    }
}
