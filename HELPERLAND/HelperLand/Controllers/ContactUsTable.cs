using System;
using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class ContactUsTable : Controller
    {

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public ContactUsTable(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
        public ActionResult ContactUsData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUsData(ContactUsDataViewModel contactUsDataViewModel)
        {
            
            if (ModelState.IsValid)
            {
                ContactU user1 = new ContactU();
                user1.PhoneNumber = contactUsDataViewModel.PhoneNumber.ToString();
                user1.Name = (contactUsDataViewModel.FirstName +" "+ contactUsDataViewModel.LastName).ToString();
                user1.Email = contactUsDataViewModel.Email;
                user1.Subject = contactUsDataViewModel.Subject;
                user1.Message = contactUsDataViewModel.Message;

                _coreDBContext.ContactUs.Add(user1);
                _coreDBContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("About", "Home");
            }
        }
    }
}
