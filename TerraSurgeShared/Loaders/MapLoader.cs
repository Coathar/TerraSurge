using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraSurgeShared.Attributes;
using TerraSurgeShared.Models;
using static TerraSurgeShared.Loaders.Loader;

namespace TerraSurgeShared.Loaders
{
    [Attributes.Loader(DependsOn = typeof(MapTypeLoader))]
    public class MapLoader : Loader<MapLoader.BaseMap, Models.Map>
    {
        Dictionary<Guid, long> MapTypesByGuid = new Dictionary<Guid, long>();

        protected override void Setup()
        {
            MapTypesByGuid = DbContext.MapType.ToDictionary(x => x.SystemGuid, x => x.MapTypeID);
        }

        protected override BaseMap ProcessLine(string[] fileLine)
        {
            return new BaseMap(new Guid(fileLine[0]), fileLine[1], MapTypesByGuid[new Guid(fileLine[2])]);
        }

        protected override bool CompareHash(BaseMap fileRecord, BaseMap dbRecord)
        {
            return Util.CreateLongHashCode(fileRecord.Name, fileRecord.MapTypeID) != Util.CreateLongHashCode(dbRecord.Name, dbRecord.MapTypeID);
        }

        public class BaseMap : IBase<Map>
        {
            public Guid SystemGuid { get; set; }

            public string Name { get; set; }

            public long MapTypeID { get; set; }

            public BaseMap(Guid systemGuid, string name, long mapTypeID)
            {
                SystemGuid = systemGuid;
                Name = name;
                MapTypeID = mapTypeID;
            }

            static IBase IBase<Map>.CreateFromDatabaseObject(Map databaseObject) => new BaseMap(databaseObject.SystemGuid, databaseObject.Name, databaseObject.MapTypeID);

            public Map ToDatabaseObject()
            {
                Map toReturn = new Map();

                toReturn.SystemGuid = SystemGuid;
                toReturn.Name = Name;
                toReturn.MapTypeID = MapTypeID;

                return toReturn;
            }

            public void UpdateDatabaseObject(Map map)
            {
                map.Name = Name;
                map.MapTypeID = MapTypeID;
            }

            
        }
    }
}
