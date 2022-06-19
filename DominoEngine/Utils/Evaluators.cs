namespace DominoEngine.Utils.Evaluators;

///<summary>
///Group of pre-made Evaluators
///</summary>
public class Evaluators<T>  where T : IEvaluable {

    


    ///<summary>
    ///Default additive evaluator
    ///</summary>
    ///<returns> The sum of the values of each output of the token </returns>
    public static int AdditiveEvaluator(Token<T>? token) {

        if(token == null) return 0;

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }
        return sum;
    }

    public static int PruebaEvaluator(Token<T>? token) {

        if(token == null) return 0;

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }
        return sum;
    }
}