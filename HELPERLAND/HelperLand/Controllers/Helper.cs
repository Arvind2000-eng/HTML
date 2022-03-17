using System;
using System.Collections.Generic;
using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class Helper : Controller
    {
        public enum status
        {
            Completed = 1,
            Canceled = 2,
            Refunded = 3
        }
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Helper(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            //var id = 1;
            HelperDashBoardViewModel helperDashBoardViewModel=new HelperDashBoardViewModel();
            helperDashBoardViewModel.dashboardData = new List<ServiceRequest>();
            helperDashBoardViewModel.serviceHistory = new List<ServiceRequest>();
            helperDashBoardViewModel.userAddressData = new List<UserAddress>();
            var a = _coreDBContext.ServiceRequests.Where(x => x.ServiceProviderId == id).ToList();
            int? userid = null;
            if (a != null)
            {
                foreach (var item in a)
                {
                    if (item.Status == IdForCanceled || item.Status == IdForRefunded || item.Status == IdForCompleted)
                    {
                        helperDashBoardViewModel.serviceHistory.Add(item);
                        userid = item.UserId;
                        var c = _coreDBContext.UserAddresses.Where(x => x.UserId == userid).ToList();
                        helperDashBoardViewModel.userAddressData.Add(c[0]);
                    }
                    else if (item.ServiceStartDate > DateTime.Now)
                    {
                        helperDashBoardViewModel.dashboardData.Add(item);
                        userid = item.UserId;
                        var c = _coreDBContext.UserAddresses.Where(x => x.UserId == userid).ToList();
                        helperDashBoardViewModel.userAddressData.Add(c[0]);
                    }
                    else
                    {
                        helperDashBoardViewModel.serviceHistory.Add(item);
                        userid = item.UserId;
                        var c = _coreDBContext.UserAddresses.Where(x => x.UserId == userid).ToList();
                        helperDashBoardViewModel.userAddressData.Add(c[0]);
                    }

                }
            }

            return View(helperDashBoardViewModel);
        }
    }
}
