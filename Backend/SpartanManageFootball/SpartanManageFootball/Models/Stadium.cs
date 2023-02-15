using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}