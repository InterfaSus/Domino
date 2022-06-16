namespace DominoEngine;

///<summary>
///Stores the info of a move
///</summary>
public class Condition<T> where T : IEvaluable {
    

    public IFilter< Token<T> > Filter { get; private set; }
    public int Value { get; private set; }

    public Condition(IFilter< Token<T> > filter, int value)
    {
        Filter = filter;
        Value = value;
    }

    internal IFilter < Token<T> > GetFilter => Filter;
    internal int GetValue => Value;

    ///<summary>
    ///Creates a new condition
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    

}


