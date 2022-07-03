namespace DominoEngine.Utils.Evaluators;

///<summary>
///Group of pre-made Evaluators
///</summary>
public class Evaluators<T>  where T : IEvaluable {

    ///<summary>
    ///Default additive evaluator
    ///</summary>
    ///<returns> The sum of the values of each output of the token </returns>
    public static int AdditiveEvaluator(Token<T> token) {

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }
        return sum;
    }

    ///<summary>
    ///When the value of the token its a multiple of 5, it doubles its value. In other cases its normal addtion
    ///</summary>
    ///<returns> The sum of the values of each output of the token </returns>
    public static int FiveMultiplesPriority(Token<T> token) {

        T[] outputs = token.Outputs;
        int sum = 0;

        foreach (var face in outputs) {
            sum += face.Value;
        }

        if(sum%5 == 0) sum = sum*2;
        return sum;
    }
}