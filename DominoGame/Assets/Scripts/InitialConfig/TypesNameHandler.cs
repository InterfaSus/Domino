using System;
using System.Collections;
using System.Collections.Generic;

public static class TypesNameHandler {

    public static string[] GetNames<T>(object[] tuples) {

        List<string> result = new List<string>();
        var collection = (Tuple<string, T>[])tuples;

        foreach (var item in collection) {
            result.Add(item.Item1);
        }

        return result.ToArray();
    }

    public static T ImplementationByName<T>(object[] tuple, string name) {

        if (name == "None") return default(T);

        var collection = (Tuple<string, T>[])tuple;

        foreach (var item in collection) {
            if(item.Item1 == name) return item.Item2;
        }

        return default(T);
    }
}