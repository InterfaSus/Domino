using DominoEngine;

public class RegularEvaluator : IEvaluator {

    public int Evaluate(Ficha t) {

        int[] outputs = new int[2]; // = t.Outputs()
        int sum = 0;
        foreach (int face in outputs) {
            sum += face;
        }
        return sum;
    }
}