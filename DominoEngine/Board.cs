using DominoEngine.Algorithms;
namespace DominoEngine;

///<summary>
///Represents the game board. Contains the played tokens
///</summary>
public class Board {

    private readonly List<Token> _placedTokens;
    private readonly List<(Token, Token)> _graph; // Currently the graph has no real use
    private Token? firstToken;

    public Board() {

        _placedTokens = new List<Token>();
        _graph = new List<(Token, Token)>();
    }

    ///<summary>
    ///Returns the available outputs where a token can be placed
    ///</summary>
    ///<returns>An array containing the free outputs on the board</returns>
    public int[] FreeOutputs {
        get {
            HashSet<int> outputs = new HashSet<int>();

            foreach (var token in _placedTokens) {
                
                int[] tokenFreeOutputs = token.FreeOutputs;
                foreach (var head in tokenFreeOutputs) {
                    outputs.Add(head);
                }
            }

            return outputs.ToArray();
        }
    }

    ///<summary>
    ///Tells if the given output is free on the board or not
    ///</summary>
    ///<param name="output">The output to check</param>
    ///<returns>A boolean indicating if the output is available</returns>
    public bool HasOutput(int output) {
        return ArrayOperations.Find<int>(FreeOutputs, output) != -1;
    }

    ///<summary>
    ///Places the first token on the board. Calling it more than once without the output parameter will throw exception
    ///</summary>
    ///<param name="token">The token to be placed</param>
    public void PlaceToken(Token token) {
        if (firstToken != null) {
            throw new Exception("The first token was already placed. Output field is required");
        }

        firstToken = token;
        _placedTokens.Add(token);
    }

    ///<summary>
    ///Places the first token on the board. Calling it more than once without the output parameter will throw exception
    ///</summary>
    ///<param name="token">The token to be placed</param>
    ///<param name="output">The output on the board where the token will be placed. Must match with one of the token's outputs</param>
    public void PlaceToken(Token token, int output) {
        
        if (!token.HasOutput(output)) {
            throw new ArgumentException("The provided token and output don't match");
        }

        foreach (var placedToken in _placedTokens) {
            if (token.HasOutput(output)) {

                placedToken.PlaceTokenOn(output);
                token.PlaceTokenOn(output);

                _placedTokens.Add(token);
                _graph.Add((token, placedToken));

                return;
            }
        }

        throw new ArgumentException("There is no free output in the board that matches with the provided output");
    }
}