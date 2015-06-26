using System.Collections.Generic;

namespace Trivia
{
    class PlayerPool
    {
        private readonly List<Player> players;
        private int currentPlayerIndex;

        public Player CurrentPlayer
        {
            get { return players[currentPlayerIndex]; }
        }

        public PlayerPool()
        {
            players = new List<Player>();
            currentPlayerIndex = 0;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
        
        public void NextPlayer()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex == HowManyPlayers())
            {
                currentPlayerIndex = 0;
            }
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }
    }
}
