using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{
    public class Referee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Experience { get; set; }
        public string City { get; set; }
        public string Position { get; set; }
    }
}
