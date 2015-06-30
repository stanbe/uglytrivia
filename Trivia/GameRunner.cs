﻿using System;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;

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
                game.Roll();

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

