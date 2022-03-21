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
            Refunded = 3,
            CanceledFromHelper=4
        }
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;
        int IdForCanceledFromHelper = (int)status.CanceledFromHelper;

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

            helperDashBoardViewModel.uData = _coreDBContext.Users.Where(x => x.UserId == id).First();

            helperDashBoardViewModel.HAddress= _coreDBContext.UserAddresses.Where(x => x.UserId == id).FirstOrDefault();


            helperDashBoardViewModel.newServiceRequest = new List<ServiceRequest>();
            helperDashBoardViewModel.dashboardData = new List<ServiceRequest>();
            helperDashBoardViewModel.serviceHistory = new List<ServiceRequest>();
            helperDashBoardViewModel.userAddressData = new List<UserAddress>();
            var a = _coreDBContext.ServiceRequests.Where(x => x.ServiceProviderId == id).ToList();
            //int? userid = null;
            if (a != null)
            {
                foreach (var item in a)
                {
                    if (item.Status == IdForCanceled || item.Status == IdForRefunded || item.Status == IdForCompleted || item.Status==IdForCanceledFromHelper)
                    {
                        helperDashBoardViewModel.serviceHistory.Add(item);
                        
                    }
                    
                    else if (item.ServiceStartDate > DateTime.Now)
                    {
                        helperDashBoardViewModel.dashboardData.Add(item);
                        if (item.SpacceptedDate == null)
                        {
                            helperDashBoardViewModel.newServiceRequest.Add(item);
                        }

                    }
                    else
                    {
                        helperDashBoardViewModel.serviceHistory.Add(item);
                    }

                }
            }

            
            
            //helperDashBoardViewModel.acceptService = new List<ServiceRequest>();
            //var b = _coreDBContext.ServiceRequests.Where(x => x.SpacceptedDate == null).ToList();
            //var c = b.Where(x => x.ServiceProviderId == id).ToList();

            //if (c != null)
            //{
            //    foreach (var item in c)
            //    {
            //        helperDashBoardViewModel.acceptService.Add(item);

            //    }
            //}

            helperDashBoardViewModel.upcommingService = new List<ServiceRequest>();
            var d = _coreDBContext.ServiceRequests.Where(x => x.SpacceptedDate != null).ToList();
            var e = d.Where(x => x.ServiceProviderId == id).ToList();

            if (e != null)
            {
                foreach (var item in e)
                {
                    if (item.ServiceStartDate > DateTime.Now)
                    {
                        if(item.Status == 3)
                        {
                            
                        }
                        else
                        {
                            helperDashBoardViewModel.upcommingService.Add(item);
                        }
                    }
                }
            }



            return View(helperDashBoardViewModel);
        }

        [HttpPost]
        public IActionResult NewServiceRequestAccept(HelperDashBoardViewModel helperDashBoardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var b = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == helperDashBoardViewModel.acceptServiceno).ToList();
            
            b[0].SpacceptedDate = DateTime.Now;
            _coreDBContext.SaveChanges();

            return RedirectToAction("Index","Helper");
        }

        [HttpPost]
        public IActionResult UpcommingServiceCancel(HelperDashBoardViewModel helperDashBoardViewModel)
        {
            var b = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == helperDashBoardViewModel.upcommingServiceno).ToList();

            b[0].Status = 3;
           
            _coreDBContext.SaveChanges();

            return RedirectToAction("Index", "Helper");
        }

        public IActionResult EditMyDetail(HelperDashBoardViewModel helperDashBoardViewModel)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var a = _coreDBContext.Users.Where(x => x.UserId == id).First();

            a.FirstName = helperDashBoardViewModel.uData.FirstName;
            a.LastName = helperDashBoardViewModel.uData.LastName;
            a.DateOfBirth = helperDashBoardViewModel.uData.DateOfBirth;
            a.Mobile = helperDashBoardViewModel.uData.Mobile;
            a.ModifiedBy = id;
            a.ModifiedDate = System.DateTime.Now;

            var b = _coreDBContext.UserAddresses.Where(x => x.UserId == id).FirstOrDefault();
            if (b != null)
            {
                b.AddressLine1 = helperDashBoardViewModel.HAddress.AddressLine1;
                b.AddressLine2 = helperDashBoardViewModel.HAddress.AddressLine2;
                b.PostalCode = helperDashBoardViewModel.HAddress.PostalCode;
                b.City = helperDashBoardViewModel.HAddress.City;
                b.IsDefault = true;
                b.IsDeleted = false;
            }
            else
            {
                UserAddress helper1=new UserAddress();
                helper1.UserId = id;
                helper1.AddressLine1 = helperDashBoardViewModel.HAddress.AddressLine1;
                helper1.AddressLine2 = helperDashBoardViewModel.HAddress.AddressLine2;
                helper1.PostalCode = helperDashBoardViewModel.HAddress.PostalCode; 
                helper1.City = helperDashBoardViewModel.HAddress.City;
                helper1.IsDefault = true;
                helper1.IsDeleted = false;
                _coreDBContext.UserAddresses.Add(helper1);
            }

            _coreDBContext.SaveChanges();

            return RedirectToAction("Index", "Helper");

        }

        public IActionResult ChangePass(HelperDashBoardViewModel model)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var a = _coreDBContext.Users.Where(x => x.UserId == id).First();

            if(a.Password == model.changePassViewModel.OldPassword)
            {
                a.Password = model.changePassViewModel.Password;
                _coreDBContext.SaveChanges();
            }

            return RedirectToAction("Index", "Helper");
        }
    }
}
