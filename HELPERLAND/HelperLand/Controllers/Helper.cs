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
            New = 0,
            Completed = 1,
            Canceled = 2,
            Refunded = 3,
            CanceledFromHelper=4,
            Pending = 5,
            Accepted=6
        }
        int IdForNew = (int)status.New;
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;
        int IdForCanceledFromHelper = (int)status.CanceledFromHelper;
        int IdForPending = (int)status.Pending;
        int IdForAccepted = (int)status.Accepted;

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Helper(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));


            HelperDashBoardViewModel helperDashBoardViewModel=new HelperDashBoardViewModel();

            helperDashBoardViewModel.uData = _coreDBContext.Users.Where(x => x.UserId == id).First();

            helperDashBoardViewModel.HAddress= _coreDBContext.UserAddresses.Where(x => x.UserId == id).FirstOrDefault();


            helperDashBoardViewModel.newServiceRequest = new List<ServiceRequest>();
            helperDashBoardViewModel.dashboardData = new List<ServiceRequest>();
            helperDashBoardViewModel.serviceHistory = new List<ServiceRequest>();
            helperDashBoardViewModel.userAddressData = new List<UserAddress>();
            var a = _coreDBContext.ServiceRequests.Where(x => x.ServiceProviderId == id).ToList();
           
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

            var x1 = _coreDBContext.ServiceRequests.ToList();
            helperDashBoardViewModel.userBasicData=new List<UserBasicData>();
            
            foreach (var n in x1)
            {
                helperDashBoardViewModel.userBasicData1 = new UserBasicData();
                helperDashBoardViewModel.userBasicData1.ServiceId = null;
                helperDashBoardViewModel.userBasicData1.UserId = null;
                helperDashBoardViewModel.userBasicData1.ServiceRequestId = null;
                helperDashBoardViewModel.userBasicData1.FirstName = null;
                helperDashBoardViewModel.userBasicData1.LastName = null;

                helperDashBoardViewModel.userBasicData1.AddressLine1 = null;
                helperDashBoardViewModel.userBasicData1.AddressLine2 = null;
                helperDashBoardViewModel.userBasicData1.PostalCode = null;
                helperDashBoardViewModel.userBasicData1.City = null;

                helperDashBoardViewModel.userBasicData.Add(helperDashBoardViewModel.userBasicData1);
            }

            for(var i = 0; i < x1.Count; i++)
            {
                helperDashBoardViewModel.userBasicData[i].UserId = x1[i].UserId;
                helperDashBoardViewModel.userBasicData[i].ServiceId = x1[i].ServiceId;
                helperDashBoardViewModel.userBasicData[i].ServiceRequestId = x1[i].ServiceRequestId;

                var x2 = _coreDBContext.Users.Where(x => x.UserId == x1[i].UserId).First();
                helperDashBoardViewModel.userBasicData[i].FirstName = x2.FirstName;
                helperDashBoardViewModel.userBasicData[i].LastName = x2.LastName;

                var x3 = _coreDBContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == x1[i].ServiceRequestId).First();
                helperDashBoardViewModel.userBasicData[i].AddressLine1 = x3.AddressLine1;
                helperDashBoardViewModel.userBasicData[i].AddressLine2 = x3.AddressLine2;
                helperDashBoardViewModel.userBasicData[i].PostalCode = x3.PostalCode;
                helperDashBoardViewModel.userBasicData[i].City = x3.City;

                helperDashBoardViewModel.userBasicData.Add(helperDashBoardViewModel.userBasicData[i]);

            }
            




            return View(helperDashBoardViewModel);
        }

        public IActionResult HelperSetting()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));

            HelperDashBoardViewModel helperDashBoardViewModel = new HelperDashBoardViewModel();
            helperDashBoardViewModel.uData = _coreDBContext.Users.Where(x => x.UserId == id).First();
            helperDashBoardViewModel.HAddress = _coreDBContext.UserAddresses.Where(x => x.UserId == id).FirstOrDefault();

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
