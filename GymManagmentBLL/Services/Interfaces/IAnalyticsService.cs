using GymManagmentBLL.ViewModels.AnalyticsViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IAnalyticsService
    {

        public Task<AnalyticsViewModel> GetAnalyticsDataAsync();
    }
}
