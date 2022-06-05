namespace DominoEngine;

public interface ITokenEvaluator<T> where T : IEvaluable {

    int Evaluate(Token<T> t);
}

public interface IEvaluable {

    int Value { get; }
}

public interface IToken<out T> where T : IEvaluable
{
    T[] Outputs { get; }
    T[] FreeOutputs { get; }
}
