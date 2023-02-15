namespace SpartanManageFootball.Models
{
    public class UserRoleViewModel
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public int IdentityNumber { get; set; }
        public string  UserId { get; set; }
        public UserRoleViewModel(string userName, string name, string email, int identityNumber, string id)
        {
            Username = userName;
            RoleName = name;
            Email = email;
            IdentityNumber = identityNumber;
            UserId = id;
        }
    }
}
