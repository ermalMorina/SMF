using MediatR;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Models;

namespace SpartanManageFootball.Interfaces
{
    public interface IIdentityService
    {
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<string> GetUserIdAsync(string userName);
        Task<bool> SigninUserAsync(string email, string password);
        Task<UserManagerResponse> ForgetPasswordAsync(string email);
        Task<UserManagerResponse> ChangePasswordAsync(ChangePasswordModel model);
        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<List<UserRoleViewModel>> GetUsersDetailsAsync();
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<bool> AssignUserToRole(string userName, IList<string> roles);
        Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);
        Task<bool> DeleteUserAsync(string userId);

        Task<List<Player>> GetPlayersOfSquad(int SquadTeamId);
        // LeagueSquads Methods
        Task<Unit> AddSquadsToLeague(LeagueSquadDto dto);
        Task<List<League>> GetSquadsInLeagues(int leagueId);

        //Algorithm to generate games
        Task<UserManagerResponse> GenerateGames(int id);//id is the id of the league
        Task<UserManagerResponse> AddPointsStandings(List<StandingsDTO> dto, string results);
        Task<UserManagerResponse> DefaultPointsStandings(int leaugeid);
        Task<List<Standings>> GetStandingsTable();
        Task<List<NamesSquadDTO>> GetMatches();
        Task<UserManagerResponse> GenerateRefereesForMatches(Match match);

    }
}