using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Number { get; set; }
        public string Position { get; set; } 
        public int SquadTeamIdsTeamId { get; set; } 
    }
}
