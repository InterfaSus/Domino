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
    ///If the player cant play, the token and output returned in the PlayData object will be null
    ///</summary>
    ///<returns> <c>A play data object with info of the played token</c> </returns>
    public PlayData<T> Play(T[] BoardOutputs, GameStatus<T> status)
    {
        Token<T>[] optionsToPlay = AvailableOptions(hand.ToArray(), BoardOutputs);

        if(optionsToPlay.Length == 0) return new PlayData<T>(this.Name);

        (Token<T>, T) Move = playersStrategy(status, optionsToPlay, BoardOutputs);
        hand.Remove(Move.Item1);

        return new PlayData<T>(this.Name, Move.Item1, Move.Item2);
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