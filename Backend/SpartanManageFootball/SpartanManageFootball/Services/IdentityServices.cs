using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SpartanManageFootball.Services
{
    public class IdentityServices : IIdentityService
    {
        private readonly UserManager<RegisterUser> _userManager;
        private readonly SignInManager<RegisterUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly SMFContext _smfcontext;

        public IdentityServices(UserManager<RegisterUser> userManager, SignInManager<RegisterUser> signInManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SMFContext smfcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _smfcontext = smfcontext;

        }

        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return roles.Select(role => (role.Id, role.Name)).ToList();
        }

        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return (user.Id, user.UserName, user.UserName, user.Email, roles);
        }
        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }
        public async Task<bool> SigninUserAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, false);
            return result.Succeeded;
        }
        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);

            result = await _userManager.AddToRolesAsync(user, usersRole);

            return result.Succeeded;
        }
        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string localHostUrl = "http://localhost:3000";
            string url = $"{localHostUrl}/reset-password?email={email}&token={validToken}";


            var senderEmail = _configuration["ReturnPaths:SenderEmail"];

            await _userManager.FindByEmailAsync(email);
            await _emailSender.SendEmailAsync(senderEmail, email, "Reset Password", "To reset the password click on the url: " +
              url);

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }
        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)

            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user is associated with this email!"
                };
            }

            if (model.NewPassword != model.ConfirmPassword)

            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Passwords do not match!"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);
            if (model.token != normalToken)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Passwords do not match!"
                };
            }
            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public async Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }
        public async Task<List<UserRoleViewModel>> GetUsersDetailsAsync()
        {
            var UserRolesView = new List<UserRoleViewModel>();
            var userRoles = _smfcontext.UserRoles.ToList();

            foreach (var userrole in userRoles)
            {

                var identitynumber = _smfcontext.Users.Where(x => x.Id == userrole.UserId).FirstOrDefault();
                var email = _smfcontext.Users.Where(x => x.Id == userrole.UserId).FirstOrDefault();
                var username = _smfcontext.Users.Where(x => x.Id == userrole.UserId).FirstOrDefault();
                var role = _smfcontext.Roles.Where(x => x.Id == userrole.RoleId).FirstOrDefault();
                var userid = _userManager.Users.Where(x => x.Id == userrole.UserId).FirstOrDefault();
                UserRolesView.Add(new UserRoleViewModel(username.UserName, role.Name, email.Email, identitynumber.IdentityNumber, userid.Id));
            }
            return UserRolesView;
        }
        public async Task<UserManagerResponse> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User doesn't exist",
                    IsSuccess = false,
                };
            }

            if (string.Compare(model.NewPassword, model.ConfirmPassword) != 0)
            {
                return new UserManagerResponse
                {
                    Message = "Passwords don't match",
                    IsSuccess = false,
                };
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return new UserManagerResponse
                {
                    Message = errors[0],
                    IsSuccess = false,
                };

            }
            return new UserManagerResponse
            {
                Message = "Password has been changed successfully!",
                IsSuccess = true,
            };
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<Unit> AddSquadsToLeague(LeagueSquadDto dto)
        {
            var league = await _smfcontext.Leagues.FindAsync(dto.LeaguesLeagueId);
            var userRoles = _smfcontext.Squads;

            foreach (var userrole in userRoles)
            {
                var identitynumber = _smfcontext.Squads.Where(x => x.TeamId == userrole.TeamId).FirstOrDefault();

                league.Squads.Add(userrole);
                var squads = dto.SquadsTeamId;

                foreach (var squad in squads)
                {
                    var team = _smfcontext.Squads.Where(x => x.TeamId == squad.TeamId).FirstOrDefault();
                    league.Squads.Add(team);
                }

                if (league == null)
                {
                    throw new Exception("League not found");
                }
            };

            await _smfcontext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<List<Player>> GetPlayersOfSquad(int SquadTeamId)
        {
            var players = await _smfcontext.Players.Where(x => x.SquadTeamIdsTeamId == SquadTeamId).ToListAsync();

            return players;
        }

        public async Task<List<League>> GetSquadsInLeagues(int leagueId)
        {
            var squads = await _smfcontext.Leagues
                .Where(x => x.LeagueId == leagueId)
                .Include(x => x.Squads)
                .ToListAsync();

            return squads;
        }
        public async Task<UserManagerResponse> AddRefereeToMatch(MatchRefereeDTO matchrefereeDTO)
        {
            if (matchrefereeDTO == null)
            {
                return new UserManagerResponse
                {
                    Message = "No null values allowed!",
                    IsSuccess = false,
                };
            }
            
                var refereeToMatch = new MatchReferee
                {
                    MatchIds = matchrefereeDTO.MatchIds,
                    RId = matchrefereeDTO.RId
                };
                await _smfcontext.MatchReferee.AddAsync(refereeToMatch);
                await _smfcontext.SaveChangesAsync();
            
            return new UserManagerResponse
            {
                Message = "Referees added to game",
                IsSuccess = true,
            };
        }
        //id parameter is the id of the league
        public async Task<UserManagerResponse> GenerateGames(int id)
        {
            var squads = await _smfcontext.Leagues
                .Where(x => x.LeagueId == id)
                .Include(x => x.Squads)
                .ToListAsync();

            if (squads == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no team in this league",
                    IsSuccess = false,
                };
            }
            //string bla = squads[0].Squads[0].Name;

            var squadNames = new List<int>();


            //Iterating to the list to get only the names of the squads
            for (int i = 0; i < squads[0].Squads.Count; i++)
            {
                squadNames.Add(squads[0].Squads[i].TeamId);
            }

            //Checks if the list of the squads is odd otherwise it cant generate games
            if (squads[0].Squads.Count % 2 != 0)
            {
                return new UserManagerResponse
                {
                    Message = "Teams in a league should be ODD!",
                    IsSuccess = false,
                };
            }


            Random rnd = new Random();
            var shuffledSquads = squadNames.OrderBy(a => rnd.Next()).ToList();

            var matchesToBeAdded = new List<Match>();
            var firstHalf = GenerateFirstHalfMatches(squadNames);
            var secondHalf = GenerateSecondtHalfMatches(firstHalf, shuffledSquads.Count);

            matchesToBeAdded.AddRange(firstHalf);
            matchesToBeAdded.AddRange(secondHalf);
            int numberOdDays = 0;
            bool days = true;
            int lengthOfArray = squadNames.Count;
            int count = 0;
            int matchesPerWeek = lengthOfArray / 2;
            var matches = 0;
            if (matchesPerWeek % 2 == 0)
            {
                matches = matchesPerWeek / 2;
            }
            else
            {
                matches = matchesPerWeek / 2 + 1;
            }
            DateTime today = DateTime.Today;
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime d = today.AddDays(daysUntilSaturday);

            foreach (var item in matchesToBeAdded)
            {
                count++;

                    if (!(count < matches))
                    {

                        if (days)
                        {
                            count = 0;
                            numberOdDays += 1;
                            days = false;
                        }
                        else
                        {
                            numberOdDays += 6;
                            days = true;
                            count = 1;
                        }
                    }

                Match match = new Match
                {
                    HomeTeamTeamIdTeamId = item.HomeTeamTeamIdTeamId,
                    AwayTeamTeamIdTeamId = item.AwayTeamTeamIdTeamId,
                    MatchWeek = item.MatchWeek,
                    MatchDate = d.AddDays(numberOdDays),
                    Result = null,
                };
                
                await _smfcontext.Matches.AddAsync(match);
                

                var success = await _smfcontext.SaveChangesAsync() > 0;
                await GenerateRefereesForMatches(match);
            }
            return null;
        }

        private List<Match> GenerateFirstHalfMatches(List<int> squads)
        {
            var listOfMatches = new List<Match>();
            var firstTeam = squads[0];

            int matchWeek = 1;
            while (listOfMatches.Count < squads.Count * (squads.Count - 1) / 2)
            {
                for (int i = 0; i < squads.Count; i += 2)
                {
                    var match = new Match
                    {
                        HomeTeamTeamIdTeamId = squads[i],
                        AwayTeamTeamIdTeamId = squads[i + 1],
                        MatchWeek = matchWeek
                    };

                    listOfMatches.Add(match);

                    if (listOfMatches.Count % (squads.Count / 2) == 0)
                    {
                        matchWeek++;
                    }
                }

                for (int i = squads.Count - 1; i > 1; i--)
                {
                    var tempSquad = squads[i - 1];
                    squads[i - 1] = squads[i];
                    squads[i] = tempSquad;
                }
            }

            for (int i = 2; i < squads.Count; i += 2)
            {
                var matchesFromMatchweek = listOfMatches?.Where(x => x.MatchWeek == i).ToList();
                var matchToSwap = matchesFromMatchweek?.FirstOrDefault(x => x.HomeTeamTeamIdTeamId == firstTeam);

                int indexOfMatchToBeSwaped = listOfMatches.IndexOf(matchToSwap);

                var match = new Match
                {
                    AwayTeamTeamIdTeamId = matchToSwap.HomeTeamTeamIdTeamId,
                    HomeTeamTeamIdTeamId = matchToSwap.AwayTeamTeamIdTeamId,
                    MatchWeek = matchToSwap.MatchWeek
                };

                listOfMatches[indexOfMatchToBeSwaped] = match;
            }

            return listOfMatches;
        }

        private List<Match> GenerateSecondtHalfMatches(List<Match> firstHalf, int numOfSquads)
        {
            var secondHalf = new List<Match>();

            for (int i = 0; i < firstHalf.Count; i++)
            {
                var match = new Match
                {
                    HomeTeamTeamIdTeamId = firstHalf[i].AwayTeamTeamIdTeamId,
                    AwayTeamTeamIdTeamId = firstHalf[i].HomeTeamTeamIdTeamId,
                    MatchWeek = firstHalf[i].MatchWeek + numOfSquads - 1
                };

                secondHalf.Add(match);
            }

            return secondHalf;
        }

        public async Task<UserManagerResponse> AddPointsStandings(List<StandingsDTO> dto, string resultsOfTheGame)
        {
            bool Succeded = false;
            string[] goalsScored = resultsOfTheGame.Split("-");
            int[] goals = new int[2];

            goals[0] = Int32.Parse(goalsScored[0]);
            goals[1] = Int32.Parse(goalsScored[1]);

            int biggestgoals = goals[0] > goals[1] ? goals[0] : goals[1];
            int lowestgoals = goals[0] < goals[1] ? goals[0] : goals[1];
            bool win = goals[0] > goals[1];

            foreach (var teams in dto)
            {
                var team = _smfcontext.Standings.Where(x => x.SquadTeamName == teams.SquadTeamName).FirstOrDefault();

                if (win)
                {
                    win = false;
                    team.Wins++;
                    team.Points += 3;
                    team.GoalsScored += biggestgoals;
                    team.GoalsConceded += lowestgoals;
                    team.GoalsDifference = team.GoalsScored - team.GoalsConceded;
                }
                else if (biggestgoals == lowestgoals)
                {
                    team.Draws++;
                    team.Points++;
                    team.GoalsScored = goals[0];
                    team.GoalsConceded = goals[1];
                    team.GoalsDifference = team.GoalsScored - team.GoalsConceded;
                }
                else
                {
                    team.Losses++;
                    team.GoalsConceded += biggestgoals;
                    team.GoalsScored += lowestgoals;
                    team.GoalsDifference = team.GoalsScored - team.GoalsConceded;
                    win = true;
                }

                var success = await _smfcontext.SaveChangesAsync() > 0;

                if (success)
                {
                    Succeded = true;
                }
            }

            if (Succeded)
            {
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "Succeded",
                };

            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Something went wrong",
            };

        }

        public async Task<UserManagerResponse> DefaultPointsStandings(int leagueId)
        {
            bool Success = false;
            var squads = await _smfcontext.Leagues
                  .Where(x => x.LeagueId == leagueId)
                  .Include(x => x.Squads)
                  .ToListAsync();

            for (int i = 0; i < squads[0].Squads.Count; i++)
            {
                //squadNames.Add();
                var sdto = new Standings
                {
                    SquadTeamName = squads[0].Squads[i].Name,
                    Leagueid = leagueId,
                    Points = 0,
                    Wins = 0,
                    Losses = 0,
                    Draws = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    GoalsDifference = 0,
                };
                _smfcontext.Standings.Add(sdto);
                var result = await _smfcontext.SaveChangesAsync() > 0;
                Success = result;
            }
            if (Success)
            {
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "League has started",
                };

            }
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Something went wrong",
            };

        }
        public async Task<UserManagerResponse> GenerateRefereesForMatches(Match match)
        {
            var listOfReferees = await _smfcontext.Referees.ToListAsync();
            var HomeSquad = _smfcontext.Squads.Where(x => x.TeamId == match.HomeTeamTeamIdTeamId).FirstOrDefault();
            var AwaySquad = _smfcontext.Squads.Where(x => x.TeamId == match.AwayTeamTeamIdTeamId).FirstOrDefault();
            int whileIterator = 0;
            int refereesAdded = 0;
            while (refereesAdded < 5)
            {
                if(whileIterator >= listOfReferees.Count)
                {
                    whileIterator = 0;
                }
                whileIterator++;
                if (!(listOfReferees[whileIterator].City.Equals(HomeSquad.City)
                    || listOfReferees[whileIterator].City.Equals(AwaySquad.City)))
                {
                    refereesAdded++;
                    var matchReferee = new MatchRefereeDTO
                    {
                        MatchIds = match.MatchId,
                        RId = listOfReferees[whileIterator].Id
                    };
                    await AddRefereeToMatch(matchReferee);


                }
            }

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Succeded",
            };
        }

        public async Task<List<Standings>> GetStandingsTable()
        {
            var result = await _smfcontext.Standings.ToListAsync();
            return result;
        }

        public async Task<List<NamesSquadDTO>> GetMatches()
        {
            //var result = await _smfcontext.Matches.ToListAsync();
            /* var squads = await _smfcontext.Leagues
                 .Where(x => x.LeagueId == id)
                 .Include(x => x.Squads)
                 .ToListAsync();
            */
            var matches = await _smfcontext.Matches.ToListAsync();
            string team1 = "";
            string team2 = "";
            var dtoMatch = new List<NamesSquadDTO>();
            int counter = 0;
            foreach (var match in matches)
            {
                var nameOfSquad =await _smfcontext.Squads.Where(x=> x.TeamId == match.HomeTeamTeamIdTeamId ).FirstOrDefaultAsync();
                var nameOfSquad2 = await _smfcontext.Squads.Where(x => x.TeamId == match.AwayTeamTeamIdTeamId).FirstOrDefaultAsync();
                team1 = nameOfSquad.Name;
                team2 = nameOfSquad2.Name;
                var nsdto = new NamesSquadDTO {
                    MID = match.MatchId,
                    MatchWeek = match.MatchWeek,
                    NameOne = team1,
                    NameTwo = team2,
                    date = (DateTime)match.MatchDate,
                    isPlayed = 0,
                    Result = null

                };
                dtoMatch.Add(nsdto);

            }
                
            return dtoMatch;
        }
    }
}

