namespace DominoEngine;

public interface IEvaluator {

    int Evaluate(Ficha t);
}

public interface IToken
    {
        int[] Outputs { get; }
        int[] FreeOutputs { get; }
    }
