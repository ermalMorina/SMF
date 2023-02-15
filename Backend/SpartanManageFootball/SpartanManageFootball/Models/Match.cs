using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpartanManageFootball.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public int HomeTeamTeamIdTeamId { get; set; }
        public int AwayTeamTeamIdTeamId { get; set; }
        public DateTime? MatchDate { get; set; }
        public string? Result { get; set; }
        public bool IsPlayed { get; set; }
        public int MatchWeek { get; set; }


    }
}
