using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraSurge.Game
{
    public static class GameStateFactory
    {
        public static IGameState CreateGameState(GameState gameState, GameMonitor gameMonitor, TerraSurge terraSurge)
        {
            switch (gameState)
            {
                case GameState.InMenu:
                    return new MenuGameState(gameMonitor, terraSurge);
                case GameState.InGame:
                    return new InGameState(gameMonitor, terraSurge);
            }

            return null;
        }
    }
}
