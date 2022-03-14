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
            bool found = _coreDBContext.Users.Any(x => x.ZipCode == model.ZipcodeValue);
            return found;
        }

        [HttpPost]
        public void NewAddressSave(AddressViewModel model)
        {
            //var d = (from Id in _coreDBContext.Cities select City).ToList();
            UserAddress user1 = new UserAddress();
            var s = _coreDBContext.States.ToList();
            var c = _coreDBContext.Cities.ToList();
            var z = _coreDBContext.Zipcodes.ToList();
            var control = 0;
            for(int i = 0; i < s.Count; i++)
            {
                for(int j = 0; j < c.Count; j++)
                {
                    if(s[i].StateId == c[j].StateId)
                    {
                        for (int k = 0; k < z.Count; k++)
                        {
                            if (c[i].Id == z[k].CityId)
                            {
                                if (model.PostalCode == z[k].ZipcodeValue)
                                {
                                    user1.State = s[i].StateName;
                                    control = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (control == 1)
                    {
                        break;
                    }
                    
                }
                if (control == 1)
                {
                    break;
                }
            }
            
            user1.AddressLine1 = model.Street;
            user1.AddressLine2 = model.HouseNumber;
            user1.PostalCode = model.PostalCode;
            user1.UserId = int.Parse(HttpContext.Session.GetString("UserId")); ;
            user1.City = model.City;
            user1.Mobile = model.Mobile;
            var emailAdd = _coreDBContext.Users.Where(x => x.UserId == int.Parse(HttpContext.Session.GetString("UserId"))).ToList();
            user1.Email = emailAdd[0].Email;


            _coreDBContext.UserAddresses.Add(user1);
            _coreDBContext.SaveChanges();

            //return View(BookService());

            //var id = int.Parse(HttpContext.Session.GetString("UserId"));

            //BookServiceViewModel bookServiceViewModel = new BookServiceViewModel();
            //bookServiceViewModel.userAddresses1 = new List<UserAddress>();
            //var a = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();

            //if (a != null)
            //{
            //    foreach (var item in a)
            //    {
            //        bookServiceViewModel.userAddresses1.Add(item);
            //    }
            //}
            //return bookServiceViewModel;
        }

        //public void SaveBillingAddress(AddressViewModel model1)
        //{
        //    ServiceRequestAddress user1 = new ServiceRequestAddress();
        //    user1.AddressLine1 = model1.Street;
        //    user1.AddressLine2 = model1.HouseNumber;
        //    user1.PostalCode = model1.PostalCode;
            
        //    user1.City = model1.City;

        //    _coreDBContext.ServiceRequestAddresses.Add(user1);
        //    _coreDBContext.SaveChanges();
        //    //return RedirectToAction("Index", "Home");

        //}

       
        public void SaveScheduleAndPlan(ScheduleAndPlanViewModel model2)
        {
            var b = _coreDBContext.Users.Where(x => x.UserTypeId == 2).ToList();

            ServiceRequest request = new ServiceRequest();
            request.UserId = int.Parse(HttpContext.Session.GetString("UserId")); ;
            request.ServiceId= 1;
            request.ServiceStartDate = model2.ServiceStartDate;
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


            _coreDBContext.ServiceRequests.Add(request);
            _coreDBContext.SaveChanges();
        }


        public void SaveServiceRequestAddress(modelServiceAdd model)
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));

            var a = _coreDBContext.UserAddresses.Where(x => x.UserId == id).ToList();
            var serReqId = 1000 + id;
            ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress();
            
                //serviceRequestAddress.ServiceRequestId = serReqId;
                serviceRequestAddress.AddressLine1 = a[model.saveadd].AddressLine1;
                serviceRequestAddress.AddressLine2 = a[model.saveadd].AddressLine2;
                serviceRequestAddress.City = a[model.saveadd].City;
                serviceRequestAddress.Email = a[model.saveadd].Email;
                serviceRequestAddress.Mobile = a[model.saveadd].Mobile;
                serviceRequestAddress.PostalCode = a[model.saveadd].PostalCode;
                //serviceRequestAddress.State = a[model.saveadd].State;
           
            

            _coreDBContext.ServiceRequestAddresses.Add(serviceRequestAddress);
            _coreDBContext.SaveChanges();

        }

        


    }
    public class modelServiceAdd
    {
        public int saveadd { get; set; }
    }

}