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
        public static string[] DefaultCriteria(GameStatus<T> gameStatus, Player<T>[] Players, IFilter<T>? Filter = null, int Value = 0)
        {

            evaluator<T> Evaluator = gameStatus.Evaluator;
            List<PlayData<T>> history = gameStatus.history;

            for (int i = 0; i < Players.Length; i++)
            {
                if( NoTokensLeft( Players[i] )) return new string[] {Players[i].Name};
            }
            
            if(NoOneCanPlay(history, Players.Length))
            {

            int[] scores = new int[Players.Length];
            Token<T>[][] hands = GetHands(Players);

            GetScores(scores, hands, Evaluator);

            List<string> Winners= new List<string>();
            int lowestValue = int.MaxValue;

            for (int i = 0; i < scores.Length; i++)
            {
                if( scores[i] < lowestValue)
                {
                    Winners = new List<string>();
                   
                    lowestValue = scores[i];
                    Winners.Add(Players[i].Name);
                }

                else if ( scores[i] == lowestValue)
                {
                    Winners.Add(Players[i].Name);
                }
            }
            
            return Winners.ToArray();
            }

            return new string[0];
        }


         ///<summary>
        ///Surpass Sum victory criteria
        ///</summary>
        ///<returns> The first player that surpass the value of the condition win</returns>
        public static string[] SurpassSumCriteria(GameStatus<T> gameStatus, Player<T>[] Players, IFilter<T>? Filter = null, int Value = 0)
        {
            int[] Scores = new int[Players.Length];
            int index = 0;   
           
            List<PlayData<T>> history = gameStatus.history;
            evaluator<T> evaluator = gameStatus.Evaluator;

            for (int i = 0; i < history.Count; i++)
            {
                if(index >= Players.Length) index = 0;
                
                Scores[index] += evaluator(history[i].Token);
                
                if(Scores[index] >= Value) return new string[] {Players[index].Name};

                index++;
            }

            return new string[0];
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