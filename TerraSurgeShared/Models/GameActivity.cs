using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraSurgeShared.Models
{
    public class GameActivity
    {
        public long GameActivityID { get; set; }

        public long CreatedTime { get; set; }

        public IEnumerable<HeroSwapActivity> HeroSwapActivities { get; set; }
    }
}
