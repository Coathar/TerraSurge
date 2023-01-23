using TerraSurgeShared.Models;

namespace TerraSurgeShared.Loaders
{
    [Attributes.Loader]
    public class HeroLoader : Loader<HeroLoader.BaseHero, Hero>
    {
        protected override bool CompareHash(BaseHero fileRecord, BaseHero dbRecord)
        {
            return Util.CreateLongHashCode(fileRecord.Name, fileRecord.Role) != Util.CreateLongHashCode(fileRecord.Name, fileRecord.Role);
        }

        protected override BaseHero ProcessLine(string[] fileLine)
        {
            return new BaseHero(new Guid(fileLine[0]), fileLine[1], Enum.Parse<Hero.Roles>(fileLine[2]));
        }

        public class BaseHero : IBase<Hero>
        {
            public Guid SystemGuid { get; set; }

            public string Name { get; set; }

            public Hero.Roles Role { get; set; }

            public BaseHero(Guid systemGuid, string name, Hero.Roles role)
            {
                SystemGuid = systemGuid;
                Name = name;
                Role = role;
            }

            public static IBase CreateFromDatabaseObject(Hero databaseObject)
            {
                return new BaseHero(databaseObject.SystemGuid, databaseObject.Name, databaseObject.Role);
            }

            public Hero ToDatabaseObject()
            {
                Hero toReturn = new Hero();

                toReturn.SystemGuid = SystemGuid;
                toReturn.Name = Name;
                toReturn.Role = Role;

                return toReturn;
            }

            public void UpdateDatabaseObject(Hero toUpdate)
            {
                toUpdate.Name = Name;
                toUpdate.Role = Role;
            }
        }
    }
}
