using System.Collections.Generic;
using HelperLand.Models;

namespace HelperLand.ViewModels
{
    public class DashboardViewModel
    {
        public List<ServiceRequest> dashboardData { get; set; }

        public List<ServiceRequest> serviceHistory { get; set; }

        public List<User> userData { get; set; }

        public User uData { get; set; }

        public List<UserAddress> userAddressData { get; set; }

        public ChangePassViewModel changePassViewModel { get; set; }
    }
}
