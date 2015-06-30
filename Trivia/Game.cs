using System;
using System.IO;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerPool playerPool = new PlayerPool();
        private readonly QuestionPool questionPool = new QuestionPool();

        private readonly TextWriter outputWriter;

        public Game(TextWriter @out)
        {
            outputWriter = @out;
        }

        public bool Add(String playerName)
        {
            var player = new Player(playerName);
            playerPool.AddPlayer(player);

            outputWriter.WriteLine(playerName + " was added");
            outputWriter.WriteLine("They are player number " + playerPool.HowManyPlayers());
            return true;
        }

        public void Roll(int roll)
        {
            var currentPlayer = playerPool.CurrentPlayer;
            outputWriter.WriteLine(currentPlayer.Name + " is the current player");
            outputWriter.WriteLine("They have rolled a " + roll);

            if (currentPlayer.InPenaltyBox)
            {
                currentPlayer.SetGetOutOfPenaltyBox(roll);
                if (roll % 2 != 0)
                {
                    outputWriter.WriteLine(currentPlayer.Name + " is getting out of the penalty box");
                    AdvancePlayerAndAskQuestion(roll);
                }
                else
                {
                    outputWriter.WriteLine(currentPlayer.Name + " is not getting out of the penalty box");
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

            outputWriter.WriteLine(currentPlayer.Name + "'s new location is " + currentPlayer.Place);
            outputWriter.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            var question = questionPool.GetQuestion(CurrentCategory());
            outputWriter.WriteLine(question);
        }

        private QuestionCategory CurrentCategory()
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
            outputWriter.WriteLine("Answer was correct!!!!");
            var currentPlayer = playerPool.CurrentPlayer;
            currentPlayer.AddPurse();
            outputWriter.WriteLine(currentPlayer.Name + " now has " + currentPlayer.Purse + " Gold Coins.");

            bool notWinner = !currentPlayer.DidPlayerWin();
            playerPool.NextPlayer();

            return notWinner;
        }

        public bool WrongAnswer()
        {
            outputWriter.WriteLine("Question was incorrectly answered");
            outputWriter.WriteLine(playerPool.CurrentPlayer.Name + " was sent to the penalty box");
            playerPool.CurrentPlayer.PutInPenaltyBox();

            playerPool.NextPlayer();
            return true;
        }
    }
}
