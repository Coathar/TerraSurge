namespace TerraSurgeShared.Models
{
    public class Map : ISystemLoaded
    {
        public long MapID { get; set; }

        public Guid SystemGuid { get; set; }

        public string Name { get; set; }

        public long MapTypeID { get; set; }

        public MapType MapType { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
