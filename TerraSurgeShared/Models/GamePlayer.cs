namespace TerraSurgeShared.Models
{
    public class GamePlayer
    {
        public long GamePlayerID { get; set; }

        public string Name { get; set; }

        public ICollection<EliminationActivity> PlayerEliminationActivities { get; set; }

        public ICollection<EliminationActivity> VictimEliminationActivities { get; set; }
    }
}
