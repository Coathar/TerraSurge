namespace TerraSurgeShared.Models
{
    public class MapType : ISystemLoaded
    {
        public long MapTypeID { get; set; }

        public Guid SystemGuid { get; set; }

        public string Name { get; set; }

        public ICollection<Map> Maps { get; set; }
    }
}
