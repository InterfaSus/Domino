using DominoEngine;

public class AditiveEvaluator<T> : IEvaluator<Token<T>> where T : IEvaluable {

    public int Evaluate(Token<T> token) {

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }
        return sum;
    }
}