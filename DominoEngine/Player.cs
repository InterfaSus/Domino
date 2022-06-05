using DominoEngine.Utils;
namespace DominoEngine;

///<summary>
///Represents a player with a set of Tokens
///</summary>
public class Player<T> where T : IEvaluable
{
    private HashSet< Token<T> > hand;
    private readonly string name;
    private strategy<T> playersStrategy;

    public Player(string Name, Token<T>[] Hand, strategy<T> PlayersStrategie)
    {
        foreach (var item in Hand)
        {
            System.Console.Write(item + " ");
        }
        System.Console.WriteLine();
        
        this.name = Name;
        this.hand = new HashSet< Token<T> >(Hand);
        this.playersStrategy = PlayersStrategie;
    }

    ///<summary>
    ///Makes the players select a Token to play, and removes it from their Set.
    ///If the player cant play returns (null, default(T))
    ///</summary>
    ///<returns> <c>The Token to be played</c> </returns>
    public (Token<T>?, T?) Play(T[] BoardOutputs, GameStatus<T> status)
    {
        Token<T>[] optionsToPlay = AvailableOptions(hand.ToArray(), BoardOutputs);

        if(optionsToPlay.Length == 0) return (null, default(T));

        (Token<T>, T) Move = playersStrategy(status, optionsToPlay, BoardOutputs);

        hand.Remove(Move.Item1);

        if (hand.Count == 0) System.Console.WriteLine(Name + " gano!");

        return Move;
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

    public string Name => name;

}