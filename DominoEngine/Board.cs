using DominoEngine.Algorithms;
namespace DominoEngine;

public class Board {

    private readonly List<Ficha> _placedTokens;
    private readonly List<(Ficha, Ficha)> _graph; // Currently the graph has no real use

    public Board() {

        _placedTokens = new List<Ficha>();
        _graph = new List<(Ficha, Ficha)>();
    }

    public int[] BoardFreeOutputs() {

        HashSet<int> outputs = new HashSet<int>();

        foreach (var token in _placedTokens) {
            
            int[] tokenFreeOutputs = new int[2]; // token.FreeOutputs();
            foreach (var head in tokenFreeOutputs) {
                outputs.Add(head);
            }
        }

        return outputs.ToArray();
    }

    public void PlaceToken(Ficha token, int output) {

        if (ArrayOperations<int>.Find(new int[2], output) == -1) { // token.FreeOutputs()
            throw new ArgumentException("The provided token and output don't match");
        }

        foreach (var placedToken in _placedTokens) {
            if (ArrayOperations<int>.Find(new int[2], output) != -1) { // placedToken.FreeOutputs(), output

                // placedToken.Cover(output);
                // token.Cover(output);
                _placedTokens.Add(token);
                _graph.Add((token, placedToken));
                return;
            }
        }

        throw new ArgumentException("There is no free output in the board that matches with the provided output");
    }
}