using System.ComponentModel.DataAnnotations;

namespace HelperLand.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
