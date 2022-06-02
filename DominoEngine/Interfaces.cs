namespace DominoEngine;

public interface IEvaluator<T> {

    int Evaluate(T t);
}

public interface IEvaluable {

    int Value { get; }
}

public interface IToken<out T>
{
    T[] Outputs { get; }
    T[] FreeOutputs { get; }
}
