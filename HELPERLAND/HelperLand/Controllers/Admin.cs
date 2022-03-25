using System.Collections.Generic;
using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class Admin : Controller
    {
        public enum status
        {
            New=0,
            Completed = 1,
            Canceled = 2,
            Refunded = 3,
            CanceledFromHelper = 4,
            Pending=5,
            Accepted=6
        }
        int IdForNew = (int)status.New;
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;
        int IdForCanceledFromHelper = (int)status.CanceledFromHelper;
        int IdForPending=(int)status.Pending;
        int IdForAccepted = (int)status.Accepted;

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Admin(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            AdminViewModel adminViewModel = new AdminViewModel();


            adminViewModel.IdForCompleted = IdForCompleted;
            adminViewModel.IdForCanceled = IdForCanceled;
            adminViewModel.IdForRefunded = IdForRefunded;
            adminViewModel.IdForCanceledFromHelper= IdForCanceledFromHelper;
            adminViewModel.IdForPending = IdForPending;


            adminViewModel.serviceRequest = new List<ServiceRequest>();

            var a = _coreDBContext.ServiceRequests.ToList();
            adminViewModel.totalServiceRequest = a.Count;
            if (a != null)
            {
                foreach (var item in a)
                {
                    adminViewModel.serviceRequest.Add(item);
                }
            }

            adminViewModel.userData = new List<User>();
            var b= _coreDBContext.Users.ToList();
            if (b != null)
            {
                foreach (var item in b)
                {
                    adminViewModel.userData.Add(item);
                }
            }

            var c = _coreDBContext.Users.ToList();
            adminViewModel.cityData = new List<UserCity>();
            
            if (c != null)
            {
                
                foreach (var item in c)
                {
                    adminViewModel.city = new UserCity();
                    var c1 = item.ZipCode;
                    int c2 = item.UserId;
                    adminViewModel.city.uId = c2;
                    adminViewModel.city.PostalCode = c1;
                    var c3 = _coreDBContext.Zipcodes.Where(x=>x.ZipcodeValue==c1).First();
                    var c4 = _coreDBContext.Cities.Where(x => x.Id == c3.CityId).First();
                    adminViewModel.city.CityName = c4.CityName;

                    adminViewModel.cityData.Add(adminViewModel.city);
                    
                }
            }

            var x1 = _coreDBContext.ServiceRequests.ToList();
            adminViewModel.userBasicDataAdmin = new List<UserBasicData>();

            foreach (var n in x1)
            {
                adminViewModel.userBasicData2 = new UserBasicData();
                adminViewModel.userBasicData2.ServiceId = null;
                adminViewModel.userBasicData2.UserId = null;
                adminViewModel.userBasicData2.ServiceRequestId = null;
                adminViewModel.userBasicData2.FirstName = null;
                adminViewModel.userBasicData2.LastName = null;

                adminViewModel.userBasicData2.AddressLine1 = null;
                adminViewModel.userBasicData2.AddressLine2 = null;
                adminViewModel.userBasicData2.PostalCode = null;
                adminViewModel.userBasicData2.City = null;

                adminViewModel.userBasicDataAdmin.Add(adminViewModel.userBasicData2);
            }

            for (var i = 0; i < x1.Count; i++)
            {
                adminViewModel.userBasicDataAdmin[i].UserId = x1[i].UserId;
                adminViewModel.userBasicDataAdmin[i].ServiceId = x1[i].ServiceId;
                adminViewModel.userBasicDataAdmin[i].ServiceRequestId = x1[i].ServiceRequestId;

                var x2 = _coreDBContext.Users.Where(x => x.UserId == x1[i].UserId).First();
                adminViewModel.userBasicDataAdmin[i].FirstName = x2.FirstName;
                adminViewModel.userBasicDataAdmin[i].LastName = x2.LastName;

                var x3 = _coreDBContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == x1[i].ServiceRequestId).First();
                adminViewModel.userBasicDataAdmin[i].AddressLine1 = x3.AddressLine1;
                adminViewModel.userBasicDataAdmin[i].AddressLine2 = x3.AddressLine2;
                adminViewModel.userBasicDataAdmin[i].PostalCode = x3.PostalCode;
                adminViewModel.userBasicDataAdmin[i].City = x3.City;

                adminViewModel.userBasicDataAdmin.Add(adminViewModel.userBasicDataAdmin[i]);

            }




            /////////////////rating Start //////////////////////////
            adminViewModel.ratingData = new List<Rating>();
            adminViewModel.rattingdataList = new List<RattingData>();
            
            var rat = _coreDBContext.Ratings.ToList();
            var u=_coreDBContext.ServiceRequests.ToList();
            
            var hId= new List<int?>();
            hId.Add(u[0].ServiceProviderId);
            for (int i = 0; i < u.Count; i++)
            {
                if(hId.Count!=0)
                {
                    var k = 0;
                    for(int j = 0; j < hId.Count; j++)
                    {
                        if(u[i].ServiceProviderId==hId[j])
                        {
                            k = 1;
                            break;
                        }
                    }
                    if(k==0)
                    {
                        hId.Add(u[i].ServiceProviderId);
                    }
                }
            }
            
            
            if (rat != null)
            {
                for(var i=0 ;i < hId.Count; i++)
                {
                    var k = 0;
                    float sum = 0;
                    for(var j=0; j < rat.Count; j++)
                    {
                        if (hId[i].Value == rat[j].RatingTo)
                        {
                            sum=sum+(float)rat[j].Ratings;
                            k++;
                        }
                    }
                    adminViewModel.rattingdata1 = new RattingData();
                    adminViewModel.rattingdata1.totalRatting = sum / k;
                    adminViewModel.rattingdata1.serviceProviderId = hId[i].Value;

                    adminViewModel.rattingdataList.Add(adminViewModel.rattingdata1);
                }
            }
            ///////////////////Rating End///////////////////////
            ///

            adminViewModel.AdminData = new User();
            var v1=_coreDBContext.Users.Where(u=>u.UserId == id).First();
            adminViewModel.AdminData = v1;

            return View(adminViewModel);
        }

        public IActionResult ActiveOrDeactiveUser(AdminViewModel model)
        {
            var a=_coreDBContext.Users.Where(x=>x.UserId==model.UserIdForAD).First();
            if (a != null)
            {
                if (a.IsActive == true)
                {
                    a.IsActive = false;
                }
                else
                {
                    a.IsActive = true;
                }
            }
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}
