using System;
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

        public int totalDashboardCount { get; set; }
        public int totalNewServicerequestCount { get; set; }
        public int totalUpcommingServiceCount { get; set; }
        public int totalServiceSceduleCount { get; set; }
        public int totalServiceHistoryCount { get; set; }





        public User uData { get; set; }
        public UserAddress HAddress { get; set; }
        public ChangePassViewModel changePassViewModel { get; set; }

        public UserBasicData userBasicData1 { get; set; }
        public List<UserBasicData> userBasicData { get; set; }


        public List<FavoriteAndBlocked> favoriteAndBlockeds { get; set; }

        public List<User> userDataforfandb { get; set; }
        public int TargetUserId { get; set; }

        public MyrattingData myrate { get; set; }
        public List<MyrattingData> myrateList { get; set; }
        public int totalmyRattingData { get; set; }
    }

    public class UserBasicData
    {
        public int? UserId { get; set; }
        public int? ServiceId { get; set; }
        public int? ServiceRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

    }

    public class MyrattingData
    {
        public int ServiceId { get; set; }
        public string RattingFrom { get; set; }
        public float Rate { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime ServiceStartTime { get; set; }
        public Decimal SubTotalTime { get; set; }
        public string RattingComment { get; set; }

    }
}
