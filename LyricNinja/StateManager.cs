using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyricNinja
{
    class StateManager
    {
        public const int PRELOAD = 0;
        public const int TITLE = 1;
        public const int SELECT = 2;
        public const int ASK = 3;
        public const int ANSWER = 4;
        public const int SCORE = 5;

        public void HandleState(Game1 gameObject, int gameState)
        {
            switch (gameState)
            {
                case PRELOAD:
                    
                    break;
                case TITLE:
                    gameObject.DrawTitle();
                    break;
                case SELECT:
                    gameObject.SelectQuestion();
                    break;
                case ASK:
                    gameObject.AskQuestion();
                    break;
                case ANSWER:
                    gameObject.AnswerQuestion();
                    break;
                case SCORE:
                    
                    break;
            }
        }
    }
}
