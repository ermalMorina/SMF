using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SpartanManageFootball.Models
{

    public class MatchReferee
    {
        [Key]
        public int GID { get; set; }
        public int MatchIds { get; set; }
        public int RId { get; set; }
    }
}
