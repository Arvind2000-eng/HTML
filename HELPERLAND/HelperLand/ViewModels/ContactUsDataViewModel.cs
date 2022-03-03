using System;
using System.ComponentModel.DataAnnotations;

namespace HelperLand.ViewModels
{
    public class ContactUsDataViewModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }


        public string UploadFileName { get; set; }
        public string FileName { get; set; }
    }
}
