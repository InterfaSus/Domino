using DominoEngine.Algorithms;
using DominoEngine.Utils;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.VictoryCriteria;

namespace DominoEngine;

///<summary>
///An instance of this class represents a game. It controls everithing it happens
///</summary>
public class GameManager<T> : IGameManager<T> where T : IEvaluable {

    public GameStatus<T> Status { get; }
    private readonly Board<T> _board;
    private readonly Player<T>[] _players;
    private readonly Token<T>[] TokenUniverse;
    
    ///<summary>
    ///Returns an array containing each player's name and hand
    ///</summary>
    public (string, Token<T>[])[] PlayersTokens {
        get {
            (string, Token<T>[])[] result = new (string, Token<T>[])[_players.Length];

            int i = 0;
            foreach (var player in _players) {
                result[i] = (player.Name, player.TokensInHand);
                i++;
            }
            
            return result;
        }
    }

    private T[]? tokenTypes;
    private int lastPlayerIndex = -1;

    ///<summary>
    ///Constructor of GameManager class
    ///</summary>
    ///<param name="strategies">An array with the strategies to be used by each player</param>
    ///<param name="generator">The function that generates outputs of the desired type</param>
    ///<param name="tokenTypeAmount">The amount of different outputs to be generated</param>
    ///<param name="tokensInHand">The amount of tokens to be dealed to each player</param>
    ///<param name="outputsAmount">The amount of outputs of each token. By default 2</param>
    ///<param name="evaluator">An ITokenEvaluator implementation to calculate token values. Defaults to an AditiveEvaluator</param>
    public GameManager(strategy<T>[] strategies, Generator<T> generator, int tokenTypeAmount, int tokensInHand, int outputsAmount = 2, string[]? playerNames = null, evaluator<T>? evaluator = null, victoryCriteria<T>? criteria = null) {
        
        if (playerNames == null) {

            playerNames = new string[strategies.Length];
            for (int i = 0; i < strategies.Length; i++) {
                playerNames[i] = "Player #" + (i + 1);
            }
        }
        else if (playerNames.Length != strategies.Length) {
            throw new ArgumentException("playerNames array has different size that strategies array");
        }

        if (evaluator == null) evaluator = Evaluators<T>.AdditiveEvaluator;
        if (criteria == null) criteria = VictoryCriteria<T>.DefaultCriteria;

        _players = new Player<T>[strategies.Length];
        _board = new Board<T>();
        Status = new GameStatus<T>(evaluator, criteria);
        
        TokenUniverse = GenerateTokens(tokenTypeAmount, generator, outputsAmount);
        ArrayOperations.RandomShuffle<Token<T>>(TokenUniverse);

        if (tokensInHand * strategies.Length > TokenUniverse.Length) {
            throw new ArgumentException($"Can't deal {tokensInHand} tokens to each of the {strategies.Length} players. There are {TokenUniverse.Length} tokens in total");
        }

        for (int i = 0; i < strategies.Length; i++) {
            _players[i] = new Player<T>(playerNames[i], TokenUniverse[(i * tokensInHand)..(i * tokensInHand + tokensInHand)], strategies[i]);
        }
    }
    
    ///<summary>
    ///Makes the player in turn move. Returns a PlayData object. If the player passed, the token and output will be null
    ///</summary>
    public PlayData<T> MakeMove() {

        Player<T> currentPlayer = NextPlayer();

        T[] availableOutputs = _board.FreeOutputs;
        bool firstMove = false;

        if (availableOutputs.Length == 0) {

            availableOutputs = tokenTypes!;
            firstMove = true;
        }
        var (playerName, token, output) = currentPlayer.Play(availableOutputs, Status);

        if (token != null) {

            if (firstMove) _board.PlaceToken(token);
            else _board.PlaceToken(token, output!);
        };
        if (firstMove) output = default(T);
        Status.AddMove(playerName, token!, output!);

        string[] winners = new string[0]; // = CheckWinners() -> Method that returns all the winner players

        return new WinnerPlayData<T>(playerName, token, output, winners);
    }

    #region Private Methods

    Player<T> NextPlayer() {
        
        lastPlayerIndex = (lastPlayerIndex + 1) % _players.Length;
        return _players[lastPlayerIndex];
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
    
    #endregion
}