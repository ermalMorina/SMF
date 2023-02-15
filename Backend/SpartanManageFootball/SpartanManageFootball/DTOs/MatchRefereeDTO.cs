using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Models;

namespace SpartanManageFootball.DTOs
{
    public class MatchRefereeDTO
    {
        public int MatchIds { get; set; }
        public int RId { get; set; }
    }
}
