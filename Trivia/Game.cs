using System;
using System.IO;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerPool playerPool = new PlayerPool();
        private readonly QuestionPool questionPool = new QuestionPool();

        private readonly Screen screen;
        private readonly Random random;

        public Game(TextWriter @out, Random random)
        {
            screen = new Screen(@out);
            this.random = random;
        }

        public bool Add(String playerName)
        {
            var player = new Player(playerName);
            playerPool.AddPlayer(player);

            screen.PrintAddedPlayer(playerName, playerPool.HowManyPlayers());
            return true;
        }

        public void Roll()
        {
            int roll = random.Next(5) + 1;
            var currentPlayer = playerPool.CurrentPlayer;
            screen.PrintCurrentPlayer(currentPlayer);
            screen.PrintRoll(roll);

            if (currentPlayer.InPenaltyBox)
            {
                currentPlayer.SetGetOutOfPenaltyBox(roll);
                screen.PrintWhetherPlayerIsGettingOutOfPenaltyBox(currentPlayer);
            }
            if (currentPlayer.IsStuckInPenaltyBox)
            {
                return;
            }

            currentPlayer.AddPlace(roll);
            screen.PrintNewLocationInfo(currentPlayer, playerPool.CurrentPlayer.CurrentCategory());
            var question = questionPool.GetQuestion(playerPool.CurrentPlayer.CurrentCategory());
            screen.PrintQuestion(question);
        }

        public bool WasCorrectlyAnswered()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            if (currentPlayer.IsStuckInPenaltyBox)
            {
                return true;
            }
            currentPlayer.AddPurse();

            screen.PrintCorrectAnswer(currentPlayer);

            return !currentPlayer.DidPlayerWin();
        }

        public bool WrongAnswer()
        {
            screen.PrintWrongAnswer(playerPool.CurrentPlayer);
            playerPool.CurrentPlayer.PutInPenaltyBox();

            return true;
        }

        public void NextPlayer()
        {
            playerPool.NextPlayer();
        }
    }
}
