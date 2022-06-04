namespace DominoEngine.Strategies
{
    ///<summary>
    ///Generic Strategie Delegate
    ///</summary>
    ///<returns> <c>The Token to be played</c> </returns>
    public delegate (Token<T>, T output) strategie<T>(object History, Token<T>[] Hand, T[] AvailableOutputs) where T : IEvaluable;

    ///<summary>
    ///Group of pre-made strategies 
    ///</summary>
    public static class  Strategies<T> where T : IEvaluable
    {
        ///<summary>
        ///It choose the most valuable token that its possible to place
        ///</summary>
        public static (Token<T>, T output) BiggestOption(object History, Token<T>[] Hand, T[] AvailableOutputs)
        {
            Token<T> BiggestToken = Hand[0];

            for (int i = 0; i < Hand.Length; i++)
            {
                if (BiggestToken.Value < Hand[i].Value)
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
        public static (Token<T>, T outputToPlay) RandomOption(object History, Token<T>[] Hand, T[] AvailableOutputs)
        {
            Random r = new Random(Hand.Length);

            Token<T> RandomToken = Hand[r.Next()];
            T outputToPlay = SelectRandomOutput(RandomToken, AvailableOutputs);
            
            return (RandomToken, outputToPlay);
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

    }
}