using System;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(String[] args)
        {
            var random = new Random();
            var aGame = new Game(Console.Out, random);
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            Run(aGame, random);
            Console.ReadLine();
        }

        public static void Run(Game game, Random random)
        {
            do
            {
                var roll = game.RollDice();

                if (!game.IsPlayerStuckInPenaltyBox(roll))
                {
                    game.AdvancePlayer(roll);
                    game.AskQuestion();
			    }

                if (ShouldAnswerWrong(random))
                {
                    game.AnswerWrong();
                }
                else
                {
                    game.AnswerCorrect();
                }

                game.NextPlayer();

            } while (!game.DidLastPlayerWin());
        }

        private static bool ShouldAnswerWrong(Random random)
        {
            return random.Next(9) == 7;
        }
    }
}

