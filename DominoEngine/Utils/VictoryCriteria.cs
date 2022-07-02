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
        public static string[]? DefaultCriteria(GameStatus<T> gameStatus, Player<T>[] Players, int Value = 0)
        {

            evaluator<T> Evaluator = gameStatus.Evaluator;
            List<PlayData<T>> history = gameStatus.history;

            for (int i = 0; i < Players.Length; i++)
            {
                if( NoTokensLeft( Players[i] )) return new string[] {Players[i].Name};
            }
            
            if(NoOneCanPlay(history, Players.Length))
            {

            Token<T>[][] hands = GetHands(Players);
            return LowestHandPlayers(Players, Evaluator);
            }

            return null;
        }


         ///<summary>
        ///Surpass Sum victory criteria
        ///</summary>
        ///<returns> The first player that surpass the value of the condition win</returns>
        public static string[]? SurpassSumCriteria(GameStatus<T> gameStatus, Player<T>[] Players, int Value)
        {
            List<PlayData<T>> history = gameStatus.history;
            evaluator<T> evaluator = gameStatus.Evaluator;

            if (NoOneCanPlay(history, Players.Length)) {
                return new string[0];
            }

            Dictionary<string, int> scores = new Dictionary<string, int>();

            for (int i = 0; i < history.Count; i++)
            {   
                if (!scores.ContainsKey(history[i].PlayerName)) scores.Add(history[i].PlayerName, 0);
                scores[history[i].PlayerName] += evaluator(history[i].Token);
                if(scores[history[i].PlayerName] >= Value) return new string[] { history[i].PlayerName };
            }

            return null;
        }

         ///<summary>
        ///The game is considered ended after the players pass "Value" times in total
        ///</summary>
        ///<returns> The players with the less amount of value in hand</returns>
        public static string[]? EndAtXPass(GameStatus<T> gameStatus, Player<T>[] Players, int Value)
        {
            List<PlayData<T>> history = gameStatus.history;
            evaluator<T> evaluator = gameStatus.Evaluator;

            int passAmount = 0;
            foreach (var move in history) {
                if (move.Token == null) passAmount++;
            }

            if (NoOneCanPlay(history, Players.Length) || passAmount >= Value) {
                return LowestHandPlayers(Players, evaluator);
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

        private static string[] LowestHandPlayers(Player<T>[] players, evaluator<T> Evaluator)
        {   
            int[] scores = new int[players.Length];
            Token<T>[][] hands = GetHands(players);

            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = 0; j < hands[i].Length; j++)
                {
                    scores[i] += Evaluator(hands[i][j]); 
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
                    Winners.Add(players[i].Name);
                }

                else if ( scores[i] == lowestValue)
                {
                    Winners.Add(players[i].Name);
                }
            }
            
            return Winners.ToArray();
        }
#endregion
        
    }
}