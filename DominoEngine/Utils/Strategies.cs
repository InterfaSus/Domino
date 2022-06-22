namespace DominoEngine.Utils.Strategies
{

    ///<summary>
    ///Group of pre-made strategies 
    ///</summary>
    public static class  Strategies<T> where T : IEvaluable
    {
        ///<summary>
        ///It choose the most valuable token that its possible to place
        ///</summary>
        public static (Token<T>, T output) BiggestOption(GameStatus<T> status, Token<T>[] Hand, T[] AvailableOutputs)
        {
            Token<T> BiggestToken = Hand[0];

            for (int i = 0; i < Hand.Length; i++)
            {
                if (status.Evaluator(BiggestToken) < status.Evaluator(Hand[i]))
                {
                    BiggestToken = Hand[i];
                }
            }

            T outputToPlay = SelectRandomOutput(BiggestToken, AvailableOutputs);

            return (BiggestToken, outputToPlay);
        }
       
        ///<summary>
        ///It choose a random token that its possible to play
        ///</summary>
        public static (Token<T>, T outputToPlay) RandomOption(GameStatus<T> status, Token<T>[] Hand, T[] AvailableOutputs)
        {
            Random r = new Random();

            Token<T> RandomToken = Hand[r.Next(Hand.Length - 1)];
            T outputToPlay = SelectRandomOutput(RandomToken, AvailableOutputs);
            
            return (RandomToken, outputToPlay);
        }

        ///<summary>
        ///If a player has pass on a specific Token, that output will have more chances to be played
        ///</summary>
        public static (Token<T>, T output) PreventOtherPlayersFromPlaying(GameStatus<T> status, Token<T>[] Hand, T[] AvailableOutputs)
        {
            int[] valuesOfTokens = new int[Hand.Length];

            for (int i = 0; i < valuesOfTokens.Length; i++)
            {
                valuesOfTokens[i] = status.Evaluator(Hand[i]);
            }

            HashSet<T> HashPassOutputs = new HashSet<T>();

            List<PlayData<T>> history = status.history;

            for (int i = 0; i < history.Count; i++)
            {
                if(history[i].Token == null) AddPassOutputs(HashPassOutputs, history[i].availableOutputs);
            }

            T[] PassOutputs = HashPassOutputs.ToArray();

            for (int i = 0; i < Hand.Length; i++)
            {
                for (int j = 0; j < AvailableOutputs.Length; j++)
                {
                    for (int k = 0; k < PassOutputs.Length; k++)
                    {
                      if(Hand[i].HasOutput(AvailableOutputs[j]) && 
                         Hand[i].HasOutput(PassOutputs[k])      && 
                         Hand[i].HasOutput(AvailableOutputs[j]) != Hand[i].HasOutput(PassOutputs[k]))
                         {
                             valuesOfTokens[i] = valuesOfTokens[i]*2;
                         }
                        
                    }
                }
            }

            int biggestValueIndex = 0;
            int biggestValue = int.MinValue;

            for (int i = 0; i < valuesOfTokens.Length; i++)
            {
                if(valuesOfTokens[i] > biggestValue)
                {
                    biggestValueIndex = i;
                    biggestValue = valuesOfTokens[i];
                }
            }

            T outputToPlay = SelectRandomOutput(Hand[biggestValueIndex], AvailableOutputs);

            return (Hand[biggestValueIndex], outputToPlay);
        }

        private static T SelectRandomOutput(Token<T> Token, T[] AvailableOutputs)
        {
            T outputToPlay = AvailableOutputs[0];
            
            for (int i = 0; i < AvailableOutputs.Length; i++)
            {
                if (Token.HasOutput( AvailableOutputs[i] ))
                {
                    outputToPlay = AvailableOutputs[i];
                    break;
                }                    
            }

            return outputToPlay;
        }
        private static void AddPassOutputs(HashSet<T> PassOutputs, T[] Outputs)
        {
            for (int i = 0; i < Outputs.Length; i++)
            {
                PassOutputs.Add(Outputs[i]);
            }
        }
    }
}