namespace DominoEngine;

///<summary>
///Stores the info of a move
///</summary>
public class PlayData<T> where T : IEvaluable {

    public string PlayerName { get; private set; }
    public Token<T>? Token { get; private set; }
    public T? Output { get; private set; }
    public T[] availableOutputs;

    ///<summary>
    ///Creates a new instance of PlayData
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    ///<param name="AvailableOutputs">The outputs every turn</param>
    public PlayData(string playerName, Token<T>? token = null, T? output = default(T), T[]? AvailableOutputs = null) {
        
        this.PlayerName = playerName;
        this.Token = token;
        this.Output = output;
        if(token == null && AvailableOutputs == null) throw new Exception ("If a player pass, the outputs that were available must be pass to the constructor");
        if(AvailableOutputs == null) availableOutputs = new T[0];
        else availableOutputs = AvailableOutputs;
    }
}


public class WinnerPlayData<T> : PlayData<T> where T : IEvaluable {

    public string[]? WinnersName { get; private set; }

    ///<summary>
    ///Creates a new instance of PlayData
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    ///<param name="winnersName">An array with the names of the winners</param>
    ///<param name="AvailableOutputs">The outputs every turn</param>
    public WinnerPlayData(string playerName, Token<T> token, T? output, string[] winnersName, T[]? AvailableOutputs = null)
    : base(playerName, token, output, AvailableOutputs) {
        this.WinnersName = winnersName;  
    }
}