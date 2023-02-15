namespace SpartanManageFootball.DTOs
{
    public class NamesSquadDTO
    {
        public int MID { get; set; }
        public string NameOne { get; set; }
        public string NameTwo { get; set; }
        public DateTime date { get; set; }

        public string? Result { get; set; }
        public int MatchWeek { get; set; }
        public int isPlayed { get; set; }
    }
}
