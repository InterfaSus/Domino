using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
///A class storing the moves history
///</summary>
public class History<T> where T : IEvaluable {

    private readonly List<PlayData<T>> _moves;

    public History() {
        _moves = new List<PlayData<T>>();
    }

    ///<summary>
    ///Adds a move to the game history
    ///</summary>
    ///<param name="player">The player who made the move</param>
    ///<param name="token">The placed token</param>
    ///<param name="output">The output where the token was placed</param>
    internal void AddMove(string playerName, Token<T> token, T output, T[] AvailableOutputs) {
        
        _moves.Add((new PlayData<T>(playerName, AvailableOutputs, token, output)));
    }

    internal PlayData<T>[] MovesHistory => _moves.ToArray();
}