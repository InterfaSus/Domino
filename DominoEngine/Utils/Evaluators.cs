namespace DominoEngine.Utils.Evaluators;

///<summary>
///Calculates a token value by adding the values of all of its outputs
///</summary>
public class AditiveEvaluator<T> : ITokenEvaluator<T> where T : IEvaluable {

    public int Evaluate(Token<T> token) {

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }
        return sum;
    }
}