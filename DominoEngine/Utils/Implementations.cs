using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.VictoryCriteria;

namespace DominoEngine.Utils;

///<summary>
/// Class to encapsulate methods to obtain several implementations
///</summary>
public static class Implementations {

    ///<summary>
    /// Returns every delegate of type evaluator`T with its name
    ///</summary>
    public static Tuple<string, evaluator<T>>[] GetEvaluators<T>() where T : IEvaluable {

        return new Tuple<string, evaluator<T>>[] {
            new Tuple<string, evaluator<T>>("Additive Evaluator", Evaluators<T>.AdditiveEvaluator),
            new Tuple<string, evaluator<T>>("Five Multiples Priority", Evaluators<T>.FiveMultiplesPriority)
        };
    }

    ///<summary>
    /// Returns every delegate of type strategy`T with its name
    ///</summary>
    public static Tuple<string, strategy<T>>[] GetStrategies<T>() where T : IEvaluable {

        return new Tuple<string, strategy<T>>[] {
            new Tuple<string, strategy<T>>("Biggest Option", Strategies<T>.BiggestOption),
            new Tuple<string, strategy<T>>("Random Option", Strategies<T>.RandomOption),
            new Tuple<string, strategy<T>>("Prevent Others From Playing", Strategies<T>.PreventOtherPlayersFromPlaying),
        };
    }

    ///<summary>
    /// Returns every delegate of type victoryCriteria`T with its name
    ///</summary>
    public static Tuple<string, victoryCriteria<T>>[] GetCriteria<T>() where T : IEvaluable {

        return new Tuple<string, victoryCriteria<T>>[] {
            new Tuple<string, victoryCriteria<T>>("Default Criteria", VictoryCriteria<T>.DefaultCriteria),
            new Tuple<string, victoryCriteria<T>>("Surpass Sum Criteria", VictoryCriteria<T>.SurpassSumCriteria)
        };
    }

    ///<summary>
    /// Returns every delegate of type tokenFilter`T with its name
    ///</summary>
    public static Tuple<string, tokenFilter<T>>[] GetFilters<T>() where T : IEvaluable {

        return new Tuple<string, tokenFilter<T>>[] {
            new Tuple<string, tokenFilter<T>>("All Equals", Filters<T>.AllEqual),
            new Tuple<string, tokenFilter<T>>("All Different", Filters<T>.AllDifferent),
            new Tuple<string, tokenFilter<T>>("Same Parity", Filters<T>.SameParity),
        };
    }

    ///<summary>
    /// Returns every delegate representing an effect with its name
    ///</summary>
    public static Tuple<string, Action<IGameManager<T>>>[] GetEffects<T>() where T : IEvaluable {

        return new Tuple<string, Action<IGameManager<T>>>[] {
            new Tuple<string, Action<IGameManager<T>>>("Dominuno Skip", Effects<T>.DominunoSkip),
            new Tuple<string, Action<IGameManager<T>>>("Dominuno Flip", Effects<T>.DominunoFlip),
            new Tuple<string, Action<IGameManager<T>>>("Dominuno Give Two Tokens", Effects<T>.DominunoGiveTwoTokens),
        };
    }

    ///<summary>
    /// Returns the name of every class implementing IEvaluable
    ///</summary>
    public static string[] GetOutputTypeNames() {

        return new string[] { "Number", "Letter" };
    }

    ///<summary>
    /// Returns every class implementing IGameManager
    ///</summary>
    public static Tuple<string, Type>[] GetGameManagers() {

        return new Tuple<string, Type>[] {
            new Tuple<string, Type>("Standard", typeof(GameManager<>)),
            new Tuple<string, Type>("Dominuno", typeof(GameManagerDominuno<>))
        };
    }
}