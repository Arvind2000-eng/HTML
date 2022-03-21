using System.Collections.Generic;
using HelperLand.Models;

namespace HelperLand.ViewModels
{
    public class HelperDashBoardViewModel
    {
        public List<ServiceRequest> dashboardData { get; set; }

        public List<ServiceRequest> newServiceRequest { get; set; }

        public List<ServiceRequest> serviceHistory { get; set; }



        public List<ServiceRequest> acceptService { get; set; }

        public List<ServiceRequest> upcommingService { get; set; }
        public List<UserAddress> userAddressData { get; set; }

        public int acceptServiceno { get; set; }

        public int upcommingServiceno { get; set; }









        public User uData { get; set; }

        public UserAddress HAddress { get; set; }


        public ChangePassViewModel changePassViewModel { get; set; }
    }
}
