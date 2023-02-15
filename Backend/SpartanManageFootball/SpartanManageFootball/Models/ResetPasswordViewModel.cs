using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{
    public class ResetPasswordViewModel
    {
        public string token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
