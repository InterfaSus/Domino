using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
/// Class encapsulating a kind of token, and its effect
///</summary>
public class Power<T> where T : IEvaluable {

    public tokenFilter<T> Filter { get; private set; }
    public Action<IGameManager<T>> Effect { get; private set; }

    public Power(tokenFilter<T> filter, Action<IGameManager<T>> effect) {
        this.Filter = filter;
        this.Effect = effect;
    }
}

///<summary>
/// Represents a collection of Power`T
///</summary>
public class Powers<T> where T : IEvaluable {

    Power<T>[] _powers;

    public Powers(Power<T>[] powers) {
        this._powers = (Power<T>[])powers.Clone();
    }

    ///<summary>
    /// Receives a token and returns an array of Actions indicating the powers triggered by the token
    ///</summary>
    ///<param name="token">The token to be analyzed</param>
    public Action<IGameManager<T>>[] GetEffects(Token<T>? token) {

        List<Action<IGameManager<T>>> result = new List<Action<IGameManager<T>>>();

        if (token == null) return result.ToArray();

        foreach (var item in _powers) {
            
            if (item.Filter(token)) result.Add(item.Effect);
        }

        return result.ToArray();
    }
}