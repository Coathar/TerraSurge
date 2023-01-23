using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraSurgeShared.Models
{
    public class HeroSwapActivity : BaseGameActivity
    {
        public long HeroSwapActivityID { get; set; }

        public long FromHeroID { get; set; }

        public Hero FromHero { get; set; }

        public long ToHeroID { get; set; }

        public Hero ToHero { get; set; }
    }
}
