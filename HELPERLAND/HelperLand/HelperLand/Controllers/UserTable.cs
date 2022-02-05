using System;
using System.Collections.Generic;
using System.Linq;
using HelperLand.Data;
using HelperLand.Models;
using HelperLand.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelperLand.Controllers
{
    public class UserTable : Controller
    {

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public UserTable(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
        public IActionResult Index()
        {
            List<User> users = _coreDBContext.Users.ToList();
            return View(users);
        }
        public IActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _coreDBContext.Users.Add(user);
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            User user = _coreDBContext.Users.Where(x => x.UserId == id)
                .FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            _coreDBContext.Users.Update(user);
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            User user = _coreDBContext.Users.Where(x => x.UserId == id)
                .FirstOrDefault();
            return View(user);
        }
        public IActionResult Delete(int? id)
        {
            User user = _coreDBContext.Users.Where(x => x.UserId == id)
                .FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            _coreDBContext.Users.Remove(user);
            _coreDBContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                User user = new User();
                user.CreatedDate = DateTime.Today;
                user.ModifiedDate = DateTime.Today;
                user.Mobile = userViewModel.Mobile.ToString();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                user.Password = userViewModel.Password;
                user.UserTypeId = 1;
                _coreDBContext.Users.Add(user);
                _coreDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult HelperRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HelperRegistration(HelperViewModel helperViewModel)
        {
            if (ModelState.IsValid) 
            {
                User user = new User();
                user.CreatedDate = DateTime.Today;
                user.ModifiedDate = DateTime.Today;
                user.Mobile = helperViewModel.Mobile.ToString();
                user.FirstName = helperViewModel.FirstName;
                user.LastName = helperViewModel.LastName;
                user.Email = helperViewModel.Email;
                user.Password = helperViewModel.Password;
                user.UserTypeId = 2;

                _coreDBContext.Users.Add(user);
                _coreDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {


            return RedirectToAction("Index","Home");

        }
    }
}
