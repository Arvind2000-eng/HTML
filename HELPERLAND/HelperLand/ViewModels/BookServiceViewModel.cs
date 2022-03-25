

using System.Collections.Generic;
using HelperLand.Models;

namespace HelperLand.ViewModels
{
    public class BookServiceViewModel
    {
        public ZipCodeViewModel zipcode { get; set; }

        public ScheduleAndPlanViewModel scheduleAndPlan { get; set; }
        public AddressViewModel address { get; set; }

        public List<UserAddress> userAddresses1 { get; set; }
        public List<User> Helper { get; set; }

        public float startTime { get; set; }
        public float totalamount { get; set; }
        public float totalservicetime { get; set; }
        public float extraservicetime { get; set; }
        public float basichrs { get; set; }





        public UserState state { get; set; }
    }

    public class UserState
    {
        public int uId { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }

        public string PostalCode { get; set; }
    }
}