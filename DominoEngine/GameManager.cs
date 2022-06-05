using DominoEngine.Algorithms;
using DominoEngine.Utils;
using DominoEngine.Utils.Evaluators;

namespace DominoEngine;

///<summary>
///An instance of this class represents a game. It controls everithing it happens
///</summary>
public class GameManager<T> where T : IEvaluable {

    private readonly Board<T> _board;
    private readonly Player<T>[] _players;
    private readonly GameStatus<T> _status;
    private readonly Token<T>[] TokenUniverse;
    
    private T[]? tokenTypes;
    private int lastPlayerIndex = -1;
    private int turnDirection = 1;

    ///<summary>
    ///Constructor of GameManager class
    ///</summary>
    ///<param name="strategies">An array with the strategies to be used by each player</param>
    ///<param name="generator">The function that generates outputs of the desired type</param>
    ///<param name="tokenTypeAmount">The amount of different outputs to be generated</param>
    ///<param name="tokensInHand">The amount of tokens to be dealed to each player</param>
    ///<param name="outputsAmount">The amount of outputs of each token. By default 2</param>
    ///<param name="evaluator">An ITokenEvaluator implementation to calculate token values. Defaults to an AditiveEvaluator</param>
    public GameManager(strategy<T>[] strategies, Generator<T> generator, int tokenTypeAmount, int tokensInHand, int outputsAmount = 2, ITokenEvaluator<T>? evaluator = null) {

        if (evaluator == null) evaluator = new AditiveEvaluator<T>();

        _players = new Player<T>[strategies.Length];
        _board = new Board<T>();
        _status = new GameStatus<T>(evaluator);
        
        TokenUniverse = GenerateTokens(tokenTypeAmount, generator, outputsAmount);
        ArrayOperations.RandomShuffle<Token<T>>(TokenUniverse);

        if (tokensInHand * strategies.Length > TokenUniverse.Length) {
            throw new ArgumentException($"Can't deal {tokensInHand} tokens to each of the {strategies.Length} players. There are {TokenUniverse.Length} tokens in total");
        }

        for (int i = 0; i < strategies.Length; i++) {
            _players[i] = new Player<T>("Player #" + (i + 1), TokenUniverse[(i * tokensInHand)..(i * tokensInHand + tokensInHand)], strategies[i]);
        }

    }
    
    ///<summary>
    ///Makes the player in turn move. Returns a tuple with the played token and the output where it was played. If the player didn't place a token, returns (null, default(T))
    ///</summary>
    public (Token<T>, T) MakeMove() {

        Player<T> currentPlayer = NextPlayer();

        T[] availableOutputs = _board.FreeOutputs;
        bool firstMove = false;

        if (availableOutputs.Length == 0) {

            availableOutputs = tokenTypes!;
            firstMove = true;
        }
        var (token, output) = currentPlayer.Play(availableOutputs, _status);

        if (token != null) {

            if (firstMove) _board.PlaceToken(token);
            else _board.PlaceToken(token, output!);
        };
        if (firstMove) output = default(T);
        _status.AddMove(currentPlayer, token!, output!);

        return (token!, output!);
    }

    Player<T> NextPlayer() {
        
        lastPlayerIndex = (lastPlayerIndex + turnDirection) % _players.Length;
        return _players[lastPlayerIndex];
    }

    void ReverseTurnOrder() {
        turnDirection *= -1;
    }
    
    List<Token<T>>? generatingTokens;
    HashSet<string>? tokenStrings;
    Token<T>[] GenerateTokens(int n, Generator<T> generator, int outputsAmount) {
        
        generatingTokens = new List<Token<T>>();
        tokenStrings = new HashSet<string>();

        tokenTypes = generator(n);
        GenerateAll(new T[outputsAmount], 0);

        return generatingTokens.ToArray();
    }
    
    void GenerateAll(T[] currentOutputs, int pos) {

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