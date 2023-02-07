using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraSurge.Game
{
    public interface IGameState
    {
        public void ProcessImage(Bitmap toProcess);
    }
}
