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
            Completed = 1,
            Canceled = 2,
            Refunded = 3,
            CanceledFromHelper = 4
        }
        int IdForCompleted = (int)status.Completed;
        int IdForCanceled = (int)status.Canceled;
        int IdForRefunded = (int)status.Refunded;
        int IdForCanceledFromHelper = (int)status.CanceledFromHelper;

        private readonly HelperLand_DatabaseContext _coreDBContext;
        public Admin(HelperLand_DatabaseContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
        public IActionResult Index()
        {
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.serviceRequest = new List<ServiceRequest>();

            var a = _coreDBContext.ServiceRequests.ToList();
            if (a != null)
            {
                foreach (var item in a)
                {
                    adminViewModel.serviceRequest.Add(item);
                }
            }

            return View(adminViewModel);
        }
    }
}
