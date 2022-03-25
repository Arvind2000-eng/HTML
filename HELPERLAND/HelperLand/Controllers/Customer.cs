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
            New = 0,
            Completed = 1,
            Canceled = 2,
            Refunded = 3,
            CanceledFromHelper = 4,
            Pending = 5,
            Accepted = 6
        }
        int IdForNew = (int)status.New;
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;
        int IdForCanceledFromHelper = (int)status.CanceledFromHelper;
        int IdForPending = (int)status.Pending;
        int IdForAccepted = (int)status.Accepted;
       
        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Customer(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }


        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            
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

            var e=_coreDBContext.Ratings.ToList();
            dashboardViewModel.ratingDataTable = new List<Rating>();
            if(e != null)
            {
                int i = 0;
                foreach(var item in e)
                {

                    dashboardViewModel.ratingDataTable.Add(item);
                    i++;
                }
            }

            dashboardViewModel.userDataAll = new List<User>();
            var d = _coreDBContext.Users.ToList();
            if (d != null)
            {
                foreach (var item in d)
                {
                    dashboardViewModel.userDataAll.Add(item);
                }
            }


            return View(dashboardViewModel);

        }



        public IActionResult UserSetting()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));

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


            var b = _coreDBContext.Users.Where(x => x.UserId == id).ToList();
            dashboardViewModel.userData = new List<User>();
            if (b != null)
            {
                foreach (var item in b)
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

            return RedirectToAction("UserSetting", "Customer");

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

            return RedirectToAction("UserSetting","Customer");

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

            return RedirectToAction("UserSetting", "Customer");
        }


        public IActionResult DeleteMyAddress(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var c = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();

            _coreDBContext.UserAddresses.Remove(c[dashboardViewModel.EditPerticularAddress]);

            _coreDBContext.SaveChanges();

            return RedirectToAction("UserSetting", "Customer");
        }

        public IActionResult CancelService(DashboardViewModel dashboardViewModel)
        {
            var id = dashboardViewModel.CancelServiceId;
            var c = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == id).ToList();

            c[0].Status = IdForCanceled;

            _coreDBContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public IActionResult RateSP(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var ServiceRequestId = dashboardViewModel.rating.servreqid;
            Models.Rating r =new Models.Rating();
            r.ServiceRequestId = ServiceRequestId;
            r.RatingFrom = id;
            r.RatingTo = dashboardViewModel.rating.serproid;
            r.RatingDate = DateTime.Now;
            r.OnTimeArrival = dashboardViewModel.rating.OnTimeArrival;
            r.Friendly = dashboardViewModel.rating.Friendly;
            r.QualityOfService = dashboardViewModel.rating.QuantityOfService;
            r.Ratings = (r.OnTimeArrival + r.Friendly + r.QualityOfService) / 3;
            r.Comments= dashboardViewModel.rating.comments;

            _coreDBContext.Ratings.Add(r);
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public IActionResult ServiceReschedule(DashboardViewModel dashboardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var c = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == dashboardViewModel.ress.serviceRequestId).First();
            if(c!= null)
            {
                c.ModifiedBy = id;
                c.ModifiedDate= DateTime.Now;
                c.ServiceStartDate = dashboardViewModel.ress.newDate.Date + dashboardViewModel.ress.newTime.TimeOfDay;
                _coreDBContext.SaveChanges();
            }
            return RedirectToAction("Index", "Customer");
        }
    }
}
