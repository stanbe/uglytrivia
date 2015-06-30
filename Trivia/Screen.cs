using System.IO;

namespace Trivia
{
    public class Screen
    {
        private readonly TextWriter outputWriter;

        public Screen(TextWriter outputWriter)
        {
            this.outputWriter = outputWriter;
        }

        public void PrintAddedPlayer(string playerName, int howManyPlayers)
        {
            outputWriter.WriteLine(playerName + " was added");
            outputWriter.WriteLine("They are player number " + howManyPlayers);
        }

        public void PrintRoll(int roll)
        {
            outputWriter.WriteLine("They have rolled a " + roll);
        }

        public void PrintCurrentPlayer(Player currentPlayer)
        {
            outputWriter.WriteLine(currentPlayer.Name + " is the current player");
        }

        public void PrintNewLocationInfo(Player currentPlayer, QuestionCategory category)
        {
            outputWriter.WriteLine(currentPlayer.Name + "'s new location is " + currentPlayer.Place);
            outputWriter.WriteLine("The category is " + category);
        }

        public void PrintWrongAnswer(Player player)
        {
            outputWriter.WriteLine("Question was incorrectly answered");
            outputWriter.WriteLine(player.Name + " was sent to the penalty box");
        }

        public void PrintCorrectAnswer(Player currentPlayer)
        {
            outputWriter.WriteLine("Answer was correct!!!!");
            outputWriter.WriteLine(currentPlayer.Name + " now has " + currentPlayer.Purse + " Gold Coins.");
        }

        public void PrintQuestion(string question)
        {
            outputWriter.WriteLine(question);
        }

        public void PrintWhetherPlayerIsGettingOutOfPenaltyBox(Player player)
        {
            if (player.IsGettingOUtOfPenaltyBox)
            {
                outputWriter.WriteLine(player.Name + " is getting out of the penalty box");
            }
            else
            {
                outputWriter.WriteLine(player.Name + " is not getting out of the penalty box");
            }
        }
    }
}
