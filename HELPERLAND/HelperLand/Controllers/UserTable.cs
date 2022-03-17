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
    public class UserTable : Controller
    {
        
        public enum usertype{
            User=1,
            Helper=2
        }
        int userNo= (int)usertype.User;
        int helperNo= (int)usertype.Helper;

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public UserTable(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
            ViewData["Invalid"] = "Email Not Exist";
        }
       
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel userViewModel)
        {
            User user = _coreDBContext.Users.Where(x => x.Email == userViewModel.Email).FirstOrDefault();
            if (user == null && ModelState.IsValid)
            {
                
                User user1 = new User();
                user1.CreatedDate = DateTime.Today;
                user1.ModifiedDate = DateTime.Today;
                user1.Mobile = userViewModel.Mobile.ToString();
                user1.FirstName = userViewModel.FirstName;
                user1.LastName = userViewModel.LastName;
                user1.Email = userViewModel.Email;
                user1.Password = userViewModel.Password;
                user1.UserTypeId = userNo;
                _coreDBContext.Users.Add(user1);
                _coreDBContext.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult HelperRegistration()
        {
            return View();
        }

        [HttpPost]
        public bool HelperRegistration(HelperViewModel helperViewModel)
        {
            bool found = _coreDBContext.Users.Any(x => x.Email == helperViewModel.Email);
            if (!found)
            {
                User user2 = new User();
                user2.CreatedDate = DateTime.Today;
                user2.ModifiedDate = DateTime.Today;
                user2.Mobile = helperViewModel.Mobile.ToString();
                user2.FirstName = helperViewModel.FirstName;
                user2.LastName = helperViewModel.LastName;
                user2.Email = helperViewModel.Email;
                user2.Password = helperViewModel.Password;
                user2.UserTypeId = helperNo;

                _coreDBContext.Users.Add(user2);
                _coreDBContext.SaveChanges();
                return found;
            }
            else
            {
                return found;
            }

        }

        public void Login()
        {
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            User user = _coreDBContext.Users.Where(x => x.Email == loginViewModel.Email).FirstOrDefault();
            
            if (user != null && user.Email == loginViewModel.Email && user.Password == loginViewModel.Password && user.UserTypeId == 1)
            {
                HttpContext.Session.SetString("UserId",user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.FirstName);
                HttpContext.Session.SetString("UserTypeId", user.UserTypeId.ToString());

                return RedirectToAction("Index", "Customer");
            }
            else if (user != null && user.Email == loginViewModel.Email && user.Password == loginViewModel.Password && user.UserTypeId == 2)
            {
                HttpContext.Session.SetString("UserId",user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.FirstName);
                HttpContext.Session.SetString("UserTypeId", user.UserTypeId.ToString());

                return RedirectToAction("Index", "Helper");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
                ViewBag.modal = string.Format("invalid");
                return View("~/Views/Home/Index.cshtml");
            }
        
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel ForgotPasswordViewModel)
        {

            return View();
        }
    }
}
