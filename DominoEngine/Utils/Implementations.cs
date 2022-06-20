using System.Reflection;
using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.VictoryCriteria;

namespace DominoEngine.Utils;

///<summary>
/// Class to encapsulate methods to obtain several implementations using reflection
///</summary>
public static class Implementations {

    ///<summary>
    /// Returns every delegate of type evaluator`T with its name
    ///</summary>
    public static (string, evaluator<T>)[] GetEvaluators<T>() where T : IEvaluable {

        List<(string, evaluator<T>)> result = new List<(string, evaluator<T>)>();

        var methods = typeof(Evaluators<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (evaluator<T>)Delegate.CreateDelegate(typeof(evaluator<T>), item)));
        }
        
        return result.ToArray();
    }

    ///<summary>
    /// Returns every delegate of type strategy`T with its name
    ///</summary>
    public static (string, strategy<T>)[] GetStrategies<T>() where T : IEvaluable {

        List<(string, strategy<T>)> result = new List<(string, strategy<T>)>();

        var methods = typeof(Strategies<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (strategy<T>)Delegate.CreateDelegate(typeof(strategy<T>), item)));
        }
        
        return result.ToArray();
    }

    ///<summary>
    /// Returns every delegate of type victoryCriteria`T with its name
    ///</summary>
    public static (string, victoryCriteria<T>)[] GetCriteria<T>() where T : IEvaluable {

        List<(string, victoryCriteria<T>)> result = new List<(string, victoryCriteria<T>)>();

        var methods = typeof(VictoryCriteria<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (victoryCriteria<T>)Delegate.CreateDelegate(typeof(victoryCriteria<T>), item)));
        }
        
        return result.ToArray();
    }

    ///<summary>
    /// Returns every delegate of type tokenFilter`T with its name
    ///</summary>
    public static (string, tokenFilter<T>)[] GetFilters<T>() where T : IEvaluable {

        List<(string, tokenFilter<T>)> result = new List<(string, tokenFilter<T>)>();

        var methods = typeof(Filters<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (tokenFilter<T>)Delegate.CreateDelegate(typeof(tokenFilter<T>), item)));
        }
        
        return result.ToArray();
    }

    ///<summary>
    /// Returns every delegate representing an effect with its name
    ///</summary>
    public static (string, Action<IGameManager<T>>)[] GetEffects<T>() where T : IEvaluable {

        List<(string, Action<IGameManager<T>>)> result = new List<(string, Action<IGameManager<T>>)>();

        var methods = typeof(Effects<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (Action<IGameManager<T>>)Delegate.CreateDelegate(typeof(Action<IGameManager<T>>), item)));
        }
        
        return result.ToArray();
    }

    ///<summary>
    /// Returns every class implementing IEvaluable
    ///</summary>
    public static (string, Type)[] GetOutputTypes() {

        List<(string, Type)> result = new List<(string, Type)>();

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IEvaluable).IsAssignableFrom(p));

        foreach (var item in types) {

            if (item.Name == "IEvaluable") continue;
            result.Add((item.Name, item));       
        }

        return result.ToArray();
    }

    ///<summary>
    /// Returns every class implementing IGameManager`T
    ///</summary>
    public static Type[] GetGameManagers<T>() where T : IEvaluable {

        List<IGameManager<T>> result = new List<IGameManager<T>>();

        var types = GetAllTypesImplementingOpenGenericType(typeof(IGameManager<>));
        return types.ToArray();
    }

    static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType) {

        return from x in Assembly.GetExecutingAssembly().GetTypes()
        from z in x.GetInterfaces()
        let y = x.BaseType
        where
        (y != null && y.IsGenericType &&
        openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
        (z.IsGenericType &&
        openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
        select x;
    }
}