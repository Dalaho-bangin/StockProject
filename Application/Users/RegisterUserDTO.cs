using Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Users
{
    public class RegisterUserDTO:CaptchaViewModel
    {
        [Required(ErrorMessage =ValidationMessage.IsRequired)]
        public string FirstName { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string LastName { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        public string Password { get;  set; }


        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}