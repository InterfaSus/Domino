namespace DominoEngine;

public interface ITokenEvaluator<T> where T : IEvaluable {

    int Evaluate(Token<T> t);
}

public interface IEvaluable {

    int Value { get; }
}

public interface IGameManager<T> where T : IEvaluable {

    GameStatus<T> Status { get; }
    (string, Token<T>[])[] PlayersTokens { get; }
    WinnerPlayData<T> MakeMove();
}
