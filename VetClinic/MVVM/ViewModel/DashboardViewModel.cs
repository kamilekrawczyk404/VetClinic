using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Services;

namespace VetClinic.MVVM.ViewModel
{
    public class DashboardViewModel : ViewModel
    {
        private IUserSessionService _userSessionService;

        public DashboardViewModel(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
    }
}
