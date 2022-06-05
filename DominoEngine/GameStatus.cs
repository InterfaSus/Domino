namespace DominoEngine;

public class GameStatus<T> where T : IEvaluable {

    public ITokenEvaluator<T> Evaluator { get; }

    private readonly List<(Player<T>, Token<T>, T)> _moves;

    public GameStatus(ITokenEvaluator<T> evaluator) {

        _moves = new List<(Player<T>, Token<T>, T)>();
        Evaluator = evaluator;
    }

    public void AddMove(Player<T> player, Token<T> token, T output) {
        _moves.Add((player, token, output));
    }
}