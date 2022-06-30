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
    protected readonly Player<T>[] _players;
    protected readonly Stack<Token<T>> _tokenPool;
    private readonly CriteriaCollection<T> _victoryCheckerCollection;
    private readonly Powers<T> _powers;
    
    ///<summary>
    ///Returns an array containing the free outputs on the board with the amount of times each one appears
    ///</summary>
    public KeyValuePair<T, int>[] FreeOutputsAmount { get => _board.OutputsAmount; }

    ///<summary>
    ///Returns an array containing each player's name and hand
    ///</summary>
    public Tuple<string, Token<T>[]>[] PlayersTokens {
        get {
            Tuple<string, Token<T>[]>[] result = new Tuple<string, Token<T>[]>[_players.Length];

            int i = 0;
            foreach (var player in _players) {
                result[i] = new Tuple<string, Token<T>[]>(player.Name, player.TokensInHand);
                i++;
            }
            
            return result;
        }
    }

    private T[]? _tokenTypes;
    protected int _lastPlayerIndex = -1;

    ///<summary>
    ///Constructor of GameManager class
    ///</summary>
    ///<param name="strategies">An array with the strategies to be used by each player</param>
    ///<param name="generator">The function that generates outputs of the desired type</param>
    ///<param name="tokenTypeAmount">The amount of different outputs to be generated</param>
    ///<param name="tokensInHand">The amount of tokens to be dealed to each player</param>
    ///<param name="outputsAmount">The amount of outputs of each token. By default 2</param>
    ///<param name="playerNames">An array with the players' names. Must have the same size of "strategies". Defaults to "Player #1", "Player #2" and so on</param>
    ///<param name="evaluator">A function to calculate token values. Defaults to an AditiveEvaluator</param>
    ///<param name="victoryCheckerCollection">A collection of victory criteria. Defaults to the DefaultCriteria</param>
    ///<param name="powers">A collection of Power objects representing certain tokens and their actions on play. Defaults to an empty collection</param>
    public GameManager(strategy<T>[] strategies,
                       Generator<T> generator,
                       int tokenTypeAmount,
                       int tokensInHand,
                       int outputsAmount = 2,
                       string[]? playerNames = null,
                       evaluator<T>? evaluator = null,
                       CriteriaCollection<T>? victoryCheckerCollection = null,
                       Powers<T>? powers = null) {
        
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
        if (victoryCheckerCollection == null) _victoryCheckerCollection = new CriteriaCollection<T>( new VictoryChecker<T>(VictoryCriteria<T>.DefaultCriteria) ); 
        else _victoryCheckerCollection = victoryCheckerCollection;

        this._powers = (powers == null ? new Powers<T>(new Power<T>[0]) : powers);

        _players = new Player<T>[strategies.Length];
        _board = new Board<T>();
        Status = new GameStatus<T>(evaluator);
        
        var generatedResult = TokenGeneration<T>.GenerateTokens(tokenTypeAmount, generator, outputsAmount);
        _tokenTypes = generatedResult.Item1;
        ArrayOperations.RandomShuffle<Token<T>>(generatedResult.Item2);
        _tokenPool = new Stack<Token<T>>(generatedResult.Item2);

        if (tokensInHand * strategies.Length > _tokenPool.Count) {
            throw new ArgumentException("Can't deal " + tokensInHand + " tokens to each of the " + strategies.Length + " players. There are " + _tokenPool.Count + " tokens in total");
        }

        for (int i = 0; i < strategies.Length; i++) {

            _players[i] = new Player<T>(playerNames[i], strategies[i]);
            for (int j = 0; j < tokensInHand; j++) {
                _players[i].AddToken(_tokenPool.Pop());
            }
        }
    }
    
    ///<summary>
    ///Makes the player in turn move. Returns a PlayData object. If the player passed, the token and output will be null
    ///</summary>
    public WinnerPlayData<T> MakeMove() {

        Player<T> currentPlayer = NextPlayer();

        T[] availableOutputs = _board.FreeOutputs;
        bool firstMove = false;

        if (availableOutputs.Length == 0) {

            availableOutputs = _tokenTypes!;
            firstMove = true;
        }
        var playData = currentPlayer.Play(availableOutputs, Status);

        if (playData.Token != null) {

            if (firstMove) _board.PlaceToken(playData.Token!);
            else _board.PlaceToken(playData.Token!, playData.Output!);
        };
        T? output = playData.Output;
        if (firstMove) output = default(T);
                
        Status.AddMove(playData.PlayerName, playData.Token!, output!, this._board.FreeOutputs);

        var effects = _powers.GetEffects(playData.Token);
        foreach (var item in effects) {
            item(this);
        }

        string[]? winners = _victoryCheckerCollection.RunCheck(Status, _players);

        return new WinnerPlayData<T>(playData.PlayerName, playData.Token!, output, winners!, this._board.FreeOutputs);
    }

    #region Private Methods

    protected virtual Player<T> NextPlayer() {
        
        _lastPlayerIndex = (_lastPlayerIndex + 1) % _players.Length;
        return _players[_lastPlayerIndex];
    }
    
    #endregion
}