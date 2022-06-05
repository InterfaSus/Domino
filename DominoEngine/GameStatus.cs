namespace DominoEngine;

///<summary>
///A class storing some of the rules of the game and the moves history
///</summary>
public class GameStatus<T> where T : IEvaluable {

    public ITokenEvaluator<T> Evaluator { get; }

    private readonly List<(Player<T>, Token<T>, T)> _moves;

    public GameStatus(ITokenEvaluator<T> evaluator) {

        _moves = new List<(Player<T>, Token<T>, T)>();
        Evaluator = evaluator;
    }

    ///<summary>
    ///Adds a move to the game history
    ///</summary>
    ///<param name="player">The player who made the move</param>
    ///<param name="token">The placed token</param>
    ///<param name="output">The output where the token was placed</param>
    public void AddMove(Player<T> player, Token<T> token, T output) {
        _moves.Add((player, token, output));
    }
}