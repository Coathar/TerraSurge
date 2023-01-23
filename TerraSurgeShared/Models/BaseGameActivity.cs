using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraSurgeShared.Models
{
    public abstract class BaseGameActivity
    {
        [Column(Order = 1)]
        public long GameActivityID { get; set; }

        public GameActivity GameActivity { get; set; }

        [Column(Order = 2)]
        public long BNetPlayerID { get; set; }

        public BNetPlayer BNetPlayer { get; set; }
    }
}
