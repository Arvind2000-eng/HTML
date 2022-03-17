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
    public class Customer : Controller
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
        //private LearnASPNETMVCWithRealAppsEntities db = new LearnASPNETMVCWithRealAppsEntities();

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

            dashboardViewModel.userData= _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            var a = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();

            if (a != null)
            {
                foreach (var item in a)
                {
                    if (item.Status == IdForCanceled || item.Status == IdForRefunded || item.Status == IdForCompleted)
                    {
                        dashboardViewModel.serviceHistory.Add(item);
                    }
                    else if (item.ServiceStartDate > DateTime.Now)
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

            dashboardViewModel.userData = _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            var a = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();
            
            if (a != null)
            {
                foreach (var item in a)
                {
                    if (item.Status == IdForCanceled || item.Status == IdForRefunded || item.Status == IdForCompleted)
                    {
                        dashboardViewModel.serviceHistory.Add(item);
                    }
                    else if(item.ServiceStartDate > DateTime.Now)
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

        public IActionResult EditMyAddress(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var c = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();

            c[dashboardViewModel.EditPerticularAddress].AddressLine1 = dashboardViewModel.userAddress1.AddressLine1;
            c[dashboardViewModel.EditPerticularAddress].AddressLine2 = dashboardViewModel.userAddress1.AddressLine2;
            c[dashboardViewModel.EditPerticularAddress].PostalCode = dashboardViewModel.userAddress1.PostalCode;
            c[dashboardViewModel.EditPerticularAddress].City = dashboardViewModel.userAddress1.City;
            c[dashboardViewModel.EditPerticularAddress].Mobile = dashboardViewModel.userAddress1.Mobile;
            
            _coreDBContext.SaveChanges();

            return RedirectToAction("MySetting", "Customer");
        }


        public IActionResult DeleteMyAddress(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var c = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();

            _coreDBContext.UserAddresses.Remove(c[dashboardViewModel.EditPerticularAddress]);

            _coreDBContext.SaveChanges();

            return RedirectToAction("MySetting", "Customer");
        }

        public IActionResult CancelService(DashboardViewModel dashboardViewModel)
        {
            var id = dashboardViewModel.CancelServiceId;
            var c = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == id).ToList();

            c[0].Status = IdForCanceled;

            _coreDBContext.SaveChanges();
            return RedirectToAction("MySetting", "Customer");
        }
    }
}
