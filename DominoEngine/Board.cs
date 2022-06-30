using DominoEngine.Algorithms;
namespace DominoEngine;

///<summary>
///Represents the game board. Contains the played tokens
///</summary>
public class Board<T> where T : IEvaluable {

    private readonly List<Token<T>> _placedTokens;
    private Token<T>? firstToken;

    public Board() {

        _placedTokens = new List<Token<T>>();
    }

    ///<summary>
    ///Returns the available outputs where a token can be placed
    ///</summary>
    ///<returns>An array containing the free outputs on the board</returns>
    public T[] FreeOutputs {
        get {
            HashSet<T> outputs = new HashSet<T>();

            foreach (var token in _placedTokens) {
                
                T[] tokenFreeOutputs = token.FreeOutputs;
                foreach (var head in tokenFreeOutputs) {
                    outputs.Add(head);
                }
            }

            return outputs.ToArray();
        }
    }

    ///<summary>
    ///Returns the available outputs with the amount of times each one appears
    ///</summary>
    public KeyValuePair<T, int>[] OutputsAmount {
        get {
            Dictionary<T, int> dict = new Dictionary<T, int>();

            foreach (var token in _placedTokens) {

                T[] tokenFreeOutputs = token.FreeOutputs;
                foreach (var head in tokenFreeOutputs) {
                    
                    if (dict.ContainsKey(head)) dict[head]++;
                    else dict.Add(head, 1);
                }   
            }

            return dict.ToArray();
        }
    }

    ///<summary>
    ///Tells if the given output is free on the board or not
    ///</summary>
    ///<param name="output">The output to check</param>
    ///<returns>A boolean indicating if the output is available</returns>
    public bool HasOutput(T output) {
        return ArrayOperations.Find<T>(FreeOutputs, output) != -1;
    }

    ///<summary>
    ///Places the first token on the board. Calling it more than once without the output parameter will throw exception
    ///</summary>
    ///<param name="token">The token to be placed</param>
    public void PlaceToken(Token<T> token) {
        if (firstToken != null) {
            throw new Exception("The first token was already placed. Output field is required");
        }

        firstToken = token;
        _placedTokens.Add(token);
    }

    ///<summary>
    ///Places a token on the board. Calling it more than once without the output parameter will throw exception
    ///</summary>
    ///<param name="token">The token to be placed</param>
    ///<param name="output">The output on the board where the token will be placed. Must match with one of the token's outputs</param>
    public void PlaceToken(Token<T> token, T output) {
        
        if (!token.HasOutput(output)) {
            throw new ArgumentException("The provided token and output don't match");
        }

        foreach (var placedToken in _placedTokens) {
            if (placedToken.HasOutput(output)) {

                placedToken.PlaceTokenOn(output);
                token.PlaceTokenOn(output);
                
                _placedTokens.Add(token);

                return;
            }
        }

        throw new ArgumentException("There is no free output in the board that matches with the provided output");
    }
}