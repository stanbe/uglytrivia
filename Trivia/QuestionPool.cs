using System.Collections.Generic;

namespace Trivia
{
    class QuestionPool
    {
        private readonly LinkedList<string> popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> rockQuestions = new LinkedList<string>();

        public QuestionPool()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public string GetQuestion(QuestionCategory category)
        {
            string question = null;
            if (category == QuestionCategory.Pop)
            {
                question = popQuestions.First.Value;
                popQuestions.RemoveFirst();
            }
            if (category == QuestionCategory.Science)
            {
                question = scienceQuestions.First.Value;
                scienceQuestions.RemoveFirst();
            }
            if (category == QuestionCategory.Sports)
            {
                question = sportsQuestions.First.Value;
                sportsQuestions.RemoveFirst();
            }
            if (category == QuestionCategory.Rock)
            {
                question = rockQuestions.First.Value;
                rockQuestions.RemoveFirst();
            }
            return question;
        }
    }
}
