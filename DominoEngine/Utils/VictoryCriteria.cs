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
        public static string[]? DefaultCriteria(PlayData<T>[] history, evaluator<T> evaluator, Tuple<string ,Token<T>[]>[] PlayersTokens, int Value = 0)
        {   

            for (int i = 0; i < PlayersTokens.Length; i++)
            {
                if( NoTokensLeft( PlayersTokens[i].Item2 )) return new string[] {PlayersTokens[i].Item1};
            }
            
            if(NoOneCanPlay(history, PlayersTokens.Length))
            {

            return LowestHandPlayers(PlayersTokens, evaluator);
            }

            return null;
        }


         ///<summary>
        ///Surpass Sum victory criteria
        ///</summary>
        ///<returns> The first player that surpass the value of the condition win</returns>
        public static string[]? SurpassSumCriteria(PlayData<T>[] history, evaluator<T> evaluator, Tuple<string, Token<T>[]>[] PlayersTokens, int Value)
        {
            if (NoOneCanPlay(history, PlayersTokens.Length)) {
                return new string[0];
            }

            Dictionary<string, int> scores = new Dictionary<string, int>();

            for (int i = 0; i < history.Length; i++)
            {   
                if (history[i].Token == null) continue;

                if (!scores.ContainsKey(history[i].PlayerName)) scores.Add(history[i].PlayerName, 0);
                scores[history[i].PlayerName] += evaluator(history[i].Token!);
                if(scores[history[i].PlayerName] >= Value) return new string[] { history[i].PlayerName };
            }

            return null;
        }

         ///<summary>
        ///The game is considered ended after the players pass "Value" times in total
        ///</summary>
        ///<returns> The players with the less amount of value in hand</returns>
        public static string[]? EndAtXPass(PlayData<T>[] history, evaluator<T> evaluator, Tuple<string, Token<T>[]>[] PlayersTokens, int Value)
        {
            int passAmount = 0;
            foreach (var move in history) {
                if (move.Token == null) passAmount++;
            }

            if (NoOneCanPlay(history, PlayersTokens.Length) || passAmount >= Value) {
                return LowestHandPlayers(PlayersTokens, evaluator);
            }

            return null;
        }

#region Utils

        private static bool NoTokensLeft(Token<T>[] hand)
        {
            return hand.Length == 0;
        }
        private static bool NoOneCanPlay(PlayData<T>[] history, int playersNum)
        {
            for (int i = history.Length-1; i >= history.Length - playersNum; i--)
            {
                if(history[i].Token != null) return false;   
            }

            return true;
        }

        private static string[] LowestHandPlayers(Tuple<string, Token<T>[]>[] PlayersTokens, evaluator<T> Evaluator)
        {   
            int[] scores = new int[PlayersTokens.Length];

            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = 0; j < PlayersTokens[i].Item2.Length; j++)
                {   
                    scores[i] += Evaluator(PlayersTokens[i].Item2[j]); 
                }
            }

            List<string> Winners= new List<string>();
            int lowestValue = int.MaxValue;

            for (int i = 0; i < scores.Length; i++)
            {
                if( scores[i] < lowestValue)
                {
                    Winners = new List<string>();
                   
                    lowestValue = scores[i];
                    Winners.Add(PlayersTokens[i].Item1);
                }

                else if ( scores[i] == lowestValue)
                {
                    Winners.Add(PlayersTokens[i].Item1);
                }
            }
            
            return Winners.ToArray();
        }
#endregion
        
    }
}