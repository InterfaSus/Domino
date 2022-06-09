namespace DominoEngine;

///<summary>
///Stores the info of a move
///</summary>
public class PlayData<T> where T : IEvaluable {

    public string PlayerName { get; private set; }
    public Token<T>? Token { get; private set; }
    public T? Output { get; private set; }

    ///<summary>
    ///Creates a new instance of PlayData
    ///</summary>
    ///<param name="playerName">The name of the player that made the move</param>
    ///<param name="token">The played token. If the player passed, the token is null</param>
    ///<param name="output">The output where the token was placed. If the player passed, or was the first move, the output is null</param>
    public PlayData(string playerName, Token<T>? token = null, T? output = default(T)) {
        
        this.PlayerName = playerName;
        this.Token = token;
        this.Output = output;
    }

    public void Deconstruct(out string name, out Token<T>? token, out T? output) {

        name = this.PlayerName;
        token = this.Token;
        output = this.Output;
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
    public WinnerPlayData(string playerName, Token<T>? token, T? output, string[] winnersName)
    : base(playerName, token, output) {
        this.WinnersName = winnersName;   
    }

    public void Deconstruct(out string name, out Token<T>? token, out T? output, out string[]? winnersName) {

        name = this.PlayerName;
        token = this.Token;
        output = this.Output;
        winnersName = (this.WinnersName == null ? null : (string[])this.WinnersName.Clone());
    }
}