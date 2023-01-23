using TerraSurgeShared.Models;

namespace TerraSurgeShared.Loaders
{
    [Attributes.Loader(DependsOn = typeof(HeroLoader))]
    internal class HeroAbilityLoader : Loader<HeroAbilityLoader.BaseHeroAbility, HeroAbility>
    {
        Dictionary<Guid, long> HeroIDsByGuid;

        protected override void Setup()
        {
            HeroIDsByGuid = DbContext.Hero.ToDictionary(x => x.SystemGuid, x => x.HeroID);
        }

        protected override bool CompareHash(BaseHeroAbility fileRecord, BaseHeroAbility dbRecord)
        {
            return Util.CreateLongHashCode(fileRecord.HeroID, fileRecord.AbilityName, fileRecord.AbilitySlot) != Util.CreateLongHashCode(dbRecord.HeroID, dbRecord.AbilityName, dbRecord.AbilitySlot);
        }

        protected override BaseHeroAbility ProcessLine(string[] fileLine)
        {
            return new BaseHeroAbility(ToGuid(fileLine[0]),
                HeroIDsByGuid[ToGuid(fileLine[1])],
                fileLine[2],
                ToEnum<HeroAbility.AbilitySlots>(fileLine[3]),
                ToDateTime(fileLine[4]),
                ToDateTime(fileLine[5]));
        }

        public class BaseHeroAbility : IBase<HeroAbility>
        {
            public Guid SystemGuid { get; set; }

            public long HeroID { get; set; }

            public string AbilityName { get; set; }

            public HeroAbility.AbilitySlots AbilitySlot { get; set; }

            public DateTime ValidLow { get; set; }

            public DateTime ValidHigh { get; set; }

            public BaseHeroAbility(Guid systemGuid, long heroID, string abilityName, HeroAbility.AbilitySlots abilitySlot, DateTime validLow, DateTime validHigh)
            {
                SystemGuid = systemGuid;
                HeroID = heroID;
                AbilityName = abilityName;
                AbilitySlot = abilitySlot;
                ValidLow = validLow;
                ValidHigh = validHigh;
            }

            public static IBase CreateFromDatabaseObject(HeroAbility databaseObject)
            {
                return new BaseHeroAbility(databaseObject.SystemGuid, databaseObject.HeroID, databaseObject.AbilityName, databaseObject.AbilitySlot, databaseObject.ValidLow, databaseObject.ValidHigh);
            }

            public HeroAbility ToDatabaseObject()
            {
                HeroAbility toReturn = new HeroAbility();

                toReturn.SystemGuid = SystemGuid;
                toReturn.HeroID = HeroID;
                toReturn.AbilityName = AbilityName;
                toReturn.AbilitySlot = AbilitySlot;
                toReturn.ValidLow = ValidLow;
                toReturn.ValidHigh = ValidHigh;

                return toReturn;
            }

            public void UpdateDatabaseObject(HeroAbility toUpdate)
            {
                toUpdate.HeroID = HeroID;
                toUpdate.AbilityName = AbilityName;
                toUpdate.AbilitySlot = AbilitySlot;
                toUpdate.ValidLow = ValidLow;
                toUpdate.ValidHigh = ValidHigh;
            }
        }
    }
}
