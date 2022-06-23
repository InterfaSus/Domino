namespace DominoEngine;

public interface IEvaluable {

    int Value { get; }
}

public interface IGameManager<T> where T : IEvaluable {

    GameStatus<T> Status { get; }
    Tuple<string, Token<T>[]>[] PlayersTokens { get; }
    WinnerPlayData<T> MakeMove();
}
