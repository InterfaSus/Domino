using DominoEngine.Algorithms;

namespace DominoEngine.Utils.Effects;

///<summary>
/// Class to encapsulate implementations of token effects
///</summary>
public static class Effects<T> where T : IEvaluable {

    static RandomGen random = new RandomGen();

    ///<summary>
    /// Makes the next player skip its turn
    ///</summary>
    public static void SkipNext(EffectsExecution<T> execution) {
        execution.SetLastPlayerIndex((execution.LastPlayerIndex + 1) % execution.PlayersAmount);
    }

    ///<summary>
    /// Gives two tokens from the pool to the next player
    ///</summary>
    public static void GiveTwoTokens(EffectsExecution<T> execution) {
        execution.GiveToken(2);
    }

    ///<summary>
    /// Makes the player move again
    ///</summary>
    public static void PlayAgain(EffectsExecution<T> execution) {
        execution.SetLastPlayerIndex((execution.LastPlayerIndex - 1 + execution.PlayersAmount) % execution.PlayersAmount);
    }

    ///<summary>
    /// The next next turn will be for a random player
    ///</summary>
    public static void RandomTurn(EffectsExecution<T> execution) {
        execution.SetLastPlayerIndex((execution.LastPlayerIndex + random.Next()) % execution.PlayersAmount);
    }
}