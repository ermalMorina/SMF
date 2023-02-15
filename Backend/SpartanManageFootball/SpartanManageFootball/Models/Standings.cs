namespace SpartanManageFootball.Models
{
    public class Standings
    {
        public int id {get; set;}
        public string SquadTeamName {get; set;}
        public int Leagueid {get; set;}
        public int Points { get; set;} 
        public int Wins {get; set;}
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsDifference { get; set; }


    }
}
