using Microsoft.AspNetCore.Identity;

namespace SpartanManageFootball.Models
{
    public class RegisterUser : IdentityUser
    {

        public string FullName  { get; set; }
        public string PhoneNumber { get; set; }
        public int IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Photo>? Photos { get; set; }
    }
}