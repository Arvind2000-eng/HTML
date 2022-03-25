using System;
using System.Collections.Generic;

using System.Linq;
using HelperLand.Data;
using HelperLand.ViewModels;
using HelperLand.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HelperLand.Controllers
{
    public class BookingService : Controller
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
        public BookingService(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public IActionResult BookService()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            
            BookServiceViewModel bookServiceViewModel = new BookServiceViewModel();
            bookServiceViewModel.userAddresses1 = new List<UserAddress>();
            var a=_coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();

            if (a != null)
            {
                foreach(var item in a)
                {
                    bookServiceViewModel.userAddresses1.Add(item);
                }
            }

            
            bookServiceViewModel.Helper = new List<User>();
            var b = _coreDBContext.Users.Where(x => x.UserTypeId == 2).ToList();

            if (b != null)
            {
                foreach (var item in b)
                {
                    bookServiceViewModel.Helper.Add(item);
                }
            }
            
            return View(bookServiceViewModel);
            //return View();
        }

        

        [HttpPost]
        public bool ZipCodeCheck(ZipCodeViewModel model)
        {
            bool found = _coreDBContext.Zipcodes.Any(x => x.ZipcodeValue == model.ZipcodeValue);
            return found;
        }

        [HttpPost]
        public void NewAddressSave(AddressViewModel model)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            UserAddress user1 = new UserAddress();
            
            var z = _coreDBContext.Zipcodes.Where(x=>x.ZipcodeValue==model.PostalCode).First();
            
            var c = _coreDBContext.Cities.Where(x=>x.Id==z.CityId).First();
            var s = _coreDBContext.States.Where(x=>x.StateId==c.StateId).First();

            user1.State = s.StateName;

            user1.AddressLine1 = model.Street;
            user1.AddressLine2 = model.HouseNumber;
            user1.PostalCode = model.PostalCode;
            user1.UserId = id ;
            user1.City = model.City;
            user1.Mobile = model.Mobile;
            var emailAdd = _coreDBContext.Users.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId"))).First();
            user1.Email = emailAdd.Email;

            _coreDBContext.UserAddresses.Add(user1);
            _coreDBContext.SaveChanges();

        }

        public int SaveScheduleAndPlan(ScheduleAndPlanViewModel model2)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            var b = _coreDBContext.Users.Where(x => x.UserTypeId == 2).ToList();
            var c = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();
            
            //DateTime d1= model2.StartTime.TimeOfDay;
            //DateTime d2 = d1 + model2.StartTime.TimeOfDay.Minutes;
            if(model2.ServiceStartDate.Date< DateTime.Today.Date)
            {
                return 2;
            }

            if (model2.ServiceStartDate.Date == DateTime.Today.Date)
            {
                if (model2.StartTime.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    return 1;
                }
            }
            

            DateTime starttime = model2.ServiceStartDate.Date + model2.StartTime.TimeOfDay;

            ServiceRequest request = new ServiceRequest();
            request.UserId = id;
            request.ServiceId= 10000+c.Count+id*10;
            request.ServiceStartDate = starttime;
            request.ZipCode=model2.Code;
            request.ServiceHours = model2.ServiceHrs;
            request.ExtraHours = model2.ExtraHours;
            request.SubTotal = (decimal)(model2.ServiceHrs + model2.ExtraHours);
            request.TotalCost = (decimal)model2.Total;
            request.PaymentDue = false;
            request.HasPets = model2.HavePets;
            request.CreatedDate=DateTime.Now;
            request.ModifiedDate=DateTime.Now;
            request.Distance = 0;
            request.Comments = model2.Comments;
            request.ServiceProviderId = b[model2.saveHelper].UserId;
            request.Status=IdForNew;

            _coreDBContext.ServiceRequests.Add(request);
            _coreDBContext.SaveChanges();
            return 0;
        }


        public void SaveServiceRequestAddress(modelServiceAdd model)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            BookServiceViewModel bookServiceViewModel = new BookServiceViewModel();

            var a = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();
            var c = _coreDBContext.ServiceRequests.Where(x => x.UserId == id).ToList();

            //For Service Request Id..........///////////////
            int serId = 10000 - 1 + c.Count + id * 10;
            var c1=_coreDBContext.ServiceRequests.Where(x=>x.ServiceId == serId).First();
            ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress();

            serviceRequestAddress.ServiceRequestId = c1.ServiceRequestId;
            serviceRequestAddress.AddressLine1 = a[model.saveadd].AddressLine1;
            serviceRequestAddress.AddressLine2 = a[model.saveadd].AddressLine2;
            serviceRequestAddress.City = a[model.saveadd].City;
            serviceRequestAddress.Email = a[model.saveadd].Email;
            serviceRequestAddress.Mobile = a[model.saveadd].Mobile;
            serviceRequestAddress.PostalCode = a[model.saveadd].PostalCode;

            //For State Name..................
            var c2 = _coreDBContext.ServiceRequests.Where(x => x.ServiceId == serId).First();
            var c3= _coreDBContext.Zipcodes.Where(x => x.ZipcodeValue==c2.ZipCode).First();
            var c4= _coreDBContext.Cities.Where(x => x.Id==c3.CityId).First();
            var c5= _coreDBContext.States.Where(x => x.StateId==c4.StateId).First();

            serviceRequestAddress.State = c5.StateName;
           
            

            _coreDBContext.ServiceRequestAddresses.Add(serviceRequestAddress);
            _coreDBContext.SaveChanges();

        }

        


    }
    public class modelServiceAdd
    {
        public int saveadd { get; set; }
    }

}