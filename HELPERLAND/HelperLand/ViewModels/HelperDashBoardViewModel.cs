using System.Collections.Generic;
using HelperLand.Models;

namespace HelperLand.ViewModels
{
    public class HelperDashBoardViewModel
    {
        public List<ServiceRequest> dashboardData { get; set; }

        public List<ServiceRequest> serviceHistory { get; set; }
        public List<UserAddress> userAddressData { get; set; }
    }
}
