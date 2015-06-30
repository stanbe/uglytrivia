using System;
using System.IO;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerPool playerPool = new PlayerPool();
        private readonly QuestionPool questionPool = new QuestionPool();

        private readonly Screen screen;

        public Game(TextWriter @out)
        {
            screen = new Screen(@out);
        }

        public bool Add(String playerName)
        {
            var player = new Player(playerName);
            playerPool.AddPlayer(player);

            screen.PrintAddedPlayer(playerName, playerPool.HowManyPlayers());
            return true;
        }

        public void Roll(int roll)
        {
            var currentPlayer = playerPool.CurrentPlayer;
            screen.PrintCurrentPlayer(currentPlayer);
            screen.PrintRoll(roll);

            if (currentPlayer.InPenaltyBox)
            {
                currentPlayer.SetGetOutOfPenaltyBox(roll);
                if (roll % 2 != 0)
                {
                    screen.PrintGettingOutOfPenaltyBox(currentPlayer);
                    AdvancePlayerAndAskQuestion(roll);
                }
                else
                {
                    screen.PrintNotGettingOutOfPenaltyBox(currentPlayer);
                }
            }
            else
            {
                AdvancePlayerAndAskQuestion(roll);
            }

        }

        private void AdvancePlayerAndAskQuestion(int roll)
        {
            var currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPlace(roll);

            screen.PrintNewLocationInfo(currentPlayer, CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            var question = questionPool.GetQuestion(CurrentCategory());
            screen.PrintQuestion(question);
        }

        public QuestionCategory CurrentCategory()
        {
            var place = playerPool.CurrentPlayer.Place;

            var result = QuestionCategory.Rock;
            if (place % 4 == 0) result = QuestionCategory.Pop;
            if (place % 4 == 1) result = QuestionCategory.Science;
            if (place % 4 == 2) result = QuestionCategory.Sports;
            return result;
        }

        public bool WasCorrectlyAnswered()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            if (currentPlayer.InPenaltyBox && !currentPlayer.IsGettingOUtOfPenaltyBox)
            {
                playerPool.NextPlayer();
                return true;
            }
            return AddPursesAndDidPlayerWin();
        }

        private bool AddPursesAndDidPlayerWin()
        {
            var currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPurse();

            screen.PrintCorrectAnswer(currentPlayer);
           
            bool notWinner = !currentPlayer.DidPlayerWin();
            playerPool.NextPlayer();

            return notWinner;
        }

        public bool WrongAnswer()
        {
            screen.PrintWrongAnswer(playerPool.CurrentPlayer);
            playerPool.CurrentPlayer.PutInPenaltyBox();

            playerPool.NextPlayer();
            return true;
        }
    }
}
