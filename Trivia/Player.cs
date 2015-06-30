﻿namespace Trivia
{
    public class Player
    {
        public string Name { get; private set; }
        public int Place { get; set; }
        public int Purse { get; set; }
        public bool InPenaltyBox { get; set; }
        public bool IsGettingOUtOfPenaltyBox { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public void AddPlace(int roll)
        {
            Place = (Place + roll) % 12;
        }

        public void AddPurse()
        {
            Purse = Purse + 1;
        }

        public void PutInPenaltyBox()
        {
            InPenaltyBox = true;
        }

        public void SetGetOutOfPenaltyBox(int roll)
        {
            IsGettingOUtOfPenaltyBox = roll % 2 != 0;
        }

        public bool DidPlayerWin()
        {
            return Purse == 6;
        }

        public QuestionCategory CurrentCategory()
        {
            var result = QuestionCategory.Rock;
            if (Place % 4 == 0) result = QuestionCategory.Pop;
            if (Place % 4 == 1) result = QuestionCategory.Science;
            if (Place % 4 == 2) result = QuestionCategory.Sports;
            return result;
        }
    }
}