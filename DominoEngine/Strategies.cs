namespace DominoEngine.Strategies
{
    ///<summary>
    ///Generic Strategie Delegate
    ///</summary>
    ///<returns> <c>The Token to be played</c> </returns>
    public delegate Token<T> Strategie<T>(object History, Token<T>[] Hand, T[] Availables) where T : IEvaluable;

    ///<summary>
    ///Group of pre-made strategies 
    ///</summary>
    public static class  Strategies<T> where T : IEvaluable
    {
        ///<summary>
        ///It choose the most valuable token that its possible to place
        ///</summary>
        public static Token<T> BiggestOption(object History, Token<T>[] Hand, T[] Availables)
        {

            Token<T>[] options = AvailableOptions(Hand, Availables);
            Token<T> BiggestToken = options[0];

            for (int i = 0; i < options.Length; i++)
            {
                if (BiggestToken.Value < options[i].Value)
                {
                    BiggestToken = options[i];
                }
            }

            return BiggestToken;
        }
       
        ///<summary>
        ///It choose a random token that its possible to play
        ///</summary>
        public static Token<T> RandomOption(object History, Token<T>[] Hand, T[] Availables)
        {
            Token<T>[] options = AvailableOptions(Hand, Availables);
            Random r = new Random(options.Length);

            return options[r.Next()];
        }

        private static Token<T>[] AvailableOptions( Token<T>[] Hand, T[] Availables)
        {
             HashSet< Token<T> > optionsHashS = new HashSet< Token<T> >();

            for (int i = 0; i < Hand.Length; i++)
            {
                for (int j = 0; j < Availables.Length; j++)
                {
                    if(Hand[i].HasOutput(Availables[j])) optionsHashS.Add(Hand[i]);
                }
            }

            return optionsHashS.ToArray();
        }
    }
}