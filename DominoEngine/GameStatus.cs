using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
///A class storing some of the rules of the game and the moves history
///</summary>
public class GameStatus<T> where T : IEvaluable {

    public evaluator<T> Evaluator { get; }

    private readonly List<PlayData<T>> _moves;
    private readonly victoryCriteria<T> _criteria;

    public GameStatus(evaluator<T> evaluator, victoryCriteria<T> criteria) {

        _moves = new List<PlayData<T>>();
        _criteria = criteria;
        Evaluator = evaluator;
    }

    ///<summary>
    ///Adds a move to the game history
    ///</summary>
    ///<param name="player">The player who made the move</param>
    ///<param name="token">The placed token</param>
    ///<param name="output">The output where the token was placed</param>
    internal void AddMove(string playerName, Token<T> token, T output) {
        _moves.Add((new PlayData<T>(playerName, token, output)));
    }

    internal List<PlayData<T>> history => _moves;
}