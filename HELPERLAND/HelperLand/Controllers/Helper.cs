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
            helperDashBoardViewModel.totalDashboardCount = helperDashBoardViewModel.dashboardData.Count;
            helperDashBoardViewModel.totalServiceHistoryCount = helperDashBoardViewModel.serviceHistory.Count;

            helperDashBoardViewModel.upcommingService = new List<ServiceRequest>();
            var d = _coreDBContext.ServiceRequests.Where(x => x.SpacceptedDate != null).ToList();
            var e = d.Where(x => x.ServiceProviderId == id).ToList();

            if (e != null)
            {
                foreach (var item in e)
                {
                    if (item.ServiceStartDate > DateTime.Now)
                    {
                        if(item.Status == IdForAccepted)
                        {
                            helperDashBoardViewModel.upcommingService.Add(item);
                        }
                    }
                }
            }
            helperDashBoardViewModel.totalUpcommingServiceCount = helperDashBoardViewModel.upcommingService.Count;

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


            helperDashBoardViewModel.favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            //helperDashBoardViewModel.favoriteAndBlockeds.Add(new FavoriteAndBlocked());
            var z1 = _coreDBContext.FavoriteAndBlockeds.Where(x=>x.UserId==id).ToList();
            if(z1.Count > 0)
            {
                foreach(var item in z1)
                {
                    helperDashBoardViewModel.favoriteAndBlockeds.Add(item);
                }
            }


            helperDashBoardViewModel.userDataforfandb = new List<User>();
            var z2 = _coreDBContext.Users.ToList();
            if (z2.Count != 0)
            {
                foreach(var item in z2)
                {
                    if (item.UserTypeId == 1)
                    {
                        helperDashBoardViewModel.userDataforfandb.Add(item);
                    }
                }
            }

            var r1 = _coreDBContext.Ratings.Where(x => x.RatingTo == id).ToList();
            helperDashBoardViewModel.myrateList = new List<MyrattingData>();
            if (r1.Count != 0)
            {
                foreach (var item in r1)
                {
                    helperDashBoardViewModel.myrate = new MyrattingData();

                    helperDashBoardViewModel.myrate.Rate = (float)item.Ratings;
                    helperDashBoardViewModel.myrate.RattingComment = item.Comments;

                    var r2=_coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == item.ServiceRequestId).First();
                    helperDashBoardViewModel.myrate.ServiceId = r2.ServiceId;
                    helperDashBoardViewModel.myrate.ServiceStartDate = r2.ServiceStartDate.Date;
                    helperDashBoardViewModel.myrate.ServiceStartTime = r2.ServiceStartDate.Date + r2.ServiceStartDate.TimeOfDay;
                    helperDashBoardViewModel.myrate.SubTotalTime = r2.SubTotal;

                    var r3= _coreDBContext.Users.Where(x => x.UserId==r2.UserId).First();
                    helperDashBoardViewModel.myrate.RattingFrom = r3.FirstName+" "+r3.LastName;

                    helperDashBoardViewModel.myrateList.Add(helperDashBoardViewModel.myrate);
                }
            }
            helperDashBoardViewModel.totalmyRattingData= helperDashBoardViewModel.myrateList.Count();







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
            var b = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == helperDashBoardViewModel.acceptServiceno).First();
            b.SpacceptedDate = DateTime.Now;
            b.Status = IdForAccepted;
            _coreDBContext.SaveChanges();

            return RedirectToAction("Index","Helper");
        }

        [HttpPost]
        public IActionResult UpcommingServiceCancel(HelperDashBoardViewModel helperDashBoardViewModel)
        {
            var b = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == helperDashBoardViewModel.upcommingServiceno).First();
            b.Status = IdForCanceledFromHelper;
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index", "Helper");
        }

        public IActionResult UpcommingServiceComplete(HelperDashBoardViewModel helperDashBoardViewModel)
        {
            var b = _coreDBContext.ServiceRequests.Where(x => x.ServiceRequestId == helperDashBoardViewModel.upcommingServiceno).ToList();
            b[0].Status = IdForCompleted;
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

        public IActionResult BlockAndUnblock(HelperDashBoardViewModel model)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var f1=_coreDBContext.FavoriteAndBlockeds.Where(x => x.UserId == id).ToList();
            if (f1.Count == 0)
            {
                FavoriteAndBlocked favandblock = new FavoriteAndBlocked();
                favandblock.UserId = id;
                favandblock.TargetUserId = model.TargetUserId;
                favandblock.IsBlocked = true;

                _coreDBContext.FavoriteAndBlockeds.Add(favandblock);
                _coreDBContext.SaveChanges();
            }
            else
            {
                foreach(var kk1 in f1)
                {
                    if (kk1.UserId==id && kk1.TargetUserId==model.TargetUserId)
                    {
                        var ok=_coreDBContext.FavoriteAndBlockeds.Where(_x => _x.UserId == id).ToList();
                        var ok1=ok.Where(_x => _x.TargetUserId == model.TargetUserId).First();
                        if (ok1.IsBlocked == true)
                        {
                            ok1.IsBlocked = false;
                        }
                        else
                        {
                            ok1.IsBlocked = true;
                        }
                        _coreDBContext.SaveChanges();
                    }
                    else
                    {
                        FavoriteAndBlocked favandblock = new FavoriteAndBlocked();
                        favandblock.UserId = id;
                        favandblock.TargetUserId = model.TargetUserId;
                        favandblock.IsBlocked = true;

                        _coreDBContext.FavoriteAndBlockeds.Add(favandblock);
                        _coreDBContext.SaveChanges();
                    }
                }
                
            }
            
            return RedirectToAction("Index", "Helper");
        }


    }
}
