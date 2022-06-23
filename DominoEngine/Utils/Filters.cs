namespace DominoEngine.Utils.Filters;

///<summary>
///Group of pre-made token filters
///</summary>
public static class Filters<T> where T : IEvaluable {

    ///<summary>
    ///Returns true if all the token outputs are equal
    ///</summary>
    public static bool AllEqual(Token<T> token) {

        T? temp = default(T);
        foreach (var item in token.Outputs) {
            
            if (temp == null) temp = item;
            else {
                if (!temp.Equals(item)) return false;
            }
        }
        return true;
    }

    ///<summary>
    ///Returns true if all the token outputs are different
    ///</summary>
    public static bool AllDifferent(Token<T> token) {

        HashSet<T> set = new HashSet<T>();

        foreach (var item in token.Outputs) {
            set.Add(item);
        }
        return set.Count == token.Outputs.Length;
    }

    ///<summary>
    ///Returns true if all the token outputs' value have the same parity
    ///</summary>
    public static bool SameParity(Token<T> token) {

        int temp = -1;
        foreach (var item in token.Outputs) {
            
            if (temp == 1) temp = item.Value % 2;
            else {
                if (temp != item.Value % 2) return false;
            }
        }
        return true;
    }
}