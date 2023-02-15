using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Models;
using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.DTOs
{
    [Keyless]
    public class LeagueSquadDto
    { 
        public int LeaguesLeagueId { get; set; }
        [Key]
        public List<SquadDto> SquadsTeamId { get; set; }
    }
}