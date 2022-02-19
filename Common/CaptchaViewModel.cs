using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CaptchaViewModel
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Captcha { get; set; }
    }
}
