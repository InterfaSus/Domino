using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
/// A class that provides a similar game to the card game UNO
///</summary>
public class GameManagerDominuno<T> : GameManager<T> where T : IEvaluable
{

    private int _turnDirection = 1;

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
    public GameManagerDominuno(strategy<T>[] strategies,
                               Generator<T> generator,
                               int tokenTypeAmount,
                               int tokensInHand,
                               int outputsAmount = 2,
                               string[]? playerNames = null,
                               evaluator<T>? evaluator = null,
                               CriteriaCollection<T>? victoryCheckerCollection = null,
                               Powers<T>? powers = null)
    : base(strategies, generator, tokenTypeAmount, tokensInHand, outputsAmount, playerNames, evaluator, victoryCheckerCollection, powers)
    {

    }

    ///<summary>
    /// Skips the turn of the next player
    ///</summary>
    internal void SkipPlayer() {
        NextPlayer();
    }

    ///<summary>
    /// Reverts the turn order
    ///</summary>
    internal void FlipOrder() {
        _turnDirection *= -1;
    }

    ///<summary>
    /// Gives n tokens from the pool to the next player
    ///</summary>
    internal void GiveToken(int n) {
        for (int i = 0; i < n; i++) {

            if (_tokenPool.Count > 0) {
                Token<T> token = _tokenPool.Pop();
                _players[NextIndex()].AddToken(token);
            }
        }
    }

    protected override Player<T> NextPlayer() {
        
        _lastPlayerIndex = NextIndex();
        return _players[_lastPlayerIndex];
    }

    private int NextIndex() {
        int nextIndex = (_lastPlayerIndex + _turnDirection) % _players.Length;
        if (nextIndex == -1) nextIndex = _players.Length - 1;
        return nextIndex;
    }
}