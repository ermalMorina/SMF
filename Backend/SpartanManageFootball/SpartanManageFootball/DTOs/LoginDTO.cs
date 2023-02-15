using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
