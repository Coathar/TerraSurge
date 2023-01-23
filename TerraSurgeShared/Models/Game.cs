using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraSurgeShared.Models
{
    public class Game
    {
        public long? GameID { get; set; }

        public Guid GameGuid { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime CompletedTime { get; set; }

        public short Team1Score { get; set; }

        public short Team2Score { get; set; }

        public long GameMapID { get; set; }

        public Map Map { get; set; }

        public long BNetPlayerID { get; set; }

        public BNetPlayer BNetPlayer { get; set; }

        public ICollection<GamePlayer> GamePlayers { get; set; }

        public ICollection<GameHero> HeroesPlayed { get; set; }

    }
}
