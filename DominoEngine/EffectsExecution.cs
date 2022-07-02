namespace DominoEngine;

///<summary>
/// Class to encapsulate methods for the powers to use to do stuff on the game
/// These methods limit the reach of those powers
///</summary>
public class EffectsExecution<T> where T : IEvaluable {

    private GameManager<T> _manager;

    public EffectsExecution(GameManager<T> manager) {
        _manager = manager;
    }

    internal int LastPlayerIndex { get => _manager._lastPlayerIndex; }
    internal int PlayersAmount { get => _manager._players.Length; }

    ///<summary>
    /// Gives n tokens from the pool to the next player
    ///</summary>
    internal void GiveToken(int n) {
        for (int i = 0; i < n; i++) {

            if (_manager._tokenPool.Count > 0) {
                Token<T> token = _manager._tokenPool.Pop();
                _manager._players[(LastPlayerIndex + 1) % PlayersAmount].AddToken(token);
            }
        }
    }
    
    ///<summary>
    /// Moves the pointer of the last player who moved to another player
    ///</summary>
    internal void SetLastPlayerIndex(int index) {
        
        if (index >= PlayersAmount || index < 0) throw new IndexOutOfRangeException("The player index was outside the bounds of the players collection");

        while (_manager._lastPlayerIndex != index) {
            _manager.NextPlayer();
        }
    }
}