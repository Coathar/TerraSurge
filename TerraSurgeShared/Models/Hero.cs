namespace TerraSurgeShared.Models
{
    public class Hero : ISystemLoaded
    {
        public long HeroID { get; set; }

        public Guid SystemGuid { get; set; }

        public string Name { get; set; }

        public Roles Role { get; set; }

        public ICollection<HeroAbility> HeroAbilities { get; set; }

        public ICollection<HeroSwapActivity> ToHeroSwapActivities { get; set; }

        public ICollection<HeroSwapActivity> FromHeroSwapActivities { get; set; }

        public ICollection<EliminationActivity> PlayerEliminationActivities { get; set; }

        public ICollection<EliminationActivity> VictimEliminationActivities { get; set; }

        public enum Roles
        {
            Tank,
            Damage,
            Support
        }
    }
}
