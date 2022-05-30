using DominoEngine;

public class RegularEvaluator : IEvaluator {

    public int Evaluate(Token token) {

        int[] outputs = token.Outputs;
        int sum = 0;
        foreach (int face in outputs) {
            sum += face;
        }
        return sum;
    }
}