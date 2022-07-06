using DominoEngine.Algorithms;
namespace DominoEngine.Utils.Strategies
{

    ///<summary>
    ///Group of pre-made strategies 
    ///</summary>
    public static class  Strategies<T> where T : IEvaluable
    {
        static RandomGen random = new RandomGen();

        ///<summary>
        ///It choose the most valuable token that its possible to place
        ///</summary>
        public static Tuple<Token<T>, T> BiggestOption(PlayData<T>[] history, evaluator<T> evaluator, Token<T>[] Hand, T[] AvailableOutputs)
        {
            Token<T> BiggestToken = Hand[0];

            for (int i = 0; i < Hand.Length; i++)
            {
                if (evaluator(BiggestToken) < evaluator(Hand[i]))
                {
                    BiggestToken = Hand[i];
                }
            }

            T outputToPlay = SelectRandomOutput(BiggestToken, AvailableOutputs);

            return new Tuple<Token<T>, T>(BiggestToken, outputToPlay);
        }
       
        ///<summary>
        ///It choose a random token that its possible to play
        ///</summary>
        public static Tuple<Token<T>, T> RandomOption(PlayData<T>[] history, evaluator<T> evaluator, Token<T>[] Hand, T[] AvailableOutputs)
        {
            int r = random.Next(Hand.Length);
            Token<T> RandomToken = Hand[r];
            T outputToPlay = SelectRandomOutput(RandomToken, AvailableOutputs);
            
            return  new Tuple<Token<T>, T>(RandomToken, outputToPlay);
        }

        ///<summary>
        ///If a player has pass on a specific Token, that output will have more chances to be played
        ///</summary>
        public static Tuple<Token<T>, T> PreventOtherPlayersFromPlaying(PlayData<T>[] history, evaluator<T> evaluator, Token<T>[] Hand, T[] AvailableOutputs)
        {
            int[] valuesOfTokens = new int[Hand.Length];

            for (int i = 0; i < valuesOfTokens.Length; i++)
            {
                valuesOfTokens[i] = evaluator(Hand[i]);
            }

            HashSet<T> HashPassOutputs = new HashSet<T>();

            for (int i = 0; i < history.Length; i++)
            {
                if(history[i].Token == null) AddPassOutputs(HashPassOutputs, history[i].AvailableOutputs);
            }

            T[] PassOutputs = HashPassOutputs.ToArray();
            Dictionary<T, int> outputPriority = new Dictionary<T, int>();

            for (int i = 0; i < Hand.Length; i++)
            {
                bool atLeastOne = false, decreased = false;
                for (int j = 0; j < AvailableOutputs.Length; j++)
                {
                    if (!outputPriority.ContainsKey(AvailableOutputs[j])) outputPriority.Add(AvailableOutputs[j], 0);
                    for (int k = 0; k < PassOutputs.Length; k++)
                    {
                        if(Hand[i].HasOutput(AvailableOutputs[j]) && 
                            Hand[i].HasOutput(PassOutputs[k])) {
                            if (!AvailableOutputs[j].Equals(PassOutputs[k])) {

                                outputPriority[AvailableOutputs[j]]++;
                                valuesOfTokens[i] *= 2;
                                if (decreased) valuesOfTokens[i] *= 2;

                                atLeastOne = true;
                                decreased = false;
                            }
                            else if (!atLeastOne) {

                                outputPriority[AvailableOutputs[j]]--;
                                valuesOfTokens[i] /= 2;
                                decreased = true;
                            }
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

            // Selecting output to play
            T? outputToPlay = default(T);
            int bestValue = int.MinValue;

            foreach (var output in Hand[biggestValueIndex].Outputs) {
                if (outputPriority.ContainsKey(output) && outputPriority[output] > bestValue) {
                    bestValue = outputPriority[output];
                    outputToPlay = output;
                }
            }

            return new Tuple<Token<T>, T>(Hand[biggestValueIndex], outputToPlay!);
        }

        private static T SelectRandomOutput(Token<T> Token, T[] AvailableOutputs)
        {
            HashSet<T> outputs = new HashSet<T>();
            
            for (int i = 0; i < AvailableOutputs.Length; i++)
            {
                if (Token.HasOutput( AvailableOutputs[i] ))
                {
                    outputs.Add(AvailableOutputs[i]);
                }                    
            }
            
            return outputs.ToArray()[random.Next(outputs.Count)];
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