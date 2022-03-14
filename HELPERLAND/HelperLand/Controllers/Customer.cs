using System.Collections.Generic;
using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class Customer : Controller
    {
        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Customer(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }


        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            //var id = 1;
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.dashboardData = new List<ServiceRequest>();
            dashboardViewModel.serviceHistory = new List<ServiceRequest>();
            var a = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();

            if (a != null)
            {
                foreach (var item in a)
                {
                    if(item.ServiceStartDate > System.DateTime.Now)
                    {
                        dashboardViewModel.dashboardData.Add(item);
                    }
                    else
                    {
                        dashboardViewModel.serviceHistory.Add(item);
                    }
                    
                }
            }
            return View(dashboardViewModel);
            
        }

        public IActionResult MySetting()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            //var id = 1;
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.dashboardData = new List<ServiceRequest>();
            dashboardViewModel.serviceHistory = new List<ServiceRequest>();
            var a = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();
            
            if (a != null)
            {
                foreach (var item in a)
                {
                    if (item.ServiceStartDate > System.DateTime.Now)
                    {
                        dashboardViewModel.dashboardData.Add(item);
                    }
                    else
                    {
                        dashboardViewModel.serviceHistory.Add(item);
                    }

                }
            }


            var b = _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            dashboardViewModel.userData = new List<User>();
            if(b != null)
            {
                foreach(var item in b)
                {
                    dashboardViewModel.userData.Add(item);
                }
            }

            var c = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();
            dashboardViewModel.userAddressData = new List<UserAddress>();
            if (c != null)
            {
                foreach (var item in c)
                {
                    dashboardViewModel.userAddressData.Add(item);
                }
            }


            return View(dashboardViewModel);

        }


        public void MyDetails()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var a = _coreDBContext.Users.Where(x => x.UserId == id).ToList();

        }

        [HttpPost]
        public IActionResult ChangePass(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var a = _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            if(dashboardViewModel.changePassViewModel.OldPassword==a[0].Password)
            {
                a[0].Password = dashboardViewModel.changePassViewModel.Password;
               
                _coreDBContext.SaveChanges();
            }

            return RedirectToAction("MySetting", "Customer");

        }

        [HttpPost]
        public IActionResult EditMyDetail(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var a = _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            
            a[0].FirstName = dashboardViewModel.uData.FirstName;
            a[0].LastName = dashboardViewModel.uData.LastName;
            a[0].DateOfBirth = dashboardViewModel.uData.DateOfBirth;
            a[0].Mobile = dashboardViewModel.uData.Mobile;
            a[0].ModifiedBy = id;
            a[0].ModifiedDate = System.DateTime.Now;

            _coreDBContext.SaveChanges();

            return RedirectToAction("MySetting","Customer");

        }
    }
}
