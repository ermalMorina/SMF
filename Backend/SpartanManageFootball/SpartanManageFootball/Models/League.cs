using SpartanManageFootball.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{
    public class League
    {
        [Key]
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }

        // Referee not need here
        /*[Required(ErrorMessage = "List of referees is required")]
        public List<Referee> Referees { get; set; }*/

        // We will remove the require for now (testing mode)
        /*[Required(ErrorMessage = "List of squads is required")]*/
        public List<Squad> Squads { get; set; } = new List<Squad>();
    }
}
