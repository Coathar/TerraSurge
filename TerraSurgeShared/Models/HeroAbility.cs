namespace TerraSurgeShared.Models
{
    public class HeroAbility : ISystemLoaded
    {
        public long HeroAbilityID { get; set; }

        public Guid SystemGuid { get; set; }

        public long HeroID { get; set; }

        public Hero Hero { get; set; }

        public string AbilityName { get; set; }

        public AbilitySlots AbilitySlot { get; set; }

        public enum AbilitySlots
        {
            PrimaryFire,
            SecondaryFire,
            Ability1,
            Ability2,
            UltimateAbility,
            Passive,
            Jump,
            Crouch,
        }

        public DateTime ValidLow { get; set; }

        public DateTime ValidHigh { get; set; }
    }
}
