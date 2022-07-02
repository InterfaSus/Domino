using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;
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
            new Tuple<string, victoryCriteria<T>>("Surpass Sum Criteria", VictoryCriteria<T>.SurpassSumCriteria),
            new Tuple<string, victoryCriteria<T>>("Ends At X Pass", VictoryCriteria<T>.EndAtXPass),
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
    public static Tuple<string, Action<EffectsExecution<T>>>[] GetEffects<T>() where T : IEvaluable {

        return new Tuple<string, Action<EffectsExecution<T>>>[] {
            new Tuple<string, Action<EffectsExecution<T>>>("Skip Next", Effects<T>.SkipNext),
            new Tuple<string, Action<EffectsExecution<T>>>("Give Two Tokens", Effects<T>.GiveTwoTokens),
            new Tuple<string, Action<EffectsExecution<T>>>("Play Again", Effects<T>.PlayAgain),
            new Tuple<string, Action<EffectsExecution<T>>>("Random Turn", Effects<T>.RandomTurn),
        };
    }

    ///<summary>
    /// Returns the generator of every class implementing IEvaluable and its name
    ///</summary>
    public static Tuple<string, Generator<IEvaluable>>[] GetOutputTypes() {

        return new Tuple<string, Generator<IEvaluable>>[] {
            new Tuple<string, Generator<IEvaluable>>("Number", Number.Generate),
            new Tuple<string, Generator<IEvaluable>>("Letter", Letter.Generate),
            };
    }
}