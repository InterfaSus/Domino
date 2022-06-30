using DominoEngine.Utils;
using DominoEngine.Algorithms;

namespace DominoEngine;

///<summary>
/// A class that provides a game with turn order change rules
///</summary>
public class GameManagerTurntwist<T> : GameManager<T> where T : IEvaluable
{

    private RandomGen randomGen = new RandomGen();

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
    public GameManagerTurntwist(strategy<T>[] strategies,
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
    /// Allows the previos player moving again
    ///</summary>
    public void PlayAgain() {

        for (int i = 0; i < base._players.Length - 1; i++) {
            base.NextPlayer();
        }
    }

    ///<summary>
    /// The next move will be for a random player
    ///</summary>
    public void RandomTurn() {

        base._lastPlayerIndex = randomGen.Next(base._players.Length);
    }
}