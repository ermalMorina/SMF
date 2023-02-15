using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Models;

namespace SpartanManageFootball.Persistence
{
    public class SMFContext : IdentityDbContext<RegisterUser>
    {
        public SMFContext(DbContextOptions<SMFContext> options) : base(options)
        {
        }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; } 
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Squad> Squads { get; set; }
        public DbSet<LeagueSquadDto> LeagueSquads { get; set; }
        public DbSet<MatchReferee> MatchReferee { get; set; }
        public DbSet<Standings> Standings { get; set; }
    }
}