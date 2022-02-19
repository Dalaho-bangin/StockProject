using Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Users
{
    public class EditUserDTo
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsEmailActive { get; set; }

        public bool IsMobileActive { get; set; }

        public string Avatar { get; set; }


    }

     public enum EditUserResult
    {
        SuccessEdit,
        ErrorEdit,
        IsExistMobileNumber,
        NotFoundUser
    }
}