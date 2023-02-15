using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public string UserId { get; set; }
    }
}
