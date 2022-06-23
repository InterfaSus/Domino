namespace DominoEngine.Utils.Effects;

///<summary>
/// Class to encapsulate implementations of token effects
///</summary>
public static class Effects<T> where T : IEvaluable {

    ///<summary>
    /// Makes the next player skip its turn
    ///</summary>
    public static void DominunoSkip(IGameManager<T> manager) {

        if (manager is GameManagerDominuno<T> mg) {
            mg.SkipPlayer();
        }
        else Error(manager.GetType().Name, "SkipPlayer");
    }

    ///<summary>
    /// Reverts the turn order
    ///</summary>
    public static void DominunoFlip(IGameManager<T> manager) {

        if (manager is GameManagerDominuno<T> mg) {
            mg.FlipOrder();
        }
        else Error(manager.GetType().Name, "FlipOrder");
    }

    ///<summary>
    /// Gives two tokens from the pool to the next player
    ///</summary>
    public static void DominunoGiveTwoTokens(IGameManager<T> manager) {

        if (manager is GameManagerDominuno<T> mg) {
            mg.GiveToken(2);
        }
        else Error(manager.GetType().Name, "GiveToken(int)");
    }

    static void Error(string manager, string method) {
        throw new ArgumentException("Mismatch between IGameManager implementation and effect. " + manager + " does not contain the method " + method);
    }
}