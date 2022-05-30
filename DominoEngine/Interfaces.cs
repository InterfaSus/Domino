namespace DominoEngine;

public interface IEvaluator {

    int Evaluate(Token t);
}

public interface IToken
    {
        int[] Outputs { get; }
        int[] FreeOutputs { get; }
    }
