using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreCraftApi.Enities
{
    public class UserTeam
    {
        public Guid RefUser { get; set; }
        public int RefTeam { get; set; }

        [NotMapped]
        public virtual List<int>? RefTeams { get; set; }

        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}
