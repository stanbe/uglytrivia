using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        PlayerPool playerPool = new PlayerPool();

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        private readonly TextWriter outputWriter;

        public Game(TextWriter @out)
        {
            outputWriter = @out;

            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
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
            if (CurrentCategory() == QuestionCategory.Pop)
            {
                outputWriter.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Science)
            {
                outputWriter.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Sports)
            {
                outputWriter.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == QuestionCategory.Rock)
            {
                outputWriter.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
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
