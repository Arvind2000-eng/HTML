using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class BookingService : Controller
    {
        private readonly HelperLand_DatabaseContext _coreDBContext;
        public BookingService(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public IActionResult Index()
        {
            return View("BookService");
        }

        [HttpPost]
        public bool ZipCodeCheck(ZipCodeViewModel model)
        {
            bool found = _coreDBContext.Users.Any(x => x.ZipCode == model.ZipcodeValue);
            
            return found;
        }

        [HttpPost]
        public void NewPasswordSave(AddressViewModel model)
        {
            
                UserAddress user1 = new UserAddress();
                user1.AddressLine1 = model.Street;
                user1.AddressLine2 = model.HouseNumber;
                user1.PostalCode = "382450";
                user1.UserId = 1;
                user1.IsDefault = true;
                user1.IsDeleted = false;
                user1.City = "Bonn";
                user1.Mobile = model.Mobile;

                _coreDBContext.UserAddresses.Add(user1);
                _coreDBContext.SaveChanges();
                //return RedirectToAction("Index", "Home");
           
        }

        public void SaveBillingAddress(AddressViewModel model1)
        {

            UserAddress user1 = new UserAddress();
            user1.AddressLine1 = model1.Street;
            user1.AddressLine2 = model1.HouseNumber;
            user1.PostalCode = model1.PostalCode;
            user1.UserId = 1;
            user1.IsDefault = true;
            user1.IsDeleted = false;
            user1.City = model1.City;
            user1.Mobile = model1.Mobile;

            _coreDBContext.UserAddresses.Add(user1);
            _coreDBContext.SaveChanges();
            //return RedirectToAction("Index", "Home");

        }
        
    }
}