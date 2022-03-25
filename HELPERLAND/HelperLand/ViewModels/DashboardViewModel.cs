using System;
using System.Collections.Generic;
using HelperLand.Models;

namespace HelperLand.ViewModels
{
    public class DashboardViewModel
    {
        public List<ServiceRequest> dashboardData { get; set; }

        public List<ServiceRequest> serviceHistory { get; set; }

        public List<User> userData { get; set; }

        public List<User> userDataAll { get; set; }

        public User uData { get; set; }

        public List<UserAddress> userAddressData { get; set; }

        public UserAddress userAddress1 { get; set; }

        public int EditPerticularAddress { get; set; }

        public int CancelServiceId { get; set; }

        public ChangePassViewModel changePassViewModel { get; set; }

        public List<Rating> ratingDataTable { get; set; }
        public List<Rating1> ratingData { get; set; }

        public Rating1 rating { get; set; }
        public RescheduleService ress { get; set; }
        //public List<ServiceProviderDetail> spdData { get; set; }

    }

    public class Rating1
    {
        public int OnTimeArrival { get; set; }

        public int Friendly { get; set; }

        public int QuantityOfService { get; set; }

        public int servreqid { get; set; }

        public int serproid { get; set; }

        public string comments { get; set; }
    }

    public class RescheduleService
    {
        public int serviceRequestId { get; set; }
        public DateTime newDate { get; set; }
        public DateTime newTime { get; set; }
    }

    //public class ServiceProviderDetail
    //{
    //    public int SPId { get; set; }
    //    public string PFirstName { get; set; }
    //    public string PLastName { get; set; }
    //}
}
