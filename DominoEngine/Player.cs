using DominoEngine.Utils;
namespace DominoEngine;

///<summary>
///Represents a player with a set of Tokens
///</summary>
public class Player<T> where T : IEvaluable
{
    private List<Token<T>> hand;
    public string Name { get; private set; }
    private strategy<T> playersStrategy;

    public Player(string Name, strategy<T> PlayersStrategy)
    {        
        this.Name = Name;
        this.hand = new List<Token<T>>();
        this.playersStrategy = PlayersStrategy;
    }

    ///<summary>
    ///Returns a copy of the tokens on the player's hand
    ///</summary>
    internal Token<T>[] TokensInHand { 
        get {
            Token<T>[] result = new Token<T>[hand.Count];

            int i = 0;            
            foreach (var item in hand) {
                result[i] = (Token<T>)item.Clone();
                i++;
            }
            return result;
        }
    }

    internal void AddToken(Token<T> token) {
        hand.Add(token);
    }

    ///<summary>
    ///Makes the players select a Token to play, and removes it from their Set.
    ///If the player cant play, the token and output returned in the PlayData object will be null
    ///</summary>
    ///<returns> <c>A play data object with info of the played token</c> </returns>
    internal PlayData<T> Play(T[] BoardOutputs, PlayData<T>[] movesHistory, evaluator<T> evaluator)
    {
        Token<T>[] optionsToPlay = AvailableOptions(TokensInHand, BoardOutputs);

        if(optionsToPlay.Length == 0) return new PlayData<T>(this.Name, AvailableOutputs: BoardOutputs);

        var Move = playersStrategy(movesHistory, evaluator, optionsToPlay, BoardOutputs);
        if (!Move.Item1.HasOutput(Move.Item2)) {
            throw new ArgumentException("Wrong strategy token management. The returned token doesn't have the returned output available");
        }

        Token<T>? originalToken = null;
        foreach (var token in hand) {
            if (token.Equals(Move.Item1)) {

                hand.Remove(token);
                originalToken = token;
                break;
            }
        }        
        return new PlayData<T>(this.Name, BoardOutputs, originalToken, Move.Item2);
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