using System.ComponentModel.DataAnnotations;

namespace HelperLand.ViewModels
{
    public class ZipCodeViewModel
    {
        [Required(ErrorMessage = "Please enter Postal code")]
        public string ZipcodeValue { get; set; }
    }
}