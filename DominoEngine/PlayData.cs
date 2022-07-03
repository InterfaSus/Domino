namespace DominoEngine;

///<summary>
///Stores the info of a move
///</summary>
public class PlayData<T> where T : IEvaluable {

    public string PlayerName { get; private set; }
    public Token<T>? Token { get; private set; }
    public T? Output { get; private set; }
    public T[] AvailableOutputs { get; private set; }

    ///<summary>
    ///Creates a new instance of PlayData
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="AvailableOutputs">The outputs every turn</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    public PlayData(string playerName, T[] AvailableOutputs, Token<T>? token = null, T? output = default(T)) {
        
        this.PlayerName = playerName;
        this.AvailableOutputs = AvailableOutputs;
        this.Token = (token == null ? null : (Token<T>)token.Clone());
        this.Output = output;
        if(token == null && AvailableOutputs == null) throw new Exception ("If a player pass, the outputs that were available must be pass to the constructor");
    }
}


public class WinnerPlayData<T> : PlayData<T> where T : IEvaluable {

    public string[]? WinnersName { get; private set; }

    ///<summary>
    ///Creates a new instance of PlayData
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="AvailableOutputs">The outputs every turn</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    ///<param name="winnersName">An array with the names of the winners</param>
    public WinnerPlayData(string playerName, T[] AvailableOutputs, Token<T>? token = null, T? output = default(T), string[]? winnersName = null)
    : base(playerName, AvailableOutputs, token, output) {
        this.WinnersName = winnersName;  
    }
}