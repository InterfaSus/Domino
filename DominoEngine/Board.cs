using DominoEngine.Algorithms;
namespace DominoEngine;

public class Board {

    private readonly List<Token> _placedTokens;
    private readonly List<(Token, Token)> _graph; // Currently the graph has no real use
    private Token? firstToken;

    public Board() {

        _placedTokens = new List<Token>();
        _graph = new List<(Token, Token)>();
    }

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

    public void PlaceToken(Token token) {

        if (firstToken != null) {
            throw new Exception("The first token was already placed. Output field is required");
        }

        firstToken = token;
        _placedTokens.Add(token);
    }

    public void PlaceToken(Token token, int output) {
        
        if (ArrayOperations<int>.Find(token.FreeOutputs, output) == -1) {
            throw new ArgumentException("The provided token and output don't match");
        }

        foreach (var placedToken in _placedTokens) {
            if (ArrayOperations<int>.Find(placedToken.FreeOutputs, output) != -1) {

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