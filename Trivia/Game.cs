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

        public bool IsPlayerStuckInPenaltyBox(int roll)
        {
            var currentPlayer = playerPool.CurrentPlayer;
            if (currentPlayer.InPenaltyBox)
            {
                currentPlayer.SetGetOutOfPenaltyBox(roll);
                screen.PrintWhetherPlayerIsGettingOutOfPenaltyBox(currentPlayer);
            }

            return currentPlayer.IsStuckInPenaltyBox;
        }

        public void AskQuestion()
        {
            var question = questionPool.GetQuestion(playerPool.CurrentPlayer.CurrentCategory());
            screen.PrintQuestion(question);
        }

        public void AdvancePlayer(int roll)
        {
            var currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPlace(roll);
            screen.PrintNewLocationInfo(currentPlayer, currentPlayer.CurrentCategory());
        }

        public int RollDice()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            int roll = random.Next(5) + 1;
            screen.PrintCurrentPlayer(currentPlayer);
            screen.PrintRoll(roll);
            return roll;
        }

        public void AnswerCorrect()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            if (!currentPlayer.IsStuckInPenaltyBox)
            {
                currentPlayer.AddPurse();

                screen.PrintCorrectAnswer(currentPlayer);
            }
        }

        public void AnswerWrong()
        {
            screen.PrintWrongAnswer(playerPool.CurrentPlayer);
            playerPool.CurrentPlayer.PutInPenaltyBox();
        }

        public void NextPlayer()
        {
            playerPool.NextPlayer();
        }

        public bool DidLastPlayerWin()
        {
            return playerPool.LastPlayer.DidPlayerWin();
        }
    }
}
