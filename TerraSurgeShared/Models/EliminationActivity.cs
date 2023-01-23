namespace TerraSurgeShared.Models
{
    public class EliminationActivity : BaseGameActivity
    {
        public long EliminationActivityID { get; set; }

        public long PlayerID { get; set; }

        public GamePlayer Player { get; set; }

        public long PlayerHeroID { get; set; }

        public Hero PlayerHero { get; set; }

        public long VictimID { get; set; }

        public GamePlayer Victim { get; set; }

        public long VictimHeroID { get; set; }

        public Hero VictimHero { get; set; }
    }
}
