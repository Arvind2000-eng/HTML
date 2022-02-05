using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;




namespace HelperLand.ViewModels
{
    public class HelperViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Mobile { get; set; }

    }
}
