using System.Reflection;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.VictoryCriteria;

namespace DominoEngine.Utils;

public static class Implementations {

    public static (string, evaluator<T>)[] GetEvaluators<T>() where T : IEvaluable {

        List<(string, evaluator<T>)> result = new List<(string, evaluator<T>)>();

        var methods = typeof(Evaluators<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            System.Console.WriteLine(item.Name);
            result.Add((item.Name, (evaluator<T>)Delegate.CreateDelegate(typeof(evaluator<T>), item)));
        }
        
        return result.ToArray();
    }

    public static (string, strategy<T>)[] GetStrategies<T>() where T : IEvaluable {

        List<(string, strategy<T>)> result = new List<(string, strategy<T>)>();

        var methods = typeof(Strategies<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (strategy<T>)Delegate.CreateDelegate(typeof(strategy<T>), item)));
        }
        
        return result.ToArray();
    }

    public static (string, victoryCriteria<T>)[] GetCriteria<T>() where T : IEvaluable {

        List<(string, victoryCriteria<T>)> result = new List<(string, victoryCriteria<T>)>();

        var methods = typeof(VictoryCriteria<T>).GetMethods(BindingFlags.Static | BindingFlags.Public);

        foreach (var item in methods) {
            result.Add((item.Name, (victoryCriteria<T>)Delegate.CreateDelegate(typeof(victoryCriteria<T>), item)));
        }
        
        return result.ToArray();
    }

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
}