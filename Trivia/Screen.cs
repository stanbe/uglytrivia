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

        public TextWriter OutputWriter
        {
            get { return outputWriter; }
        }

        public void PrintAddedPlayer(string playerName, int howManyPlayers)
        {
            OutputWriter.WriteLine(playerName + " was added");
            OutputWriter.WriteLine("They are player number " + howManyPlayers);
        }

        public void PrintRoll(int roll)
        {
            OutputWriter.WriteLine("They have rolled a " + roll);
        }

        public void PrintCurrentPlayer(Player currentPlayer)
        {
            OutputWriter.WriteLine(currentPlayer.Name + " is the current player");
        }

        public void PrintNotGettingOutOfPenaltyBox(Player currentPlayer)
        {
            OutputWriter.WriteLine(currentPlayer.Name + " is not getting out of the penalty box");
        }

        public void PrintGettingOutOfPenaltyBox(Player currentPlayer)
        {
            OutputWriter.WriteLine(currentPlayer.Name + " is getting out of the penalty box");
        }

        public void PrintNewLocationInfo(Player currentPlayer, QuestionCategory category)
        {
            OutputWriter.WriteLine(currentPlayer.Name + "'s new location is " + currentPlayer.Place);
            OutputWriter.WriteLine("The category is " + category);
        }

        public void PrintWrongAnswer(Player player)
        {
            OutputWriter.WriteLine("Question was incorrectly answered");
            OutputWriter.WriteLine(player.Name + " was sent to the penalty box");
        }

        public void PrintCorrectAnswer(Player currentPlayer)
        {
            OutputWriter.WriteLine("Answer was correct!!!!");
            OutputWriter.WriteLine(currentPlayer.Name + " now has " + currentPlayer.Purse + " Gold Coins.");
        }

        public void PrintQuestion(string question)
        {
            OutputWriter.WriteLine(question);
        }
    }
}
