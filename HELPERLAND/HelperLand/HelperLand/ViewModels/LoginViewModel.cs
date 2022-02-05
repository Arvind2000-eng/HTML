using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace HelperLand.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
