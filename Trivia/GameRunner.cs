using System;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;

        public static void Main(String[] args)
        {
            var aGame = new Game(Console.Out);
            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            var random = new Random();

            Run(aGame, random);
            Console.ReadLine();
        }

        public static void Run(Game game, Random random)
        {
            do
            {
                game.Roll(random.Next(5) + 1);

                if (random.Next(9) == 7)
                {
                    notAWinner = game.WrongAnswer();
                }
                else
                {
                    notAWinner = game.WasCorrectlyAnswered();
                }
            } while (notAWinner);
        }
    }

}

