namespace TerraSurgeShared.Models
{
    public class AbilityActivity : BaseGameActivity
    {
        public long AbilityActivityID { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public long HeroAbilityID { get; set; }

        public HeroAbility HeroAbility { get; set; }
    }
}
