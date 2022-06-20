using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
/// Class encapsulating the generation of all the in-game tokens
///</summary>
internal static class TokenGeneration<T> where T : IEvaluable {

    static List<Token<T>>? generatingTokens;
    static HashSet<string>? tokenStrings;
    static T[]? tokenTypes;

    ///<summary>
    /// Generates all of the possible outputs and all the possible tokens for the given configuration
    ///</summary>
    ///<param name="n">The size of the set of possible outputs</param>
    ///<param name="generator">The generator of the desired type of output</param>
    ///<param name="outputsAmount">The amount of outputs of every token</param>

    internal static (T[], Token<T>[]) GenerateTokens(int n, Generator<T> generator, int outputsAmount) {

        generatingTokens = new List<Token<T>>();
        tokenStrings = new HashSet<string>();

        tokenTypes = generator(n);
        GenerateAll(new T[outputsAmount], 0);

        return (tokenTypes, generatingTokens.ToArray());
    }

    static void GenerateAll(T[] currentOutputs, int pos) {

        if (pos == currentOutputs.Length) {

            Token<T> token = new Token<T>(currentOutputs);
            if (!tokenStrings!.Contains(token.ToString())) {

                tokenStrings.Add(token.ToString());
                generatingTokens!.Add(token);
            }
            return;
        }

        for (int i = 0; i < tokenTypes!.Length; i++) {
            
            currentOutputs[pos] = tokenTypes[i];
            GenerateAll(currentOutputs, pos + 1);
        }
    }
}