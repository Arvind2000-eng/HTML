using System.ComponentModel.DataAnnotations;

namespace HelperLand.ViewModels
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "OldPassword Is Required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="Password Is Required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\\-_+!@#$%^&*().=<>,?/:;|~{}])\\S{8,24}$", ErrorMessage = "Please Fullfill Required Condition for Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
