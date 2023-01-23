namespace TerraSurgeShared.Models
{
    public class GameHero
    {
        public long GameHeroID { get; set; }

        public long HeroID { get; set; }
    
        public Hero Hero { get; set; }

        public long ObjectiveTime { get; set; }

        public int DamageDealt { get; set; }

        public short Eliminations { get; set; }

        public short Assists { get; set; }

        public int Healing { get; set; }

        public short FinalBlows { get; set; }
    }
}
