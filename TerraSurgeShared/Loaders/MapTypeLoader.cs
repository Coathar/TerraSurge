using TerraSurgeShared.Models;

namespace TerraSurgeShared.Loaders
{
    [Attributes.Loader]
    public class MapTypeLoader : Loader<MapTypeLoader.BaseMapType, MapType>
    {
        protected override bool CompareHash(BaseMapType fileRecord, BaseMapType dbRecord)
        {
            return fileRecord.Name.GetHashCode() != dbRecord.Name.GetHashCode();
        }

        protected override BaseMapType ProcessLine(string[] fileLine)
        {
            return new BaseMapType(new Guid(fileLine[0]), fileLine[1]);
        }

        public class BaseMapType : IBase<MapType>
        {
            public Guid SystemGuid { get; set; }

            public string Name { get; set; }

            public BaseMapType(Guid systemGuid, string name)
            {
                SystemGuid = systemGuid;
                Name = name;
            }

            static IBase IBase<MapType>.CreateFromDatabaseObject(MapType databaseObject) => new BaseMapType(databaseObject.SystemGuid, databaseObject.Name);

            public BaseMapType(MapType mapType)
            {
                SystemGuid = mapType.SystemGuid;
                Name = mapType.Name;
            }

            public MapType ToDatabaseObject()
            {
                MapType mapType = new MapType();

                mapType.SystemGuid = SystemGuid;
                mapType.Name = Name;

                return mapType;
            }

            public void UpdateDatabaseObject(MapType toUpdate)
            {
                toUpdate.Name = Name;
            }
        }
    }

    
}
