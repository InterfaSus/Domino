using DominoEngine.Utils.Evaluators;

namespace DominoEngine.Utils.VictoryCriteria
{   

    ///<summary>
    ///Group of pre-made victory criteria
    ///</summary>
    public class VictoryCriteria<T> where T : IEvaluable
    {
        ///<summary>
        ///Default victory criteria
        ///</summary>
        ///<returns> The first player that use all their tokens, or if no one has play in a full round, the player/s with the lowest tokens. </returns>
        public static Player<T>[]? DefaultCriteria(GameStatus<T> gameStatus, Player<T>[] Players, Condition<T> condition)
        {

            evaluator<T> Evaluator = gameStatus.Evaluator;
            List<PlayData<T>> history = gameStatus.history;

            for (int i = 0; i < Players.Length; i++)
            {
                if( NoTokensLeft( Players[i] )) return new Player<T>[] {Players[i]};
            }
            
            if(NoOneCanPlay(history, Players.Length))
            {

            int[] scores = new int[Players.Length];
            Token<T>[][] hands = GetHands(Players);

            GetScores(scores, hands, Evaluator);

            List<Player<T>> Winners= new List<Player<T>>();
            int lowestValue = int.MaxValue;

            for (int i = 0; i < scores.Length; i++)
            {
                if( scores[i] < lowestValue)
                {
                    Winners = new List<Player<T>>();
                   
                    lowestValue = scores[i];
                    Winners.Add(Players[i]);
                }

                else if ( scores[i] == lowestValue)
                {
                    Winners.Add(Players[i]);
                }
            }
            
            return Winners.ToArray();
            }

            return new Player<T>[0];
        }


         ///<summary>
        ///Surpass Sum victory criteria
        ///</summary>
        ///<returns> The first player that surpass the value of the condition win</returns>
        public static Player<T>[]? SurpassSumCriteria(GameStatus<T> gameStatus, Player<T>[] Players, Condition<T> condition)
        {
            int[] Scores = new int[Players.Length];
            int index = 0;   
           
            List<PlayData<T>> history = gameStatus.history;
            evaluator<T> evaluator = gameStatus.Evaluator;

            for (int i = 0; i < history.Count; i++)
            {
                if(index >= Players.Length) index = 0;
                
                Scores[index] += evaluator(history[i].Token);
                
                if(Scores[index] >= condition.GetValue) return new Player<T>[] {Players[index]};
            }

            return null;
        }  

#region Utils

    private static bool NoTokensLeft( Player<T> Player)
        {
            return Player.TokensInHand.Length == 0;
        }
    private static bool NoOneCanPlay( List<PlayData<T>> history, int playersNum)
        {
            for (int i = history.Count-1; i >= history.Count - playersNum; i--)
            {
                if(history[i].Token != null) return false;   
            }

            return true;
        }

        private static Token<T>[][] GetHands(Player<T>[] Players)
        {
            Token<T>[][] hands = new Token<T>[Players.Length][];

            for (int i = 0; i < Players.Length; i++)
            {
                hands[i] = Players[i].TokensInHand;
            }

            return hands;
        }

        private static void GetScores( int[] scores,  Token<T>[][] hands, evaluator<T> Evaluator)
        {
             for (int i = 0; i < scores.Length; i++)
            {
                for (int j = 0; j < hands[i].Length; j++)
                {
                    scores[i] += Evaluator(hands[i][j]); 
                }
            }
        }
#endregion
        
    }
}